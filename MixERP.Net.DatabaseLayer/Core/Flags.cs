using System.Diagnostics.CodeAnalysis;
using MixERP.Net.DBFactory;
using Npgsql;
using System.Collections.ObjectModel;

namespace MixERP.Net.DatabaseLayer.Core
{
    [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
    public static class Flags
    {
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag"), SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag")]
        public static void CreateFlag(int userId, int flagTypeId, string resourceName, string resourceKey, Collection<int> resourceIds)
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
