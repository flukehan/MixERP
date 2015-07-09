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
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Data.Admin;
using MixERP.Net.Framework;
using MixERP.Net.Framework.Controls;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using Serilog;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.BackOffice.Admin
{
    public partial class ChangePassword : MixERPUserControl
    {
        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.LocalhostAdmin; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ChangePasswordControl changePassword = new ChangePasswordControl())
            {
                this.Placeholder1.Controls.Add(changePassword);
            }
        }
    }

    public class ChangePasswordControl : CompositeControl
    {
        private HtmlGenericControl panel;

        protected override void CreateChildControls()
        {
            this.panel = new HtmlGenericControl();
            this.CreateHeader(this.panel);
            this.CreateDivider(this.panel);
            this.CreateFormSegment(this.panel);
            this.CreateMessageLabel(this.panel);

            this.Controls.Add(this.panel);
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.panel.RenderControl(w);
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = Titles.ChangePassword;

                container.Controls.Add(header);
            }
        }

        private void CreateDivider(Control container)
        {
            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                container.Controls.Add(divider);
            }
        }

        private void CreateFormSegment(Control container)
        {
            using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment("ui tiny form segment"))
            {
                this.CreateUserField(formSegment);
                this.CreatePasswordField(formSegment);
                this.CreateUpdateButton(formSegment);
                container.Controls.Add(formSegment);
            }
        }

        private void CreateUserField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.SelectUser, "UserSelect"))
                {
                    field.Controls.Add(label);
                }

                this.userSelect = new DropDownList();
                this.userSelect.ID = "UserSelect";

                this.userSelect.DataTextField = "UserName";
                this.userSelect.DataValueField = "UserId";

                this.userSelect.DataSource = User.GetUserSelectorView(AppUsers.GetCurrentUserDB());
                this.userSelect.DataBind();


                field.Controls.Add(this.userSelect);

                container.Controls.Add(field);
            }
        }

        private void CreatePasswordField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (
                    HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.EnterNewPassword,
                        "PasswordInputPassword"))
                {
                    field.Controls.Add(label);
                }

                this.passwordInputPassword = new HtmlInputPassword();
                this.passwordInputPassword.ID = "PasswordInputPassword";

                field.Controls.Add(this.passwordInputPassword);

                container.Controls.Add(field);
            }
        }

        private void CreateUpdateButton(HtmlGenericControl container)
        {
            this.updateButton = new Button();
            this.updateButton.ID = "UpdateButton";
            this.updateButton.Text = Titles.Update;
            this.updateButton.CssClass = "ui positive button";

            this.updateButton.Click += this.UpdateButton_Click;

            container.Controls.Add(this.updateButton);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string username = this.userSelect.SelectedItem.Text;
            string password = this.passwordInputPassword.Value;

            if (string.IsNullOrWhiteSpace(username))
            {
                this.ShowError(Warnings.InvalidUser);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                this.ShowError(Warnings.PasswordCannotBeEmpty);
                return;
            }

            try
            {
                User user = new User();

                user.SetNewPassword(AppUsers.GetCurrentUserDB(), AppUsers.GetCurrent().View.UserId.ToInt(), username,
                    password);

                this.messageLabel.InnerText = Titles.PasswordUpdated;
            }
            catch (MixERPException ex)
            {
                Log.Warning("Cannot change password. {Exception}.", ex);
                this.ShowError(ex.Message);
            }
        }

        private void ShowError(string message)
        {
            this.messageLabel.Attributes["class"] = "big error";
            this.messageLabel.InnerText = message;
        }

        private void CreateMessageLabel(Control container)
        {
            this.messageLabel = new HtmlGenericControl("div");
            this.messageLabel.Attributes.Add("class", "ui large green header");

            container.Controls.Add(this.messageLabel);
        }

        #region IDisposable

        private DropDownList userSelect;
        private HtmlInputPassword passwordInputPassword;
        private Button updateButton;
        private HtmlGenericControl messageLabel;

        #endregion
    }
}