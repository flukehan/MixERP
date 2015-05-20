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

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Caching;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Office;

namespace MixERP.Net.FrontEnd.Cache
{
    public static class AppUsers
    {
        public static void SetCurrentLogin()
        {
            long globalLoginId = Conversion.TryCastLong(HttpContext.Current.User.Identity.Name);
            SetCurrentLogin(globalLoginId);
        }

        public static void SetCurrentLogin(long globalLoginId)
        {
            if (globalLoginId > 0)
            {
                string key = globalLoginId.ToString(CultureInfo.InvariantCulture);

                if (MemoryCache.Default[key] == null)
                {
                    GlobalLogin globalLogin = Data.Office.User.GetGloblalLogin(globalLoginId);
                    Dictionary<string, object> dictionary = GetDictionary(globalLogin);

                    CacheFactory.AddToDefaultCache("Dictionary" + key, dictionary);
                    CacheFactory.AddToDefaultCache(key, globalLogin);
                }
            }
        }

        private static Dictionary<string, object> GetDictionary(GlobalLogin globalLogin)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            if (globalLogin == null)
            {
                return dictionary;
            }

            dictionary.Add("Catalog", globalLogin.Catalog);
            dictionary.Add("AddressLine1", globalLogin.View.AddressLine1);
            dictionary.Add("AddressLine2", globalLogin.View.AddressLine2);
            dictionary.Add("City", globalLogin.View.City);
            dictionary.Add("Country", globalLogin.View.Country);
            dictionary.Add("Culture", globalLogin.View.Culture);
            dictionary.Add("CurrencyCode", globalLogin.View.CurrencyCode);
            dictionary.Add("Email", globalLogin.View.Email);
            dictionary.Add("Fax", globalLogin.View.Fax);
            dictionary.Add("FullName", globalLogin.View.FullName);
            dictionary.Add("NickName", globalLogin.View.NickName);
            dictionary.Add("Office", globalLogin.View.Office);
            dictionary.Add("OfficeCode", globalLogin.View.OfficeCode);
            dictionary.Add("OfficeId", globalLogin.View.OfficeId);
            dictionary.Add("OfficeName", globalLogin.View.OfficeName);
            dictionary.Add("PanNumber", globalLogin.View.PanNumber);
            dictionary.Add("Phone", globalLogin.View.Phone);
            dictionary.Add("RegistrationDate", globalLogin.View.RegistrationDate);
            dictionary.Add("RegistrationNumber", globalLogin.View.RegistrationNumber);
            dictionary.Add("Role", globalLogin.View.Role);
            dictionary.Add("RoleCode", globalLogin.View.RoleCode);
            dictionary.Add("RoleName", globalLogin.View.RoleName);
            dictionary.Add("State", globalLogin.View.State);
            dictionary.Add("Street", globalLogin.View.Street);
            dictionary.Add("Url", globalLogin.View.Url);
            dictionary.Add("UserId", globalLogin.View.UserId);
            dictionary.Add("UserName", globalLogin.View.UserName);
            dictionary.Add("ZipCode", globalLogin.View.ZipCode);

            return dictionary;
        }

        public static string GetCurrentUserDB()
        {
            string catalog = GetCurrentLogin().Catalog;
            return catalog;
        }

        public static GlobalLogin GetCurrentLogin()
        {
            GlobalLogin login = new GlobalLogin();

            string globalLoginId = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(globalLoginId))
            {
                login = CacheFactory.GetFromDefaultCacheByKey(globalLoginId) as GlobalLogin;
            }

            if (login == null)
            {
                login = new GlobalLogin();
            }

            if (login.View == null)
            {
                login.View = new SignInView();
            }

            return login;
        }
    }
}