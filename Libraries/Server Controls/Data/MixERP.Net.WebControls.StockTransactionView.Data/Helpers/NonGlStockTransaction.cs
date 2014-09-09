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

using MixERP.Net.Common.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Helpers
{
    public static class NonGlStockTransaction
    {
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