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
using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
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
        ///     Use this parameter on the Page_Init event of member pages.
        ///     This parameter ensures that the user is not redirected to the login page
        ///     even when the user is not logged in.
        /// </summary>
        public bool NoLogOn { get; set; }

        /// <summary>
        ///     Since we save the menu on the database, this parameter is only used
        ///     when there is no associated record of this page's url or path in the menu table.
        ///     Use this to override or fake the page's url or path. This forces navigation menus
        ///     on the left hand side to be displayed in regards with the specified path.
        /// </summary>
        public string OverridePath { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.OverridePath))
            {
                this.OverridePath = this.Page.Request.Url.AbsolutePath;
            }

            Literal contentMenuLiteral = ((Literal) PageUtility.FindControlIterative(this.Master, "ContentMenuLiteral"));

            string menu = "<div id=\"tree\" style='display:none;'><ul id='treeData'>";

            Collection<Menu> collection = Data.Core.Menu.GetMenuCollection(0, 0);

            if (collection == null)
            {
                return;
            }

            if (collection.Count > 0)
            {
                foreach (Menu model in collection)
                {
                    string menuText = model.MenuText;
                    string url = model.Url;
                    string id = Conversion.TryCastString(model.MenuId);

                    string subMenu = GetContentPageMenu(this.Page, url, this.OverridePath);
                    menu += string.Format(Thread.CurrentThread.CurrentCulture,
                        "<li id='node{0}'>" +
                        "<a id='anchorNode{0}' href='javascript:void(0);' title='{1}'>{1}</a>" +
                        "{2}" +
                        "</li>",
                        id,
                        menuText,
                        subMenu);
                }
            }

            menu += "</ul></div>";

            if (contentMenuLiteral != null)
            {
                contentMenuLiteral.Text = menu;
            }

            base.OnLoad(e);
        }

        public static string GetContentPageMenu(Control page, string path, string currentPage)
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
                    menu = "<ul>";

                    foreach (Menu rootMenu in rootMenus)
                    {
                        string anchor = string.Format(Thread.CurrentThread.CurrentCulture,
                            "<a href=\"javascript:void(0);\">{0}</a>", rootMenu.MenuText);

                        Collection<Menu> childMenus = Data.Core.Menu.GetMenuCollection(rootMenu.MenuId, 2);

                        if (childMenus.Count > 0)
                        {
                            menu += "<li>";
                            menu += anchor;
                            menu += "<ul>";

                            foreach (Menu childMenu in childMenus)
                            {
                                string id = Conversion.TryCastString(childMenu.MenuId);

                                if (childMenu.Url.Equals(currentPage))
                                {
                                    menu += string.Format(Thread.CurrentThread.CurrentCulture,
                                        "<li id='node{0}' class='expanded' data-selected='true' data-menucode='{1}' data-jstree='{{\"type\":\"active\"}}'><a id='anchorNode{0}' href='{2}' title='{3}'>{3}</a></li>",
                                        id, childMenu.MenuCode, page.ResolveUrl(childMenu.Url), childMenu.MenuText);
                                }
                                else
                                {
                                    menu += string.Format(Thread.CurrentThread.CurrentCulture,
                                        "<li id='item{0}' data-menucode='{1}' data-jstree='{{\"type\":\"file\"}}'><a href='{2}' title='{3}'>{3}</a></li>",
                                        id, childMenu.MenuCode, page.ResolveUrl(childMenu.Url), childMenu.MenuText);
                                }
                            }

                            menu += "</ul>";
                        }
                        else
                        {
                            menu += "<li data-jstree='{\"type\":\"file\"}'>" + anchor;
                        }
                    }

                    menu += "</li></ul>";
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
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now,
                DateTime.Now.AddMinutes(30), rememberMe, String.Empty, FormsAuthentication.FormsCookiePath);
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

            foreach (string cookie in HttpContext.Current.Request.Cookies.AllKeys)
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