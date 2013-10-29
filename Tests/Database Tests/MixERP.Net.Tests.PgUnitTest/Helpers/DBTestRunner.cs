using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using MixERP.Net.DBFactory;
using MixERP.Net.Common;

namespace MixERP.Net.Tests.PgUnitTest.Helpers
{
    public class DBTestRunner
    {
        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public bool RunTests()
        {
            bool result = Helpers.TestInstaller.InstallTests();

            if (!result)
            {
                this.message = "Could not install test scripts.";
                return false;
            }

            return this.Run();
        }


        private bool Run()
        {
            string sql = "BEGIN TRANSACTION; SELECT * FROM unit_tests.begin(); ROLLBACK TRANSACTION;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = DBOperations.GetDataTable(command))
                {
                    if (table != null)
                    {
                        if (table.Rows.Count.Equals(1))
                        {
                            this.message = Conversion.TryCastString(table.Rows[0]["message"]);
                            return Conversion.TryCastString(table.Rows[0]["result"]).Equals("Y");
                        }
                       
                    }
                }
            }

            this.message = "Failed to run unit tests on PostgreSQL Server.";
            return false;
        }
    }
}