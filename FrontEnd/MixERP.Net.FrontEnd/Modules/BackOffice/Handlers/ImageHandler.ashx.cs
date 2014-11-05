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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System;
using System.Drawing;
using System.Globalization;
using System.Web;

namespace MixERP.Net.Core.Modules.BackOffice.Handlers
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            ProcessImage(context);
        }

        private static void ProcessImage(HttpContext context)
        {
            string path = string.Empty;
            int width = 0;
            int height = 0;

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["Path"]))
            {
                path = Conversion.TryCastString(context.Request.QueryString["Path"]);
            }

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["W"]))
            {
                width = Convert.ToInt32(context.Request.QueryString["W"], CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["H"]))
            {
                height = Convert.ToInt32(context.Request.QueryString["H"], CultureInfo.InvariantCulture);
            }

            context.Response.Clear();

            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(path));
                using (Bitmap originalImage = new Bitmap(context.Server.MapPath(path)))
                {
                    byte[] buffer = ImageHelper.GetResizedImage(originalImage, width, height);

                    context.Response.ContentType = ImageHelper.GetContentType(file.Extension);
                    context.Response.Cache.SetCacheability(HttpCacheability.Public);
                    context.Response.Cache.SetExpires(DateTime.Now.AddMonths(1));
                    context.Response.Cache.SetMaxAge(new TimeSpan(30, 0, 0, 0, 0));
                    context.Response.AddHeader("Last-Modified", DateTime.Now.ToShortDateString());

                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}