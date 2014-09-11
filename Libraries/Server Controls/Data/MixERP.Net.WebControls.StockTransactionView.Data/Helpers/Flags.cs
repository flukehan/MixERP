using MixERP.Net.Common.Helpers;
using MixERP.Net.DBFactory;
using Npgsql;
using System.Collections.ObjectModel;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Helpers
{
    public static class Flags
    {
        public static void CreateFlag(int flagTypeId, string resourceName, string resourceKey, Collection<int> resourceIds)
        {
            int userId = SessionHelper.GetUserId();
            CreateFlag(userId, flagTypeId, resourceName, resourceKey, resourceIds);
        }

        private static void CreateFlag(int userId, int flagTypeId, string resourceName, string resourceKey, Collection<int> resourceIds)
        {
            if (userId <= 0)
            {
                return;
            }

            if (flagTypeId <= 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                return;
            }

            if (resourceIds == null)
            {
                return;
            }

            const string sql = "SELECT core.create_flag(@UserId, @FlagTypeId, @Resource, @ResourceKey, @ResourceId);";

            foreach (int resourceId in resourceIds)
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@FlagTypeId", flagTypeId);
                    command.Parameters.AddWithValue("@Resource", resourceName);
                    command.Parameters.AddWithValue("@ResourceKey", resourceKey);
                    command.Parameters.AddWithValue("@ResourceId", resourceId);

                    DbOperations.ExecuteNonQuery(command);
                }
            }
        }
    }
}