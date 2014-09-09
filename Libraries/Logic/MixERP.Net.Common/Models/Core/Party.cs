namespace MixERP.Net.Common.Models.Core
{
    public class Party
    {
        public int PartyId { get; set; }

        public int PartyTypeId { get; set; }

        public bool IsSupplier { get; set; }

        public string PartyType { get; set; }

        public string PartyCode { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PartyName { get; set; }

        public string POBox { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public bool AllowCredit { get; set; }

        public int MaximumCreditPeriod { get; set; }

        public decimal MaximumCreditAmount { get; set; }

        public bool ChargeInterest { get; set; }

        public decimal InterestRate { get; set; }

        public string InterestCompoundingFrequency { get; set; }

        public string PANNumber { get; set; }

        public string SSTNumber { get; set; }

        public string CSTNumber { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Cell { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string GLHead { get; set; }
    }
}