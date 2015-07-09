using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.AttachmentFactory.Helpers
{
    public static class ConfigurationHelper
    {
        private const string configFile = "AttachmentFactoryConfigFileLocation";

        public static string GetAllowedExtensions(string catalog)
        {
            return DbConfig.GetAttachmentParameter(catalog, "AllowedExtensions");
        }

        public static string GetUploadHandlerUrl(string catalog)
        {
            string url = DbConfig.GetAttachmentParameter(catalog, "UploadHandlerUrl");
            return PageUtility.ResolveUrl(url);
        }

        public static string GetAttachmentsDirectory(string catalog)
        {
            string url = DbConfig.GetAttachmentParameter(catalog, "AttachmentsDirectory");
            return PageUtility.ResolveUrl(url);
        }

        public static string GetUndoUploadServiceUrl(string catalog)
        {
            string url = DbConfig.GetAttachmentParameter(catalog, "UndoUploadServiceUrl");
            return PageUtility.ResolveUrl(url);
        }
    }
}