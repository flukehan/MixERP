using System;
using MixERP.Net.Utility.Installer.Helpers;

namespace MixERP.Net.Utility.Installer.Domains
{
    public class Office
    {
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string NickName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public string HundredthName { get; set; }
        public string AdminName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private bool _isValid;
        public bool IsValid
        {
            get { return this._isValid; }
        }

        public void Validate()
        {
            if (StringUtility.AreNullOrWhitespace(this.OfficeCode, this.OfficeName, this.NickName, this.CurrencyCode,
                this.CurrencySymbol, this.CurrencyName, this.HundredthName, this.AdminName, this.UserName, this.Password))
            {
                this._isValid = false;
                return;
            }

            this._isValid = true;
        }
    }
}