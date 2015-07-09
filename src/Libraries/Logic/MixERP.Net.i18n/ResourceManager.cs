using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Resources;
using System.Runtime.Caching;
using System.Threading;

namespace MixERP.Net.i18n
{
    public class ResourceManager
    {
        private static readonly bool supressException = ConfigurationManager.AppSettings["SupressMissingResourceException"].ToUpperInvariant().Equals("TRUE");

        public static string GetString(string resourceClass, string resourceKey, string cultureCode = null)
        {
            string result = TryGetResourceFromCache(resourceClass, resourceKey, cultureCode);

            if (result == null)
            {
                if (supressException)
                {
                    return resourceKey;
                }

                throw new MissingManifestResourceException("Resource " + resourceClass + "." + resourceKey + " was not found.");
            }

            return result;
        }

        public static string TryGetResourceFromCache(string resourceClass, string resourceKey, string cultureCode = null)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            if (!string.IsNullOrWhiteSpace(cultureCode))
            {
                culture = new CultureInfo(cultureCode);
            }


            IDictionary<string, string> cache;
            var cacheItem = MemoryCache.Default.Get("Resources");

            if (cacheItem is CacheItem)
            {
                CacheItem item = (CacheItem) cacheItem;

                cache = (IDictionary<string, string>)item.Value;
            }
            else
            {
                cache = (IDictionary<string, string>)cacheItem;                
            }


            if (cache == null || cache.Count.Equals(0))
            {
                InitializeResources();
                return TryGetResourceFromCache(resourceClass, resourceKey, cultureCode);
            }

            string cacheKey = resourceClass + "." + culture.Name + "." + resourceKey;
            string result;
            cache.TryGetValue(cacheKey, out result);

            if (result != null)
            {
                return result;
            }

            //Fall back to parent culture
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(culture.Parent.Name))
                {
                    cacheKey = resourceClass + "." + culture.Parent.Name + "." + resourceKey;
                    cache.TryGetValue(cacheKey, out result);

                    if (result != null)
                    {
                        return result;
                    }

                    culture = culture.Parent;
                    continue;
                }
                break;
            }

            //Fall back to invariant culture
            cacheKey = resourceClass + "." + resourceKey;
            cache.TryGetValue(cacheKey, out result);

            return result;
        }

        private static string GetFallBackResource(ref IDictionary<string, string> cache, CultureInfo culture, string resourceClass, string resourceKey)
        {
            throw new NotImplementedException();
        }


        private static void InitializeResources()
        {
            IDictionary<string, string> resources = DbResources.GetLocalizedResources();
            CacheFactory.AddToDefaultCache("Resources", resources);
        }
    }
}