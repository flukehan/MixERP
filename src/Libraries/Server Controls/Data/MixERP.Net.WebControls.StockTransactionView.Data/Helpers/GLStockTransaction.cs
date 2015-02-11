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

using System;
using System.Collections.Generic;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Transactions;

namespace MixERP.Net.WebControls.StockTransactionViewFactory.Data.Helpers
{
    public static class GLStockTransaction
    {
        public static IEnumerable<DbGetProductViewResult> GetView(string book, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return GetView(CurrentSession.GetUserId(), book, CurrentSession.GetOfficeId(), dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }

        private static IEnumerable<DbGetProductViewResult> GetView(int userId, string book, int officeId, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return Factory.Get<DbGetProductViewResult>("SELECT * FROM transactions.get_product_view(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10);", userId, book, officeId, dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }
    }
}