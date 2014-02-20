/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace MixERP.Net.WebControls.ReportEngine.Helpers
{
    public static class XmlHelper
    {
        public static XmlNodeList GetNodes(string path, string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc.SelectNodes(name);
        }

        public static string GetNodeText(string path, string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            var selectSingleNode = doc.SelectSingleNode(name);

            if (selectSingleNode != null)
            {
                return selectSingleNode.InnerXml;
            }

            return string.Empty;
        }

        [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static XmlNode GetNode(string path, string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc.SelectSingleNode(name);
        }
    }
}
