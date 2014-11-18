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

using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MixERP.Net.Tests.PgUnitTest.Helpers
{
    public static class TestInstaller
    {
        private static List<string> files;

        public static bool InstallTests()
        {
            RunInstallScript();
            InstallUnitTests();
            return true;
        }

        private static string CombineScripts(string directory)
        {
            string sql = string.Empty;

            files = new List<string>();
            RecursiveSearch(directory);

            foreach (string file in files)
            {
                sql += File.ReadAllText(file) + ";";
            }

            return sql;
        }

        private static string GetScript()
        {
            var directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent;

            if (directoryInfo != null)
            {
                if (directoryInfo.Parent != null)
                {
                    string root = directoryInfo.Parent.FullName;
                    string unitTestDirectory = ConfigurationManager.AppSettings["UnitTestDirectory"];
                    string directory = Path.Combine(root, unitTestDirectory);

                    string sql = CombineScripts(directory);
                    return sql;
                }
            }

            return string.Empty;
        }

        private static void InstallUnitTests()
        {
            string sql = GetScript();

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DbOperations.ExecuteNonQuery(command);
            }
        }

        private static void RecursiveSearch(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath)) return;
            if (!Directory.Exists(directoryPath)) return;

            foreach (string subDirectory in Directory.GetDirectories(directoryPath))
            {
                foreach (string file in Directory.GetFiles(subDirectory, "*.sql"))
                {
                    files.Add(file);
                }

                RecursiveSearch(subDirectory);
            }
        }

        private static void RunInstallScript()
        {
            bool run = Conversion.TryCastBoolean(ConfigurationManager.AppSettings["RunInstallScript"]);

            if (!run)
            {
                return;
            }

            string script = ConfigurationManager.AppSettings["InstallScriptPath"];

            using (NpgsqlCommand command = new NpgsqlCommand(script))
            {
                DbOperations.ExecuteNonQuery(command);
            }
        }
    }
}