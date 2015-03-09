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

using System;
using System.Net.Mail;
using System.Security;
using System.Web.Hosting;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Messaging.Email.Helpers
{
    internal class Config
    {
        public Config()
        {
            this.FromName = ConfigurationHelper.GetMessagingParameter("FromDisplayName");
            this.FromEmail = ConfigurationHelper.GetMessagingParameter("FromEmailAddress");
            this.SmtpHost = ConfigurationHelper.GetMessagingParameter("SMTPHost");
            this.EnableSsl = Conversion.TryCastBoolean(ConfigurationHelper.GetMessagingParameter("SMTPEnableSSL"));
            this.SmtpPort = Conversion.TryCastInteger(ConfigurationHelper.GetMessagingParameter("SMTPPort"));
            this.SmtpUsername = ConfigurationHelper.GetMessagingParameter("SMTPUserName");
            this.SmtpUserPassword = GetSmtpUserPassword();

            this.DeliveryMethod = GetSmtpDeliveryMethod();
            this.PickupDirectory =
                HostingEnvironment.MapPath(ConfigurationHelper.GetMessagingParameter("SpecifiedPickupDirectoryLocation"));
        }

        public bool EnableSsl { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public SecureString SmtpUserPassword { get; set; }
        public string PickupDirectory { get; set; }

        private static SmtpDeliveryMethod GetSmtpDeliveryMethod()
        {
            SmtpDeliveryMethod method;

            if (Enum.TryParse(ConfigurationHelper.GetMessagingParameter("SmtpDeliveryMethod"), true, out method))
            {
                return method;
            }

            return new SmtpDeliveryMethod();
        }

        private static SecureString GetSmtpUserPassword()
        {
            SecureString secureString = new SecureString();

            if (GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                return secureString;
            }

            string password = ConfigurationHelper.GetMessagingParameter("SMTPPassword");

            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }
    }
}