using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Data.Verification
{
    public static class VerificationStatus
    {
        public static DataRow GetVerificationStatusDataRow(long transactionMasterId)
        {
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
                            return row;
                        }
                    }
                }
            }

            return null;
        }
    }
}