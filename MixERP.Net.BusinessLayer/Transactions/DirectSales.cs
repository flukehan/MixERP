using MixERP.Net.Common.Models.Transactions;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;

namespace MixERP.Net.BusinessLayer.Transactions
{
    public static class DirectSales
    {
        public static long Add(DateTime valueDate, int storeId, bool isCredit, string partyCode, int agentId, int priceTypeId, Collection<StockMasterDetailModel> details, int shipperId, string shippingAddressCode, decimal shippingCharge, int cashRepositoryId, int costCenterId, string referenceNumber, string statementReference)
        {
            MixERP.Net.Common.Models.Transactions.StockMasterModel stockMaster = new MixERP.Net.Common.Models.Transactions.StockMasterModel();
            long transactionMasterId = 0;

            stockMaster.PartyCode = partyCode;
            stockMaster.IsCredit = isCredit;
            stockMaster.PriceTypeId = priceTypeId;
            stockMaster.ShipperId = shipperId;
            stockMaster.ShippingAddressCode = shippingAddressCode;
            stockMaster.ShippingCharge = shippingCharge;
            stockMaster.AgentId = agentId;
            stockMaster.CashRepositoryId = cashRepositoryId;
            stockMaster.StoreId = storeId;

            transactionMasterId = MixERP.Net.DatabaseLayer.Transactions.DirectSales.Add(valueDate, MixERP.Net.BusinessLayer.Helpers.SessionHelper.OfficeId(), MixERP.Net.BusinessLayer.Helpers.SessionHelper.UserId(), MixERP.Net.BusinessLayer.Helpers.SessionHelper.LogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details);
            MixERP.Net.DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}
