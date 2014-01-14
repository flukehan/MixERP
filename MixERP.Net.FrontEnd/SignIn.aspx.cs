using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd
{
    public partial class SignIn : System.Web.UI.Page
    {
        private void CheckDBConnectivity()
        {
            if (!MixERP.Net.BusinessLayer.DBFactory.ServerConnectivity.IsDBServerAvailable())
            {
                this.RedirectToOfflinePage();
            }
        }

        private void RedirectToOfflinePage()
        {
            Response.Redirect("~/offline.html");
        }

        private void BindBranchDropDownList()
        {
            using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Office.Offices.GetOffices())
            {
                BranchDropDownList.DataSource = table;
                BranchDropDownList.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckDBConnectivity();

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

            UserIdTextBox.Focus();

            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    string user = User.Identity.Name;
                    if (!string.IsNullOrWhiteSpace(user))
                    {
                        string sessionUser = MixERP.Net.Common.Conversion.TryCastString(this.Page.Session["UserName"]);

                        if (string.IsNullOrWhiteSpace(sessionUser))
                        {
                            if (MixERP.Net.BusinessLayer.Security.User.SetSession(this.Page, user))
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
            Response.Redirect("~/Dashboard/Index.aspx", true);
        }

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            int officeId = MixERP.Net.Common.Conversion.TryCastInteger(BranchDropDownList.SelectedItem.Value);
            bool results = MixERP.Net.BusinessLayer.Security.User.SignIn(officeId, UserIdTextBox.Text, PasswordTextBox.Text, LanguageDropDownList.SelectedItem.Value, RememberMe.Checked, this.Page);

            if (!results)
            {
                MessageLiteral.Text = "<span class='error-message'>" + Resources.Warnings.UserIdOrPasswordIncorrect + "</span>";
            }
        }
    }
}