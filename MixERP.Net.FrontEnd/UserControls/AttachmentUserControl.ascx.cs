using System;
using System.Configuration;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.UserControls
{
    public partial class AttachmentUserControl : UserControl
    {
        public string GetAllowedExtensions()
        {
            return ConfigurationManager.AppSettings["AllowedExtensions"];
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckPermission();
        }

        private void CheckPermission()
        {
            var folder = ConfigurationManager.AppSettings["AttachmentsDirectory"];
            var permission = new FileIOPermission(FileIOPermissionAccess.Write, Server.MapPath(folder));
            var permissionSet = new PermissionSet(PermissionState.None);
            permissionSet.AddPermission(permission);

            if (!permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
            {
                WarningLabel.Text = String.Format(CultureInfo.CurrentUICulture, "The directory \"{0}\" is write protected", folder);
                //AttachmentPanel.Enabled = false;
            }

        }
    }
}