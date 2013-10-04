using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using Npgsql;

namespace MixERP.Net.WebControls.ReportEngine.Data
{
    public static class TableHelper
    {
        public static DataTable GetDataTable(string sql, Collection<KeyValuePair<string, string>> parameters)
        {
            if(string.IsNullOrWhiteSpace(sql))
            {
                return null;
            }

            using(NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                if(parameters != null)
                {
                    foreach(KeyValuePair<string, string> p in parameters)
                    {
                        command.Parameters.AddWithValue(p.Key, p.Value);
                    }
                }

                return MixERP.Net.DBFactory.DBOperations.GetDataTable(command);
            }
        }
    }
}
