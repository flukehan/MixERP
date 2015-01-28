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
using PetaPoco;

namespace MixERP.Net.Entities.Models.Core
{
    [TableName("get_offices")] //transactions.get_party_transaction_summary(@OfficeId, @PartyId)
    [ExplicitColumns]
    public class PartyDueModel
    {
        [Column("accrued_interest")]
        public decimal AccruedInterest { get; set; }

        [Column("currency_code")]
        public string CurrencyCode { get; set; }

        [Column("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [Column("last_receipt_date")]
        public DateTime LastReceiptDate { get; set; }

        [Column("office_due_amount")]
        public decimal OfficeDueAmount { get; set; }

        [Column("total_due_amount")]
        public decimal TotalDueAmount { get; set; }

        [Column("transaction_value")]
        public decimal TransactionValue { get; set; }
    }
}