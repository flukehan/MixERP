using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.ApplicationState;
using MixERP.Net.ApplicationState.Cache;

namespace MixERP.Net.CurrencyConversion.Currencylayer
{
    internal static class Config
    {        
        internal const string ConfigFileName = "CurrencylayerConfigFile";
        internal static readonly string UserAgent = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "UserAgent");
        internal static readonly string MediaType = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "MediaType");
        internal static readonly string AccessKey = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "APIAccessKey");
        internal static readonly string ApiUrl = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "APIUrl");
        internal static readonly string AccessKeyName = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "AccessKeyName");
        internal static readonly string CurrenciesKey = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "CurrenciesKey");
        internal static readonly string SourceKey= DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "SourceKey");
        internal static readonly int DecimalPlaces = Conversion.TryCastInteger(DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "DecimalPlaces"));
        internal static readonly string FormatKey = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "FormatKey");
        internal static readonly string DefaultFormat = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "DefaultFormat");
        internal static readonly string ResultSubKey = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "ResultSubKey");
        internal static readonly bool RemoveSourceCurrencyFromResult = DbConfig.GetCurrencylayerParameter(AppUsers.GetCurrentUserDB(), "RemoveSourceCurrencyFromResult").ToUpperInvariant().Equals("TRUE");
    }
}
