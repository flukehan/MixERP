using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using MixERP.Net.BusinessLayer.DBFactory;
using MixERP.Net.BusinessLayer.Office;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Office;
using Resources;

namespace MixERP.Net.FrontEnd
{
    public partial class SignIn : Page
    {
        private void CheckDbConnectivity()
        {
            if (!ServerConnectivity.IsDbServerAvailable())
            {
                this.RedirectToOfflinePage();
            }
        }

        private void RedirectToOfflinePage()
        {
            this.Response.Redirect("~/offline.html");
        }

        private void BindBranchDropDownList()
        {
            Collection<Office> offices = Offices.GetOffices();
            this.BranchDropDownList.DataSource = offices;
            this.BranchDropDownList.DataBind();
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
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
                    string user = this.User.Identity.Name;
                    if (!string.IsNullOrWhiteSpace(user))
                    {
                        string sessionUser = Conversion.TryCastString(this.Page.Session["UserName"]);

                        if (string.IsNullOrWhiteSpace(sessionUser))
                        {
                            if (BusinessLayer.Security.User.SetSession(this.Page, user))
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

        private void RedirectToDashboard()
        {
            this.Response.Redirect("~/Dashboard/Index.aspx", true);
        }

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            int officeId = Conversion.TryCastInteger(this.BranchDropDownList.SelectedItem.Value);
            bool results = BusinessLayer.Security.User.SignIn(officeId, this.UserIdTextBox.Text, this.PasswordTextBox.Text, this.LanguageDropDownList.SelectedItem.Value, this.RememberMe.Checked, this.Page);

            if (!results)
            {
                this.MessageLiteral.Text = @"<span class='error-message'>" + Warnings.UserIdOrPasswordIncorrect + @"</span>";
            }
        }
    }
}