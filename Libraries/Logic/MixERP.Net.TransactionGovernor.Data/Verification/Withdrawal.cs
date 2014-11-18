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

namespace MixERP.Net.TransactionGovernor.Data.Verification
{
    public static class Withdrawal
    {
        public static bool WithdrawTransaction(long transactionMasterId, int userId, string reason, short status)
        {
            const string sql = "UPDATE transactions.transaction_master SET verification_status_id=@Status, verified_by_user_id=@UserId, verification_reason=@Reason WHERE transactions.transaction_master.transaction_master_id=@TransactionMasterId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Reason", reason);
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);

                return DbOperations.ExecuteNonQuery(command);
            }
        }
    }
}