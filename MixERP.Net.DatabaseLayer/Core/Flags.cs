using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Collections.ObjectModel;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Flags
    {
        public static void CreateFlag(int userId, int flagTypeId, string resourceName, string resourceKey, Collection<int> resourceIds)
        {
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

                    MixERP.Net.DBFactory.DBOperations.ExecuteNonQuery(command);
                }
            }
        }
    }
}
