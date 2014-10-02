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

using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

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