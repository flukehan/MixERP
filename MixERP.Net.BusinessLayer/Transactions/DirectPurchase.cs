using MixERP.Net.Common.Models.Transactions;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.BusinessLayer.Transactions
{
    public static class DirectPurchase
    {
        public static long Add(DateTime valueDate, int storeId, bool isCredit, string partyCode, Collection<StockMasterDetailModel> details, int cashRepositoryId, int costCenterId, string referenceNumber, string statementReference)
        {
            MixERP.Net.Common.Models.Transactions.StockMasterModel stockMaster = new MixERP.Net.Common.Models.Transactions.StockMasterModel();
            long transactionMasterId = 0;

            stockMaster.PartyCode = partyCode;
            stockMaster.StoreId = storeId;
            stockMaster.CashRepositoryId = cashRepositoryId;
            stockMaster.IsCredit = isCredit;
            
            if(!string.IsNullOrWhiteSpace(statementReference))
            {
                statementReference = statementReference.Replace("&nbsp;", " ").Trim();
            }

            transactionMasterId = MixERP.Net.DatabaseLayer.Transactions.DirectPurchase.Add(valueDate, MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetOfficeId(), MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetUserId(), MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetLogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details);
            MixERP.Net.DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}
