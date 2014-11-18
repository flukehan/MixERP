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

namespace MixERP.Net.Common.Models.Transactions
{
    public sealed class TransactionMasterModel
    {
        public long TransactionMasterId { get; set; }

        public int TransactionCounter { get; set; }

        public string TransactionCode { get; set; }

        public string Book { get; set; }

        public DateTime ValueDate { get; set; }

        public DateTime TransactionTimestamp { get; set; }

        public long LogOnId { get; set; }

        public int SysUserId { get; set; }

        public int OfficeId { get; set; }

        public int CostCenterId { get; set; }

        public string ReferenceNumber { get; set; }

        public string StatementReference { get; set; }

        public VerificationType Verification { get; set; }
    }
}