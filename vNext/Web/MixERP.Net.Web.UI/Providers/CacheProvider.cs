using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Caching;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Office;

namespace MixERP.Net.Web.UI.Providers
{
    public static class CacheProvider
    {
        public static void SetSignInView()
        {
            long signInId = Conversion.TryCastLong(HttpContext.Current.User.Identity.Name);
            SetSignInView(signInId);
        }

        public static void SetSignInView(long signInId)
        {
            if (signInId <= 0) return;

            string key = signInId.ToString(CultureInfo.InvariantCulture);

            if (MemoryCache.Default[key] == null)
            {
                SignInView signInView = Data.Office.User.GetSignInView(signInId);
                Dictionary<string, object> dictionary = GetDictionary(signInView);

                CacheFactory.AddToDefaultCache("Dictionary" + key, dictionary);
                CacheFactory.AddToDefaultCache(key, signInView);
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
            while (true)
            {
                string signInId = HttpContext.Current.User.Identity.Name;

                if (string.IsNullOrWhiteSpace(signInId))
                {
                    return new SignInView();
                }

                SignInView view = CacheFactory.GetFromDefaultCacheByKey(signInId) as SignInView;

                if (view != null) return view;

                SetSignInView();
            }
        }
    }
}