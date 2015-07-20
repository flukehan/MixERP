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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Data.Admin;
using MixERP.Net.Framework.Controls;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Menu = MixERP.Net.Core.Modules.BackOffice.Data.Policy.Menu;

namespace MixERP.Net.Core.Modules.BackOffice.Policy
{
    public partial class MenuAccess : MixERPUserControl
    {
        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.LocalhostAdmin; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateFormPanel(this.Placeholder1);
            this.CreateGridPanel(this.Placeholder1);
            this.CreateHiddenField(this.Placeholder1);
            this.BindGrid();
        }

        private void CreateHiddenField(Control container)
        {
            this.selectedMenusHidden = new HiddenField();
            this.selectedMenusHidden.ID = "SelectedMenusHidden";
            container.Controls.Add(this.selectedMenusHidden);
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = HtmlControlHelper.GetPageHeader(Titles.MenuAccessPolicy))
            {
                container.Controls.Add(header);
            }
        }

        private void CreateFormPanel(Control container)
        {
            using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment("ui tiny pink form segment"))
            {
                formSegment.Attributes.Add("style", "position:fixed;right:4px;top:40px;");
                this.CreateHeader(formSegment);

                using (HtmlGenericControl field = HtmlControlHelper.GetField())
                {
                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.SelectUser, "UserSelect"))
                    {
                        field.Controls.Add(label);
                    }

                    this.userSelect = new DropDownList();
                    this.userSelect.ID = "UserSelect";
                    field.Controls.Add(this.userSelect);

                    this.userSelect.DataSource = User.GetUserSelectorView(AppUsers.GetCurrentUserDB());
                    this.userSelect.DataTextField = "UserName";
                    this.userSelect.DataValueField = "UserId";
                    this.userSelect.DataBind();

                    formSegment.Controls.Add(field);
                }

                using (HtmlGenericControl field = HtmlControlHelper.GetField())
                {
                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.SelectOffice, "OfficeSelect"))
                    {
                        field.Controls.Add(label);
                    }

                    this.officeSelect = new DropDownList();
                    this.officeSelect.ID = "OfficeSelect";
                    field.Controls.Add(this.officeSelect);

                    this.officeSelect.DataSource = Office.GetOffices(AppUsers.GetCurrentUserDB());
                    this.officeSelect.DataTextField = "OfficeName";
                    this.officeSelect.DataValueField = "OfficeId";
                    this.officeSelect.DataBind();

                    formSegment.Controls.Add(field);
                }

                this.CreateButtons(formSegment);

                container.Controls.Add(formSegment);
            }
        }

        private void CreateButtons(Control container)
        {
            using (HtmlGenericControl buttons = new HtmlGenericControl("div"))
            {
                buttons.Attributes.Add("class", "ui buttons vpad8");

                this.showButton = new Button();
                this.showButton.CssClass = "ui red button";
                this.showButton.Text = Titles.Show;
                this.showButton.Click += this.ShowButton_Click;
                buttons.Controls.Add(this.showButton);

                using (HtmlInputButton checkAllButton = new HtmlInputButton())
                {
                    checkAllButton.ID = "CheckAllButton";
                    checkAllButton.Attributes.Add("class", "ui blue button");
                    checkAllButton.Value = Titles.CheckAll;

                    buttons.Controls.Add(checkAllButton);
                }

                using (HtmlInputButton uncheckAllButton = new HtmlInputButton())
                {
                    uncheckAllButton.ID = "UncheckAllButton";
                    uncheckAllButton.Attributes.Add("class", "ui pink button");
                    uncheckAllButton.Value = Titles.UncheckAll;

                    buttons.Controls.Add(uncheckAllButton);
                }

                this.saveButton = new Button();
                this.saveButton.CssClass = "ui green button";
                this.saveButton.Text = Titles.Save;
                this.saveButton.Click += this.SaveButton_Click;
                this.saveButton.OnClientClick = "return updateSelection();";
                buttons.Controls.Add(this.saveButton);

                container.Controls.Add(buttons);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            int userId = Conversion.TryCastInteger(this.userSelect.SelectedValue);
            int officeId = Conversion.TryCastInteger(this.officeSelect.SelectedValue);

            string menus = this.selectedMenusHidden.Value;

            if (userId.Equals(0) || officeId.Equals(0))
            {
                return;
            }

            Menu.SaveMenuPolicy(AppUsers.GetCurrentUserDB(), userId, officeId, menus);
            this.BindGrid();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        private void CreateGridPanel(Control container)
        {
            this.grid = new MixERPGridView();
            grid.ID = "MenuAccessGridView";
            grid.RowDataBound += this.Grid_RowDataBound;

            container.Controls.Add(grid);
        }

        private void BindGrid()
        {
            int userId = Conversion.TryCastInteger(this.userSelect.SelectedValue);
            int officeId = Conversion.TryCastInteger(this.officeSelect.SelectedValue);
            string culture = AppUsers.GetCurrent().View.Culture;

            if (userId.Equals(0) || officeId.Equals(0))
            {
                return;
            }

            grid.DataSource = Menu.GetMenuPolicy(AppUsers.GetCurrentUserDB(), userId, officeId, culture);
            grid.DataBind();
        }

        private void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].Text = this.GetLocalizedResource(e.Row.Cells[i].Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string url = e.Row.Cells[5].Text;

                url = Server.HtmlDecode(url).Trim();

                using (HtmlAnchor anchor = new HtmlAnchor())
                {
                    anchor.InnerText = url;
                    anchor.Target = "_blank";
                    anchor.HRef = this.Page.ResolveUrl(url);
                    e.Row.Cells[5].Text = string.Empty;
                    e.Row.Cells[5].Controls.Add(anchor);
                }
            }
        }

        private string GetLocalizedResource(string key)
        {
            return Titles.Get(key);
        }

        #region IDisposable

        private DropDownList userSelect;
        private DropDownList officeSelect;
        private MixERPGridView grid;
        private Button showButton;
        private Button saveButton;
        private HiddenField selectedMenusHidden;
        private bool disposed;

        public override sealed void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }


        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.userSelect != null)
            {
                this.userSelect.Dispose();
                this.userSelect = null;
            }

            if (this.officeSelect != null)
            {
                this.officeSelect.Dispose();
                this.officeSelect = null;
            }

            if (this.grid != null)
            {
                this.grid.RowDataBound -= Grid_RowDataBound;
                this.grid.Dispose();
                this.grid = null;
            }

            if (this.showButton != null)
            {
                this.showButton.Click -= this.ShowButton_Click;
                this.showButton.Dispose();
                this.showButton = null;
            }

            if (this.saveButton != null)
            {
                this.saveButton.Click -= this.SaveButton_Click;
                this.saveButton.Dispose();
                this.saveButton = null;
            }

            if (this.selectedMenusHidden != null)
            {
                this.selectedMenusHidden.Dispose();
                this.selectedMenusHidden = null;
            }

            this.disposed = true;
        }

        #endregion
    }
}