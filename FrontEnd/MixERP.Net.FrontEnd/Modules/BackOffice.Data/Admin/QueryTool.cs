using Npgsql;
using System;
using System.Data;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Admin
{
    public static class QueryTool
    {
        public static DataTable GetDataTable(NpgsqlCommand command)
        {
            return DBFactory.DbOperations.GetDataTable(command);
        }
    }
}