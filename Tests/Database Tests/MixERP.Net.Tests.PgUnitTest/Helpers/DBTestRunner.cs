/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;
using Npgsql;
using MixERP.Net.DBFactory;
using MixERP.Net.Common;

namespace MixERP.Net.Tests.PgUnitTest.Helpers
{
    public class DbTestRunner
    {
        public string Message { get; private set; }

        public bool RunTests()
        {
            bool result = TestInstaller.InstallTests();

            if (!result)
            {
                this.Message = "Could not install test scripts.";
                return false;
            }

            return this.Run();
        }


        private bool Run()
        {
            const string sql = "BEGIN TRANSACTION; SELECT * FROM unit_tests.begin(); ROLLBACK TRANSACTION;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = DbOperations.GetDataTable(command))
                {
                    if (table != null)
                    {
                        if (table.Rows.Count.Equals(1))
                        {
                            this.Message = Conversion.TryCastString(table.Rows[0]["message"]);
                            return Conversion.TryCastString(table.Rows[0]["result"]).Equals("Y");
                        }
                       
                    }
                }
            }

            this.Message = "Failed to run unit tests on PostgreSQL Server.";
            return false;
        }
    }
}