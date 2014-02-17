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
            StockMasterModel stockMaster = new StockMasterModel();
            long nonGlStockMasterId = 0;

            stockMaster.PartyCode = partyCode;
            stockMaster.PriceTypeId = priceTypeId;

            nonGlStockMasterId = DatabaseLayer.Transactions.NonGLStockTransaction.Add(book, valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), referenceNumber, statementReference, stockMaster, details, transactionIdCollection);
            return nonGlStockMasterId;
        }

        public static DataTable GetView(string book, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return DatabaseLayer.Transactions.NonGLStockTransaction.GetView(SessionHelper.GetUserId(), book, SessionHelper.GetOfficeId(), dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }

        public static bool TransactionIdsBelongToSameParty(Collection<int> ids)
        {
            return DatabaseLayer.Transactions.NonGLStockTransaction.TransactionIdsBelongToSameParty(ids);
        }

        public static bool AreSalesQuotationsAlreadyMerged(Collection<int> ids)
        {
            return DatabaseLayer.Transactions.NonGLStockTransaction.AreSalesQuotationsAlreadyMerged(ids);
        }

        public static bool AreSalesOrdersAlreadyMerged(Collection<int> ids)
        {
            return DatabaseLayer.Transactions.NonGLStockTransaction.AreSalesOrdersAlreadyMerged(ids);
        }
    }
}