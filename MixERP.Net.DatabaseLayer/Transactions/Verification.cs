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
using System.Data;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Transactions
{
    public static class Verification
    {
        public static VerificationModel GetVerificationStatus(long transactionMasterId)
        {
            VerificationModel model = new VerificationModel();
            const string sql = "SELECT verification_status_id, office.get_user_name_by_user_id(verified_by_user_id) AS verified_by_user_name, verified_by_user_id, last_verified_on, verification_reason FROM transactions.transaction_master WHERE transaction_master_id=@TransactionMasterId;";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);

                using (DataTable table = DbOperations.GetDataTable(command))
                {
                    if (table != null)
                    {
                        if (table.Rows.Count.Equals(1))
                        {
                            DataRow row = table.Rows[0];

                            model.Verification = Conversion.TryCastShort(row["verification_status_id"]);
                            model.VerifierUserId = Conversion.TryCastInteger(row["verified_by_user_id"]);
                            model.VerifierName = Conversion.TryCastString(row["verified_by_user_name"]);
                            model.VerifiedDate = Conversion.TryCastDate(row["last_verified_on"]);
                            model.VerificationReason = Conversion.TryCastString(row["verification_reason"]);
                        }
                    }
                }
            }


            return model;
        }

        public static bool WithdrawTransaction(long transactionMasterId, int userId, string reason)
        {
            short status = VerificationDomain.GetVerification(VerificationType.Withdrawn);

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

        public static bool CallAutoVerification(long transactionMasterId)
        {
            if (Switches.EnableAutoVerification())
            {
                const string sql = "SELECT transactions.auto_verify(@TransactionMasterId::bigint);";
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                    return DbOperations.ExecuteNonQuery(command);
                }
            }

            return false;
        }
    }
}
