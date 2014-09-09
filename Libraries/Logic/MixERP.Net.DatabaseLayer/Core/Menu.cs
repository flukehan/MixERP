using MixERP.Net.DBFactory;
using Npgsql;

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

using System.Data;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Menu
    {
        public static DataTable GetMenuTable(string path, short level, int userId, int officeId, string culture)
        {
            if (userId.Equals(0))
            {
                return null;
            }

            if (officeId.Equals(0))
            {
                return null;
            }

            const string sql = "SELECT * FROM policy.get_menu(@UserId, @OfficeId, @Culture) WHERE parent_menu_id=(SELECT menu_id FROM core.menus WHERE url=@Url) AND level=@Level ORDER BY menu_id;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@Culture", culture);
                command.Parameters.AddWithValue("@Url", path);
                command.Parameters.AddWithValue("@Level", level);

                return DbOperations.GetDataTable(command);
            }
        }

        public static DataTable GetRootMenuTable(string path, int userId, int officeId, string culture)
        {
            if (userId.Equals(0))
            {
                return null;
            }

            if (officeId.Equals(0))
            {
                return null;
            }

            const string sql = "SELECT * FROM policy.get_menu(@UserId, @OfficeId, @Culture) WHERE parent_menu_id=core.get_root_parent_menu_id(@Url) ORDER BY menu_id;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@Culture", culture);
                command.Parameters.AddWithValue("@Url", path);
                return DbOperations.GetDataTable(command);
            }
        }

        public static DataTable GetMenuTable(int parentMenuId, short level, int userId, int officeId, string culture)
        {
            if (userId.Equals(0))
            {
                return null;
            }

            if (officeId.Equals(0))
            {
                return null;
            }

            string sql = "SELECT * FROM policy.get_menu(@UserId, @OfficeId, @Culture) WHERE parent_menu_id is null ORDER BY menu_id;";

            if (parentMenuId > 0)
            {
                sql = "SELECT * FROM policy.get_menu(@UserId, @OfficeId, @Culture) WHERE parent_menu_id=@ParentMenuId AND level=@Level ORDER BY menu_id;";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@Culture", culture);

                if (parentMenuId > 0)
                {
                    command.Parameters.AddWithValue("@ParentMenuId", parentMenuId);
                    command.Parameters.AddWithValue("@Level", level);
                }

                return DbOperations.GetDataTable(command);
            }
        }
    }
}