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
    public static class SalesDelivery
    {
        public static long Add(DateTime valueDate, int storeId, string partyCode, int priceTypeId, Collection<StockMasterDetailModel> details, int shipperId, decimal shippingCharge, int costCenterId, string referenceNumber, int agentId, string statementReference, Collection<int> transactionIdCollection)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.StoreId = storeId;
            stockMaster.PriceTypeId = priceTypeId;
            stockMaster.ShipperId = shipperId;
            stockMaster.ShippingCharge = shippingCharge;
            stockMaster.AgentId = agentId;


            long transactionMasterId = DatabaseLayer.Transactions.SalesDelivery.Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details, transactionIdCollection);
            DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}
