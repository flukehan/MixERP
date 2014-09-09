/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
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

                    if (this.GetAllowedExtensions().Contains(extension.Replace(".", "")))
                    {
                        string fileName = this.RandomNumber().ToString(CultureInfo.InvariantCulture) + "_" + Path.GetFileName(file.FileName);
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