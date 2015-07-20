using System.Collections.Generic;
using MixERP.Net.Framework.Contracts.Currency;
using MixERP.Net.Common.Helpers;
using MixERP.Net.ApplicationState.Cache;

namespace MixERP.Net.CurrencyConversion.OpenExchangeRates
{
    public sealed class Converter : ICurrencyConverter
    {
        public string ConverterName
        {
            get { return "Open Exchange Rates"; }
        }

        public string BaseCurrency { get; set; }
        public List<string> CurrencyCodes { get; set; }

        public bool Enabled
        {
            get
            {
                return
                    DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "Enabled")
                        .ToUpperInvariant()
                        .Equals("TRUE");
            }
        }

        public string ConfigFileName
        {
            get { return Config.ConfigFileName; }
        }

        public IEnumerable<CurrencyConversionResult> GetResult()
        {
            Processor processor = new Processor(Config.ApiUrl, Config.AppIdKey, Config.AppId, Config.CurrenciesKey,
                this.CurrencyCodes, Config.BaseCurrencyKey, this.BaseCurrency, Config.SpecificCurrencies,
                Config.MediaType, Config.UserAgent, Config.ResultSubKey, Config.DecimalPlaces);
            return processor.Process();
        }
    }
}