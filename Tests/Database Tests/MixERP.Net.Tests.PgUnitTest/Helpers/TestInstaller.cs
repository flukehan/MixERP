/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Configuration;
using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;
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

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
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

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private static void InstallUnitTests()
        {
            string sql = GetScript();

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DbOperations.ExecuteNonQuery(command);
            }
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
    }
}
