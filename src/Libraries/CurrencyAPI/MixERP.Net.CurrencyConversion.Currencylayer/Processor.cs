using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using MixERP.Net.Framework.Contracts.Currency;

namespace MixERP.Net.CurrencyConversion.Currencylayer
{
    internal sealed class Processor
    {
        internal Processor(string apiUrl, string accessKeyName, string accessKey, string currenciesKey,
            List<string> currencies, string sourceKey, string source, string mediaType,
            bool removeSourceCurrencyFromResult, string userAgent, string resultSubKey, int decimalPlaces,
            string formatKey, string defaultFormat)
        {
            this.ApiUrl = apiUrl;
            this.AccessKeyName = accessKeyName;
            this.AccessKey = accessKey;
            this.CurrenciesKey = currenciesKey;
            this.Currencies = currencies;
            this.SourceKey = sourceKey;
            this.Source = source;
            this.MediaType = mediaType;
            this.RemoveSourceCurrencyFromResult = removeSourceCurrencyFromResult;
            this.UserAgent = userAgent;
            this.ResultSubKey = resultSubKey;
            this.DecimalPlaces = decimalPlaces;
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
        internal string MediaType { get; private set; }
        internal bool RemoveSourceCurrencyFromResult { get; private set; }
        internal string UserAgent { get; private set; }
        internal string ResultSubKey { get; private set; }
        internal int DecimalPlaces { get; private set; }
        internal string FormatKey { get; private set; }
        internal string DefaultFormat { get; private set; }

        internal IEnumerable<CurrencyConversionResult> Process()
        {
            string url =
                new UrlHelper(this.ApiUrl, this.AccessKeyName, this.AccessKey, this.CurrenciesKey, this.Currencies,
                    this.SourceKey, this.Source, this.FormatKey, this.DefaultFormat).GetUrl();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.MediaType));
                client.DefaultRequestHeaders.Add("User-Agent", this.UserAgent);

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    dynamic content = response.Content.ReadAsAsync<dynamic>().Result;
                    Parser parser = new Parser(content, this.Source, this.RemoveSourceCurrencyFromResult,
                        this.ResultSubKey, this.DecimalPlaces);
                    return parser.Parse();
                }
            }

            return null;
        }
    }
}