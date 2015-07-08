using MixERP.Net.Common.Helpers;

namespace MixERP.Net.ReportManager
{
    internal static class Config
    {
        private static string ReadConfig(string keyName)
        {
            return ConfigurationHelper.GetConfigurationValue("ReportAPIConfigFileLocation", keyName);
        }

        internal static readonly string ApiUrl = ReadConfig("ApiUrl");
        internal static readonly string FileNameKey = ReadConfig("FileNameKey");
        internal static readonly string DownloadUrlKey = ReadConfig("DownloadUrlKey");
        internal static readonly string MediaType = ReadConfig("MediaType");
        internal static readonly string UserAgent = ReadConfig("UserAgent");
        internal static readonly string ReportUrlExpression = ReadConfig("ReportUrlExpression");
    }
}