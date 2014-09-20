using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class CostCenters
    {
        public static DataTable GetCostCenterDataTable()
        {
            return FormHelper.GetTable("office", "cost_centers", "cost_center_id");
        }
    }
}