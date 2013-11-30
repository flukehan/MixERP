/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class SessionHelper
    {
        public static long GetLogOnId()
        {
            return MixERP.Net.Common.Conversion.TryCastLong(HttpContext.Current.Session["LogOnId"]);
        }
        
        public static int GetUserId()
        {
            return MixERP.Net.Common.Conversion.TryCastInteger(HttpContext.Current.Session["UserId"]);
        }

        public static string GetUserName()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["UserName"]);
        }

        public static string GetRole()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["Role"]);
        }

        public static bool IsAdmin()
        {
            return MixERP.Net.Common.Conversion.TryCastBoolean(HttpContext.Current.Session["IsAdmin"]);
        }

        public static bool IsSystem()
        {
            return MixERP.Net.Common.Conversion.TryCastBoolean(HttpContext.Current.Session["IsSystem"]);
        }

        public static int GetOfficeId()
        {
            return MixERP.Net.Common.Conversion.TryCastInteger(HttpContext.Current.Session["OfficeId"]);
        }

        public static string GetNickname()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["NickName"]);
        }

        public static string GetOfficeName()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["OfficeName"]);
        }

        public static DateTime GetRegistrationDate()
        {
            return MixERP.Net.Common.Conversion.TryCastDate(HttpContext.Current.Session["RegistrationDate"]);
        }

        public static string GetRegistrationNumber()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["RegistrationNumber"]);
        }

        public static string GetPanNumber()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["PanNumber"]);
        }

        public static string GetStreet()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["Street"]);
        }

        public static string GetCity()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["City"]);
        }

        public static string GetState()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["State"]);
        }

        public static string GetCountry()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["Country"]);
        }

        public static string GetZipCode()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["ZipCode"]);
        }

        public static string GetPhone()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["Phone"]);
        }

        public static string GetFax()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["Fax"]);
        }

        public static string GetEmail()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["Email"]);
        }

        public static string GetUrl()
        {
            return MixERP.Net.Common.Conversion.TryCastString(HttpContext.Current.Session["Url"]);
        }

        public static CultureInfo GetCulture()
        {
            return MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture();
        }
    }
}
