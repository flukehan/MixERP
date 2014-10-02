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