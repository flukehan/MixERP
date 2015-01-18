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

using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.StockAdjustmentFactory;
using System;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.Common;

namespace MixERP.Net.Core.Modules.Inventory
{

    public partial class Adjustment : MixERPUserControl
    {


        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateTopFormSegment(this.Placeholder1);
            this.CreateGridView(this.Placeholder1);
            this.CreateBottomPanel(this.Placeholder1);
            base.OnControlLoad(sender, e);
        }

        #region Bottom Panel

        private void CreateBottomPanel(Control container)
        {
            this.CreateErrorDivBottom(container);
            this.CreateBottomForm(container);

        }

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

        private void CreateErrorDivBottom(Control container)
        {
            using (HtmlGenericControl errorDiv = new HtmlGenericControl("div"))
            {
                errorDiv.ID = "ErrorDiv";
                errorDiv.Attributes.Add("class", "big error vpad16");
                container.Controls.Add(errorDiv);
            }
        }

        #endregion

        #region IDisposable

        private Button showButton;
        private HiddenField storeHiddenField;
        private MixERPGridView grid;
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
            if (!disposing)
            {
                return;
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

        #region Header

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = "Stock Adjustment";
                container.Controls.Add(header);
            }
        }

        #endregion


        #region Form

        private void CreateTopFormSegment(Control container)
        {
            using (HtmlGenericControl form = new HtmlGenericControl("div"))
            {
                using (HtmlGenericControl fields = HtmlControlHelper.GetFields())
                {
                    form.Attributes.Add("class", "ui form segment");
                    this.CreateStoreField(fields);
                    this.CreateShowButton(fields);
                    form.Controls.Add(fields);
                }
                container.Controls.Add(form);
            }
        }


        private void CreateStoreField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("four wide column field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel("Select Store", "StoreSelect"))
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

        private void CreateShowButton(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel("List Items", "ShowButton"))
                {
                    field.Controls.Add(label);
                }

                this.showButton = new Button();
                this.showButton.ID = "ShowButton";
                this.showButton.TabIndex = 0;
                this.showButton.CssClass = "ui small positive button";
                this.showButton.Text = "Show";
                this.showButton.OnClientClick = "return ShowButton_ClientClick();";
                this.showButton.Click += this.ShowButton_Click;

                field.Controls.Add(this.showButton);

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

            using (DataTable table = MixERP.Net.Core.Modules.Inventory.Data.Reports.StockItems.ListClosingStock(storeId))
            {
                table.Columns.Add("Actual");
                table.Columns.Add("Difference");
                this.grid.DataSource = table;
                this.grid.DataBind();
            }

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
                e.Row.Cells[i].Text = LocalizationHelper.GetResourceString(Assembly.GetAssembly(typeof(Adjustment)), "MixERP.Net.Core.Modules.Inventory.Resources.ScrudResource", e.Row.Cells[i].Text);
            }
        }

        #endregion

    }
}