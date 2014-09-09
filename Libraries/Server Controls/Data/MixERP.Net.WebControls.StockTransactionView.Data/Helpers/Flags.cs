using MixERP.Net.Common.Helpers;
using System.Collections.ObjectModel;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Helpers
{
    public static class Flags
    {
        public static void CreateFlag(int flagTypeId, string resourceName, string resourceKey, Collection<int> resourceIds)
        {
            int userId = SessionHelper.GetUserId();
            DatabaseLayer.Core.Flags.CreateFlag(userId, flagTypeId, resourceName, resourceKey, resourceIds);
        }
    }
}