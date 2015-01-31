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

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;

namespace MixERP.Net.FrontEnd.Site.Account
{
    public partial class ChangePassword : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateDivider(this.Placeholder1);
            this.CreateMessage(this.Placeholder1);
            this.CreateForm(this.Placeholder1);
            this.CreateJavasriptLocalizedMessages();
        }

        private void CreateDivider(Control container)
        {
            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                container.Controls.Add(divider);
            }
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h1"))
            {
                header.InnerText = Resources.Titles.ChangePassword;

                container.Controls.Add(header);
            }
        }

        private void CreateJavasriptLocalizedMessages()
        {
            string javascript = "var enterCurrentPasswordLocalized = '{0}';var newPasswordCannotBeOldPasswordLocalized = '{1}';var enterNewPasswordLocalized = '{2}';var confirmationPasswordDoesNotMatchLocalized = '{3}';";
            javascript = string.Format(javascript, Resources.Warnings.PleaseEnterCurrentPassword, Resources.Warnings.NewPasswordCannotBeOldPassword, Resources.Warnings.PleaseEnterNewPassword, Resources.Warnings.ConfirmationPasswordDoesNotMatch);

            PageUtility.RegisterJavascript("ChangePassowrd_Localized", javascript, this.Page, true);
        }

        private void CreateMessage(Control container)
        {
            this.message = new HtmlGenericControl("div");
            container.Controls.Add(this.message);
        }

        #region Form

        public void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            string userName = CurrentSession.GetUserName();
            string currentPassword = this.passwordInputPassword.Value;
            string newPassword = this.newPasswordInputPassword.Value;
            string confirmPassword = this.confirmPasswordInputPassword.Value;

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                this.errorMessage.InnerText = Resources.Warnings.PleaseEnterCurrentPassword;
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                this.errorMessage.InnerText = Resources.Warnings.PleaseEnterNewPassword;
                return;
            }

            if (currentPassword.Equals(newPassword))
            {
                this.errorMessage.InnerText = Resources.Warnings.NewPasswordCannotBeOldPassword;
                return;
            }

            if (!newPassword.Equals(confirmPassword))
            {
                this.errorMessage.InnerText = Resources.Warnings.ConfirmationPasswordDoesNotMatch;
                return;
            }

            try
            {
                if (MixERP.Net.FrontEnd.Data.Office.User.ChangePassword(userName, currentPassword, newPassword))
                {
                    this.ShowMessage(Resources.Labels.YourPasswordWasChanged, "ui large green header");
                }
            }
            catch (MixERPException ex)
            {
                this.ShowMessage(ex.Message, "ui large red header");
            }
        }

        private void CreateButtons(HtmlGenericControl container)
        {
            this.changePasswordButton = new Button();
            this.changePasswordButton.Text = Resources.Titles.ChangePassword;
            this.changePasswordButton.CssClass = "ui pink button";
            this.changePasswordButton.OnClientClick = "return $('.form').form('validate form');";
            this.changePasswordButton.Click += this.ChangePasswordButton_Click;
            container.Controls.Add(this.changePasswordButton);

            using (HtmlAnchor cancelAnchor = new HtmlAnchor())
            {
                cancelAnchor.Attributes.Add("class", "ui purple button");
                cancelAnchor.HRef = "/";
                cancelAnchor.InnerText = Resources.Titles.Cancel;

                container.Controls.Add(cancelAnchor);
            }
        }

        private void CreateConfirmPasswordField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Resources.Titles.ConfirmPassword, "ConfirmPasswordInputPassword"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlGenericControl iconInput = HtmlControlHelper.GetLeftIconInput())
                {
                    this.confirmPasswordInputPassword = new HtmlInputPassword();
                    this.confirmPasswordInputPassword.ID = "ConfirmPasswordInputPassword";

                    iconInput.Controls.Add(this.confirmPasswordInputPassword);

                    using (HtmlGenericControl icon = HtmlControlHelper.GetIcon("key icon"))
                    {
                        iconInput.Controls.Add(icon);
                    }

                    field.Controls.Add(iconInput);
                }

                container.Controls.Add(field);
            }
        }

        private void CreateCurrentPasswordField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Resources.Titles.CurrentPassword, "PasswordInputPassword"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlGenericControl iconInput = HtmlControlHelper.GetLeftIconInput())
                {
                    this.passwordInputPassword = new HtmlInputPassword();
                    this.passwordInputPassword.ID = "PasswordInputPassword";

                    iconInput.Controls.Add(this.passwordInputPassword);

                    using (HtmlGenericControl icon = HtmlControlHelper.GetIcon("key icon"))
                    {
                        iconInput.Controls.Add(icon);
                    }

                    field.Controls.Add(iconInput);
                }

                container.Controls.Add(field);
            }
        }

        private void CreateForm(Control container)
        {
            using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment("ui tiny form segment"))
            {
                this.CreateUserNameField(formSegment);
                this.CreateCurrentPasswordField(formSegment);
                this.CreateNewPasswordField(formSegment);
                this.CreateConfirmPasswordField(formSegment);
                this.CreateButtons(formSegment);
                this.CreateMessageContainer(formSegment);

                container.Controls.Add(formSegment);
            }
        }

        private void CreateMessageContainer(HtmlGenericControl container)
        {
            this.errorMessage = new HtmlGenericControl("div");
            this.errorMessage.Attributes.Add("class", "ui inverted pink error message segment");
            container.Controls.Add(this.errorMessage);
        }

        private void CreateNewPasswordField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Resources.Titles.NewPassword, "NewPasswordInputPassword"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlGenericControl iconInput = HtmlControlHelper.GetLeftIconInput())
                {
                    this.newPasswordInputPassword = new HtmlInputPassword();
                    this.newPasswordInputPassword.ID = "NewPasswordInputPassword";

                    iconInput.Controls.Add(this.newPasswordInputPassword);

                    using (HtmlGenericControl icon = HtmlControlHelper.GetIcon("key icon"))
                    {
                        iconInput.Controls.Add(icon);
                    }

                    field.Controls.Add(iconInput);
                }

                container.Controls.Add(field);
            }
        }

        private void CreateUserNameField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Resources.Titles.Username, "UserNameInputText"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlGenericControl iconInput = HtmlControlHelper.GetLeftIconInput())
                {
                    using (HtmlInputText userNameInputText = new HtmlInputText())
                    {
                        userNameInputText.ID = "UserNameInputText";
                        userNameInputText.Attributes.Add("readonly", "readonly");
                        userNameInputText.Value = CurrentSession.GetUserName();

                        iconInput.Controls.Add(userNameInputText);
                    }

                    using (HtmlGenericControl icon = HtmlControlHelper.GetIcon("user icon"))
                    {
                        iconInput.Controls.Add(icon);
                    }

                    field.Controls.Add(iconInput);
                }

                container.Controls.Add(field);
            }
        }

        #region IDisposable

        private Button changePasswordButton;
        private HtmlInputPassword confirmPasswordInputPassword;
        private bool disposed;
        private HtmlGenericControl errorMessage;
        private HtmlGenericControl message;
        private HtmlInputPassword newPasswordInputPassword;
        private HtmlInputPassword passwordInputPassword;

        public override void Dispose()
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

            if (this.changePasswordButton != null)
            {
                this.changePasswordButton.Dispose();
                this.changePasswordButton = null;
            }

            if (this.confirmPasswordInputPassword != null)
            {
                this.confirmPasswordInputPassword.Dispose();
                this.confirmPasswordInputPassword = null;
            }

            if (this.errorMessage != null)
            {
                this.errorMessage.Dispose();
                this.errorMessage = null;
            }

            if (this.message != null)
            {
                this.message.Dispose();
                this.message = null;
            }

            if (this.newPasswordInputPassword != null)
            {
                this.newPasswordInputPassword.Dispose();
                this.newPasswordInputPassword = null;
            }

            if (this.passwordInputPassword != null)
            {
                this.passwordInputPassword.Dispose();
                this.passwordInputPassword = null;
            }
            this.disposed = true;
        }

        #endregion

        private void ShowMessage(string msg, string cssClass)
        {
            this.message.InnerText = msg;
            this.message.Attributes.Add("class", cssClass);
        }

        #endregion    }
    }
}