/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Models.Core;
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
    public static class DirectSales
    {
        public static long Add(DateTime valueDate, int storeId, bool isCredit, string partyCode, int agentId, int priceTypeId, Collection<StockMasterDetailModel> details, int shipperId, string shippingAddressCode, decimal shippingCharge, int cashRepositoryId, int costCenterId, string referenceNumber, string statementReference, Collection<Attachment> attachments)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.IsCredit = isCredit;
            stockMaster.PriceTypeId = priceTypeId;
            stockMaster.ShipperId = shipperId;
            stockMaster.ShippingAddressCode = shippingAddressCode;
            stockMaster.ShippingCharge = shippingCharge;
            stockMaster.AgentId = agentId;
            stockMaster.CashRepositoryId = cashRepositoryId;
            stockMaster.StoreId = storeId;

            long transactionMasterId = DatabaseLayer.Transactions.DirectSales.Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details, attachments);
            DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}
