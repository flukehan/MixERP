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

using System.Configuration;
using System.Web;
using Serilog;

namespace MixERP.Net.Common
{
    public static class PageUtility
    {
        private static string GetUserHostAddress()
        {
            string host = string.Empty;

            if (HttpContext.Current == null) return host;

            if (HttpContext.Current.Request.UserHostAddress != null)
            {
                host = HttpContext.Current.Request.UserHostAddress;
            }

            return host;
        }

        private static HttpBrowserCapabilities GetBrowser()
        {
            if (HttpContext.Current == null) return new HttpBrowserCapabilities();

            return HttpContext.Current.Request.UserHostAddress != null
                ? HttpContext.Current.Request.Browser
                : new HttpBrowserCapabilities();
        }

        public static bool MaxInvalidAttemptsReached(HttpSessionStateBase session)
        {
            if (session == null) return false;

            int triedAttempts = InvalidPasswordAttempts(session);
            int allowedAttemps =
                Conversion.TryCastInteger(ConfigurationManager.AppSettings["MaxInvalidPasswordAttempts"]);

            if (triedAttempts > 0)
            {
                Log.Warning(
                    "{Count} of {Allowed} allowed invalid sign-in attempts from {Host}/{IP} using {Browser}.",
                    triedAttempts, allowedAttemps, GetUserHostAddress(), GetUserIpAddress(), GetBrowser().Browsers);
            }

            if (triedAttempts < allowedAttemps) return false;

            Log.Error("Disallowed access to {Host}/{IP} using {Browser}.", GetUserHostAddress(),
                GetUserIpAddress(), GetBrowser().Browsers);
            return true;
        }

        public static string GetUserIpAddress()
        {
            if (HttpContext.Current == null)
            {
                return string.Empty;
            }

            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ip))
            {
                string[] ipRange = ip.Split(',');
                ip = ipRange[0];
            }
            else
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip.Trim();
        }

        public static int InvalidPasswordAttempts(HttpSessionStateBase session, int increment = 0)
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
                Log.Warning("{Count} Invalid attempt to sign in from {Host}/{IP} using {Browser}.", retVal,
                    GetUserHostAddress(), GetUserIpAddress(), GetBrowser().Browsers);
            }

            return retVal;
        }
    }
}