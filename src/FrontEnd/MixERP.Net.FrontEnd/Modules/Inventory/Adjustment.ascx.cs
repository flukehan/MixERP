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
using MixERP.Net.Core.Modules.Inventory.Data.Reports;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Inventory
{
    public partial class Adjustment : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateTopFormSegment(this.Placeholder1);
            this.CreateGridView(this.Placeholder1);
            this.CreateBottomPanel(this.Placeholder1);            
        }

        #region Header

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = Titles.StockAdjustment;
                container.Controls.Add(header);
            }
        }

        #endregion

        #region Bottom Panel

        private void CreateBottomForm(Control container)
        {
            using (HtmlGenericControl form = HtmlControlHelper.GetForm())
            {
                form.Attributes.Add("style", "width:300px;");


                using (HtmlGenericControl field = HtmlControlHelper.GetField())
                {
                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.StatementReference, "StatementReferenceTextArea"))
                    {
                        field.Controls.Add(label);
                    }

                    using (HtmlTextArea statementReferenceTextArea = new HtmlTextArea())
                    {
                        statementReferenceTextArea.ID = "StatementReferenceTextArea";
                        statementReferenceTextArea.Rows = 4;
                        field.Controls.Add(statementReferenceTextArea);
                    }

                    form.Controls.Add(field);
                }

                this.CreateSaveButton(form);

                container.Controls.Add(form);
            }
        }

        private void CreateBottomPanel(Control container)
        {
            this.CreateErrorDivBottom(container);
            this.CreateBottomForm(container);
        }

        private void CreateErrorDivBottom(Control container)
        {
            using (HtmlGenericControl errorDiv = new HtmlGenericControl("div"))
            {
                errorDiv.ID = "ErrorDiv";
                errorDiv.Attributes.Add("class", "big error vpad16");
                container.Controls.Add(errorDiv);
            }
        }

        private void CreateSaveButton(HtmlGenericControl container)
        {
            using (HtmlInputButton saveButton = new HtmlInputButton())
            {
                saveButton.ID = "SaveButton";
                saveButton.Value = Titles.Save;
                saveButton.Attributes.Add("class", "ui red button");
                container.Controls.Add(saveButton);
            }
        }

        #endregion

        #region IDisposable

        private bool disposed;
        private MixERPGridView grid;
        private Button showButton;
        private HiddenField storeHiddenField;
        private DateTextBox valueDateTextBox;

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

            if (this.valueDateTextBox != null)
            {
                this.valueDateTextBox.Dispose();
                this.valueDateTextBox = null;
            }

            if (this.storeHiddenField != null)
            {
                this.storeHiddenField.Dispose();
                this.storeHiddenField = null;
            }

            if (this.showButton != null)
            {
                this.showButton.Click -= this.ShowButton_Click;

                this.showButton.Dispose();
                this.showButton = null;
            }

            if (this.grid != null)
            {
                this.grid.RowCreated -= this.Grid_RowCreated;
                this.grid.Dispose();
                this.grid = null;
            }


            this.disposed = true;
        }

        #endregion

        #region Form

        private void CreateReferenceNumberField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ReferenceNumber, "ReferenceNumberInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText referenceNumberInputText = new HtmlInputText())
                {
                    referenceNumberInputText.ID = "ReferenceNumberInputText";
                    field.Controls.Add(referenceNumberInputText);
                }

                container.Controls.Add(field);
            }
        }

        private void CreateShowButton(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ListItems, "ShowButton"))
                {
                    field.Controls.Add(label);
                }

                this.showButton = new Button();
                this.showButton.ID = "ShowButton";
                this.showButton.TabIndex = 0;
                this.showButton.CssClass = "ui small positive button";
                this.showButton.Text = Titles.Show;
                this.showButton.OnClientClick = "return ShowButton_ClientClick();";
                this.showButton.Click += this.ShowButton_Click;

                field.Controls.Add(this.showButton);

                container.Controls.Add(field);
            }
        }

        private void CreateStoreField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("four wide column field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.SelectStore, "StoreSelect"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlSelect storeSelect = new HtmlSelect())
                {
                    storeSelect.ID = "StoreSelect";
                    storeSelect.Attributes.Add("tabindex", "0");
                    field.Controls.Add(storeSelect);
                }

                this.storeHiddenField = new HiddenField();
                this.storeHiddenField.ID = "StoreHidden";
                field.Controls.Add(this.storeHiddenField);

                container.Controls.Add(field);
            }
        }

        private void CreateTopFormSegment(Control container)
        {
            using (HtmlGenericControl form = new HtmlGenericControl("div"))
            {
                using (HtmlGenericControl fields = HtmlControlHelper.GetFields())
                {
                    form.Attributes.Add("class", "ui form segment");
                    this.CreateValueDateField(fields);
                    this.CreateReferenceNumberField(fields);
                    this.CreateStoreField(fields);
                    this.CreateShowButton(fields);
                    form.Controls.Add(fields);
                }
                container.Controls.Add(form);
            }
        }

        private void CreateValueDateField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ValueDate, "ValueDateTextBox"))
                {
                    field.Controls.Add(label);
                }

                this.valueDateTextBox = new DateTextBox();
                this.valueDateTextBox.ID = "ValueDateTextBox";
                this.valueDateTextBox.Mode = FrequencyType.Today;
                this.valueDateTextBox.Catalog = AppUsers.GetCurrentUserDB();
                this.valueDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

                field.Controls.Add(this.valueDateTextBox);

                container.Controls.Add(field);
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            int storeId = Conversion.TryCastInteger(this.storeHiddenField.Value);

            if (storeId.Equals(0))
            {
                return;
            }


            this.grid.DataSource = StockItems.ListClosingStock(AppUsers.GetCurrentUserDB(), storeId);
            this.grid.DataBind();
        }

        #endregion

        #region Grid View

        private void CreateGridView(Control container)
        {
            this.grid = new MixERPGridView();
            this.grid.ID = "grid";
            this.grid.CssClass = "ui striped collapsing table form";
            this.grid.GridLines = GridLines.None;
            this.grid.RowCreated += this.Grid_RowCreated;
            container.Controls.Add(this.grid);
        }

        private void Grid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count - 2; i++)
            {
                if (!IsEmptyCell(e.Row.Cells[i]))
                {
                    e.Row.Cells[i].Text = Titles.Get(e.Row.Cells[i].Text);
                }
            }
        }

        public bool IsEmptyCell(TableCell cell)
        {
            if (string.IsNullOrWhiteSpace(cell.Text))
            {
                return true;
            }

            if (cell.Text.ToUpperInvariant().Equals("&NBSP;"))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}