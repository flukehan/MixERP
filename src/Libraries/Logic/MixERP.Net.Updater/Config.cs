using System.Configuration;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Updater
{
    public static class Config
    {
        public static string ApiUrl { get { return GetConfigValue("ApiUrl"); } }
        public static string MediaType { get { return GetConfigValue("MediaType"); } }
        public static string UserAgent { get { return GetConfigValue("UserAgent"); } }
        public static string IdKey { get { return GetConfigValue("IdKey"); } }
        public static string NameKey { get { return GetConfigValue("NameKey"); } }
        public static string TagNameKey { get { return GetConfigValue("TagNameKey"); } }
        public static string BodyKey { get { return GetConfigValue("BodyKey"); } }
        public static string DraftKey { get { return GetConfigValue("DraftKey"); } }
        public static string CreatedAtKey { get { return GetConfigValue("CreatedAtKey"); } }
        public static string PublishedAtKey { get { return GetConfigValue("PublishedAtKey"); } }
        public static string AssetsKey { get { return GetConfigValue("AssetsKey"); } }
        public static string AssetIdSubKey { get { return GetConfigValue("AssetIdSubKey"); } }
        public static string AssetNameSubKey { get { return GetConfigValue("AssetNameSubKey"); } }
        public static string AssetContentTypeSubKey { get { return GetConfigValue("AssetContentTypeSubKey"); } }
        public static string AssetStateSubKey { get { return GetConfigValue("AssetStateSubKey"); } }
        public static string AssetCreatedAtSubKey { get { return GetConfigValue("AssetCreatedAtSubKey"); } }
        public static string AssetUpdatedAtSubKey { get { return GetConfigValue("AssetUpdatedAtSubKey"); } }
        public static string AssetDownloadUrlSubKey { get { return GetConfigValue("AssetDownloadUrlSubKey"); } }

        private static string GetConfigValue(string key)
        {
            return ConfigurationHelper.GetUpdaterParameter(key);
        }
    }
}
