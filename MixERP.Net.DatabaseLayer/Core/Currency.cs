using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Currency
    {
        public static DataTable GetCurrencies()
        {
            string sql = "SELECT * FROM core.currencies;";
            return MixERP.Net.DBFactory.DbOperations.GetDataTable(new NpgsqlCommand(sql));
        }
    }
}
