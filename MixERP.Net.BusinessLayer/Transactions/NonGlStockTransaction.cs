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
using System.Data;


namespace MixERP.Net.BusinessLayer.Transactions
{
    public static class NonGlStockTransaction
    {
        public static long Add(string book, DateTime valueDate, string partyCode, int priceTypeId, Collection<StockMasterDetailModel> details, string referenceNumber, string statementReference, Collection<int> transactionIdCollection, Collection<Attachment> attachments)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.PriceTypeId = priceTypeId;

            long nonGlStockMasterId = DatabaseLayer.Transactions.NonGlStockTransaction.Add(book, valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), referenceNumber, statementReference, stockMaster, details, transactionIdCollection, attachments);
            return nonGlStockMasterId;
        }

        public static DataTable GetView(string book, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return DatabaseLayer.Transactions.NonGlStockTransaction.GetView(SessionHelper.GetUserId(), book, SessionHelper.GetOfficeId(), dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }

        public static bool TransactionIdsBelongToSameParty(Collection<int> ids)
        {
            return DatabaseLayer.Transactions.NonGlStockTransaction.TransactionIdsBelongToSameParty(ids);
        }

        public static bool AreSalesQuotationsAlreadyMerged(Collection<int> ids)
        {
            return DatabaseLayer.Transactions.NonGlStockTransaction.AreSalesQuotationsAlreadyMerged(ids);
        }

        public static bool AreSalesOrdersAlreadyMerged(Collection<int> ids)
        {
            return DatabaseLayer.Transactions.NonGlStockTransaction.AreSalesOrdersAlreadyMerged(ids);
        }
    }
}