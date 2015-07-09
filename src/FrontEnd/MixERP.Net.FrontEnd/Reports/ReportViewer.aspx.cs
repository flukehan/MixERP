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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.ReportEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Reports
{
    public partial class ReportViewer : MixERPWebReportPage
    {
        #region IDisposable

        private WebReport report;

        #endregion

        private Button updateButton = new Button();

        protected void Page_Init(object sender, EventArgs e)
        {
            this.report = new WebReport();
            this.report.Catalog = AppUsers.GetCurrentUserDB();
            this.report.AutoInitialize = false;
            this.Placeholder1.Controls.Add(this.report);
            this.AddParameters();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (this.ReportParameterTable.Rows.Count.Equals(0))
            {
                return;
            }

            Collection<KeyValuePair<string, object>> list = new Collection<KeyValuePair<string, object>>();

            foreach (TableRow row in this.ReportParameterTable.Rows)
            {
                TableCell cell = row.Cells[1];

                var box = cell.Controls[0] as TextBox;

                if (box != null)
                {
                    TextBox textBox = box;
                    list.Add(new KeyValuePair<string, object>("@" + textBox.ID.Replace("_text_box", ""), textBox.Text));
                }
            }
            this.report.Path = this.ReportPath();

            foreach (var parameter in ParameterHelper.BindParameters(this.Server.MapPath(this.ReportPath()), list))
            {
                this.report.AddParameterToCollection(parameter);
            }

            this.report.InitializeReport();
        }

        private void AddParameters()
        {
            Collection<KeyValuePair<string, string>> collection = this.GetParameters();

            if (collection == null || collection.Count.Equals(0))
            {
                this.ReportParameterPanel.Style.Add("display", "none");
                this.report.Path = this.ReportPath();
                this.report.InitializeReport();
                return;
            }

            foreach (KeyValuePair<string, string> parameter in collection)
            {
                using (TextBox textBox = new TextBox())
                {
                    textBox.ID = parameter.Key.Replace("@", "") + "_text_box";
                    string resource = ResourceManager.GetString(ConfigurationHelper.GetReportParameter("ResourceClassName"), parameter.Key.Replace("@", ""));

                    string label = "<label for='" + textBox.ID + "'>" + resource + "</label>";

                    if (parameter.Value.Equals("Date"))
                    {
                    }

                    this.AddRow(label, textBox);
                }
            }

            this.updateButton.ID = "UpdateButton";
            this.updateButton.Text = Titles.Update;
            this.updateButton.CssClass = "myButton report-button";
            this.updateButton.Click += this.UpdateButton_Click;

            this.AddRow("", this.updateButton);
        }

        private void AddRow(string label, Control control)
        {
            if (control == null)
            {
                return;
            }

            using (TableRow row = new TableRow())
            {
                using (TableCell cell = new TableCell())
                {
                    cell.Text = label;

                    using (TableCell controlCell = new TableCell())
                    {
                        controlCell.Controls.Add(control);

                        row.Cells.Add(cell);
                        row.Cells.Add(controlCell);

                        this.ReportParameterTable.Rows.Add(row);
                    }
                }
            }
        }

        private Collection<KeyValuePair<string, string>> GetParameters()
        {
            string path = this.Server.MapPath(this.ReportPath());
            Collection<KeyValuePair<string, string>> collection = ParameterHelper.GetParameters(path);
            return collection;
        }

        private string ReportPath()
        {
            string reportId = this.Request["Id"];
            string container = ConfigurationHelper.GetReportParameter("ReportContainer");

            if (string.IsNullOrWhiteSpace(reportId))
            {
                return null;
            }

            return Path.Combine(container, reportId);
        }

        #region IDispoable

        private bool disposed;

        public override sealed void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                base.Dispose();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.updateButton != null)
                {
                    this.updateButton.Dispose();
                    this.updateButton = null;
                }
            }

            this.disposed = true;
        }

        #endregion
    }
}