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
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class RetainedEarnings : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateTopPanel(this.Placeholder1);
            this.SetQueryParameters();
            if (!this.Page.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void SetQueryParameters()
        {
            DateTime date = Conversion.TryCastDate(this.Request.QueryString["Date"]);
            int factor = Conversion.TryCastInteger(this.Request.QueryString["Factor"]);

            if (!Conversion.IsEmptyDate(date))
            {
                this.dateTextBox.Text = date.ToShortDateString();
            }

            if (factor > 0)
            {
                this.factorInputText.Value = factor.ToString(CultureInfo.CurrentUICulture);
            }
        }

        #region IDispoable

        private DateTextBox dateTextBox;
        private bool disposed;
        private HtmlInputText factorInputText;
        private GridView grid;
        private HtmlInputButton printButton;
        private Button showButton;

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
            if (!disposing)
            {
                return;
            }

            if (this.dateTextBox != null)
            {
                this.dateTextBox.Dispose();
                this.dateTextBox = null;
            }

            if (this.factorInputText != null)
            {
                this.factorInputText.Dispose();
                this.factorInputText = null;
            }

            if (this.showButton != null)
            {
                this.showButton.Dispose();
                this.showButton = null;
            }

            if (this.printButton != null)
            {
                this.printButton.Dispose();
                this.printButton = null;
            }

            if (this.grid != null)
            {
                this.grid.RowDataBound -= this.Grid_RowDataBound;
                this.grid.DataBound -= this.Grid_DataBound;
                this.grid.Dispose();
                this.grid = null;
            }

            this.disposed = true;
        }

        #endregion

        #region Grid

        private void CreateBoundFields()
        {
            this.grid.AutoGenerateColumns = false;

            GridViewHelper.AddDataBoundControl(this.grid, "value_date", Titles.ValueDate, "{0:d}");
            GridViewHelper.AddDataBoundControl(this.grid, "tran_code", Titles.TranCode);
            GridViewHelper.AddDataBoundControl(this.grid, "statement_reference", Titles.StatementReference);
            GridViewHelper.AddDataBoundControl(this.grid, "debit", Titles.Debit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.grid, "credit", Titles.Credit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.grid, "balance", Titles.Balance, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.grid, "office", Titles.Office);
            GridViewHelper.AddDataBoundControl(this.grid, "account", Titles.Account);
        }

        private void CreateGridPanel(Control container)
        {
            using (HtmlGenericControl autoOverflowPanel = new HtmlGenericControl("div"))
            {
                autoOverflowPanel.Attributes.Add("class", "auto-overflow-panel");

                this.grid = new GridView();
                this.grid.ID = "RetainedEarningsGridView";
                this.grid.CssClass = "ui celled segment table nowrap";
                this.grid.GridLines = GridLines.None;
                this.grid.RowDataBound += this.Grid_RowDataBound;
                this.grid.DataBound += this.Grid_DataBound;
                this.CreateBoundFields();
                autoOverflowPanel.Controls.Add(this.grid);

                container.Controls.Add(autoOverflowPanel);
            }
        }

        private void Grid_DataBound(object sender, EventArgs e)
        {
            if (this.grid.HeaderRow != null)
            {
                this.grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        #endregion

        #region Top Panel

        private void CreateTopPanel(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = Titles.RetainedEarnings;
                container.Controls.Add(header);
            }

            this.CreateForm(container);
            this.CreateGridPanel(container);
        }

        #region Form Segment

        private void AddDateField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Date, "DateTextBox"))
                {
                    field.Controls.Add(label);
                }

                this.dateTextBox = new DateTextBox();
                this.dateTextBox.ID = "DateTextBox";
                this.dateTextBox.Mode = FrequencyType.FiscalYearEndDate;
                this.dateTextBox.Catalog = AppUsers.GetCurrentUserDB();
                this.dateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

                field.Controls.Add(this.dateTextBox);

                container.Controls.Add(field);
            }
        }

        private void AddFactorField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Factor, "FactorInputText"))
                {
                    field.Controls.Add(label);
                }

                this.factorInputText = new HtmlInputText();
                this.factorInputText.ID = "FactorInputText";
                this.factorInputText.Value = "1000";
                this.factorInputText.Attributes.Add("class", "integer");
                field.Controls.Add(this.factorInputText);

                container.Controls.Add(field);
            }
        }

        private void AddPrintButton(HtmlGenericControl container)
        {
            this.printButton = new HtmlInputButton();
            this.printButton.ID = "PrintButton";
            this.printButton.Attributes.Add("class", "ui orange button");
            this.printButton.Value = Titles.Print;

            container.Controls.Add(this.printButton);
        }

        private void AddShowButton(HtmlGenericControl container)
        {
            this.showButton = new Button();
            this.showButton.ID = "ShowButton";
            this.showButton.CssClass = "ui green button";
            this.showButton.Text = Titles.Show;
            this.showButton.Click += this.ShowButton_Click;
            container.Controls.Add(this.showButton);
        }

        private void BindGrid()
        {
            DateTime date = Conversion.TryCastDate(this.dateTextBox.Text);
            int factor = Conversion.TryCastInteger(this.factorInputText.Value);
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            using (DataTable table = Data.Reports.RetainedEarnings.GetRetainedEarningStatementDataTable(AppUsers.GetCurrentUserDB(), date, officeId, factor))
            {
                this.grid.DataSource = table;
                this.grid.DataBind();
            }
        }

        private void CreateForm(Control container)
        {
            using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment())
            {
                using (HtmlGenericControl inlineFields = HtmlControlHelper.GetInlineFields())
                {
                    this.AddDateField(inlineFields);
                    this.AddFactorField(inlineFields);
                    this.AddShowButton(inlineFields);
                    this.AddPrintButton(inlineFields);

                    formSegment.Controls.Add(inlineFields);
                }

                container.Controls.Add(formSegment);
            }
        }

        private void ShowButton_Click(object sender, EventArgs eventArgs)
        {
            this.BindGrid();
        }

        #endregion

        #endregion
    }
}