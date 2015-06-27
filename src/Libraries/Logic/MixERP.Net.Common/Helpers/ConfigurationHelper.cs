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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Web;
using System.Web.Hosting;

namespace MixERP.Net.Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetConfigurationValue(string configFileName, string sectionName)
        {
            if (configFileName == null)
            {
                return string.Empty;
            }

            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings[configFileName]);

            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = path };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
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

        public static string GetMixERPParameter(string key)
        {
            return GetConfigurationValue("MixERPConfigFileLocation", key);
        }

        public static string GetDbServerParameter(string key)
        {
            return GetConfigurationValue("DbServerConfigFileLocation", key);
        }

        public static string GetDbParameter(string key)
        {
            return GetConfigurationValue("DBParameterConfigFileLocation", key);
        }

        public static string GetMessagingParameter(string key)
        {
            return GetConfigurationValue("MessagingParameterConfigFileLocation", key);
        }

        public static string GetParameter(string key)
        {
            return GetConfigurationValue("ParameterConfigFileLocation", key);
        }


        public static string GetReportParameter(string key)
        {
            return GetConfigurationValue("ReportConfigFileLocation", key);
        }

        public static string GetScrudParameter(string key)
        {
            return GetConfigurationValue("ScrudConfigFileLocation", key);
        }

        public static string GetResourceDirectory()
        {
            return HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ResourceDirectory"]);
        }

        public static string GetStockTransactionFactoryParameter(string key)
        {
            return GetConfigurationValue("StockTransactionFactoryConfigFileLocation", key);
        }

        public static bool GetSwitch(string key)
        {
            return GetConfigurationValue("SwitchConfigFileLocation", key).ToUpperInvariant().Equals("TRUE");
        }

        public static string GetTransactionChecklistParameter(string key)
        {
            return GetConfigurationValue("TransactionChecklistConfigFileLocation", key);
        }

        public static string GetUpdaterParameter(string key)
        {
            return GetConfigurationValue("UpdaterConfigFileLocation", key);
        }

        public static string SetConfigurationValues(string configFileName, string sectionName, string value)
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;

            if (section != null)
            {
                if (section.Settings[sectionName] != null)
                {
                    section.Settings[sectionName].Value = value;
                }
            }
            config.Save(ConfigurationSaveMode.Modified);

            return string.Empty;
        }

        public static void SetConfigurationValues(string configFileName, Collection<KeyValuePair<string, string>> sections)
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            AppSettingsSection appsetting = config.GetSection("appSettings") as AppSettingsSection;

            if (appsetting == null)
            {
                return;
            }

            if (sections != null && sections.Count > 0)
            {
                foreach (var section in sections)
                {
                    if (section.Key != null)
                    {
                        if (appsetting.Settings[section.Key] != null)
                        {
                            appsetting.Settings[section.Key].Value = section.Value;
                        }
                    }
                }
            }

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}