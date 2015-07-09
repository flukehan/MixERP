using System;
using System.Xml;

namespace MixERP.Net.ReportManager
{
    internal class MenuParser
    {
        internal MenuParser(string content, string fileName)
        {
            this.Content = content;
            this.FileName = fileName;
        }

        internal string Content { get; set; }
        internal string FileName { get; set; }

        internal ReportMenu Parse()
        {
            XmlDocument xml = new XmlDocument();
            xml.PreserveWhitespace = true;
            xml.LoadXml(this.Content);

            ReportMenu menu = new ReportMenu();
            menu.FileName = this.FileName;
            menu.Text = this.ParseTitle(xml);
            menu.MenuCode = this.ParseMenuCode(xml);
            menu.ParentMenuCode = this.ParseParentMenuCode(xml);

            return menu;
        }

        private string ParseTitle(XmlDocument xml)
        {
            XmlNode title = xml.GetElementsByTagName("Title")[0];

            if (title != null)
            {
                string value = title.InnerText;

                if (value.StartsWith("{Resources", StringComparison.OrdinalIgnoreCase))
                {
                    value = ResourceHelper.TryParse(value);
                }

                return value;
            }

            return string.Empty;
        }

        private string ParseMenuCode(XmlDocument xml)
        {
            XmlNode menu = xml.GetElementsByTagName("Menu")[0];

            if (menu != null)
            {
                if (menu.Attributes != null && menu.Attributes["Code"] != null)
                {
                    return menu.Attributes["Code"].Value;
                }
            }

            return string.Empty;
        }

        private string ParseParentMenuCode(XmlDocument xml)
        {
            XmlNode menu = xml.GetElementsByTagName("Menu")[0];

            if (menu != null)
            {
                if (menu.Attributes != null && menu.Attributes["ParentMenuCode"] != null)
                {
                    return menu.Attributes["ParentMenuCode"].Value;
                }
            }

            return string.Empty;
        }
    }
}