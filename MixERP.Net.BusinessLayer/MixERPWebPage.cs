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
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common;

namespace MixERP.Net.BusinessLayer
{
    public class MixERPWebpage : Page
    {
        private class MixERPPageStatePersister : PageStatePersister
        {
            public MixERPPageStatePersister(Page p) : base(p) { }
            public override void Load() { }
            public override void Save() { }
        }

        private MixERPPageStatePersister pageStatePersister;

        protected override PageStatePersister PageStatePersister
        {
            get
            {
                if (pageStatePersister == null)
                {
                    pageStatePersister = new MixERPPageStatePersister(this);
                }

                return pageStatePersister;
            }
        }

        /// <summary>
        /// Use this parameter on the Page_Init event of member pages.
        /// This parameter ensures that the user is not redirected to the login page 
        /// even when the user is not logged in.
        /// </summary>
        public bool NoLogOn { get; set; }

        /// <summary>
        /// Since we save the menu on the database, this parameter is only used 
        /// when there is no associated record of this page's url or path in the menu table.
        /// Use this to override or fake the page's url or path. This forces navigation menus 
        /// on the left hand side to be displayed in regards with the specified path.
        /// </summary>
        public string OverridePath { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.OverridePath))
            {
                this.OverridePath = this.Page.Request.Url.AbsolutePath;
            }

            Literal menuLiteral = ((Literal)PageUtility.FindControlIterative(this.Master, "ContentMenuLiteral"));

            if (menuLiteral != null)
            {
                string menu = MenuHelper.GetContentPageMenu(this.Page, this.OverridePath);
                menuLiteral.Text = menu;
            }

            base.OnLoad(e);
        }

        protected override void InitializeCulture()
        {
            SetCulture();
            base.InitializeCulture();
        }

        protected override void OnInit(EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Request.IsAuthenticated)
                {
                    if (this.Context.Session == null)
                    {
                        this.SetSession();
                    }
                    else
                    {
                        if (this.Context.Session["UserId"] == null)
                        {
                            this.SetSession();
                        }
                    }
                }
                else
                {
                    if (!this.NoLogOn)
                    {
                        RequestLogOnPage();
                    }
                }
            }

            base.OnInit(e);
        }

        private static void SetCulture()
        {
            if (HttpContext.Current.Session["Culture"] == null)
            {
                return;
            }

            string cultureName = HttpContext.Current.Session["Culture"].ToString();
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }


        private void SetSession()
        {
            Security.User.SetSession(this.Page, this.User.Identity.Name);
        }

        public static void RequestLogOnPage()
        {
            FormsAuthentication.SignOut();

            foreach (var cookie in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpContext.Current.Request.Cookies.Remove(cookie);
            }

            string currentPage = HttpContext.Current.Request.Url.AbsolutePath;

            Page page = HttpContext.Current.Handler as Page;

            if (page == null)
            {
                return;
            }

            string loginUrl = (page).ResolveUrl(FormsAuthentication.LoginUrl);

            if (currentPage != loginUrl)
            {
                FormsAuthentication.RedirectToLoginPage(currentPage);
            }
        }
    }
}
