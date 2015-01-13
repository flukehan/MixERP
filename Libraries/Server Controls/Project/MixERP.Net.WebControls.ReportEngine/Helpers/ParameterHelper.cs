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
using System.IO;
using System.Xml;

namespace MixERP.Net.WebControls.ReportEngine.Helpers
{
    public static class ParameterHelper
    {
        public static Collection<Collection<KeyValuePair<string, object>>> BindParameters(string reportPath, Collection<KeyValuePair<string, object>> parameterCollection)
        {
            if (!File.Exists(reportPath))
            {
                return null;
            }

            Collection<Collection<KeyValuePair<string, object>>> collection = new Collection<Collection<KeyValuePair<string, object>>>();
            Collection<KeyValuePair<string, object>> parameters = new Collection<KeyValuePair<string, object>>();

            XmlNodeList dataSources = XmlHelper.GetNodes(reportPath, "//DataSource");

            foreach (XmlNode datasource in dataSources)
            {
                foreach (XmlNode parameterNodes in datasource.ChildNodes)
                {
                    if (parameterNodes.Name.Equals("Parameters"))
                    {
                        foreach (XmlNode parameterNode in parameterNodes.ChildNodes)
                        {
                            if (parameterNode.Attributes != null)
                            {
                                parameters.Add(new KeyValuePair<string, object>(parameterNode.Attributes["Name"].Value, GetParameterValue(parameterNode.Attributes["Name"].Value, parameterCollection)));
                            }
                        }
                    }
                }

                collection.Add(parameters);
            }

            return collection;
        }

        public static Collection<KeyValuePair<string, string>> GetParameters(string reportPath)
        {
            if (!File.Exists(reportPath))
            {
                return null;
            }

            Collection<KeyValuePair<string, string>> parameterCollection = new Collection<KeyValuePair<string, string>>();
            XmlNodeList dataSources = XmlHelper.GetNodes(reportPath, "//DataSource");

            foreach (XmlNode datasource in dataSources)
            {
                foreach (XmlNode parameters in datasource.ChildNodes)
                {
                    if (parameters.Name.Equals("Parameters"))
                    {
                        foreach (XmlNode parameter in parameters.ChildNodes)
                        {
                            if (parameter.Attributes != null && !KeyExists(parameter.Attributes["Name"].Value, parameterCollection))
                            {
                                parameterCollection.Add(new KeyValuePair<string, string>(parameter.Attributes["Name"].Value, parameter.Attributes["Type"].Value));
                            }
                        }
                    }
                }
            }

            return parameterCollection;
        }

        private static object GetParameterValue(string key, IEnumerable<KeyValuePair<string, object>> collection)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            if (collection == null)
            {
                return string.Empty;
            }

            foreach (KeyValuePair<string, object> item in collection)
            {
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }

            return string.Empty;
        }
        private static bool KeyExists(string key, IEnumerable<KeyValuePair<string, string>> collection)
        {
            foreach (KeyValuePair<string, string> item in collection)
            {
                if (item.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }
    }
}