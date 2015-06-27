using System.Collections.Generic;
using MixERP.Net.FrontEnd.Base;

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
                    Common.Helpers.ConfigurationHelper.GetConfigurationValue(this.ConfigFileName, "Enabled")
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