using MixERP.Net.Common;
using MixERP.Net.Core.Modules.BackOffice.Data;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Xml;

namespace MixERP.Net.Core.Modules.BackOffice.Models
{
    internal sealed class ReportBuilder
    {
        internal ReportDefinition Definition { get; set; }

        internal ReportBuilder(ReportDefinition definition)
        {
            this.Definition = definition;
        }

        internal string Build()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlTextWriter xml = new XmlTextWriter(ms, new UTF8Encoding(false)) { Formatting = Formatting.Indented })
                {
                    xml.WriteStartDocument();

                    string license = this.GetLicense();
                    xml.WriteComment(license);

                    xml.WriteStartElement("MixERPReport");
                    xml.WriteElementString("Title", this.Definition.Title);

                    this.WriteTopSection(xml);
                    this.WriteBody(xml);
                    this.WriteBottomSection(xml);

                    this.WriteDataSources(xml);

                    this.WriteMenu(xml);
                }

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        private void WriteTopSection(XmlTextWriter xml)
        {
            xml.WriteStartElement("TopSection");
            xml.WriteRaw(this.Definition.TopSection);
            xml.WriteEndElement();
        }

        private void WriteBody(XmlTextWriter xml)
        {
            xml.WriteStartElement("Body");

            xml.WriteStartElement("Content");
            xml.WriteRaw(this.Definition.Body);
            xml.WriteEndElement();

            this.WriteGridViews(xml);
            xml.WriteEndElement();
        }

        private void WriteBottomSection(XmlTextWriter xml)
        {
            xml.WriteStartElement("BottomSection");
            xml.WriteRaw(this.Definition.BottomSection);
            xml.WriteEndElement();
        }

        #region GridViews
        private void WriteGridViews(XmlTextWriter xml)
        {
            if (this.Definition.GridViews.Any())
            {
                xml.WriteStartElement("GridViews");

                foreach (ReportWriter.Grid grid in this.Definition.GridViews)
                {
                    this.WriteGridView(xml, grid);
                }

                xml.WriteEndElement();
            }
        }

        private void WriteGridView(XmlTextWriter xml, ReportWriter.Grid grid)
        {
            xml.WriteStartElement("GridView");
            xml.WriteStartElement("GridViewDataSource");
            xml.WriteAttributeString("Index", grid.DataSourceIndex.ToString());

            if (!string.IsNullOrWhiteSpace(grid.CssClass))
            {
                xml.WriteAttributeString("CssClass", grid.CssClass);
            }

            if (!string.IsNullOrWhiteSpace(grid.Style))
            {
                xml.WriteAttributeString("Style", grid.Style);
            }


            xml.WriteEndElement();
            xml.WriteEndElement();
        }
        #endregion

        #region DataSource
        private void WriteDataSources(XmlTextWriter xml)
        {
            if (this.Definition.DataSources.Any())
            {
                xml.WriteStartElement("DataSources");

                foreach (ReportWriter.DataSource dataSource in this.Definition.DataSources)
                {
                    this.WriteDataSource(xml, dataSource);
                }

                xml.WriteEndElement();
            }

        }


        private void WriteDataSource(XmlTextWriter xml, ReportWriter.DataSource dataSource)
        {
            xml.WriteStartElement("DataSource");
            xml.WriteElementString("Query", dataSource.Query);

            if (dataSource.Parameters.Any())
            {
                xml.WriteStartElement("Parameters");

                foreach (ReportWriter.ReportParameter parameter in dataSource.Parameters)
                {
                    this.WriteParameter(xml, parameter);
                }
                xml.WriteEndElement();
            }

            this.WriteRunningTotals(xml, dataSource);


            xml.WriteEndElement();
        }

        private void WriteParameter(XmlTextWriter xml, ReportWriter.ReportParameter parameter)
        {
            xml.WriteStartElement("Parameter");
            xml.WriteAttributeString("Name", parameter.Name);
            xml.WriteAttributeString("Type", parameter.Type);
            xml.WriteAttributeString("TestValue", parameter.TestValue);
            xml.WriteEndElement();
        }

        private void WriteRunningTotals(XmlTextWriter xml, ReportWriter.DataSource dataSource)
        {
            if (!string.IsNullOrWhiteSpace(dataSource.RunningTotalTextColumnIndex))
            {
                int index = Conversion.TryCastInteger(dataSource.RunningTotalTextColumnIndex);
                xml.WriteElementString("RunningTotalTextColumnIndex", index.ToString());
            }

            if (!string.IsNullOrWhiteSpace(dataSource.RunningTotalFieldIndices))
            {
                xml.WriteElementString("RunningTotalFieldIndices", dataSource.RunningTotalFieldIndices);
            }
        }

        #endregion

        private void WriteMenu(XmlTextWriter xml)
        {
            xml.WriteStartElement("Menu");
            xml.WriteAttributeString("Code", this.Definition.MenuCode);
            xml.WriteAttributeString("Text", this.Definition.Title);
            xml.WriteAttributeString("ParentMenuCode", this.Definition.ParentMenuCode);
            xml.WriteEndElement();
        }

        private string GetLicense()
        {
            string licenseFile = HostingEnvironment.MapPath("~/License.txt");

            if (!string.IsNullOrWhiteSpace(licenseFile) && File.Exists(licenseFile))
            {
                return File.ReadAllText(licenseFile);
            }

            return string.Empty;
        }
    }
}