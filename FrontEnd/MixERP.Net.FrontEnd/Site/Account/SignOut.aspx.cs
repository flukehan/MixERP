using MixERP.Net.BusinessLayer;
using System;
using System.Web.Security;

namespace MixERP.Net.FrontEnd.Site.Account
{
    public partial class SignOut : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session.Remove("UserName");
            FormsAuthentication.SignOut();
            this.Response.Redirect("~/SignIn.aspx");
        }
    }
}