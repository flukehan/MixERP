using Npgsql;
using System.Configuration;
using System.Web.Hosting;

namespace MixERP.Net.i18n
{
    internal static class ConnectionStringHelper
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

        private static string GetDbServerParameter(string keyName)
        {
            return GetConfigurationValue("DbServerConfigFileLocation", keyName);
        }

        private static string GetConfigurationValue(string configFileName, string sectionName)
        {
            if (configFileName == null)
            {
                return string.Empty;
            }

            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings[configFileName]);

            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap {ExeConfigFilename = path};
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

        private static string GetConnectionString(string host, string database, string username, string password,
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