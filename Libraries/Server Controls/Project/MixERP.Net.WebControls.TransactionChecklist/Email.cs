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

using MixERP.Net.Messaging.Email;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public class Email
    {
        public string EmailBody { get; set; }

        public string Html { get; set; }

        public string Subject { get; set; }

        public string Recipient { get; set; }

        private string tempPath;

        public Email(string html, string subject, string recipient)
        {
            this.Html = html;
            this.Subject = subject;
            this.EmailBody = Resources.Labels.EmailBody;
            this.Recipient = recipient;
            tempPath = "~/Resource/Temp/Images/" + Guid.NewGuid();
        }

        public void SendEmail()
        {
            this.StartBrowser();
        }

        private void StartBrowser()
        {
            Thread thread = new Thread(delegate()
            {
                using (WebBrowser browser = new WebBrowser())
                {
                    browser.ScrollBarsEnabled = false;
                    browser.AllowNavigation = true;

                    tempPath = HostingEnvironment.MapPath(tempPath + ".html");

                    if (tempPath == null)
                    {
                        return;
                    }

                    File.WriteAllText(this.tempPath, this.Html);
                    browser.Navigate(tempPath);
                    browser.ScriptErrorsSuppressed = true;
                    browser.Width = 1024;
                    browser.Height = 19999;
                    browser.Tag = tempPath;

                    browser.DocumentCompleted += DocumentCompleted;

                    while (browser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            if (browser == null)
            {
                return;
            }

            if (browser.Document == null || browser.Document.Body == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(browser.Tag.ToString()))
            {
                return;
            }

            File.Delete(browser.Tag.ToString());

            browser.Document.Body.Style = "zoom:100%;";

            var element = browser.Document.GetElementById("ReportParameterPanel");

            if (element != null)
            {
                element.OuterHtml = "";
                element.InnerHtml = "";
            }

            if (browser.Document.Body != null)
            {
                const int margin = 100;
                var height = browser.Document.Body.ScrollRectangle.Height + margin;
                var width = browser.Width;
                var imagePath = browser.Tag.ToString().Replace(".html", ".png");

                using (Bitmap bitmap = new Bitmap(width + 2 * margin, height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                        g.DrawImage(bitmap, 0, 0, width + 2 * margin, height);
                    }

                    browser.DrawToBitmap(bitmap, new Rectangle(margin, 0, width, height));
                    bitmap.SetResolution(3200, 3200);
                    bitmap.Save(imagePath, GetEncoder(ImageFormat.Png), null);

                    EmailProcessor processor = new EmailProcessor();
                    processor.Send(this.Recipient, this.Subject, this.EmailBody, EmailAttachment.GetAttachments(imagePath));
                }
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}