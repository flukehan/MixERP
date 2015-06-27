using System.Collections.Generic;
using System.Linq;

namespace MixERP.Net.CurrencyConversion.Currencylayer
{
    internal sealed class UrlHelper
    {
        internal UrlHelper(string apiUrl, string accessKeyName, string accessKey, string currenciesKey,
            List<string> currencies, string sourceKey, string source, string formatKey, string defaultFormat)
        {
            this.ApiUrl = apiUrl;
            this.AccessKeyName = accessKeyName;
            this.AccessKey = accessKey;
            this.CurrenciesKey = currenciesKey;
            this.Currencies = currencies;
            this.SourceKey = sourceKey;
            this.Source = source;
            this.FormatKey = formatKey;
            this.DefaultFormat = defaultFormat;
        }

        internal string ApiUrl { get; private set; }
        internal string AccessKeyName { get; private set; }
        internal string AccessKey { get; private set; }
        internal string CurrenciesKey { get; private set; }
        internal List<string> Currencies { get; private set; }
        internal string SourceKey { get; private set; }
        internal string Source { get; private set; }
        internal string FormatKey { get; private set; }
        internal string DefaultFormat { get; private set; }

        internal string GetUrl()
        {
            string baseUrl = this.ApiUrl;
            IEnumerable<QueryString> keys = this.GetKeys();

            var queryString = string.Join("&",
                keys.Select(
                    k => string.Format("{0}={1}", k.Name, k.Value))
                    .ToArray());

            return baseUrl + "?" + queryString;
        }

        private IEnumerable<QueryString> GetKeys()
        {
            List<QueryString> keys = new List<QueryString>();

            keys.Add(new QueryString(this.AccessKeyName, this.AccessKey));
            keys.Add(new QueryString(this.CurrenciesKey, string.Join(",", this.Currencies)));
            keys.Add(new QueryString(this.SourceKey, this.Source));
            keys.Add(new QueryString(this.FormatKey, this.DefaultFormat));

            return keys;
        }
    }
}