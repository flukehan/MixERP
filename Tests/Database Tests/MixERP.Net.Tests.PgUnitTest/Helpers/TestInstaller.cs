using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MixERP.Net.Common;
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
                MixERP.Net.DBFactory.DBOperations.ExecuteNonQuery(command);
            }
        }

        private static void InstallUnitTests()
        {
            string sql = GetScript();

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                MixERP.Net.DBFactory.DBOperations.ExecuteNonQuery(command);
            }
        }

        private static string GetScript()
        {
            string root = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string unitTestDirectory = ConfigurationManager.AppSettings["UnitTestDirectory"];
            string directory = System.IO.Path.Combine(root, unitTestDirectory);

            string sql = CombineScripts(directory);
            return sql;
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
            if (!System.IO.Directory.Exists(directoryPath)) return;

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
