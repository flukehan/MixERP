using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MixERP.Net.DatabaseLayer.Helpers
{
    public static class TableHelper
    {
        public static DataTable GetTable(string schema, string tableName, string exclusion)
        {
            return MixERP.Net.DBFactory.TableHelper.GetTable(schema, tableName, exclusion);
        }
    }
}
