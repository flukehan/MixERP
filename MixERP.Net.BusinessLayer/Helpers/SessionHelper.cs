/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Globalization;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class SessionHelper
    {
        public static long GetLogOnId()
        {
            return Conversion.TryCastLong(HttpContext.Current.Session["LogOnId"]);
        }
        
        public static int GetUserId()
        {
            return Conversion.TryCastInteger(HttpContext.Current.Session["UserId"]);
        }

        public static string GetUserName()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["UserName"]);
        }

        public static string GetRole()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["Role"]);
        }

        public static bool IsAdmin()
        {
            return Conversion.TryCastBoolean(HttpContext.Current.Session["IsAdmin"]);
        }

        public static bool IsSystem()
        {
            return Conversion.TryCastBoolean(HttpContext.Current.Session["IsSystem"]);
        }

        public static int GetOfficeId()
        {
            return Conversion.TryCastInteger(HttpContext.Current.Session["OfficeId"]);
        }

        public static string GetNickname()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["NickName"]);
        }

        public static string GetOfficeName()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["OfficeName"]);
        }

        public static DateTime GetRegistrationDate()
        {
            return Conversion.TryCastDate(HttpContext.Current.Session["RegistrationDate"]);
        }

        public static string GetRegistrationNumber()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["RegistrationNumber"]);
        }

        public static string GetPanNumber()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["PanNumber"]);
        }

        public static string GetStreet()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["Street"]);
        }

        public static string GetCity()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["City"]);
        }

        public static string GetState()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["State"]);
        }

        public static string GetCountry()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["Country"]);
        }

        public static string GetZipCode()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["ZipCode"]);
        }

        public static string GetPhone()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["Phone"]);
        }

        public static string GetFax()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["Fax"]);
        }

        public static string GetEmail()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["Email"]);
        }

        public static string GetUrl()
        {
            return Conversion.TryCastString(HttpContext.Current.Session["Url"]);
        }

        public static CultureInfo GetCulture()
        {
            return LocalizationHelper.GetCurrentCulture();
        }
    }
}
