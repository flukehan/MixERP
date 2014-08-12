using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common.Models.Core;

namespace MixERP.Net.FrontEnd.Services
{
    /// <summary>
    /// Summary description for UploadHelper
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class UploadHelper : WebService
    {

        [WebMethod]
        public bool UndoUpload(string uploadedFiles)
        {
            Collection<Attachment> attachments = this.GetAttachments(uploadedFiles);
            string attachmentsDirectory = ConfigurationManager.AppSettings["AttachmentsDirectory"];

            foreach (Attachment attachment in attachments)
            {
                string path = Server.MapPath(attachmentsDirectory + attachment.FilePath);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return true;
        }

        public Collection<Attachment> GetAttachments(string uploadedFiles)
        {
            Collection<Attachment> attachments = new Collection<Attachment>();

            string uploads = uploadedFiles;

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

    }
}
