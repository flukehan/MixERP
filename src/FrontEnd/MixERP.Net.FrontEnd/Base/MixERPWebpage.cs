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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.ApplicationState;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.Framework.Controls;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.Base
{
    public class MixERPWebpage : MixERPWebPageBase
    {
        private string currentPage;
        private Menu[] menus;

        /// <summary>
        ///     The pages, having no actual entry on database menu table, can have an OverridePath
        ///     which can be set to an existing page.
        /// </summary>
        public virtual string OverridePath { get; set; }

        /// <summary>
        ///     The pages which do not have actual entry on database menu table, but rather serve as placeholders are landing
        ///     pages.
        /// </summary>
        public virtual bool IsLandingPage { get; set; }

        /// <summary>
        ///     Set this to true for pages where you do not need users to be logged in.
        /// </summary>
        public bool SkipLoginCheck { get; set; }

        public static void RedirectToRootPage()
        {
            Log.Debug("Redirecting to '/' root page.");
            HttpContext.Current.Response.Redirect("~/");
        }

        public void RequestLoginPage()
        {
            FormsAuthentication.SignOut();
            Log.Information("User {UserName} was signed off.", AppUsers.GetCurrent().View.UserName);

            Log.Debug("Clearing Http Cookies.");
            foreach (string cookie in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpContext.Current.Request.Cookies.Remove(cookie);
                Log.Verbose("Cleared cookie: {Cookie}.", cookie);
            }


            Log.Verbose("Current page is {CurrentPage}.", this.currentPage);

            Page page = HttpContext.Current.Handler as Page;

            if (page == null)
            {
                return;
            }

            string loginUrl = (page).ResolveUrl(FormsAuthentication.LoginUrl);

            this.currentPage = HttpContext.Current.Request.Url.AbsolutePath;
            if (this.currentPage != loginUrl)
            {
                FormsAuthentication.RedirectToLoginPage(this.currentPage);
                HttpContext.Current.Response.Redirect("/SignIn.aspx?ReturnUrl=" + this.currentPage);
                Log.Information("Redirected to login page.");
            }
        }

        public static void SetAuthenticationTicket(HttpResponse response, long signInId, bool rememberMe)
        {
            if (response == null)
            {
                return;
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                signInId.ToString(CultureInfo.InvariantCulture), DateTime.Now, DateTime.Now.AddMinutes(30), rememberMe,
                String.Empty, FormsAuthentication.FormsCookiePath);
            string encryptedCookie = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = ticket.CookiePath;

            response.Cookies.Add(cookie);
            //response.Redirect(FormsAuthentication.GetRedirectUrl(signInId.ToString(CultureInfo.InvariantCulture), rememberMe));
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
                    if (AppUsers.GetCurrent().View.LoginId.ToLong().Equals(0))
                    {
                        AppUsers.SetCurrentLogin();
                        if (AppUsers.GetCurrent().View.LoginId.ToLong().Equals(0))
                        {
                            this.RequestLoginPage();
                        }
                    }
                }
                else
                {
                    if (!this.SkipLoginCheck)
                    {
                        this.RequestLoginPage();
                    }
                }
            }

            this.CheckForceLogOffFlags();
            base.OnInit(e);
        }

        private bool VerifyAccess()
        {
            bool policyExists = false;

            if (this.IsLandingPage)
            {
                return true;
            }

            foreach (Menu menu in this.menus)
            {
                if (menu != null && !string.IsNullOrWhiteSpace(menu.Url) &&
                    menu.Url.Replace("~", "").Equals(this.OverridePath))
                {
                    policyExists = true;
                    break;
                }
            }

            if (!policyExists)
            {
                Server.Transfer("~/Site/AccessIsDenied.aspx");
            }

            return policyExists;
        }

        private string GetMenu(int menuId)
        {
            string menu = string.Empty;
            if (menuId.Equals(0))
            {
                return menu;
            }

            Menu[] items = menus.Where(m => m.ParentMenuId.Equals(menuId)).ToArray();

            if (items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == 0)
                    {
                        menu += "<ul>";
                    }

                    //If this menu has any children, fetch them.
                    string childMenu = GetMenu(items[i].MenuId);
                    string id = items[i].MenuId.ToString(CultureInfo.InvariantCulture);
                    string url = string.IsNullOrWhiteSpace(items[i].Url)
                        ? "javascript:void(0);"
                        : this.Page.ResolveUrl(items[i].Url);

                    string cssClass = string.Empty;
                    string dataJSTree = string.Empty;
                    string dataSelected = string.Empty;

                    if (string.IsNullOrWhiteSpace(childMenu))
                    {
                        dataJSTree = "data-jstree='{\"type\":\"file\"}'";
                    }

                    if (url.Equals(this.OverridePath))
                    {
                        dataSelected = "data-selected='true'";
                        cssClass = "class='expanded'";
                        dataJSTree = "data-jstree='{\"type\":\"active\"}'";
                    }

                    string anchor = "<a href='" + url + "'>" + items[i].MenuText + "</a>";

                    menu += string.Format(Thread.CurrentThread.CurrentCulture,
                        "<li id='node{0}' {1} {2} data-menucode='{3}' {4}>",
                        id, cssClass, dataSelected, items[i].MenuCode, dataJSTree);


                    menu += anchor;
                    menu += childMenu;
                    menu += "</li>";

                    if (i == items.Length - 1)
                    {
                        menu += "</ul>";
                    }
                }
            }


            return menu;
        }

        private static void SetCulture()
        {
            var logins = AppUsers.GetCurrent();

            if (logins == null || logins.View == null)
            {
                return;
            }

            string cultureName = logins.View.Culture;

            if (string.IsNullOrWhiteSpace(cultureName))
            {
                return;
            }

            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        private void CheckForceLogOffFlags()
        {
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            Collection<FrequencyDates> applicationDates =
                Dates.GetFrequencyDates(AppUsers.GetCurrentUserDB());

            if (applicationDates != null)
            {
                FrequencyDates model = applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));

                if (model != null)
                {
                    if (model.ForcedLogOffTimestamp != null && !model.ForcedLogOffTimestamp.Equals(DateTime.MinValue))
                    {
                        if (model.ForcedLogOffTimestamp <= DateTime.Now &&
                            model.ForcedLogOffTimestamp >= AppUsers.GetCurrent().View.LoginDateTime)
                        {
                            this.RequestLoginPage();
                        }
                    }
                }
            }
        }
    }
}