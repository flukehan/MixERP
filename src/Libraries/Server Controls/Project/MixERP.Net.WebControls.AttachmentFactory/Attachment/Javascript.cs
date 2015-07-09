using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using ConfigurationHelper = MixERP.Net.WebControls.AttachmentFactory.Helpers.ConfigurationHelper;

[assembly:
    WebResource("MixERP.Net.WebControls.AttachmentFactory.AttachmentFactory.js", "application/x-javascript",
        PerformSubstitution = true)]
[assembly:
    WebResource("MixERP.Net.WebControls.AttachmentFactory.ajax-file-upload.js", "application/x-javascript",
        PerformSubstitution = true)]

namespace MixERP.Net.WebControls.AttachmentFactory
{
    public sealed partial class Attachment
    {
        [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
        private void AddJavascript()
        {
            JSUtility.AddJSReference(this.Page, "MixERP.Net.WebControls.AttachmentFactory.ajax-file-upload.js",
                "AttachmentAjaxFileUpload", typeof (Attachment));
            JSUtility.AddJSReference(this.Page, "MixERP.Net.WebControls.AttachmentFactory.AttachmentFactory.js",
                "Attachment", typeof (Attachment));
        }

        private void RegisterJavascript()
        {
            string script = JSUtility.GetVar("allowedExtensions", ConfigurationHelper.GetAllowedExtensions(this.Catalog));

            script += "allowedExtensions = allowedExtensions.split(',');";
            script += JSUtility.GetVar("undoUploadServiceUrl", ConfigurationHelper.GetUndoUploadServiceUrl(this.Catalog));
            script += JSUtility.GetVar("uploadHandlerUrl", ConfigurationHelper.GetUploadHandlerUrl(this.Catalog));

            PageUtility.RegisterJavascript("AttachmentUserControl_Resources", script, this.Page, true);
        }
    }
}