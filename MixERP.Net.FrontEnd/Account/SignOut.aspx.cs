/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web.Security;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Account
{
    public partial class SignOut : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session.Remove("UserName");
            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
            this.Response.Redirect("~/SignIn.aspx");
        }
    }
}