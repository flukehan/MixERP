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
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;


namespace MixERP.Net.BusinessLayer.Transactions
{
    public static class NonGLStockTransaction
    {
        public static long Add(string book, DateTime valueDate, string partyCode, int priceTypeId, Collection<StockMasterDetailModel> details, string referenceNumber, string statementReference, Collection<int> transactionIdCollection)
        {
            MixERP.Net.Common.Models.Transactions.StockMasterModel stockMaster = new MixERP.Net.Common.Models.Transactions.StockMasterModel();
            long nonGlStockMasterId = 0;

            stockMaster.PartyCode = partyCode;
            stockMaster.PriceTypeId = priceTypeId;

            nonGlStockMasterId = MixERP.Net.DatabaseLayer.Transactions.NonGLStockTransaction.Add(book, valueDate, MixERP.Net.BusinessLayer.Helpers.SessionHelper.OfficeId(), MixERP.Net.BusinessLayer.Helpers.SessionHelper.UserId(), MixERP.Net.BusinessLayer.Helpers.SessionHelper.LogOnId(), referenceNumber, statementReference, stockMaster, details, transactionIdCollection);
            return nonGlStockMasterId;
        }

        public static System.Data.DataTable GetView(string book, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return MixERP.Net.DatabaseLayer.Transactions.NonGLStockTransaction.GetView(MixERP.Net.BusinessLayer.Helpers.SessionHelper.UserId(), book, MixERP.Net.BusinessLayer.Helpers.SessionHelper.OfficeId(), dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }

        public static bool TransactionIdsBelongToSameParty(Collection<int> ids)
        {
            return MixERP.Net.DatabaseLayer.Transactions.NonGLStockTransaction.TransactionIdsBelongToSameParty(ids);
        }

        public static bool AreSalesQuotationsAlreadyMerged(Collection<int> ids)
        {
            return MixERP.Net.DatabaseLayer.Transactions.NonGLStockTransaction.AreSalesQuotationsAlreadyMerged(ids);
        }

        public static bool AreSalesOrdersAlreadyMerged(Collection<int> ids)
        {
            return MixERP.Net.DatabaseLayer.Transactions.NonGLStockTransaction.AreSalesOrdersAlreadyMerged(ids);
        }
    }
}