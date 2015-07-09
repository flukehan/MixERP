using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using MixERP.Net.Framework.Contracts.Currency;

namespace MixERP.Net.CurrencyConversion.OpenExchangeRates
{
    internal sealed class Processor
    {
        internal Processor(string apiUrl, string appIdKey, string appId, string currenciesKey, List<string> currencies,
            string baseCurrencyKey, string baseCurrency, bool specificCurrencies, string mediaType, string userAgent,
            string resultSubKey, int decimalPlaces)
        {
            this.ApiUrl = apiUrl;
            this.AppIdKey = appIdKey;
            this.AppId = appId;
            this.CurrenciesKey = currenciesKey;
            this.Currencies = currencies;
            this.BaseCurrencyKey = baseCurrencyKey;
            this.BaseCurrency = baseCurrency;
            this.SpecificCurrencies = specificCurrencies;
            this.MediaType = mediaType;
            this.UserAgent = userAgent;
            this.ResultSubKey = resultSubKey;
            this.DecimalPlaces = decimalPlaces;
        }

        internal string ApiUrl { get; private set; }
        internal string AppIdKey { get; private set; }
        internal string AppId { get; private set; }
        internal string CurrenciesKey { get; private set; }
        internal List<string> Currencies { get; private set; }
        internal string BaseCurrencyKey { get; private set; }
        internal string BaseCurrency { get; private set; }
        internal bool SpecificCurrencies { get; private set; }
        internal string MediaType { get; private set; }
        internal string UserAgent { get; private set; }
        internal string ResultSubKey { get; private set; }
        internal int DecimalPlaces { get; private set; }

        internal IEnumerable<CurrencyConversionResult> Process()
        {
            string url =
                new UrlHelper(this.ApiUrl, this.AppIdKey, this.AppId, this.CurrenciesKey, this.Currencies,
                    this.BaseCurrencyKey, this.BaseCurrency, this.SpecificCurrencies).GetUrl();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.MediaType));
                client.DefaultRequestHeaders.Add("User-Agent", this.UserAgent);

                HttpResponseMessage response = client.GetAsync(url).Result;

                dynamic content = response.Content.ReadAsAsync<dynamic>().Result;

                if (response.IsSuccessStatusCode)
                {
                    Parser parser = new Parser(content, this.ResultSubKey, this.DecimalPlaces);
                    return parser.Parse();
                }


                string statusCode = content["status"].ToString();
                string reasonPhrase = content["message"].ToString();
                string desscription = content["description"].ToString();

                throw new OpenExchangeRatesException(statusCode + " (" + reasonPhrase + ") - " + desscription);
            }
        }
    }
}