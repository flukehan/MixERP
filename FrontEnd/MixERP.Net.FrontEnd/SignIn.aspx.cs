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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Office;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Data.Helpers;
using MixERP.Net.FrontEnd.Data.Office;
using Resources;

namespace MixERP.Net.FrontEnd
{
    public partial class SignIn : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.CreateControls(this.Placeholder1);

            string challenge = Guid.NewGuid().ToString().Replace("-", "");

            this.Session["Challenge"] = challenge;

            PageUtility.RegisterJavascript("challenge", "var challenge = '" + challenge + "';", this.Page, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            this.CheckDbConnectivity();
            PageUtility.CheckInvalidAttempts(this.Page);

            try
            {
                this.BindBranchDropDownList();
            }
            catch
            {
                //Could not bind the branch office dropdownlist.
                //The target database does not contain mixerp schema.
                //Swallow the exception
                //and redirect to application offline page.
                this.RedirectToOfflinePage();
                return;
            }


            if (!this.IsPostBack)
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    long signInId = Conversion.TryCastLong(this.User.Identity.Name);

                    if (signInId > 0)
                    {
                        string sessionUser = Conversion.TryCastString(this.Page.Session["UserName"]);

                        if (string.IsNullOrWhiteSpace(sessionUser))
                        {
                            if (MixERPWebpage.SetSession(this.Page.Session, signInId))
                            {
                                this.RedirectToDashboard();
                            }
                        }
                        else
                        {
                            this.RedirectToDashboard();
                        }
                    }
                }
            }
        }

        private void BindBranchDropDownList()
        {
            IEnumerable<OfficeType> offices = Offices.GetOffices();
            this.branchSelect.DataSource = offices;
            this.branchSelect.DataBind();
        }

        private void CheckDbConnectivity()
        {
            if (!ServerConnectivity.IsDbServerAvailable())
            {
                this.RedirectToOfflinePage();
            }
        }

        private void RedirectToDashboard()
        {
            this.Response.Redirect("~/Dashboard/Index.aspx", true);
        }

        private void RedirectToOfflinePage()
        {
            this.Response.Redirect("~/Site/offline.html");
        }

        #region Controls

        private HtmlSelect branchSelect;

        private void CreateControls(Control container)
        {
            using (HtmlGenericControl signInForm = new HtmlGenericControl("div"))
            {
                signInForm.Attributes.Add("id", "signin-form");
                this.CreateLogo(signInForm);
                this.CreateForm(signInForm);

                container.Controls.Add(signInForm);
            }
        }

        private void CreateLogo(HtmlGenericControl container)
        {
            using (HtmlAnchor anchor = new HtmlAnchor())
            {
                anchor.HRef = "/SignIn.aspx";

                using (HtmlImage image = new HtmlImage())
                {
                    image.Src = this.ResolveClientUrl("~/Resource/Static/images/mixerp-logo.png");
                    anchor.Controls.Add(image);
                }
                container.Controls.Add(anchor);
            }
        }

        #region Form

        private void AddBranchField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "field");

                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.SelectYourBranch, "BranchSelect"))
                {
                    field.Controls.Add(label);
                }

                this.branchSelect = new HtmlSelect();
                this.branchSelect.ID = "BranchSelect";
                this.branchSelect.DataTextField = "OfficeName";
                this.branchSelect.DataValueField = "OfficeId";

                field.Controls.Add(this.branchSelect);

                container.Controls.Add(field);
            }
        }


        private void AddDivider(HtmlGenericControl container)
        {
            using (HtmlGenericControl divider = new HtmlGenericControl("div"))
            {
                divider.Attributes.Add("class", "ui divider");
                container.Controls.Add(divider);
            }
        }

        private void AddExceptionField(HtmlGenericControl container)
        {
            using (HtmlGenericControl exceptionField = new HtmlGenericControl("div"))
            {
                exceptionField.Attributes.Add("class", "exception field");
                container.Controls.Add(exceptionField);
            }
        }

        private void AddIconDivider(HtmlGenericControl container)
        {
            using (HtmlGenericControl iconDivider = new HtmlGenericControl("div"))
            {
                iconDivider.Attributes.Add("class", "ui horizontal icon divider");

                using (HtmlGenericControl icon = new HtmlGenericControl("i"))
                {
                    icon.Attributes.Add("class", "circular user icon");
                    iconDivider.Controls.Add(icon);
                }

                container.Controls.Add(iconDivider);
            }
        }

        private void AddLanguageField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "field");

                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.SelectLanguage, "LanguageSelect"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlSelect languageSelect = new HtmlSelect())
                {
                    languageSelect.ID = "LanguageSelect";
                    languageSelect.DataTextField = "Text";
                    languageSelect.DataValueField = "Value";
                    languageSelect.DataSource = this.GetLanguages();
                    languageSelect.DataBind();


                    field.Controls.Add(languageSelect);
                }
                container.Controls.Add(field);
            }
        }

        private void AddPasswordField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "field");

                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Password, "PasswordInputPassword"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputPassword passwordInputPassword = new HtmlInputPassword())
                {
                    passwordInputPassword.ID = "PasswordInputPassword";
                    passwordInputPassword.Attributes.Add("placeholder", Titles.Password);
                    field.Controls.Add(passwordInputPassword);
                }

                container.Controls.Add(field);
            }
        }

        private void AddRememberMeField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "field");


                using (HtmlGenericControl toggleCheckBox = new HtmlGenericControl("div"))
                {
                    toggleCheckBox.Attributes.Add("class", "ui toggle checkbox");


                    using (HtmlInputCheckBox rememberInputCheckBox = new HtmlInputCheckBox())
                    {
                        rememberInputCheckBox.ID = "RememberInputCheckBox";
                        toggleCheckBox.Controls.Add(rememberInputCheckBox);
                    }

                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.RememberMe))
                    {
                        toggleCheckBox.Controls.Add(label);
                    }


                    field.Controls.Add(toggleCheckBox);
                }

                container.Controls.Add(field);
            }
        }

        private void AddSignInButtonField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "field");

                using (HtmlInputButton signInButton = new HtmlInputButton())
                {
                    signInButton.ID = "SignInButton";
                    signInButton.Value = Titles.SignIn;
                    signInButton.Attributes.Add("class", "ui teal button");
                    field.Controls.Add(signInButton);
                }

                container.Controls.Add(field);
            }
        }

        private void AddUserIdField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "field");

                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.UserId, "UserNameInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText usernameInputText = new HtmlInputText())
                {
                    usernameInputText.ID = "UsernameInputText";
                    usernameInputText.Attributes.Add("placeholder", Titles.UserId);
                    field.Controls.Add(usernameInputText);

                    container.Controls.Add(field);
                }
            }
        }

        private void CreateForm(HtmlGenericControl container)
        {
            using (HtmlGenericControl form = new HtmlGenericControl("div"))
            {
                form.Attributes.Add("class", "ui form segment");
                form.Attributes.Add("style", "padding:24px 48px;");

                this.CreateHeader(form);
                this.AddDivider(form);
                this.AddUserIdField(form);
                this.AddPasswordField(form);
                this.AddRememberMeField(form);
                this.AddIconDivider(form);
                this.AddBranchField(form);
                this.AddLanguageField(form);
                this.AddExceptionField(form);
                this.AddSignInButtonField(form);

                container.Controls.Add(form);
            }
        }

        private void CreateHeader(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("div"))
            {
                header.Attributes.Add("class", "ui large header");
                header.Attributes.Add("style", "8px 0;");

                header.InnerText = Titles.SignIn;

                container.Controls.Add(header);
            }
        }

        private Collection<ListItem> GetLanguages()
        {
            Collection<ListItem> items = new Collection<ListItem>();
            items.Add(new ListItem("English (United States)", "en-US"));
            items.Add(new ListItem("English (Great Britain)", "en-GB"));
            items.Add(new ListItem("Français (France)", "fr-FR"));
            items.Add(new ListItem("Deutsch (Deutschland)", "de-DE"));
            items.Add(new ListItem("नेपाली (नेपाल)", "ne-NP"));
            items.Add(new ListItem("हिन्दी (India)", "hi-IN"));

            return items;
        }

        #endregion

        #endregion
    }
}