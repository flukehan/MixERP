using System.Configuration;
using System.Web.Hosting;
using MixERP.Net.Common;

namespace MixERP.Net.WebControls.AttachmentFactory.Helpers
{
    public static class ConfigurationHelper
    {
        private static readonly string configPath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["AttachmentFactoryConfigFileLocation"]);

        public static string GetAllowedExtensions()
        {
            return Common.Helpers.ConfigurationHelper.GetConfigurationValues(configPath, "AllowedExtensions");
        }

        public static string GetUploadHandlerUrl()
        {
            return PageUtility.ResolveUrl(Common.Helpers.ConfigurationHelper.GetConfigurationValues(configPath, "UploadHandlerUrl"));
        }

        public static string GetAttachmentsDirectory()
        {
            return PageUtility.ResolveUrl(Common.Helpers.ConfigurationHelper.GetConfigurationValues(configPath, "AttachmentsDirectory"));
        }

        public static string GetUndoUploadServiceUrl()
        {
            return PageUtility.ResolveUrl(Common.Helpers.ConfigurationHelper.GetConfigurationValues(configPath, "UndoUploadServiceUrl"));
        }
    }
}