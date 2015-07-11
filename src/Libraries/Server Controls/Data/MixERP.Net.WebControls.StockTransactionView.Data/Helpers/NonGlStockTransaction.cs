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
using System.Collections.ObjectModel;
using MixERP.Net.Entities.Transactions;
using PetaPoco;

namespace MixERP.Net.WebControls.StockTransactionViewFactory.Data.Helpers
{
    public static class NonGlStockTransaction
    {
        public static bool AreSalesOrdersAlreadyMerged(string catalog, Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }

            const string sql = @"SELECT transactions.are_sales_orders_already_merged(@0);";
            return Factory.Scalar<bool>(catalog, sql, ids);
        }

        public static bool AreSalesQuotationsAlreadyMerged(string catalog, Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }
            const string sql = @"SELECT transactions.are_sales_quotations_already_merged(@0);";
            return Factory.Scalar<bool>(catalog, sql, ids);
        }

        public static bool ContainsIncompatibleTaxes(string catalog, Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }

            const string sql = @"SELECT transactions.contains_incompatible_taxes(@0);";
            return Factory.Scalar<bool>(catalog, sql, ids);
        }

        public static bool TransactionIdsBelongToSameParty(string catalog, Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }

            //Get the count of distinct number of parties associated with the transaction id.
            const string sql =
                "SELECT COUNT(DISTINCT party_id) FROM transactions.non_gl_stock_master WHERE non_gl_stock_master_id IN(@0);";
            return Factory.Scalar<int>(catalog, sql, ids).Equals(1);
        }

        public static IEnumerable<DbGetNonGlProductViewResult> GetView(string catalog, int userId, string book,
            int officeId, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user,
            string referenceNumber, string statementReference)
        {
            const string sql =
                "SELECT * FROM transactions.get_non_gl_product_view(@0::integer, @1::text, @2::integer, @3::date, @4::date, @5::national character varying(12), @6::text, @7::text, @8::national character varying(50), @9::national character varying(24), @10::text);";

            return Factory.Get<DbGetNonGlProductViewResult>(catalog, sql, userId, book, officeId, dateFrom, dateTo,
                office, party, priceType, user, referenceNumber, statementReference);
        }
    }
}