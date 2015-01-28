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
using MixERP.Net.Entities;
using MixERP.Net.Entities.Transactions;

namespace MixERP.Net.WebControls.TransactionViewFactory.Data
{
    public static class Journal
    {
        public static IEnumerable<GetJournalView> GetJournalView(int userId, int officeId, DateTime from, DateTime to, long tranId, string tranCode, string book, string referenceNumber, string statementReference, string postedBy, string office, string status, string verifiedBy, string reason)
        {
            const string sql = "SELECT * FROM transactions.get_journal_view(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13);";
            return Factory.Get<GetJournalView>(sql, userId, officeId, from, to, tranId, tranCode, book, referenceNumber, statementReference, postedBy, office, status, verifiedBy, reason);
        }
    }
}