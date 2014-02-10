/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
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

namespace MixERP.Net.FrontEnd.Reports
{
    public partial class ReportViewer : MixERP.Net.BusinessLayer.MixERPWebReportPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.AddParameters();
        }

        private void AddParameters()
        {
            Collection<KeyValuePair<string, string>> collection = this.GetParameters();

            if (collection == null || collection.Count.Equals(0))
            {
                ReportParameterPanel.Style.Add("display", "none");
                ReportViewer11.Path = this.ReportPath();
                ReportViewer11.InitializeReport();
                return;
            }

            foreach (KeyValuePair<string, string> parameter in collection)
            {
                using (TextBox textBox = new TextBox())
                {
                    textBox.ID = parameter.Key.Replace("@", "") + "_text_box";

                    string label = "<label for='" + textBox.ID + "'>" + MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("ScrudResource", parameter.Key.Replace("@", "")) + "</label>";

                    if (parameter.Value.Equals("Date"))
                    {

                    }
                    else
                    {

                    }

                    AddRow(label, textBox);
                }
            }

            using (Button button = new Button())
            {
                button.ID = "UpdateButton";
                button.Text = Resources.Titles.Update;
                button.CssClass = "myButton";
                button.Click += UpdateButton_Click;

                AddRow("", button);
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (ReportParameterTable.Rows.Count.Equals(0))
            {
                return;
            }

            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();

            foreach (TableRow row in ReportParameterTable.Rows)
            {
                TableCell cell = row.Cells[1];

                if (cell.Controls[0] is TextBox)
                {
                    TextBox textBox = (TextBox)cell.Controls[0];
                    list.Add(new KeyValuePair<string, string>("@" + textBox.ID.Replace("_text_box", ""), textBox.Text));
                }
            }
            ReportViewer11.Path = this.ReportPath();

            //ReportViewer11.ParameterCollection = ParameterHelper.BindParameters(Server.MapPath(this.ReportPath()), list);

            foreach (var parameter in ParameterHelper.BindParameters(Server.MapPath(this.ReportPath()), list))
            {
                ReportViewer11.AddParameterToCollection(parameter);
            }
            
            ReportViewer11.InitializeReport();
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

                        ReportParameterTable.Rows.Add(row);
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
            string path = Server.MapPath(this.ReportPath());
            Collection<KeyValuePair<string, string>> collection = MixERP.Net.WebControls.ReportEngine.Helpers.ParameterHelper.GetParameters(path);
            return collection;
        }
    }
}