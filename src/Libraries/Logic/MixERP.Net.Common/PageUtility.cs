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

using MixERP.Net.i18n.Resources;
using Serilog;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.Common
{
    public static class PageUtility
    {
        public static void AddMeta(Page page, string name, string content)
        {
            using (HtmlMeta meta = new HtmlMeta())
            {
                meta.Name = name;
                meta.Content = content;
                page.Header.Controls.Add(meta);
            }
        }

        private static string GetUserHostAddress()
        {
            string host = string.Empty;

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.UserHostAddress != null)
                {
                    host = HttpContext.Current.Request.UserHostAddress;
                }
            }

            return host;
        }

        private static HttpBrowserCapabilities GetBrowser()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.UserHostAddress != null)
                {
                    return HttpContext.Current.Request.Browser;
                }
            }

            return new HttpBrowserCapabilities();
        }

        public static void CheckInvalidAttempts(Page page)
        {
            if (page != null)
            {
                int triedAttempts = InvalidPasswordAttempts(page.Session);
                int allowedAttemps = Conversion.TryCastInteger(ConfigurationManager.AppSettings["MaxInvalidPasswordAttempts"]);

                if (triedAttempts > 0)
                {
                    Log.Warning("{Count} of {Allowed} allowed invalid sign-in attempts from {Host}/{IP} using {Browser}.", triedAttempts, allowedAttemps, GetUserHostAddress(), GetUserIpAddress(), GetBrowser().Browsers);
                }

                if (triedAttempts >= allowedAttemps)
                {
                    Log.Error("Disallowed access to {Host}/{IP} using {Browser}.", GetUserHostAddress(), GetUserIpAddress(), GetBrowser().Browsers);
                    page.Response.Redirect("~/Static/AccessIsDenied.html");
                }
            }
        }

        /// <summary>
        ///     Check if the input is a valid url.
        /// </summary>
        /// <param name="url">Url to validate.</param>
        /// <returns>
        ///     Returns input if it's a valid url. If the input is not a valid url, returns empty string.
        /// </returns>
        public static string CleanUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }

            const string prefix = "http";

            if (url.Substring(0, prefix.Length) != prefix)
            {
                url = prefix + "://" + url;
            }

            using (MyClient client = new MyClient())
            {
                client.HeadOnly = true;
                try
                {
                    client.DownloadString(url);
                }
                catch (WebException)
                {
                    url = string.Empty;
                }

                return url;
            }
        }

        public static Control FindControlIterative(Control root, string id)
        {
            if (root == null)
            {
                return null;
            }

            if (root.ID == id)
            {
                return root;
            }
            foreach (Control c in root.Controls)
            {
                Control t = FindControlIterative(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        public static string GetCurrentDomainName()
        {
            if (HttpContext.Current == null)
            {
                return string.Empty;
            }

            string url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;

            if (HttpContext.Current.Request.Url.Port != 80)
            {
                url += ":" + HttpContext.Current.Request.Url.Port.ToString(CultureInfo.InvariantCulture);
            }

            return url;
        }

        public static string GetCurrentPageUrl(Page p)
        {
            if (p == null)
            {
                return string.Empty;
            }

            return p.Request.Url.AbsolutePath;
        }

        public static string GetUserIpAddress()
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page != null)
            {
                string ip = page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    string[] ipRange = ip.Split(',');
                    ip = ipRange[0];
                }
                else
                {
                    ip = page.Request.ServerVariables["REMOTE_ADDR"];
                }
                return ip.Trim();
            }

            return string.Empty;
        }

        public static int InvalidPasswordAttempts(HttpSessionState session, int increment = 0)
        {
            if (session == null)
            {
                return 0;
            }

            int retVal = 0;

            if (session["InvalidPasswordAttempts"] == null)
            {
                retVal = retVal + increment;
                session.Add("InvalidPasswordAttempts", retVal);
            }
            else
            {
                retVal = Conversion.TryCastInteger(session["InvalidPasswordAttempts"]) + increment;
                session["InvalidPasswordAttempts"] = retVal;
            }


            if (increment > 0)
            {
                Log.Warning("{Count} Invalid attempt to sign in from {Host}/{IP} using {Browser}.", retVal, GetUserHostAddress(), GetUserIpAddress(), GetBrowser().Browsers);
            }

            return retVal;
        }

        public static bool IsLocalUrl(Uri url, Page page)
        {
            if (page == null)
            {
                return false;
            }

            try
            {
                Uri requested = new Uri(page.Request.Url, url);

                if (requested.Host == page.Request.Url.Host)
                {
                    return true;
                }
            }
            catch (InvalidOperationException)
            {
                //
            }

            return false;
        }

        public static bool IsLocalhost(Page page)
        {
            if (page == null)
            {
                return false;
            }

            return page.Request.IsLocal;
        }

        public static void RefreshPage(Page page)
        {
            if (page != null)
            {
                page.Response.Redirect(page.Request.Url.AbsolutePath);
            }
        }

        public static void RegisterJavascript(string key, string javaScript, Page page, bool addScriptTags)
        {
            if (page == null)
            {
                page = HttpContext.Current.Handler as Page;
            }

            if (page == null)
            {
                throw new InvalidOperationException(Warnings.CouldNotRegisterJavascript);
            }

            ScriptManager.RegisterStartupScript(page, typeof (Page), key, javaScript, addScriptTags);
        }

        public static string ResolveAbsoluteUrl(Page page, string relativeUrl)
        {
            if (page != null)
            {
                return page.Request.Url.GetLeftPart(UriPartial.Authority) + page.ResolveUrl(relativeUrl);
            }
            return relativeUrl;
        }

        public static string ResolveUrl(string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
            {
                return string.Empty;
            }

            if (HttpContext.Current != null)
            {
                Page p = HttpContext.Current.Handler as Page;
                if (p != null)
                {
                    return p.ResolveUrl(relativeUrl);
                }
            }
            return relativeUrl;
        }

        private class MyClient : WebClient
        {
            public bool HeadOnly { private get; set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest req = base.GetWebRequest(address);

                if (req != null && (this.HeadOnly && req.Method == "GET"))
                {
                    req.Method = "HEAD";
                }
                return req;
            }
        }
    }
}