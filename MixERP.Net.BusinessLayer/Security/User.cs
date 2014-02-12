/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Data;

namespace MixERP.Net.BusinessLayer.Security
{
    public static class User
    {
        public static bool SetSession(System.Web.UI.Page page, string user)
        {
            if (page != null)
            {
                try
                {

                    MixERP.Net.Common.Models.Office.SignInView signInView = GetLastSignInView(user);
                    long LogOnId = signInView.LogOnId;

                    if (LogOnId.Equals(0))
                    {
                        MixERP.Net.BusinessLayer.MixERPWebPage.RequestLogOnPage();
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
                    page.Session["NickName"] = signInView.NickName;
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

        public static bool SignIn(int officeId, string userName, string password, string culture, bool remember, System.Web.UI.Page page)
        {
            if (page != null)
            {
                try
                {
                    string remoteAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    string remoteUser = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_USER"];

                    long LogOnId = MixERP.Net.DatabaseLayer.Security.User.SignIn(officeId, userName, MixERP.Net.Common.Conversion.HashSha512(password, userName), page.Request.UserAgent, remoteAddress, remoteUser, culture);

                    if (LogOnId > 0)
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
                catch
                {
                    //Swallow the exception here
                }
            }

            return false;
        }

        public static MixERP.Net.Common.Models.Office.SignInView GetLastSignInView(string userName)
        {
            return MixERP.Net.DatabaseLayer.Security.User.GetLastSignInView(userName);
        }

    }
}
