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
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Office;

namespace MixERP.Net.BusinessLayer.Security
{
    public static class User
    {
        public static bool SetSession(Page page, string user)
        {
            if (page != null)
            {
                try
                {

                    SignInView signInView = GetLastSignInView(user);
                    long logOnId = signInView.LogOnId;

                    if (logOnId.Equals(0))
                    {
                        MixERPWebpage.RequestLogOnPage();
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

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static bool SignIn(int officeId, string userName, string password, string culture, bool remember, Page page)
        {
            if (page != null)
            {
                try
                {
                    string remoteAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    string remoteUser = HttpContext.Current.Request.ServerVariables["REMOTE_USER"];

                    long logOnId = DatabaseLayer.Security.User.SignIn(officeId, userName, Conversion.HashSha512(password, userName), page.Request.UserAgent, remoteAddress, remoteUser, culture);

                    if (logOnId > 0)
                    {
                        page.Session["Culture"] = culture;
                        SetSession(page, userName);

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), remember, String.Empty, FormsAuthentication.FormsCookiePath);
                        string encryptedCookie = FormsAuthentication.Encrypt(ticket);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
                        cookie.Domain = FormsAuthentication.CookieDomain;
                        cookie.Path = ticket.CookiePath;

                        page.Response.Cookies.Add(cookie);
                        page.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, remember));

                        return true;
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                    //Swallow the exception here
                }
            }

            return false;
        }

        public static SignInView GetLastSignInView(string userName)
        {
            return DatabaseLayer.Security.User.GetLastSignInView(userName);
        }

    }
}
