using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.WebControls.StockTransactionView.Helpers
{
    public static class Flags
    {
        public static void CreateFlag(int userId, int flagTypeId, string resource, string resourceKey, Collection<int> resourceIds)
        {
            TransactionGovernor.Flags.CreateFlag(userId, flagTypeId, resource, resourceKey, resourceIds);
        }
    }
}