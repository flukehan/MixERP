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
using System.Globalization;
using System.Text.RegularExpressions;

namespace MixERP.Net.Messaging.Email
{
    internal class Validator
    {
        private bool isValid;

        public Validator(string emailAddress)
        {
            this.EmailAddress = emailAddress;
        }

        public bool IsValid
        {
            get { return !this.isValid; }
        }

        public string EmailAddress { get; set; }

        public void Validate()
        {
            this.isValid = false;

            if (String.IsNullOrWhiteSpace(this.EmailAddress))
            {
                return;
            }

            string emailAddress = this.EmailAddress;

            emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", this.DomainMapper);

            // Return true if address is in valid e-mail format.
            this.isValid = Regex.IsMatch(emailAddress,
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
                this.isValid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}