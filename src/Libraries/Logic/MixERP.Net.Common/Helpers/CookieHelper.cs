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

using System.Web;

namespace MixERP.Net.Common.Helpers
{
    public static class CookieHelper
    {
        public static void AddCookie(string key, string value)
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                return;
            }

            if (context.Response.Cookies[key] != null)
            {
                context.Response.Cookies[key].Value = value;
                return;
            }

            HttpCookie cookie = new HttpCookie(key, value);

            context.Response.Cookies.Add(cookie);
        }

        public static object GetCookieValue(string key)
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                return null;
            }

            if (context.Request.Cookies[key] != null)
            {
                return context.Request.Cookies[key].Value;
            }

            return null;
        }

        public static void SetCatalog(string catalog)
        {
            CatalogHelper.ValidateCatalog(catalog);
            AddCookie("Catalog", catalog);
        }

        public static string GetCatalog()
        {
            object cookie = GetCookieValue("Catalog");
            string catalog = string.Empty;

            if (cookie != null)
            {
                catalog = cookie.ToString();
            }

            if (string.IsNullOrWhiteSpace(catalog))
            {
                catalog = ConfigurationHelper.GetDbServerParameter("Database");
            }

            CatalogHelper.ValidateCatalog(catalog);

            return catalog;
        }
    }
}