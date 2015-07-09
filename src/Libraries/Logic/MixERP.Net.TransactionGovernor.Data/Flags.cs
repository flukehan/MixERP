/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.DbFactory;
using Npgsql;
using System.Collections.ObjectModel;

namespace MixERP.Net.TransactionGovernor.Data
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags"
        )]
    public static class Flags
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms",
            MessageId = "Flag"),
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms",
             MessageId = "flag")]
        public static void CreateFlag(string catalog, int userId, int flagTypeId, string resourceName,
            string resourceKey, Collection<string> resourceIds)
        {
            if (resourceIds == null)
            {
                return;
            }

            const string sql = "SELECT core.create_flag(@UserId, @FlagTypeId, @Resource, @ResourceKey, @ResourceId);";

            foreach (string resourceId in resourceIds)
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@FlagTypeId", flagTypeId);
                    command.Parameters.AddWithValue("@Resource", resourceName);
                    command.Parameters.AddWithValue("@ResourceKey", resourceKey);
                    command.Parameters.AddWithValue("@ResourceId", resourceId);

                    DbOperation.ExecuteNonQuery(catalog, command);
                }
            }
        }
    }
}