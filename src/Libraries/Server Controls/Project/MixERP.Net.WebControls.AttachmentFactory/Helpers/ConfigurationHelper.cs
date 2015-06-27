using System.Configuration;
using System.Web.Hosting;
using MixERP.Net.Common;

namespace MixERP.Net.WebControls.AttachmentFactory.Helpers
{
    public static class ConfigurationHelper
    {
        private const string configFile = "AttachmentFactoryConfigFileLocation";

        public static string GetAllowedExtensions()
        {
            return Common.Helpers.ConfigurationHelper.GetConfigurationValue(configFile, "AllowedExtensions");
        }

        public static string GetUploadHandlerUrl()
        {
            return PageUtility.ResolveUrl(Common.Helpers.ConfigurationHelper.GetConfigurationValue(configFile, "UploadHandlerUrl"));
        }

        public static string GetAttachmentsDirectory()
        {
            return PageUtility.ResolveUrl(Common.Helpers.ConfigurationHelper.GetConfigurationValue(configFile, "AttachmentsDirectory"));
        }

        public static string GetUndoUploadServiceUrl()
        {
            return PageUtility.ResolveUrl(Common.Helpers.ConfigurationHelper.GetConfigurationValue(configFile, "UndoUploadServiceUrl"));
        }
    }
}