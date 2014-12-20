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

using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Data;

namespace MixERP.Net.WebControls.TransactionViewFactory.Data
{
    public static class Journal
    {
        public static DataTable GetJournalView(int userId, int officeId, DateTime from, DateTime to, long tranId, string tranCode, string book, string referenceNumber, string statementReference, string postedBy, string office, string status, string verifiedBy, string reason)
        {
            const string sql = "SELECT * FROM transactions.get_journal_view(@UserId, @OfficeId, @From, @To, @TranId, @TranCode, @Book, @ReferenceNumber, @StatementReference, @PostedBy, @Office, @Status, @VerifiedBy, @Reason);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@From", from);
                command.Parameters.AddWithValue("@To", to);
                command.Parameters.AddWithValue("@TranId", tranId);
                command.Parameters.AddWithValue("@TranCode", tranCode);
                command.Parameters.AddWithValue("@Book", book);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);
                command.Parameters.AddWithValue("@PostedBy", postedBy);
                command.Parameters.AddWithValue("@Office", office);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@VerifiedBy", verifiedBy);
                command.Parameters.AddWithValue("@Reason", reason);

                return DbOperation.GetDataTable(command);
            }
        }
    }
}