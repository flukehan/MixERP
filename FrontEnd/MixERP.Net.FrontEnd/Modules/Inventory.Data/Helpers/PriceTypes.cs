using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class PriceTypes
    {
        public static DataTable GetPriceTypeDataTable()
        {
            return FormHelper.GetTable("core", "price_types");
        }
    }
}