using MixERP.Net.Common.Helpers;
using System;
using System.Globalization;

namespace MixERP.Net.WebControls.AttachmentFactory
{
    public sealed partial class Attachment
    {
        private void CheckPermission()
        {
            var folder = Helpers.ConfigurationHelper.GetAttachmentsDirectory(this.Catalog);
            var writable = FileSystemHelper.IsDirectoryWritable(folder);

            if (!writable)
            {
                this.warningLabel.InnerText = String.Format(CultureInfo.CurrentUICulture, "The directory \"{0}\" is write protected", folder);
            }
        }
    }
}