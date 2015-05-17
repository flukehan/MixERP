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

using System.Collections.ObjectModel;
using System.Runtime.Caching;
using MixERP.Net.Common.Models;

namespace MixERP.Net.Common.Helpers
{
    public static class CacheFactory
    {
        private const string appDatesKey = "ApplicationDates";

        public static Collection<ApplicationDateModel> GetApplicationDates(string catalog)
        {
            object dates = GetFromDefaultCacheByKey(catalog + appDatesKey);

            if (dates == null)
            {
                return new Collection<ApplicationDateModel>();
            }

            return dates as Collection<ApplicationDateModel>;
        }

        public static void SetApplicationDates(string catalog, Collection<ApplicationDateModel> applicationDates)
        {
            AddToDefaultCache(catalog + appDatesKey, applicationDates);
        }

        public static void AddToDefaultCache(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            if (value == null)
            {
                return;
            }

            var cacheItem = new CacheItem(key, value);

            if (MemoryCache.Default[key] == null)
            {
                MemoryCache.Default.Add(cacheItem, new CacheItemPolicy());
            }
            else
            {
                MemoryCache.Default[key] = cacheItem;
            }
        }

        public static object GetFromDefaultCacheByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            return MemoryCache.Default.Get(key);
        }
    }
}