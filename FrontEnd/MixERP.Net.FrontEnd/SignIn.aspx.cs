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

using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Models.Office;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Data.Helpers;
using MixERP.Net.FrontEnd.Data.Office;
using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace MixERP.Net.FrontEnd
{
    public partial class SignIn : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckDbConnectivity();

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

            this.UserIdTextBox.Focus();

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
                            if (MixERPWebpage.SetSession(this.Page, signInId))
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

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            int officeId = Conversion.TryCastInteger(this.BranchIdHiddenField.Value);

            try
            {
                bool result = Login(officeId, this.UserIdTextBox.Text, this.PasswordTextBox.Text, this.LanguageDropDownList.SelectedItem.Value, this.RememberMe.Checked, this.Page);

                if (!result)
                {
                    this.MessageLiteral.Text = @"<span class='big error'>" + Resources.Warnings.UserIdOrPasswordIncorrect + @"</span>";
                }
            }
            catch (MixERPException ex)
            {
                this.MessageLiteral.Text = @"<span class='big error'>" + ex.Message + @"</span>";
            }
        }

        private static bool Login(int officeId, string userName, string password, string culture, bool rememberMe, Page page)
        {
            long signInId = Data.Office.User.SignIn(officeId, userName, password, culture, rememberMe, page);

            if (signInId > 0)
            {
                MixERPWebpage.SetSession(page, signInId);
                MixERPWebpage.SetAuthenticationTicket(page, signInId, rememberMe);
                return true;
            }

            return false;
        }

        private void BindBranchDropDownList()
        {
            Collection<Office> offices = Offices.GetOffices();
            this.BranchDropDownList.DataSource = offices;
            this.BranchDropDownList.DataBind();
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
    }
}