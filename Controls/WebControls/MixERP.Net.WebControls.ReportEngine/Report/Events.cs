/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report : CompositeControl
    {
        public void ExcelAnchor_ServerClick(object sender, EventArgs e)
        {
            //EnsureChildControls();
            string html = reportHidden.Value;
            if(!string.IsNullOrWhiteSpace(html))
            {
                this.Page.Response.ContentType = "application/force-download";
                this.Page.Response.AddHeader("content-disposition", "attachment; filename=" + reportTitleHidden.Value + ".xls");
                this.Page.Response.Charset = "";
                this.Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.Page.Response.ContentType = "application/vnd.ms-excel";
                this.Page.Response.Write(html);
                this.Page.Response.Flush();
                this.Page.Response.Close();
            }
        }

        public void WordAnchor_ServerClick(object sender, EventArgs e)
        {
            //EnsureChildControls();
            string html = reportHidden.Value;
            if(!string.IsNullOrWhiteSpace(html))
            {
                this.Page.Response.ContentType = "application/force-download";
                this.Page.Response.AddHeader("content-disposition", "attachment; filename=" + reportTitleHidden.Value + ".doc");
                this.Page.Response.Charset = "";
                this.Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.Page.Response.ContentType = "application/vnd.ms-word";
                this.Page.Response.Write(html);
                this.Page.Response.Flush();
                this.Page.Response.Close();
            }
        }



        #region "GridView Events"
        void GridView_DataBound(object sender, EventArgs e)
        {
            GridView grid = (GridView)sender;

            int arg = MixERP.Net.Common.Conversion.TryCastInteger(grid.ID.Replace("GridView", ""));

            if(string.IsNullOrWhiteSpace(this.RunningTotalFieldIndicesCollection[arg]))
            {
                return;
            }

            if(grid.FooterRow == null)
            {
                return;
            }

            grid.FooterRow.Visible = true;

            for(int i = 0; i < this.RunningTotalTextColumnIndexCollection[arg]; i++)
            {
                grid.FooterRow.Cells[i].Visible = false;
            }

            grid.FooterRow.Cells[this.RunningTotalTextColumnIndexCollection[arg]].ColumnSpan = this.RunningTotalTextColumnIndexCollection[arg] + 1;
            grid.FooterRow.Cells[this.RunningTotalTextColumnIndexCollection[arg]].Text = this.RunningTotalText;
            grid.FooterRow.Cells[this.RunningTotalTextColumnIndexCollection[arg]].Style.Add("text-align", "right");
            grid.FooterRow.Cells[this.RunningTotalTextColumnIndexCollection[arg]].Font.Bold = true;

            foreach(string field in this.RunningTotalFieldIndicesCollection[arg].Split(','))
            {
                int index = MixERP.Net.Common.Conversion.TryCastInteger(field.Trim());

                decimal total = 0;

                if(index > 0)
                {
                    foreach(GridViewRow row in grid.Rows)
                    {
                        if(row.RowType == DataControlRowType.DataRow)
                        {
                            total += MixERP.Net.Common.Conversion.TryCastDecimal(row.Cells[index].Text);
                        }
                    }

                    grid.FooterRow.Cells[index].Text = string.Format(System.Threading.Thread.CurrentThread.CurrentCulture, "{0:N}", total);
                    grid.FooterRow.Cells[index].Font.Bold = true;
                }
            }
        }

        void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.Header)
            {
                for(int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string cellText = e.Row.Cells[i].Text;

                    cellText = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("ScrudResource", cellText, false);
                    e.Row.Cells[i].Text = cellText;
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Left;
                }
            }

            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grid = (GridView)sender;
                int arg = MixERP.Net.Common.Conversion.TryCastInteger(grid.ID.Replace("GridView", ""));

                //Apply formatting on decimal fields
                if(string.IsNullOrWhiteSpace(this.DecimalFieldIndicesCollection[arg]))
                {
                    return;
                }


                string decimalFields = this.DecimalFieldIndicesCollection[arg];
                foreach(string fieldIndex in decimalFields.Split(','))
                {
                    int index = MixERP.Net.Common.Conversion.TryCastInteger(fieldIndex);
                    decimal value = MixERP.Net.Common.Conversion.TryCastDecimal(e.Row.Cells[index].Text);
                    e.Row.Cells[index].Text = string.Format(System.Threading.Thread.CurrentThread.CurrentCulture, "{0:N}", value);
                }
            }
        }
        #endregion

    }
}
