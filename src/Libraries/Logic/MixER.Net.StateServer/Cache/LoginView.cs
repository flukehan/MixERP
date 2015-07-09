using PetaPoco;
using System;

namespace MixER.Net.ApplicationState.Cache
{
    [TableName("office.sign_in_view")]
    [ExplicitColumns]
    public class LoginView
    {
        [Column("login_id")]
        public long? LoginId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("role_id")]
        public int? RoleId { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("role_code")]
        public string RoleCode { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }

        [Column("is_admin")]
        public bool? IsAdmin { get; set; }

        [Column("is_system")]
        public bool? IsSystem { get; set; }

        [Column("browser")]
        public string Browser { get; set; }

        [Column("ip_address")]
        public string IpAddress { get; set; }

        [Column("login_date_time")]
        public DateTime? LoginDateTime { get; set; }

        [Column("remote_user")]
        public string RemoteUser { get; set; }

        [Column("culture")]
        public string Culture { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("elevated")]
        public bool? Elevated { get; set; }

        [Column("office")]
        public string Office { get; set; }

        [Column("office_id")]
        public int? OfficeId { get; set; }

        [Column("office_code")]
        public string OfficeCode { get; set; }

        [Column("office_name")]
        public string OfficeName { get; set; }

        [Column("nick_name")]
        public string NickName { get; set; }

        [Column("registration_date")]
        public DateTime? RegistrationDate { get; set; }

        [Column("currency_code")]
        public string CurrencyCode { get; set; }

        [Column("po_box")]
        public string PoBox { get; set; }

        [Column("address_line_1")]
        public string AddressLine1 { get; set; }

        [Column("address_line_2")]
        public string AddressLine2 { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("zip_code")]
        public string ZipCode { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("registration_number")]
        public string RegistrationNumber { get; set; }

        [Column("pan_number")]
        public string PanNumber { get; set; }

        [Column("allow_transaction_posting")]
        public bool? AllowTransactionPosting { get; set; }

    }
}