/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Xml;

namespace MixERP.Net.Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetDbParameter(string keyName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["DBParameterConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetParameter(string keyName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ParamterConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetSwitch(string keyName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["SwitchConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetReportParameter(string keyName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetScrudParameter(string keyName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ScrudConfigFileLocation"]);
            return GetConfigurationValues(path, keyName);
        }

        public static string GetConfigurationValues(string configFileName, string sectionName)
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap() { ExeConfigFilename = configFileName };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;

            if (section.Settings[sectionName] != null)
            {
                return section.Settings[sectionName].Value;
            }

            return string.Empty;
        }
    }
}
