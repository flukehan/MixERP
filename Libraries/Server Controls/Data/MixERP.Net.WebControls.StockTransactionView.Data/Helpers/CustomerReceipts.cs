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

using Npgsql;
using System;
using System.Data;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Helpers
{
    public static class CustomerReceipts
    {
        public static DataTable GetView(int userId, int officeId, DateTime dateFrom, DateTime dateTo, string office, string party, string user, string referenceNumber, string statementReference)
        {
            const string sql = "SELECT * FROM transactions.get_receipt_view(@UserId, @OfficeId, @DateFrom, @DateTo, @Office, @Party, @User, @ReferenceNumber, @StatementReference);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@DateFrom", dateFrom);
                command.Parameters.AddWithValue("@DateTo", dateTo);
                command.Parameters.AddWithValue("@Office", office);
                command.Parameters.AddWithValue("@Party", party);
                command.Parameters.AddWithValue("@User", user);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);

                return DBFactory.DbOperations.GetDataTable(command);
            }
        }
    }
}