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
            return GetSectionKey("MixERPDbParameters", keyName);
        }

        public static string GetParameter(string keyName)
        {
            return GetSectionKey("MixERPParameters", keyName);
        }

        public static string GetSwitch(string keyName)
        {
            return GetSectionKey("MixERPSwitches", keyName);
        }

        public static string GetReportParameter(string keyName)
        {
            return GetSectionKey("MixERPReportParameters", keyName);
        }

        public static string GetScrudParameter(string keyName)
        {
            return GetSectionKey("MixERPScrudParameters", keyName);
        }



        private static string GetSectionKey(string sectionName, string keyName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Resource/Configuration/" + sectionName + ".xml");            
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
