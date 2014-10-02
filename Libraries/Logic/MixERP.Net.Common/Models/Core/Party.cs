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