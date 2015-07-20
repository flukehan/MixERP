using System.Collections.Generic;
using MixERP.Net.Framework.Contracts.Currency;
using MixERP.Net.ApplicationState.Cache;

namespace MixERP.Net.CurrencyConversion.Currencylayer
{
    public sealed class Converter : ICurrencyConverter
    {
        public string ConverterName
        {
            get { return "Currencylayer"; }
        }

        public string BaseCurrency { get; set; }
        public List<string> CurrencyCodes { get; set; }

        public bool Enabled
        {
            get
            {
                return
                    MixERP.Net.Common.Helpers.DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "Enabled")
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
            Processor processor = new Processor(Config.ApiUrl, Config.AccessKeyName, Config.AccessKey,
                Config.CurrenciesKey, this.CurrencyCodes, Config.SourceKey, this.BaseCurrency, Config.MediaType,
                Config.RemoveSourceCurrencyFromResult, Config.UserAgent, Config.ResultSubKey, Config.DecimalPlaces,
                Config.FormatKey, Config.DefaultFormat);

            return processor.Process();
        }
    }
}