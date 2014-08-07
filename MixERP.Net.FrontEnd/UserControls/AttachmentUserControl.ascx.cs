using MixERP.Net.Common.Models.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.UserControls
{
    public partial class AttachmentUserControl : System.Web.UI.UserControl
    {
        public void Undo()
        {
            Collection<Attachment> attachments = this.GetAttachments();
            string attachmentsDirectory = ConfigurationManager.AppSettings["AttachmentsDirectory"];

            foreach (Attachment attachment in attachments)
            {
                string path = Server.MapPath(attachmentsDirectory + attachment.FilePath);
                
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            UploadedFiles.Value = "";
        }

        public string GetAllowedExtensions()
        {
            return ConfigurationManager.AppSettings["AllowedExtensions"];
        }

        public Collection<Attachment> GetAttachments()
        {
            Collection<Attachment> attachments = new Collection<Attachment>();

            string uploads = UploadedFiles.Value;

            List<string> data = uploads.Split(',').ToList();

            foreach (string item in data)
            {
                Attachment attachment = new Attachment();
                attachment.Comment = item.Split('|')[0];
                attachment.FilePath = item.Split('|')[1];

                attachments.Add(attachment);
            }


            return attachments;
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

        protected void UndoButton_ServerClick(object sender, EventArgs e)
        {
            this.Undo();
        }
    }
}