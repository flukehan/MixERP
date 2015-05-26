using System.Configuration;
using System.IO;

namespace MixERP.Net.Utility.Installer.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetSiteName()
        {
            return ReadConfiguration("SiteName");
        }

        public static string GetPostgreSQL32()
        {
            return ReadConfiguration("PostgreSQL32");
        }

        public static string GetPostgreSQL64()
        {
            return ReadConfiguration("PostgreSQL64");
        }

        public static string GetPostgreSQLInstallationDirectory()
        {
            return ReadConfiguration("PostgreSQLInstallationDirectory");
        }

        public static string GetPostgreSQLBinDirectory()
        {
            string path = GetPostgreSQLInstallationDirectory();

            path = Path.Combine(path, "bin");

            return path;
        }

        public static string GetDownloadDirectory()
        {
            return ReadConfiguration("DownloadDirectory");
        }

        public static string GetExtractDirectory()
        {
            return ReadConfiguration("ExtractDirectory");
        }

        public static string GetInstallerManifest()
        {
            return ReadConfiguration("InstallerManifest");
        }

        public static string GetUpdaterManifest()
        {
            return ReadConfiguration("UpdaterManifest");
        }

        public static string ReadConfiguration(string key)
        {
            string config = ConfigurationManager.AppSettings[key];

            return config;
        }

        public static string GetConfigurationValues(string configFileName, string sectionName)
        {
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

        public static string SetConfigurationValues(string configFileName, string key, string value)
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap {ExeConfigFilename = configFileName};
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;

            if (section != null)
            {
                if (section.Settings[key] != null)
                {
                    section.Settings[key].Value = value;
                }
            }
            config.Save(ConfigurationSaveMode.Modified);

            return string.Empty;
        }
    }
}