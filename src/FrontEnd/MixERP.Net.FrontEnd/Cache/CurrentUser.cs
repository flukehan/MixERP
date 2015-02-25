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
    public static class CurrentUser
    {
        public static void SetSignInView()
        {
            long signInId = Conversion.TryCastLong(HttpContext.Current.User.Identity.Name);
            SetSignInView(signInId);
        }

        public static void SetSignInView(long signInId)
        {
            if (signInId > 0)
            {
                string key = signInId.ToString(CultureInfo.InvariantCulture);

                if (MemoryCache.Default[key] == null)
                {
                    SignInView signInView = Data.Office.User.GetSignInView(signInId);
                    Dictionary<string, object> dictionary = GetDictionary(signInView);

                    CacheFactory.AddToDefaultCache("Dictionary" + key, dictionary);
                    CacheFactory.AddToDefaultCache(key, signInView);
                }
            }
        }

        private static Dictionary<string, object> GetDictionary(SignInView signInView)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            if (signInView == null)
            {
                return dictionary;
            }

            dictionary.Add("AddressLine1", signInView.AddressLine1);
            dictionary.Add("AddressLine2", signInView.AddressLine2);
            dictionary.Add("City", signInView.City);
            dictionary.Add("Country", signInView.Country);
            dictionary.Add("Culture", signInView.Culture);
            dictionary.Add("CurrencyCode", signInView.CurrencyCode);
            dictionary.Add("Email", signInView.Email);
            dictionary.Add("Fax", signInView.Fax);
            dictionary.Add("FullName", signInView.FullName);
            dictionary.Add("NickName", signInView.NickName);
            dictionary.Add("Office", signInView.Office);
            dictionary.Add("OfficeCode", signInView.OfficeCode);
            dictionary.Add("OfficeId", signInView.OfficeId);
            dictionary.Add("OfficeName", signInView.OfficeName);
            dictionary.Add("PanNumber", signInView.PanNumber);            
            dictionary.Add("Phone", signInView.Phone);
            dictionary.Add("RegistrationDate", signInView.RegistrationDate);
            dictionary.Add("RegistrationNumber", signInView.RegistrationNumber);
            dictionary.Add("Role", signInView.Role);
            dictionary.Add("RoleCode", signInView.RoleCode);
            dictionary.Add("RoleName", signInView.RoleName);
            dictionary.Add("State", signInView.State);
            dictionary.Add("Street", signInView.Street);
            dictionary.Add("Url", signInView.Url);
            dictionary.Add("UserId", signInView.UserId);
            dictionary.Add("UserName", signInView.UserName);
            dictionary.Add("ZipCode", signInView.ZipCode);

            return dictionary;
        }

        public static SignInView GetSignInView()
        {
            SignInView view = new SignInView();

            string signInId = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(signInId))
            {
                view = CacheFactory.GetFromDefaultCacheByKey(signInId) as SignInView;
            }

            if (view == null)
            {
                view = new SignInView();
            }

            return view;
        }


    }
}