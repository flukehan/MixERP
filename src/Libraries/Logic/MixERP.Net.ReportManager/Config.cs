using System.Configuration;
using System.Web.Hosting;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.ReportManager
{
    internal static class Config
    {
        private static string ReadConfig(string keyName)
        {
            string path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ReportAPIConfigFileLocation"]);
            return ConfigurationHelper.GetConfigurationValue(path, keyName);
        }

        internal static readonly string ApiUrl = ReadConfig("ApiUrl");
        internal static readonly string FileNameKey = ReadConfig("FileNameKey");
        internal static readonly string DownloadUrlKey = ReadConfig("DownloadUrlKey");
        internal static readonly string MediaType = ReadConfig("MediaType");
        internal static readonly string UserAgent = ReadConfig("UserAgent");
        internal static readonly string ReportUrlExpression = ReadConfig("ReportUrlExpression");
    }
}