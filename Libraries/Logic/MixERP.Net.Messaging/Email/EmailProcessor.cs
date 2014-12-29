/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Hosting;

namespace MixERP.Net.Messaging.Email
{
    public sealed class EmailProcessor
    {
        private bool invalid;

        #region Configuration Helper

        private static bool GetEnableSSL()
        {
            if (GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return false;
            }

            return Conversion.TryCastBoolean(ConfigurationHelper.GetMessagingParameter("SMTPEnableSSL"));
        }

        private static string GetFromDisplayName()
        {
            return ConfigurationHelper.GetMessagingParameter("FromDisplayName");
        }

        private static string GetFromEmailAddress()
        {
            return ConfigurationHelper.GetMessagingParameter("FromEmailAddress");
        }

        private static SmtpDeliveryMethod GetSmtpDeliveryMethod()
        {
            SmtpDeliveryMethod method;

            if (Enum.TryParse(ConfigurationHelper.GetMessagingParameter("SmtpDeliveryMethod"), true, out method))
            {
                return method;
            }

            return new SmtpDeliveryMethod();
        }

        private static string GetSmtpHost()
        {
            return ConfigurationHelper.GetMessagingParameter("SMTPHost");
        }

        private static int GetSmtpPort()
        {
            return Conversion.TryCastInteger(ConfigurationHelper.GetMessagingParameter("SMTPPort"));
        }

        private static string GetSmtpUserName()
        {
            if (GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return string.Empty;
            }
            return ConfigurationHelper.GetMessagingParameter("SMTPUserName");
        }

        private static string GetSmtpUserPassword()
        {
            if (GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return string.Empty;
            }
            return ConfigurationHelper.GetMessagingParameter("SMTPPassword");
        }

        private static string GetSpecifiedPickupDirectoryLocation()
        {
            if (GetSmtpDeliveryMethod() != SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return string.Empty;
            }

            return HostingEnvironment.MapPath(ConfigurationHelper.GetMessagingParameter("SpecifiedPickupDirectoryLocation"));
        }

        #endregion Configuration Helper

        public bool IsValidEmail(string strIn)
        {
            this.invalid = false;

            if (String.IsNullOrWhiteSpace(strIn))
            {
                return false;
            }

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
            if (this.invalid)
            {
                return false;
            }

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                RegexOptions.IgnoreCase);
        }

        public void Send(string sendTo, string subject, string body, IEnumerable<Attachment> attachments)
        {
            if (string.IsNullOrWhiteSpace(sendTo))
            {
                throw new ArgumentNullException(sendTo);
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentNullException(body);
            }

            string[] addresses = sendTo.Split(',');
            foreach (string address in addresses)
            {
                if (!this.IsValidEmail(address))
                {
                    return;
                }
            }

            addresses = addresses.Distinct().ToArray();
            sendTo = string.Join(",", addresses);

            ThreadPool.QueueUserWorkItem(callback =>
            {
                MailAddress sender = new MailAddress(GetFromEmailAddress(), GetFromDisplayName());

                using (MailMessage mail = new MailMessage(GetFromEmailAddress(), sendTo))
                {
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(attachment);
                    }

                    mail.From = sender;
                    using (SmtpClient smtp = new SmtpClient(GetSmtpHost(), GetSmtpPort()))
                    {
                        smtp.DeliveryMethod = GetSmtpDeliveryMethod();
                        smtp.PickupDirectoryLocation = GetSpecifiedPickupDirectoryLocation();

                        smtp.EnableSsl = GetEnableSSL();
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(GetSmtpUserName(), GetSmtpUserPassword());
                        try
                        {
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.SubjectEncoding = Encoding.UTF8;
                            smtp.Send(mail);
                        }
                        catch (SmtpException)
                        {
                            //Swallow
                        }
                    }
                }
            });
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                this.invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}