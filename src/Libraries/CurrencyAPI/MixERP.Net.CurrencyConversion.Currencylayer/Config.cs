using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.CurrencyConversion.Currencylayer
{
    internal static class Config
    {        
        internal const string ConfigFileName = "CurrencylayerConfigFile";
        internal static readonly string UserAgent = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "UserAgent");
        internal static readonly string MediaType = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "MediaType");
        internal static readonly string AccessKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "APIAccessKey");
        internal static readonly string ApiUrl = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "APIUrl");
        internal static readonly string AccessKeyName = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "AccessKeyName");
        internal static readonly string CurrenciesKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "CurrenciesKey");
        internal static readonly string SourceKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "SourceKey");
        internal static readonly int DecimalPlaces = Conversion.TryCastInteger(ConfigurationHelper.GetConfigurationValue(ConfigFileName, "DecimalPlaces"));
        internal static readonly string FormatKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "FormatKey");
        internal static readonly string DefaultFormat = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "DefaultFormat");
        internal static readonly string ResultSubKey = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "ResultSubKey");
        internal static readonly bool RemoveSourceCurrencyFromResult = ConfigurationHelper.GetConfigurationValue(ConfigFileName, "RemoveSourceCurrencyFromResult").ToUpperInvariant().Equals("TRUE");
    }
}
