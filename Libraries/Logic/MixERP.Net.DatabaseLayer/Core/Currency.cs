using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Currency
    {
        public static DataTable GetCurrencies()
        {
            const string sql = "SELECT * FROM core.currencies;";
            return DbOperations.GetDataTable(new NpgsqlCommand(sql));
        }
    }
}