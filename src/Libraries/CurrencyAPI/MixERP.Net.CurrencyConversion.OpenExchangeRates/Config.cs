using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;


namespace MixERP.Net.CurrencyConversion.OpenExchangeRates
{
    internal static class Config
    {
        internal const string ConfigFileName = "OpenExchangeRatesConfigFile";
        internal static readonly string UserAgent = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "UserAgent");
        internal static readonly string MediaType = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "MediaType");
        internal static readonly string AppId = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "AppId");
        internal static readonly string ApiUrl = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "APIUrl");
        internal static readonly string AppIdKey = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "AppIdKey");
        internal static readonly string CurrenciesKey = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "CurrenciesKey");
        internal static readonly bool SpecificCurrencies = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "SpecificCurrencies").ToUpperInvariant().Equals("TRUE");
        internal static readonly string BaseCurrencyKey = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "BaseCurrencyKey");
        internal static readonly int DecimalPlaces = Conversion.TryCastInteger(DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "DecimalPlaces"));
        internal static readonly string ResultSubKey = DbConfig.GetOpenExchangeRatesParameter(AppUsers.GetCurrentUserDB(), "ResultSubKey");
    }
}
