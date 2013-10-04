/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using MixERP.Net.WebControls.ReportEngine.Helpers;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
        public void InitializeReport()
        {
            EnsureChildControls();

            //Check if the set report path is a valid file.
            if(!this.IsValid())
            {
                this.InitializeLiterals();

                reportTitleLiteral.Text = this.ReportNotFoundErrorMessage;
                reportTitleHidden.Value = reportTitleLiteral.Text;
                topSectionLiteral.Text = string.Format(System.Threading.Thread.CurrentThread.CurrentCulture, this.InvalidLocationErrorMessage, this.reportPath);
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
            if(string.IsNullOrWhiteSpace(this.ReportNotFoundErrorMessage))
            {
                this.ReportNotFoundErrorMessage = "Report not found.";
            }

            if(string.IsNullOrWhiteSpace(this.InvalidLocationErrorMessage))
            {
                this.InvalidLocationErrorMessage = "Invalid location.";
            }

            if(string.IsNullOrWhiteSpace(this.RunningTotalText))
            {
                this.InvalidLocationErrorMessage = "Total";
            }
        }

        private System.Collections.ObjectModel.Collection<string> DecimalFieldIndicesCollection;
        private void SetDecimalFields()
        {
            string decimalFieldIndices = string.Empty;

            //Get the list of datasources for this report.
            XmlNodeList dataSourceList = XmlHelper.GetNodes(reportPath, "//DataSource");

            //Initializing decimal field indices collection.
            this.DecimalFieldIndicesCollection = new System.Collections.ObjectModel.Collection<string>();

            //Loop through each datasource in the datasource list.
            foreach(XmlNode dataSource in dataSourceList)
            {
                //Resetting the variable for each iteration.
                decimalFieldIndices = string.Empty;

                //Loop through each datasource child node.
                foreach(XmlNode node in dataSource.ChildNodes)
                {
                    //Selecting the nodes matching the tag <DecimalFieldIndices>.
                    if(node.Name.Equals("DecimalFieldIndices"))
                    {
                        decimalFieldIndices = node.InnerText;
                    }
                }

                //Add current "DecimalFieldIndices" to the collection object.
                //If a child node is found which matches the tag <DecimalFieldIncides> 
                //under the current node, the variable "decimalFieldIndices" will have
                //a value. If not, an empty string will be added to the collection.
                this.DecimalFieldIndicesCollection.Add(decimalFieldIndices);
            }
        }
        private System.Collections.ObjectModel.Collection<int> RunningTotalTextColumnIndexCollection;
        private System.Collections.ObjectModel.Collection<string> RunningTotalFieldIndicesCollection;
        private void SetRunningTotalFields()
        {
            //Get the list of datasources for this report.
            XmlNodeList dataSourceList = XmlHelper.GetNodes(reportPath, "//DataSource");
            int runningTotalTextColumnIndex = 0;
            string runningTotalFieldIndices = string.Empty;

            //Initializing running total text column index collection.
            this.RunningTotalTextColumnIndexCollection = new System.Collections.ObjectModel.Collection<int>();

            //Initializing running total field indices collection.
            this.RunningTotalFieldIndicesCollection = new System.Collections.ObjectModel.Collection<string>();

            //Loop through each datasource in the datasource list.
            foreach(XmlNode dataSource in dataSourceList)
            {
                //Resetting the variables for each iteration.
                runningTotalTextColumnIndex = 0;
                runningTotalFieldIndices = string.Empty;

                //Loop through each datasource child node.
                foreach(XmlNode node in dataSource.ChildNodes)
                {
                    //Selecting the nodes matching the tag <RunningTotalTextColumnIndex>.
                    if(node.Name.Equals("RunningTotalTextColumnIndex"))
                    {
                        runningTotalTextColumnIndex = MixERP.Net.Common.Conversion.TryCastInteger(node.InnerText);
                    }

                    //Selecting the nodes matching the tag <RunningTotalFieldIndices>.
                    if(node.Name.Equals("RunningTotalFieldIndices"))
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
                this.RunningTotalTextColumnIndexCollection.Add(runningTotalTextColumnIndex);
                this.RunningTotalFieldIndicesCollection.Add(runningTotalFieldIndices);
            }
        }

        private System.Collections.ObjectModel.Collection<System.Data.DataTable> DataTableCollection;
        private void SetDataSources()
        {
            int index = 0;

            //Get the list of datasources for this report.
            System.Xml.XmlNodeList dataSources = XmlHelper.GetNodes(reportPath, "//DataSource");

            //Initializing data source collection.
            this.DataTableCollection = new System.Collections.ObjectModel.Collection<System.Data.DataTable>();

            //Loop through each datasource in the datasource list.
            foreach(System.Xml.XmlNode datasource in dataSources)
            {
                //Loop through each datasource child node.
                foreach(System.Xml.XmlNode c in datasource.ChildNodes)
                {
                    //Selecting the nodes matching the tag <Query>.
                    if(c.Name.Equals("Query"))
                    {
                        index++;
                        string sql = c.InnerText;

                        //Initializing query parameter collection.
                        Collection<KeyValuePair<string, string>> parameters = new Collection<KeyValuePair<string, string>>();

                        //Check if this report needs has has parameters.
                        if(this.ParameterCollection != null)
                        {
                            //Get the parameter collection for this datasource.
                            parameters = this.ParameterCollection[index - 1];
                        }

                        //Get DataTable from SQL Query and parameter collection.
                        using(System.Data.DataTable table = MixERP.Net.WebControls.ReportEngine.Data.TableHelper.GetDataTable(sql, parameters))
                        {
                            //Add this datatable to the collection for later binding.
                            this.DataTableCollection.Add(table);
                        }
                    }
                }
            }
        }

        private void SetTitle()
        {
            string title = XmlHelper.GetNodeText(reportPath, "/PesReport/Title");
            reportTitleLiteral.Text = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseExpression(title);
            reportTitleHidden.Value = reportTitleLiteral.Text;

            if(!string.IsNullOrWhiteSpace(reportTitleLiteral.Text))
            {
                this.Page.Title = reportTitleLiteral.Text;
            }
        }

        private void SetTopSection()
        {
            string topSection = XmlHelper.GetNodeText(reportPath, "/PesReport/TopSection");
            topSection = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseExpression(topSection);
            topSection = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseDataSource(topSection, this.DataTableCollection);
            topSectionLiteral.Text = topSection;
        }

        private void SetBodySection()
        {
            string bodySection = XmlHelper.GetNodeText(reportPath, "/PesReport/Body/Content");
            bodySection = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseExpression(bodySection);
            bodySection = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseDataSource(bodySection, this.DataTableCollection);
            bodyContentsLiteral.Text = bodySection;
        }


        private void SetGridViews()
        {
            XmlNodeList gridViewDataSource = XmlHelper.GetNodes(reportPath, "//GridViewDataSource");
            string indices = string.Empty;

            foreach(XmlNode node in gridViewDataSource)
            {
                if(node.Attributes["Index"] != null)
                {
                    indices += node.Attributes["Index"].Value + ",";
                }
            }

            this.LoadGrid(string.Concat(indices));
        }

        private void LoadGrid(string indices)
        {
            foreach(string data in indices.Split(','))
            {
                string ds = data.Trim();

                if(!string.IsNullOrWhiteSpace(ds))
                {
                    //if(!ds.Contains(' '))
                    //{
                    int index = MixERP.Net.Common.Conversion.TryCastInteger(ds);

                    GridView grid = new GridView();
                    grid.EnableTheming = false;

                    grid.ID = "GridView" + ds;
                    grid.CssClass = "report";

                    grid.Width = Unit.Percentage(100);
                    grid.GridLines = GridLines.Both;
                    grid.RowDataBound += GridView_RowDataBound;
                    grid.DataBound += GridView_DataBound;
                    gridPlaceHolder.Controls.Add(grid);

                    grid.DataSource = this.DataTableCollection[index];
                    grid.DataBind();
                    //}
                }
            }

        }


        private void SetBottomSection()
        {
            string bottomSection = XmlHelper.GetNodeText(reportPath, "/PesReport/BottomSection");
            bottomSection = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseExpression(bottomSection);
            bottomSection = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseDataSource(bottomSection, this.DataTableCollection);
            bottomSectionLiteral.Text = bottomSection;
        }


        private void InstallReport()
        {
            if(this.IsValid())
            {
                XmlNode reportNode = XmlHelper.GetNode(reportPath, "/PesReport/Install/Report");

                if(reportNode == null)
                {
                    return;
                }

                string menuCode = reportNode.Attributes["MenuCode"].Value;
                string parentMenuCode = reportNode.Attributes["ParentMenuCode"].Value;
                int level = MixERP.Net.Common.Conversion.TryCastInteger(reportNode.Attributes["Level"].Value);
                string menuText = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseExpression(reportNode.Attributes["MenuText"].Value);

                string path = reportNode.Attributes["Path"].Value;

                MixERP.Net.WebControls.ReportEngine.Helpers.ReportInstaller.InstallReport(menuCode, parentMenuCode, level, menuText, path);
            }
        }


        private void CleanUp()
        {
            for(int i = 0; i < this.DataTableCollection.Count - 1; i++)
            {
                System.Data.DataTable table = this.DataTableCollection[i];
                if(table != null)
                {
                    table.Dispose();
                    if(table != null)
                    {
                        table = null;
                    }
                }

            }
        }
    }
}
