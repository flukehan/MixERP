using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MixERP.Net.BusinessLayer.Core
{
    public static class Flags
    {
        public static void CreateFlag(int flagTypeId, string resourceName, string resourceKey, Collection<int> resourceIds)
        {
            int userId = MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetUserId();
            MixERP.Net.DatabaseLayer.Core.Flags.CreateFlag(userId, flagTypeId, resourceName, resourceKey, resourceIds);
        }
    }
}
