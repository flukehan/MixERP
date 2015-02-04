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
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;

namespace MixERP.Net.FrontEnd.Base
{
    public class MixERPWebpage : MixERPWebPageBase
    {
        /// <summary>
        ///     The pages, having no actual entry on database menu table, can have an OverridePath
        ///     which can be set to an existing page.
        /// </summary>
        public virtual string OverridePath { get; set; }

        /// <summary>
        ///     Set this to true for pages where you do not need users to be logged in.
        /// </summary>
        public bool SkipLoginCheck { get; set; }

        private static string GetContentPageMenu(Control page, string path, string currentPage)
        {
            if (page != null)
            {
                IEnumerable<Entities.Core.Menu> rootMenus = Data.Core.Menu.GetRootMenuCollection(path);

                string menu = "<ul>";

                foreach (Entities.Core.Menu rootMenu in rootMenus)
                {
                    string anchor = string.Format(Thread.CurrentThread.CurrentCulture, "<a href=\"javascript:void(0);\">{0}</a>", rootMenu.MenuText);

                    IEnumerable<Entities.Core.Menu> childMenus = Data.Core.Menu.GetMenuCollection(rootMenu.MenuId, 2);

                    if (childMenus != null)
                    {
                        menu += "<li>";
                        menu += anchor;
                        menu += "<ul>";

                        foreach (Entities.Core.Menu childMenu in childMenus)
                        {
                            string id = Conversion.TryCastString(childMenu.MenuId);

                            if (childMenu.Url.Replace("~", "").Equals(currentPage))
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

                return menu;
            }

            return null;
        }

        public static void RedirectToRootPage()
        {
            Log.Debug("Redirecting to '/' root page.");
            HttpContext.Current.Response.Redirect("~/");
        }

        public static void RequestLogOnPage()
        {
            FormsAuthentication.SignOut();
            Log.Information("User {UserName} was signed off.", CurrentSession.GetUserName());

            Log.Debug("Clearing Http Cookies.");
            foreach (string cookie in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpContext.Current.Request.Cookies.Remove(cookie);
                Log.Verbose("Cleared cookie: {Cookie}.", cookie);
            }

            string currentPage = HttpContext.Current.Request.Url.AbsolutePath;

            Log.Verbose("Current page is {CurrentPage}.", currentPage);

            Page page = HttpContext.Current.Handler as Page;

            if (page == null)
            {
                return;
            }

            string loginUrl = (page).ResolveUrl(FormsAuthentication.LoginUrl);

            if (currentPage != loginUrl)
            {
                FormsAuthentication.RedirectToLoginPage(currentPage);
                Log.Information("Redirected to login page.");
            }
        }

        public static void SetAuthenticationTicket(HttpResponse response, long signInId, bool rememberMe)
        {
            if (response == null)
            {
                return;
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, signInId.ToString(CultureInfo.InvariantCulture), DateTime.Now, DateTime.Now.AddMinutes(30), rememberMe, String.Empty, FormsAuthentication.FormsCookiePath);
            string encryptedCookie = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = ticket.CookiePath;

            response.Cookies.Add(cookie);
            //response.Redirect(FormsAuthentication.GetRedirectUrl(signInId.ToString(CultureInfo.InvariantCulture), rememberMe));
        }

        public static bool SetSession(HttpSessionState session, long signInId)
        {
            if (session != null)
            {
                try
                {
                    if (signInId.Equals(0))
                    {
                        RequestLogOnPage();
                        return false;
                    }

                    Entities.Office.SignInView signInView = Data.Office.User.GetSignInView(signInId);

                    if (signInView == null || signInView.LoginId == null)
                    {
                        session.Remove("UserName");
                        FormsAuthentication.SignOut();
                        return false;
                    }

                    session["SignInTimestamp"] = DateTime.Now;
                    session["LogOnId"] = signInView.LoginId;
                    session["UserId"] = signInView.UserId;
                    session["Culture"] = signInView.Culture;
                    session["UserName"] = signInView.UserName;
                    session["FullUserName"] = signInView.FullName;
                    session["Role"] = signInView.Role;
                    session["IsSystem"] = signInView.IsSystem;
                    session["IsAdmin"] = signInView.IsAdmin;
                    session["OfficeCode"] = signInView.OfficeCode;
                    session["OfficeId"] = signInView.OfficeId;
                    session["NickName"] = signInView.NickName;
                    session["OfficeName"] = signInView.OfficeName;
                    session["RegistrationDate"] = signInView.RegistrationDate;
                    session["CurrencyCode"] = signInView.CurrencyCode;
                    session["RegistrationNumber"] = signInView.RegistrationNumber;
                    session["PanNumber"] = signInView.PanNumber;
                    session["AddressLine1"] = signInView.AddressLine1;
                    session["AddressLine2"] = signInView.AddressLine2;
                    session["Street"] = signInView.Street;
                    session["City"] = signInView.City;
                    session["State"] = signInView.State;
                    session["Country"] = signInView.Country;
                    session["ZipCode"] = signInView.ZipCode;
                    session["Phone"] = signInView.Phone;
                    session["Fax"] = signInView.Fax;
                    session["Email"] = signInView.Email;
                    session["Url"] = signInView.Url;

                    SetCulture();


                    return true;
                }
                catch (DbException ex)
                {
                    Log.Warning("Cannot set session for user with SingIn #{SigninId}. {Exception}.", signInId, ex);
                }
            }

            return false;
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
                        int userId = Conversion.TryCastInteger(this.Context.Session["UserId"]);

                        if (userId.Equals(0))
                        {
                            this.SetSession();
                        }
                    }
                }
                else
                {
                    if (!this.SkipLoginCheck)
                    {
                        RequestLogOnPage();
                    }
                }
            }

            this.ForceLogOff();
            base.OnInit(e);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            Log.Verbose("Page loaded {Page}.", this.Page);

            if (string.IsNullOrWhiteSpace(this.OverridePath))
            {
                this.OverridePath = this.Page.Request.Url.AbsolutePath;
            }

            Literal contentMenuLiteral = ((Literal) PageUtility.FindControlIterative(this.Master, "ContentMenuLiteral"));

            string menu = "<div id=\"tree\" style='display:none;'><ul id='treeData'>";

            IEnumerable<Entities.Core.Menu> collection = Data.Core.Menu.GetMenuCollection(0, 0);

            if (collection == null)
            {
                return;
            }

            foreach (Entities.Core.Menu model in collection)
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


            menu += "</ul></div>";

            if (contentMenuLiteral != null)
            {
                contentMenuLiteral.Text = menu;
            }

            base.OnLoad(e);
        }

        private static void SetCulture()
        {
            if (SessionHelper.GetSessionKey("Culture") == null)
            {
                return;
            }

            string cultureName = Conversion.TryCastString(SessionHelper.GetSessionKey("Culture"));

            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void ForceLogOff()
        {
            int officeId = CurrentSession.GetOfficeId();
            Collection<ApplicationDateModel> applicationDates = ApplicationStateHelper.GetApplicationDates();

            if (applicationDates != null)
            {
                ApplicationDateModel model = applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));
                if (model != null)
                {
                    if (model.ForcedLogOffTimestamp != null)
                    {
                        if (model.ForcedLogOffTimestamp <= DateTime.Now && model.ForcedLogOffTimestamp >= CurrentSession.GetSignInTimestamp())
                        {
                            RequestLogOnPage();
                        }
                    }
                }
            }
        }

        private void SetSession()
        {
            SetSession(this.Page.Session, Conversion.TryCastLong(this.User.Identity.Name));
        }
    }
}