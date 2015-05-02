using System.Configuration;
using System.Web;
using System.Web.Hosting;
using Npgsql;

namespace MixERP.Net.i18n
{
    internal static class ConfigurationHelper
    {
        public static string GetConnectionString()
        {
            string host = GetDbServerParameter("Server");
            string database = GetDbServerParameter("Database");
            string userId = GetDbServerParameter("UserId");
            string password = GetDbServerParameter("Password");
            int port = int.Parse(GetDbServerParameter("Port"));

            return GetConnectionString(host, database, userId, password, port);
        }

        private static string MapPath(string virtualPath)
        {
            if (string.IsNullOrWhiteSpace(virtualPath))
            {
                return string.Empty;
            }

            string path = HostingEnvironment.MapPath(virtualPath);

            if (string.IsNullOrWhiteSpace(path) && HttpContext.Current != null)
            {
                path = HttpContext.Current.Server.MapPath(virtualPath);
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                return virtualPath;
            }

            return path;
        }

        public static string GetDbServerParameter(string keyName)
        {
            string path = MapPath(ConfigurationManager.AppSettings["DbServerConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetConfigurationValues(string configFileName, string sectionName)
        {
            if (configFileName == null)
            {
                return string.Empty;
            }


            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap {ExeConfigFilename = configFileName};
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap,
                ConfigurationUserLevel.None);
            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;

            if (section != null)
            {
                if (section.Settings[sectionName] != null)
                {
                    return section.Settings[sectionName].Value;
                }
            }

            return string.Empty;
        }

        public static string GetConnectionString(string host, string database, string username, string password,
            int port)
        {
            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Database = database,
                UserName = username,
                Password = password,
                Port = port,
                SyncNotification = true,
                Pooling = true,
                SSL = true,
                SslMode = SslMode.Prefer,
                MinPoolSize = 10,
                MaxPoolSize = 100,
                ApplicationName = "MixERP"
            };

            return connectionStringBuilder.ConnectionString;
        }
    }
}