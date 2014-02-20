/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Models.Transactions;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.BusinessLayer.Transactions
{
    public static class DirectPurchase
    {
        public static long Add(DateTime valueDate, int storeId, bool isCredit, string partyCode, Collection<StockMasterDetailModel> details, int cashRepositoryId, int costCenterId, string referenceNumber, string statementReference)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.StoreId = storeId;
            stockMaster.CashRepositoryId = cashRepositoryId;
            stockMaster.IsCredit = isCredit;
            
            if(!string.IsNullOrWhiteSpace(statementReference))
            {
                statementReference = statementReference.Replace("&nbsp;", " ").Trim();
            }

            long transactionMasterId = DatabaseLayer.Transactions.DirectPurchase.Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details);
            DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}
