/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;
using System.Web.UI.WebControls;
using System.Xml;
using MixERP.Net.Common;
using MixERP.Net.WebControls.ReportEngine.Data;
using MixERP.Net.WebControls.ReportEngine.Helpers;

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
                this.topSectionLiteral.Text = string.Format(Thread.CurrentThread.CurrentCulture, this.InvalidLocationErrorMessage, this.reportPath);
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
            this.InstallReport();
            this.CleanUp();
        }


        /// <summary>
        /// Initializes the literals by providing a default value if those were not provided.
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
                this.InvalidLocationErrorMessage = "Total";
            }
        }

        private Collection<string> decimalFieldIndicesCollection;
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
        private Collection<int> runningTotalTextColumnIndexCollection;
        private Collection<string> runningTotalFieldIndicesCollection;
        private void SetRunningTotalFields()
        {
            //Get the list of datasources for this report.
            XmlNodeList dataSourceList = XmlHelper.GetNodes(this.reportPath, "//DataSource");

            //Initializing running total text column index collection.
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

        private Collection<DataTable> dataTableCollection;
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
                        Collection<KeyValuePair<string, string>> parameters = new Collection<KeyValuePair<string, string>>();

                        if (this.parameterCollection != null)
                        {
                            if (this.parameterCollection.Count >= count)
                            {
                                //Get the parameter collection for this datasource.
                                parameters = this.ParameterCollection[count - 1];
                            }
                        }


                        //Get DataTable from SQL Query and parameter collection.
                        using (DataTable table = TableHelper.GetDataTable(sql, parameters))
                        {
                            //Add this datatable to the collection for later binding.
                            this.dataTableCollection.Add(table);
                        }
                    }
                }
            }
        }

        private void SetTitle()
        {
            string title = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/Title");
            this.reportTitleLiteral.Text = ReportParser.ParseExpression(title);
            this.reportTitleHidden.Value = this.reportTitleLiteral.Text;

            if (!string.IsNullOrWhiteSpace(this.reportTitleLiteral.Text))
            {
                this.Page.Title = this.reportTitleLiteral.Text;
            }
        }

        private void SetTopSection()
        {
            string topSection = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/TopSection");
            topSection = ReportParser.ParseExpression(topSection);
            topSection = ReportParser.ParseDataSource(topSection, this.dataTableCollection);
            this.topSectionLiteral.Text = topSection;
        }

        private void SetBodySection()
        {
            string bodySection = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/Body/Content");
            bodySection = ReportParser.ParseExpression(bodySection);
            bodySection = ReportParser.ParseDataSource(bodySection, this.dataTableCollection);
            this.bodyContentsLiteral.Text = bodySection;
        }


        private void SetGridViews()
        {
            XmlNodeList gridViewDataSource = XmlHelper.GetNodes(this.reportPath, "//GridViewDataSource");
            string indices = string.Empty;

            foreach (XmlNode node in gridViewDataSource)
            {
                if (node.Attributes != null && node.Attributes["Index"] != null)
                {
                    indices += node.Attributes["Index"].Value + ",";
                }
            }

            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            this.LoadGrid(string.Concat(indices));
        }

        private void LoadGrid(string indices)
        {
            foreach (string data in indices.Split(','))
            {
                string ds = data.Trim();

                if (!string.IsNullOrWhiteSpace(ds))
                {
                    int index = Conversion.TryCastInteger(ds);

                    using (GridView grid = new GridView())
                    {
                        grid.EnableTheming = false;

                        grid.ID = "GridView" + ds;
                        grid.CssClass = "report";

                        grid.Width = Unit.Percentage(100);
                        grid.GridLines = GridLines.Both;
                        grid.RowDataBound += this.GridView_RowDataBound;
                        grid.DataBound += this.GridView_DataBound;
                        this.gridPlaceHolder.Controls.Add(grid);

                        grid.DataSource = this.dataTableCollection[index];
                        grid.DataBind();
                    }
                }
            }

        }


        private void SetBottomSection()
        {
            string bottomSection = XmlHelper.GetNodeText(this.reportPath, "/MixERPReport/BottomSection");
            bottomSection = ReportParser.ParseExpression(bottomSection);
            bottomSection = ReportParser.ParseDataSource(bottomSection, this.dataTableCollection);
            this.bottomSectionLiteral.Text = bottomSection;
        }


        private void InstallReport()
        {
            if (this.IsValid())
            {
                XmlNode reportNode = XmlHelper.GetNode(this.reportPath, "/MixERPReport/Install/Report");

                if (reportNode == null)
                {
                    return;
                }

                if (reportNode.Attributes != null)
                {
                    string menuCode = reportNode.Attributes["MenuCode"].Value;
                    string parentMenuCode = reportNode.Attributes["ParentMenuCode"].Value;
                    int level = Conversion.TryCastInteger(reportNode.Attributes["Level"].Value);
                    string menuText = ReportParser.ParseExpression(reportNode.Attributes["MenuText"].Value);

                    string path = reportNode.Attributes["Path"].Value;

                    ReportInstaller.InstallReport(menuCode, parentMenuCode, level, menuText, path);
                }
            }
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
    }
}
