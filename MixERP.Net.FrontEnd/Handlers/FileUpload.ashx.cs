using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;


namespace MixERP.Net.FrontEnd.Handlers
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string attachmentsDirectory = ConfigurationManager.AppSettings["AttachmentsDirectory"];
            Collection<string> uploadedFiles = new Collection<string>();

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string extension = Path.GetExtension(file.FileName);

                    if (this.GetAllowedExtensions().Contains(extension.Replace(".","")))
                    {
                        string fileName = this.RandomNumber().ToString() + "_" + Path.GetFileName(file.FileName);
                        string savePath = context.Server.MapPath(attachmentsDirectory + fileName);
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
            return ConfigurationManager.AppSettings["AllowedExtensions"].Split(',').ToList();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(9999999);
        }

    }
}