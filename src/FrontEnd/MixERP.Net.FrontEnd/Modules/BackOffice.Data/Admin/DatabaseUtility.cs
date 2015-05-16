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

namespace MixERP.Net.Core.Modules.BackOffice.Data.Admin
{
    /// <summary>
    ///     This class provides utility functions to execute routine PostgreSQL server functions.
    /// </summary>
    public class DatabaseUtility
    {
        /// <summary>
        ///     Asks the database server to analyze and collect statistics of the current database.
        ///     For further information, http://www.postgresql.org/docs/9.4/static/sql-analyze.html
        /// </summary>
        public void Analyze(string catalog)
        {
            const string sql = "ANALYZE;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                DbOperation.ExecuteNonQuery(catalog, command);
            }
        }

        /// <summary>
        ///     The vacuum command reclaims the storage space of the database server
        ///     against the dead/inactive database tuples.
        ///     For further information, http://www.postgresql.org/docs/9.4/static/sql-vacuum.html
        /// </summary>
        public void Vacuum(string catalog)
        {
            const string sql = "VACUUM;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                DbOperation.ExecuteNonQuery(catalog, command);
            }
        }

        /// <summary>
        ///     The vacuum full command frees the storage space of the database server against the
        ///     dead/inactive database tuples. For further information,
        ///     http://www.postgresql.org/docs/9.4/static/sql-vacuum.html.
        /// </summary>
        public void VacuumFull(string catalog)
        {
            const string sql = "VACUUM FULL;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                DbOperation.ExecuteNonQuery(catalog, command);
            }
        }
    }
}