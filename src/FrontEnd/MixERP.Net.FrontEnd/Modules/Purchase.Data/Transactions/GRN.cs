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

using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Purchase.Data.Transactions
{
    public static class GRN
    {
        public static long Add(string catalog, int officeId, int userId, long loginId, DateTime valueDate, int storeId, string partyCode, Collection<StockDetail> details, int costCenterId, string referenceNumber, string statementReference, Collection<long> transactionIdCollection, Collection<Attachment> attachments)
        {
            StockMaster stockMaster = new StockMaster();

            stockMaster.PartyCode = partyCode;
            stockMaster.StoreId = storeId;
            stockMaster.IsCredit = true;

            if (!string.IsNullOrWhiteSpace(statementReference))
            {
                statementReference = statementReference.Replace("&nbsp;", " ").Trim();
            }

            long transactionMasterId = GlTransaction.Add(catalog, valueDate, "Purchase.Receipt", officeId, userId, loginId, costCenterId, referenceNumber, statementReference, stockMaster, details, transactionIdCollection, attachments);
            return transactionMasterId;
        }
    }
}