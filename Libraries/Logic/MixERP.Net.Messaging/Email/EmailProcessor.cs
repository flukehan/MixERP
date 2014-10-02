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
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MixERP.Net.Messaging.Email
{
    public class EmailProcessor
    {
        private bool invalid = false;

        #region Configuration Helper

        private string GetSmtpHost()
        {
            return ConfigurationHelper.GetMessagingParameter("SMTPHost");
        }

        private int GetSmtpPort()
        {
            return Conversion.TryCastInteger(ConfigurationHelper.GetMessagingParameter("SMTPPort"));
        }

        private bool GetEnableSSL()
        {
            if (this.GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return false;
            }
            return Conversion.TryCastBoolean(ConfigurationHelper.GetMessagingParameter("SMTPEnableSSL"));
        }

        private SmtpDeliveryMethod GetSmtpDeliveryMethod()
        {
            SmtpDeliveryMethod method;
            Enum.TryParse(ConfigurationHelper.GetMessagingParameter("SmtpDeliveryMethod"), true, out method); ;
            return method;
        }

        private string GetFromDisplayName()
        {
            return ConfigurationHelper.GetMessagingParameter("FromDisplayName");
        }

        private string GetFromEmailAddress()
        {
            return ConfigurationHelper.GetMessagingParameter("FromEmailAddress");
        }

        private string GetSmtpUserName()
        {
            if (this.GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return string.Empty;
            }
            return ConfigurationHelper.GetMessagingParameter("SMTPUserName");
        }

        private string GetSmtpUserPassword()
        {
            if (this.GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return string.Empty;
            }
            return ConfigurationHelper.GetMessagingParameter("SMTPPassword");
        }

        private string GetSpecifiedPickupDirectoryLocation()
        {
            if (this.GetSmtpDeliveryMethod() != SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return string.Empty;
            }

            return HostingEnvironment.MapPath(ConfigurationHelper.GetMessagingParameter("SpecifiedPickupDirectoryLocation"));
        }

        #endregion Configuration Helper

        public void Send(string sendTo, string subject, string body, IEnumerable<Attachment> attachments)
        {
            if (string.IsNullOrWhiteSpace(sendTo))
            {
                throw new ArgumentNullException("sendTo");
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentNullException("body");
            }

            string[] addresses = sendTo.Split(',');
            foreach (string address in addresses)
            {
                if (!IsValidEmail(address))
                {
                    return;
                }
            }

            addresses = addresses.Distinct().ToArray();
            sendTo = string.Join(",", addresses);

            ThreadPool.QueueUserWorkItem(callback =>
            {
                MailAddress sender = new MailAddress(this.GetFromEmailAddress(), this.GetFromDisplayName());

                using (MailMessage mail = new MailMessage(this.GetFromEmailAddress(), sendTo))
                {
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(attachment);
                    }

                    mail.From = sender;
                    using (SmtpClient smtp = new SmtpClient(this.GetSmtpHost(), this.GetSmtpPort()))
                    {
                        smtp.DeliveryMethod = this.GetSmtpDeliveryMethod();
                        smtp.PickupDirectoryLocation = this.GetSpecifiedPickupDirectoryLocation();

                        smtp.EnableSsl = this.GetEnableSSL();
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(this.GetSmtpUserName(), this.GetSmtpUserPassword());
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

        public bool IsValidEmail(string strIn)
        {
            invalid = false;

            if (String.IsNullOrWhiteSpace(strIn))
            {
                return false;
            }

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
            if (invalid)
            {
                return false;
            }

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                RegexOptions.IgnoreCase);
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
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}