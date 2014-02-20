using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using MixERP.Net.BusinessLayer.Helpers;

namespace MixERP.Net.BusinessLayer.Core
{
    [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
    public static class Flags
    {
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag"), SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag")]
        public static void CreateFlag(int flagTypeId, string resourceName, string resourceKey, Collection<int> resourceIds)
        {
            int userId = SessionHelper.GetUserId();
            DatabaseLayer.Core.Flags.CreateFlag(userId, flagTypeId, resourceName, resourceKey, resourceIds);
        }
    }
}
