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
using System.Web;
using System.Web.SessionState;

namespace MixERP.Net.Common.Helpers
{
    public static class CurrentSession
    {
        public static string GetCity()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("City"));
        }

        public static string GetCountry()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("Country"));
        }

        public static CultureInfo GetCulture()
        {
            return LocalizationHelper.GetCurrentCulture();
        }

        public static string GetEmail()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("Email"));
        }

        public static string GetFax()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("Fax"));
        }

        public static long GetLogOnId()
        {
            return Conversion.TryCastLong(SessionHelper.GetSessionKey("LogOnId"));
        }

        public static string GetNickname()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("NickName"));
        }

        public static int GetOfficeId()
        {
            return Conversion.TryCastInteger(SessionHelper.GetSessionKey("OfficeId"));
        }

        public static string GetBaseCurrency()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("CurrencyCode"));
        }

        public static string GetOfficeName()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("OfficeName"));
        }

        public static string GetPanNumber()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("PanNumber"));
        }

        public static string GetPhone()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("Phone"));
        }

        public static DateTime GetRegistrationDate()
        {
            return Conversion.TryCastDate(SessionHelper.GetSessionKey("RegistrationDate"));
        }

        public static string GetRegistrationNumber()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("RegistrationNumber"));
        }

        public static string GetRole()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("Role"));
        }

        public static string GetSessionValueByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            HttpSessionState session = HttpContext.Current.Session;
            {
                if (session != null)
                {
                    if (session[key] != null)
                    {
                        return Conversion.TryCastString(session[key]);
                    }
                }
            }

            return string.Empty;
        }

        public static DateTime GetSignInTimestamp()
        {
            return Conversion.TryCastDate(SessionHelper.GetSessionKey("SignInTimestamp"));
        }
        public static string GetState()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("State"));
        }

        public static string GetStreet()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("Street"));
        }

        public static string GetUrl()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("Url"));
        }

        public static int GetUserId()
        {
            return Conversion.TryCastInteger(SessionHelper.GetSessionKey("UserId"));
        }

        public static string GetUserName()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("UserName"));
        }

        public static string GetZipCode()
        {
            return Conversion.TryCastString(SessionHelper.GetSessionKey("ZipCode"));
        }

        public static bool IsAdmin()
        {
            return Conversion.TryCastBoolean(SessionHelper.GetSessionKey("IsAdmin"));
        }

        public static bool IsSystem()
        {
            return Conversion.TryCastBoolean(SessionHelper.GetSessionKey("IsSystem"));
        }
    }
}