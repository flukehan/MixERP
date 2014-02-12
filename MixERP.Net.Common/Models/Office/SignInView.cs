using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Common.Models.Office
{
    public class SignInView
    {
        public int UserId { get; set; }
        public MixERP.Net.Common.Models.Office.User User { get; set; }

        public string Role { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSystem { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int LogOnId { get; set; }
        public int OfficeId { get; set; }
        public string Culture { get; set; }
        public string Office { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string NickName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationNumber { get; set; }
        public string PanNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Uri Url { get; set; }
    }
}
