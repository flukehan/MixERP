/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Accounts
    {
        public static bool IsCashAccount(int accountId)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE is_cash=true AND account_id=@AccountId;";
            
            using(NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountId", accountId);

                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static bool IsCashAccount(string accountCode)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE is_cash=true AND account_code=@AccountCode;";
            using(NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountCode", accountCode);

                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }
    }
}