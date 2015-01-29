using System;
using System.Configuration;
using System.Globalization;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.AttachmentFactory
{
    public sealed partial class Attachment
    {
        private void CheckPermission()
        {
            var folder = ConfigurationManager.AppSettings["AttachmentsDirectory"];
            var writable = FileSystemHelper.IsDirectoryWritable(folder, true);

            if (!writable)
            {
                this.warningLabel.InnerText = String.Format(CultureInfo.CurrentUICulture, "The directory \"{0}\" is write protected", folder);
            }
        }
    }
}