using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Stores
    {
        public static System.Data.DataTable GetStoreDataTable()
        {
            return FormHelper.GetTable("office", "stores", "store_id");
        }
    }
}