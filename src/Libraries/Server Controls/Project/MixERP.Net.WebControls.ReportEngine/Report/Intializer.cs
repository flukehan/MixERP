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
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.ReportEngine.Data;
using MixERP.Net.WebControls.ReportEngine.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using System.Xml;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
        public void InitializeReport()
        {
            this.EnsureChildControls();

            //Check if the set report path is a valid file.
            if (!this.IsValid())
            {
                this.InitializeLiterals();

                this.reportTitleLiteral.Text = this.ReportNotFoundErrorMessage;
                this.reportTitleHidden.Value = this.reportTitleLiteral.Text;
                this.topSectionLiteral.Text = string.Format(Thread.CurrentThread.CurrentCulture,
                    this.InvalidLocationErrorMessage, this.reportPath);
                return;
            }

            this.SetDecimalFields();
            this.SetRunningTotalFields();
            this.SetDataSources();
            this.SetTitle();
            this.SetTopSection();
            this.SetBodySection();
            this.SetGridViews();
            this.SetBottomSection();
            this.AddJavascript();
            this.CleanUp();
        }

        private void CleanUp()
        {
            for (int i = 0; i < this.dataTableCollection.Count - 1; i++)
            {
                DataTable table = this.dataTableCollection[i];
                if (table != null)
                {
                    table.Dispose();
                }
            }
        }

        private string GetAttributeValue(XmlNode node, string key)
        {
            if (node.Attributes != null && node.Attributes[key] != null)
            {
                return node.Attributes[key].Value;
            }

            return string.Empty;
        }

        /// <summary>
        ///     Initializes the literals by providing a default value if those were not provided.
        /// </summary>
        private void InitializeLiterals()
        {
            if (string.IsNullOrWhiteSpace(this.ReportNotFoundErrorMessage))
            {
                this.ReportNotFoundErrorMessage = "Report not found.";
            }

            if (string.IsNullOrWhiteSpace(this.InvalidLocationErrorMessage))
            {
                this.InvalidLocationErrorMessage = "Invalid location.";
            }

            if (string.IsNullOrWhiteSpace(this.RunningTotalText))
            {
                this.RunningTotalText = "Total";
            }
        }

        private void LoadGrid(string indices, string styles)
        {
            List<string> styleList = styles.Split(',').ToList();

            int counter = 0;

            foreach (string data in indices.Split(','))
            {
                string style = styleList[counter];

                string ds = data.Trim();

                if (!string.IsNullOrWhiteSpace(ds))
                {
                    int index = Conversion.TryCastInteger(ds);

                    using (MixERPGridView grid = new MixERPGridView())
                    {
                        grid.EnableTheming = false;

                        grid.ID = "GridView" + ds;
                        grid.CssClass = "report";
                        grid.Width = Unit.Percentage(100);
                        grid.GridLines = GridLines.None;
                        grid.RowDataBound += this.GridView_RowDataBound;

                        grid.DataBound += this.GridView_DataBound;
                        this.gridPlaceHolder.Controls.Add(grid);

                        grid.DataSource = this.dataTableCollection[index];
                        grid.DataBind();

                        if (!string.IsNullOrWhiteSpace(style))
                        {
                            grid.Attributes.Add("style", style);
                        }
                    }

                    counter++;
                }
            }
        }

        private string LoadPieCharts(string xml, XmlNode node, string id, int gridViewIndex, bool hideGridView,
            string type, int width, int height, int titleColumnIndex, int valueColumnIndex, Color backgroundColor,
            Color borderColor)
        {
            string pieChart = string.Format(CultureInfo.InvariantCulture,
                "<div style='background-color:{0};padding:24px;maring:0 auto;border:1px solid {1};'>" +
                "<canvas id='{2}' width='{3}px' height='{4}px'></canvas>" +
                "<br />" +
                "<div id='{2}-legend'></div></div>", ColorTranslator.ToHtml(backgroundColor),
                ColorTranslator.ToHtml(borderColor), id, width, height);

            string script = string.Format(CultureInfo.InvariantCulture, "$(document).ready(function () {{" +
                                                                        "preparePieChart('GridView{0}', '{1}', '{1}-legend', '{2}', {3}, {4}, {5});" +
                                                                        " }});", gridViewIndex, id, type,
                hideGridView.ToString().ToLower(), titleColumnIndex, valueColumnIndex);

            PageUtility.RegisterJavascript(id, script, this.Page, true);

            return xml.Replace(node.OuterXml, pieChart);
        }

        private void SetBodySection()
        {
            string bodySection = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/Body/Content");
            bodySection = ReportParser.ParseExpression(bodySection, this.dataTableCollection);
            bodySection = ReportParser.ParseDataSource(bodySection, this.dataTableCollection);
            this.bodyContentsLiteral.Text = bodySection;
        }

        private void SetBottomSection()
        {
            string bottomSection = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/BottomSection");
            bottomSection = ReportParser.ParseExpression(bottomSection, this.dataTableCollection);
            bottomSection = ReportParser.ParseDataSource(bottomSection, this.dataTableCollection);
            bottomSection = this.SetPieCharts(bottomSection);

            this.bottomSectionLiteral.Text = bottomSection;
        }

        private void SetDataSources()
        {
            int count = 0;

            //Get the list of datasources for this report.
            XmlNodeList dataSources = XmlHelper.GetNodes(this.reportPath, "//DataSource");

            //Initializing data source collection.
            this.dataTableCollection = new Collection<DataTable>();

            //Loop through each datasource in the datasource list.
            foreach (XmlNode datasource in dataSources)
            {
                //Loop through each datasource child node.
                foreach (XmlNode c in datasource.ChildNodes)
                {
                    //Selecting the nodes matching the tag <Query>.
                    if (c.Name.Equals("Query"))
                    {
                        count++;
                        string sql = c.InnerText;

                        //Initializing query parameter collection.
                        Collection<KeyValuePair<string, object>> parameters =
                            new Collection<KeyValuePair<string, object>>();

                        if (this.parameterCollection != null)
                        {
                            if (this.parameterCollection.Count >= count)
                            {
                                //Get the parameter collection for this datasource.
                                parameters = this.ParameterCollection[count - 1];
                            }
                        }

                        //Get DataTable from SQL Query and parameter collection.
                        using (DataTable table = TableHelper.GetDataTable(this.Catalog, sql, parameters))
                        {
                            //Add this datatable to the collection for later binding.
                            this.dataTableCollection.Add(table);
                        }
                    }
                }
            }
        }

        private void SetDecimalFields()
        {
            //Get the list of datasources for this report.
            XmlNodeList dataSourceList = XmlHelper.GetNodes(this.reportPath, "//DataSource");

            //Initializing decimal field indices collection.
            this.decimalFieldIndicesCollection = new Collection<string>();

            //Loop through each datasource in the datasource list.
            foreach (XmlNode dataSource in dataSourceList)
            {
                //Resetting the variable for each iteration.
                string decimalFieldIndices = string.Empty;

                //Loop through each datasource child node.
                foreach (XmlNode node in dataSource.ChildNodes)
                {
                    //Selecting the nodes matching the tag <DecimalFieldIndices>.
                    if (node.Name.Equals("DecimalFieldIndices"))
                    {
                        decimalFieldIndices = node.InnerText;
                    }
                }

                //Add current "DecimalFieldIndices" to the collection object.
                //If a child node is found which matches the tag <DecimalFieldIncides>
                //under the current node, the variable "decimalFieldIndices" will have
                //a value. If not, an empty string will be added to the collection.
                this.decimalFieldIndicesCollection.Add(decimalFieldIndices);
            }
        }

        private void SetGridViews()
        {
            XmlNodeList gridViewDataSource = XmlHelper.GetNodes(this.reportPath, "//GridViewDataSource");
            string indices = string.Empty;
            string styles = string.Empty;

            foreach (XmlNode node in gridViewDataSource)
            {
                if (node.Attributes != null)
                {
                    if (node.Attributes["Index"] != null)
                    {
                        indices += node.Attributes["Index"].Value + ",";
                    }

                    if (node.Attributes["Style"] != null)
                    {
                        styles += node.Attributes["Style"].Value + ",";
                    }
                    else
                    {
                        styles += ",";
                    }
                }
            }

            this.LoadGrid(indices, styles);
        }

        private string SetPieCharts(string xml)
        {
            XmlNodeList pieCharts = XmlHelper.GetNodesFromText(xml, "//PieChart");
            if (pieCharts == null)
            {
                return xml;
            }

            foreach (XmlNode node in pieCharts)
            {
                string id = this.GetAttributeValue(node, "ID");
                int gridViewIndex = Conversion.TryCastInteger(this.GetAttributeValue(node, "GridViewIndex"));

                string pieType = this.GetAttributeValue(node, "Type").ToLower(CultureInfo.InvariantCulture);

                int width = Conversion.TryCastInteger(this.GetAttributeValue(node, "Width"));
                int height = Conversion.TryCastInteger(this.GetAttributeValue(node, "Height"));
                int titleColumnIndex = Conversion.TryCastInteger(this.GetAttributeValue(node, "TitleColumnIndex"));
                int valueColumnIndex = Conversion.TryCastInteger(this.GetAttributeValue(node, "ValueColumnIndex"));

                bool hideGridView = Conversion.TryCastBoolean(this.GetAttributeValue(node, "HideGridView"));

                Color backgroundColor = ColorTranslator.FromHtml(this.GetAttributeValue(node, "BackgroundColor"));
                Color borderColor = ColorTranslator.FromHtml(this.GetAttributeValue(node, "BorderColor"));

                xml = this.LoadPieCharts(xml, node, id, gridViewIndex, hideGridView, pieType, width, height,
                    titleColumnIndex, valueColumnIndex, backgroundColor, borderColor);
            }

            return xml.Replace("<Charts>", "").Replace("</Charts>", "");
        }

        private void SetRunningTotalFields()
        {
            //Get the list of datasources for this report.
            XmlNodeList dataSourceList = XmlHelper.GetNodes(this.reportPath, "//DataSource");

            //Initializing running total text column gridViewIndex collection.
            this.runningTotalTextColumnIndexCollection = new Collection<int>();

            //Initializing running total field indices collection.
            this.runningTotalFieldIndicesCollection = new Collection<string>();

            //Loop through each datasource in the datasource list.
            foreach (XmlNode dataSource in dataSourceList)
            {
                //Resetting the variables for each iteration.
                int runningTotalTextColumnIndex = 0;
                string runningTotalFieldIndices = string.Empty;

                //Loop through each datasource child node.
                foreach (XmlNode node in dataSource.ChildNodes)
                {
                    //Selecting the nodes matching the tag <RunningTotalTextColumnIndex>.
                    if (node.Name.Equals("RunningTotalTextColumnIndex"))
                    {
                        runningTotalTextColumnIndex = Conversion.TryCastInteger(node.InnerText);
                    }

                    //Selecting the nodes matching the tag <RunningTotalFieldIndices>.
                    if (node.Name.Equals("RunningTotalFieldIndices"))
                    {
                        runningTotalFieldIndices = node.InnerText;
                    }
                }

                //Add current "RunningTotalTextColumnIndexCollection" and "RunningTotalFieldIndicesCollection"
                //to the collection object.
                //If child nodes are found which match the the associated tags
                //under the current node, the variable "runningTotalTextColumnIndex" and "runningTotalFieldIndices" will have
                //values. If not, an empty string for "runningTotalFieldIndices" and zero for "runningTotalTextColumnIndex"
                //will be added to the collection.
                this.runningTotalTextColumnIndexCollection.Add(runningTotalTextColumnIndex);
                this.runningTotalFieldIndicesCollection.Add(runningTotalFieldIndices);
            }
        }

        private void SetTitle()
        {
            string title = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/Title");
            string reportTitle = ReportParser.ParseExpression(title, this.dataTableCollection);

            this.reportTitleLiteral.Text = @"<h2>" + reportTitle + @"</h2>";
            this.reportTitleHidden.Value = reportTitle;

            if (!string.IsNullOrWhiteSpace(reportTitle))
            {
                this.Page.Title = reportTitle;
            }
        }

        private void SetTopSection()
        {
            string topSection = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/TopSection");
            topSection = ReportParser.ParseExpression(topSection, this.dataTableCollection);
            topSection = ReportParser.ParseDataSource(topSection, this.dataTableCollection);
            topSection = this.SetPieCharts(topSection);
            this.topSectionLiteral.Text = topSection;
        }
    }
}