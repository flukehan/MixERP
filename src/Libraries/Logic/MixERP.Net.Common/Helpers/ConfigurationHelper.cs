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
using System.Web.Hosting;

namespace MixERP.Net.Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetConfigurationValues(string configFileName, string sectionName)
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
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

        public static string GetMixERPParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["MixERPConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetDbServerParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["DbServerConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetDbParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["DBParameterConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetMessagingParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["MessagingParameterConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ParameterConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetReportParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ReportConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetScrudParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ScrudConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetSwitch(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["SwitchConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetTransactionChecklistParameter(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["TransactionChecklistConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
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