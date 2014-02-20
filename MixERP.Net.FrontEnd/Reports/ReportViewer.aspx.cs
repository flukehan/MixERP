/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.BusinessLayer;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ReportEngine.Helpers;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

namespace MixERP.Net.FrontEnd.Reports
{
    public partial class ReportViewer : MixERPWebReportPage
    {
        private bool disposed;
        private Button updateButton = new Button();
        protected void Page_Init(object sender, EventArgs e)
        {
            this.AddParameters();
        }

        public sealed override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
            base.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
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
        }

        private void AddParameters()
        {
            Collection<KeyValuePair<string, string>> collection = this.GetParameters();

            if (collection == null || collection.Count.Equals(0))
            {
                this.ReportParameterPanel.Style.Add("display", "none");
                this.ReportViewer11.Path = this.ReportPath();
                this.ReportViewer11.InitializeReport();
                return;
            }

            foreach (KeyValuePair<string, string> parameter in collection)
            {
                using (TextBox textBox = new TextBox())
                {
                    textBox.ID = parameter.Key.Replace("@", "") + "_text_box";

                    string label = "<label for='" + textBox.ID + "'>" + LocalizationHelper.GetResourceString("ScrudResource", parameter.Key.Replace("@", "")) + "</label>";

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

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (this.ReportParameterTable.Rows.Count.Equals(0))
            {
                return;
            }

            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();

            foreach (TableRow row in this.ReportParameterTable.Rows)
            {
                TableCell cell = row.Cells[1];

                var box = cell.Controls[0] as TextBox;

                if (box != null)
                {
                    TextBox textBox = box;
                    list.Add(new KeyValuePair<string, string>("@" + textBox.ID.Replace("_text_box", ""), textBox.Text));
                }
            }
            this.ReportViewer11.Path = this.ReportPath();

            foreach (var parameter in ParameterHelper.BindParameters(this.Server.MapPath(this.ReportPath()), list))
            {
                this.ReportViewer11.AddParameterToCollection(parameter);
            }

            this.ReportViewer11.InitializeReport();
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

        private string ReportPath()
        {
            string id = this.Request["Id"];
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            return "~/Reports/Sources/" + id;
        }

        private Collection<KeyValuePair<string, string>> GetParameters()
        {
            string path = this.Server.MapPath(this.ReportPath());
            Collection<KeyValuePair<string, string>> collection = ParameterHelper.GetParameters(path);
            return collection;
        }
    }
}