/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
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
