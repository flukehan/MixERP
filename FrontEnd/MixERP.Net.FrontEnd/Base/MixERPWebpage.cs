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
using MixERP.Net.FrontEnd.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Menu = MixERP.Net.Common.Models.Core.Menu;

namespace MixERP.Net.FrontEnd.Base
{
    public class MixERPWebpage : MixERPWebPageBase
    {
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

            Literal contentMenuLiteral = ((Literal)PageUtility.FindControlIterative(this.Master, "ContentMenuLiteral"));

            if (contentMenuLiteral != null)
            {
                string menu = GetContentPageMenu(this.Page, this.OverridePath);
                contentMenuLiteral.Text = menu;
            }

            base.OnLoad(e);
        }

        public static string GetContentPageMenu(Control page, string path)
        {
            if (page != null)
            {
                string menu = string.Empty;
                string relativePath = Conversion.GetRelativePath(path);
                Collection<Menu> rootMenus = Data.Core.Menu.GetRootMenuCollection(relativePath);

                if (rootMenus == null)
                {
                    return string.Empty;
                }

                if (rootMenus.Count > 0)
                {
                    menu = "<ul class=\"menu\">";

                    foreach (Menu rootMenu in rootMenus)
                    {
                        menu += string.Format(Thread.CurrentThread.CurrentCulture, "<li class=\"menu-title\"><a href=\"javascript:void(0);\">{0}</a></li>", rootMenu.MenuText);

                        Collection<Menu> childMenus = Data.Core.Menu.GetMenuCollection(rootMenu.MenuId, 2);

                        if (childMenus.Count > 0)
                        {
                            foreach (Menu childMenu in childMenus)
                            {
                                menu += string.Format(Thread.CurrentThread.CurrentCulture, "<li><a href='{0}' title='{1}' data-menucode='{2}'>{1}</a></li>", page.ResolveUrl(childMenu.Url), childMenu.MenuText, childMenu.MenuCode);
                            }
                        }
                    }

                    menu += "</ul>";
                }

                return menu;
            }

            return null;
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
            SetSession(this.Page, this.User.Identity.Name);
        }

        public static void SetAuthenticationTicket(Page page, string userName, bool rememberMe)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), rememberMe, String.Empty, FormsAuthentication.FormsCookiePath);
            string encryptedCookie = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = ticket.CookiePath;

            page.Response.Cookies.Add(cookie);
            page.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, rememberMe));
        }

        public static bool SetSession(Page page, string user)
        {
            if (page != null)
            {
                try
                {
                    SignInView signInView = Data.Office.User.GetLastSignInView(user);
                    long logOnId = signInView.LogOnId;

                    if (logOnId.Equals(0))
                    {
                        RequestLogOnPage();
                        return false;
                    }

                    page.Session["LogOnId"] = signInView.LogOnId;
                    page.Session["UserId"] = signInView.UserId;
                    page.Session["Culture"] = signInView.Culture;
                    page.Session["UserName"] = user;
                    page.Session["FullUserName"] = signInView.FullName;
                    page.Session["Role"] = signInView.Role;
                    page.Session["IsSystem"] = signInView.IsSystem;
                    page.Session["IsAdmin"] = signInView.IsAdmin;
                    page.Session["OfficeCode"] = signInView.OfficeCode;
                    page.Session["OfficeId"] = signInView.OfficeId;
                    page.Session["NickName"] = signInView.Nickname;
                    page.Session["OfficeName"] = signInView.OfficeName;
                    page.Session["RegistrationDate"] = signInView.RegistrationDate;
                    page.Session["RegistrationNumber"] = signInView.RegistrationNumber;
                    page.Session["PanNumber"] = signInView.PanNumber;
                    page.Session["AddressLine1"] = signInView.AddressLine1;
                    page.Session["AddressLine2"] = signInView.AddressLine2;
                    page.Session["Street"] = signInView.Street;
                    page.Session["City"] = signInView.City;
                    page.Session["State"] = signInView.State;
                    page.Session["Country"] = signInView.Country;
                    page.Session["ZipCode"] = signInView.ZipCode;
                    page.Session["Phone"] = signInView.Phone;
                    page.Session["Fax"] = signInView.Fax;
                    page.Session["Email"] = signInView.Email;
                    page.Session["Url"] = signInView.Url;

                    return true;
                }
                catch (DbException)
                {
                    //Swallow the exception
                }
            }

            return false;
        }

        public static void RedirectToRootPage()
        {
            HttpContext.Current.Response.Redirect("~/");
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