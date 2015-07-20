using MixERP.Net.ApplicationState.Cache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace MixERP.Net.WebControls.AttachmentFactory
{
    public class FileUploadHanlder : IHttpHandler
    {
        /// <summary>
        ///     You will need to configure this handler in the Web.config file of your
        ///     web and register it with IIS before being able to use it. For more information
        ///     see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        #region IHttpHandler Members
        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string attachmentsDirectory = Helpers.ConfigurationHelper.GetAttachmentsDirectory(AppUsers.GetCurrentUserDB());
            Collection<string> uploadedFiles = new Collection<string>();

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string extension = Path.GetExtension(file.FileName);

                    if (this.GetAllowedExtensions().Contains(extension.Replace(".", "")))
                    {
                        string fileName = this.RandomNumber().ToString(CultureInfo.InvariantCulture) + "_" + Path.GetFileName(file.FileName);
                        string savePath = context.Server.MapPath(Path.Combine(attachmentsDirectory, fileName));
                        file.SaveAs(savePath);
                        uploadedFiles.Add(fileName);
                    }
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(string.Join(",", uploadedFiles));
        }

        private List<string> GetAllowedExtensions()
        {
            return Helpers.ConfigurationHelper.GetAllowedExtensions(AppUsers.GetCurrentUserDB()).Split(',').ToList();
        }

        private int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(9999999);
        }

        #endregion
    }
}