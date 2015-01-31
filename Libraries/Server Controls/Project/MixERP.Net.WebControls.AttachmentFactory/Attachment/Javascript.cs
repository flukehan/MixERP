using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.AttachmentFactory.Resources;

[assembly: WebResource("MixERP.Net.WebControls.AttachmentFactory.AttachmentFactory.js", "application/x-javascript", PerformSubstitution = true)]
[assembly: WebResource("MixERP.Net.WebControls.AttachmentFactory.ajax-file-upload.js", "application/x-javascript", PerformSubstitution = true)]
namespace MixERP.Net.WebControls.AttachmentFactory
{
    public sealed partial class Attachment
    {

        [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
        private void AddJavascript()
        {
            JavascriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.AttachmentFactory.ajax-file-upload.js", "attachment_factory_ajax_file_upload", this.GetType());
            JavascriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.AttachmentFactory.AttachmentFactory.js", "attachment_factory", this.GetType());
        }


        private void RegisterJavascript()
        {
            string javascript = "var allowedExtensions='{0}'.split(',');" +
                                "var areYouSureLocalized='{1}';" +
                                "var duplicateFileLocalized='{2}';" +
                                "var invalidFileLocalized='{3}';" +
                                "var uploadedFilesDeletedLocalized='{4}';" +
                                "var undoUploadServiceUrl='{5}';" +
                                "var uploadHandlerUrl='{6}';";

            string allowedExtensions = Helpers.ConfigurationHelper.GetAllowedExtensions();
            string areYouSure = Messages.AreYouSure;
            string duplicateFile = Messages.DuplicateFile;
            string invalidFile = Messages.InvalidFile;
            string uploadedFilesDeleted = Messages.UploadFilesDeleted;
            string undoUploadUrl = Helpers.ConfigurationHelper.GetUndoUploadServiceUrl();
            string uploadUrl = Helpers.ConfigurationHelper.GetUploadHandlerUrl();

            javascript = string.Format(CultureInfo.InvariantCulture, javascript, allowedExtensions, areYouSure, duplicateFile, invalidFile, uploadedFilesDeleted, undoUploadUrl, uploadUrl);
            PageUtility.RegisterJavascript("AttachmentUserControl_Resources", javascript, this.Page, true);
        }
    }
}
