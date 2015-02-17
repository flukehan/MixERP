using MixERP.Net.Common.Helpers;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MixERP.Net.HtmlParser.ImageSerializer
{
    public sealed class WebBrowserImageSerializer : HtmlImageSerializer
    {
        internal override void CreateImage(string imagePath)
        {
            Thread thread = new Thread(delegate()
            {
                using (WebBrowser browser = new WebBrowser())
                {
                    browser.ScrollBarsEnabled = false;
                    browser.AllowNavigation = true;

                    string tempPath = imagePath + ".html";

                    File.WriteAllText(tempPath, this.Html);
                    browser.Navigate(tempPath);
                    browser.ScriptErrorsSuppressed = true;
                    //browser.Width = 1024;
                    //browser.Height = 19999;
                    browser.Tag = imagePath;

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

            base.CreateImage(imagePath);
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

            if (browser.Document.Body != null)
            {
                const int margin = 100;
                int height = browser.Document.Body.ScrollRectangle.Height + margin;
                int width = browser.Width;
                string imagePath = browser.Tag + ImageHelper.GetFileExtension(this.ImageFormat);

                using (Bitmap bitmap = new Bitmap(width + 2 * margin, height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                        g.DrawImage(bitmap, 0, 0, width + 2 * margin, height);
                    }

                    browser.DrawToBitmap(bitmap, new Rectangle(margin, 0, width, height));
                    bitmap.SetResolution(3200, 3200);
                    bitmap.Save(imagePath, ImageHelper.GetEncoder(this.ImageFormat), null);

                    this.OnImageSaved(new ImageSavedEventArgs(imagePath));
                }
            }
        }
    }
}