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
