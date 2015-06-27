using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.CurrencyConversion.OpenExchangeRates
{
    internal static class Config
    {
        internal const string ConfigFileName = "OpenExchangeRatesConfigFile";
        internal static readonly string UserAgent = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "UserAgent");
        internal static readonly string MediaType = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "MediaType");
        internal static readonly string AppId = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "AppId");
        internal static readonly string ApiUrl = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "APIUrl");
        internal static readonly string AppIdKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "AppIdKey");
        internal static readonly string CurrenciesKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "CurrenciesKey");
        internal static readonly bool SpecificCurrencies = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "SpecificCurrencies").ToUpperInvariant().Equals("TRUE");
        internal static readonly string BaseCurrencyKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "BaseCurrencyKey");
        internal static readonly int DecimalPlaces = Conversion.TryCastInteger(ConfigurationHelper.GetConfigurationValue(ConfigFileName, "DecimalPlaces"));
        internal static readonly string ResultSubKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "ResultSubKey");
    }
}
