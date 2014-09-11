using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Shippers
    {
        public static DataTable GetShipperDataTable()
        {
            return FormHelper.GetTable("core", "shippers");
        }
    }
}