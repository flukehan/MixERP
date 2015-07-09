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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Web.Hosting;
using System.Xml;

namespace MixERP.Net.Core.Modules.BackOffice.Models
{
    internal sealed class ReportParser
    {
        private static readonly string reportContainer =
            HostingEnvironment.MapPath(ConfigurationHelper.GetReportParameter("ReportContainer"));

        internal ReportParser(string fileName)
        {
            this.FileName = fileName;
            this.Definition = new ReportDefinition();
            this.Parse();
        }

        internal ReportDefinition Definition { get; set; }
        internal string FileName { get; set; }

        private void Parse()
        {
            string file = Path.Combine(reportContainer, this.FileName);

            XmlDocument xml = new XmlDocument();
            xml.PreserveWhitespace = true;
            xml.Load(file);

            this.ParseTitle(xml);
            this.ParseFileName();
            this.ParseMenuCode(xml);
            this.ParseParentMenuCode(xml);
            this.ParseTopSection(xml);
            this.ParseBody(xml);
            this.ParseBottomSection(xml);
            this.ParseDataSources(xml);
            this.ParseGridViews(xml);
        }

        private void ParseTitle(XmlDocument xml)
        {
            XmlNode title = xml.GetElementsByTagName("Title")[0];

            if (title != null)
            {
                this.Definition.Title = title.InnerText;
            }
        }

        private void ParseFileName()
        {
            this.Definition.FileName = this.FileName;
        }

        private void ParseMenuCode(XmlDocument xml)
        {
            XmlNode menu = xml.GetElementsByTagName("Menu")[0];

            if (menu != null)
            {
                if (menu.Attributes != null && menu.Attributes["Code"] != null)
                {
                    this.Definition.MenuCode = menu.Attributes["Code"].Value;
                }
            }
        }

        private void ParseParentMenuCode(XmlDocument xml)
        {
            XmlNode menu = xml.GetElementsByTagName("Menu")[0];

            if (menu != null)
            {
                if (menu.Attributes != null && menu.Attributes["ParentMenuCode"] != null)
                {
                    this.Definition.ParentMenuCode = menu.Attributes["ParentMenuCode"].Value;
                }
            }
        }

        private void ParseTopSection(XmlDocument xml)
        {
            XmlNode topSection = xml.GetElementsByTagName("TopSection")[0];

            if (topSection != null)
            {
                    this.Definition.TopSection = topSection.InnerXml;
            }
        }

        private void ParseBody(XmlDocument xml)
        {
            XmlNode body = xml.GetElementsByTagName("Body")[0];

            if (body != null)
            {
                XmlNode content = body.SelectSingleNode("Content");

                if (content != null)
                {
                    this.Definition.Body = content.InnerXml;
                }
            }
        }

        private void ParseBottomSection(XmlDocument xml)
        {
            XmlNode bottomSection = xml.GetElementsByTagName("BottomSection")[0];

            if (bottomSection != null)
            {
                this.Definition.BottomSection = bottomSection.InnerXml;
            }
        }

        private void ParseDataSources(XmlDocument xml)
        {
            Collection<ReportWriter.DataSource> dataSources = new Collection<ReportWriter.DataSource>();

            XmlNodeList dataSourceNodes = xml.SelectNodes("//DataSource");

            if (dataSourceNodes != null)
            {
                foreach (XmlNode dataSourceNode in dataSourceNodes)
                {
                    ReportWriter.DataSource dataSource = new ReportWriter.DataSource();

                    foreach (XmlNode node in dataSourceNode.ChildNodes)
                    {
                        if (node.Name.Equals("Query"))
                        {
                            dataSource.Query = node.InnerText;
                        }

                        if (node.Name.Equals("RunningTotalFieldIndices"))
                        {
                            dataSource.RunningTotalFieldIndices = node.InnerText;
                        }

                        if (node.Name.Equals("RunningTotalTextColumnIndex"))
                        {
                            dataSource.RunningTotalTextColumnIndex = node.InnerText;
                        }

                        if (node.Name.Equals("Parameters"))
                        {
                            Collection<ReportWriter.ReportParameter> parameters =
                                new Collection<ReportWriter.ReportParameter>();

                            foreach (XmlNode parameterNode in node.ChildNodes)
                            {
                                if (parameterNode.Name.Equals("Parameter"))
                                {
                                    ReportWriter.ReportParameter parameter = new ReportWriter.ReportParameter();

                                    if (parameterNode.Attributes != null)
                                    {
                                        if (parameterNode.Attributes["Name"] != null)
                                        {
                                            parameter.Name = parameterNode.Attributes["Name"].Value;
                                        }

                                        if (parameterNode.Attributes["Type"] != null)
                                        {
                                            parameter.Type = parameterNode.Attributes["Type"].Value;
                                        }

                                        if (parameterNode.Attributes["TestValue"] != null)
                                        {
                                            parameter.TestValue = parameterNode.Attributes["TestValue"].Value;
                                        }
                                    }

                                    parameters.Add(parameter);
                                }
                            }

                            dataSource.Parameters = parameters;
                        }
                    }
                    dataSources.Add(dataSource);
                }
            }

            this.Definition.DataSources = dataSources;
        }

        private void ParseGridViews(XmlDocument xml)
        {
            Collection<ReportWriter.Grid> grids = new Collection<ReportWriter.Grid>();
            XmlNodeList gridNodes = xml.SelectNodes("//GridViewDataSource");

            if (gridNodes != null)
            {
                foreach (XmlNode node in gridNodes)
                {
                    if (node.Attributes != null)
                    {
                        ReportWriter.Grid grid = new ReportWriter.Grid();


                        if (node.Attributes["Index"] != null)
                        {
                            grid.DataSourceIndex = Conversion.TryCastInteger(node.Attributes["Index"].Value);
                        }

                        if (node.Attributes["CssClass"] != null)
                        {
                            grid.CssClass = node.Attributes["CssClass"].Value;
                        }

                        if (node.Attributes["Style"] != null)
                        {
                            grid.Style = node.Attributes["Style"].Value;
                        }

                        grids.Add(grid);
                    }
                }
            }

            this.Definition.GridViews = grids;
        }
    }
}