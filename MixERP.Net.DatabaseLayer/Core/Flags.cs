using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
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

            string sql = "SELECT core.create_flag(@UserId, @FlagTypeId, @Resource, @ResourceKey, @ResourceId);";

            foreach (int resourceId in resourceIds)
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.Add("@UserId", userId);
                    command.Parameters.Add("@FlagTypeId", flagTypeId);
                    command.Parameters.Add("@Resource", resourceName);
                    command.Parameters.Add("@ResourceKey", resourceKey);
                    command.Parameters.Add("@ResourceId", resourceId);

                    DBOperations.ExecuteNonQuery(command);
                }
            }
        }
    }
}
