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
using MixERP.Net.Entities.Transactions;
using PetaPoco;

namespace MixERP.Net.WebControls.StockTransactionViewFactory.Data.Helpers
{
    public static class CustomerReceipts
    {
        public static IEnumerable<DbGetReceiptViewResult> GetView(string catalog, int userId, int officeId,
            DateTime dateFrom, DateTime dateTo, string office, string party, string user, string referenceNumber,
            string statementReference)
        {
            const string sql =
                "SELECT * FROM transactions.get_receipt_view(@0::integer, @1::integer, @2::date, @3::date, @4::national character varying(12), @5::text, @6::national character varying(50), @7::national character varying(24), @8::text);";

            return Factory.Get<DbGetReceiptViewResult>(catalog, sql, userId, officeId, dateFrom, dateTo, office, party,
                user, referenceNumber, statementReference);
        }
    }
}