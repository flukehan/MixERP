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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class BalanceSheet : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateTopPanel(this.Placeholder1);
            this.CreateGridPanel(this.Placeholder1);
            if (!this.Page.IsPostBack)
            {
                this.BindGrid();
            }
        }

        #region IDispoable

        private DateTextBox currentPeriodDateTextBox;
        private bool disposed;
        private HtmlInputText factorInputText;
        private GridView grid;
        private DateTextBox previousPeriodDateTextBox;
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

            if (this.currentPeriodDateTextBox != null)
            {
                this.currentPeriodDateTextBox.Dispose();
                this.currentPeriodDateTextBox = null;
            }

            if (this.previousPeriodDateTextBox != null)
            {
                this.previousPeriodDateTextBox.Dispose();
                this.previousPeriodDateTextBox = null;
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

        private void CreateGridPanel(Control container)
        {
            using (HtmlGenericControl autoOverflowPanel = new HtmlGenericControl("div"))
            {
                autoOverflowPanel.Attributes.Add("class", "auto-overflow-panel");

                this.grid = new GridView();
                this.grid.ID = "BalanceSheetGridView";
                this.grid.CssClass = "ui celled definition table segment nowrap";
                this.grid.GridLines = GridLines.None;
                this.grid.RowDataBound += this.Grid_RowDataBound;
                this.grid.DataBound += this.Grid_DataBound;
                this.CreateBoundFields();
                autoOverflowPanel.Controls.Add(this.grid);

                container.Controls.Add(autoOverflowPanel);
            }
        }

        #region Bound Fields

        private void CreateBoundFields()
        {
            this.grid.AutoGenerateColumns = false;
            GridViewHelper.AddDataBoundControl(this.grid, "item", "");
            GridViewHelper.AddDataBoundControl(this.grid, "previous_period", Titles.PreviousPeriod, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.grid, "current_period", Titles.CurrentPeriod, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.grid, "account_id", Titles.AccountId);
            GridViewHelper.AddDataBoundControl(this.grid, "is_retained_earning", string.Empty);
        }

        #endregion

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
                header.InnerText = Titles.BalanceSheet;
                container.Controls.Add(header);
            }

            this.CreateForm(container);
        }

        #region Form Segment

        private void AddCurrentPeriodField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.CurrentPeriod, "CurrentPeriodDateTextBox"))
                {
                    field.Controls.Add(label);
                }

                this.currentPeriodDateTextBox = new DateTextBox();
                this.currentPeriodDateTextBox.ID = "CurrentPeriodDateTextBox";
                this.currentPeriodDateTextBox.Mode = FrequencyType.FiscalYearEndDate;
                this.currentPeriodDateTextBox.Catalog = AppUsers.GetCurrentUserDB();
                this.currentPeriodDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

                field.Controls.Add(this.currentPeriodDateTextBox);

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

        private void AddPreviousPeriodField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.PreviousPeriod, "PreviousPeriodDateTextBox"))
                {
                    field.Controls.Add(label);
                }

                this.previousPeriodDateTextBox = new DateTextBox();
                this.previousPeriodDateTextBox.ID = "PreviousPeriodDateTextBox";
                this.previousPeriodDateTextBox.Mode = FrequencyType.FiscalYearStartDate;
                this.previousPeriodDateTextBox.Catalog = AppUsers.GetCurrentUserDB();
                this.previousPeriodDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

                field.Controls.Add(this.previousPeriodDateTextBox);

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
            DateTime previousTerm = Conversion.TryCastDate(this.previousPeriodDateTextBox.Text);
            DateTime currentTerm = Conversion.TryCastDate(this.currentPeriodDateTextBox.Text);
            int factor = Conversion.TryCastInteger(this.factorInputText.Value);
            int userId = AppUsers.GetCurrent().View.UserId.ToInt();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            using (DataTable table = Data.Reports.BalanceSheet.GetBalanceSheet(AppUsers.GetCurrentUserDB(), previousTerm, currentTerm, userId, officeId, factor))
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
                    this.AddPreviousPeriodField(inlineFields);
                    this.AddCurrentPeriodField(inlineFields);
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