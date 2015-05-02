using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Runtime.Caching;
using System.Threading;

namespace MixERP.Net.i18n
{
    public class ResourceManager
    {
        public static string GetString(string resourceClass, string resourceKey, string cultureCode = null)
        {
            return GetResourceFromCache(resourceClass, resourceKey, cultureCode);
        }

        private static string GetResourceFromCache(string resourceClass, string resourceKey, string cultureCode = null)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            if (!string.IsNullOrWhiteSpace(cultureCode))
            {
                culture = new CultureInfo(cultureCode);
            }


            var cacheItem = MemoryCache.Default.Get("Resources");
            IDictionary<string, string> cache = (IDictionary<string, string>)cacheItem;

            if (cache == null || cache.Count.Equals(0))
            {
                InitializeResources();
                return GetResourceFromCache(resourceClass, resourceKey, cultureCode);
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

            if (result == null)
            {
                throw new MissingManifestResourceException("Resource " + cacheKey + " was not found.");
            }

            return result;
        }

        private static string GetFallBackResource(ref IDictionary<string, string> cache, CultureInfo culture, string resourceClass, string resourceKey)
        {
            return null;
        }


        private static void InitializeResources()
        {
            IDictionary<string, string> resources = DbResources.GetLocalizedResources();
            CacheProvider.AddToDefaultCache("Resources", resources);
        }
    }
}