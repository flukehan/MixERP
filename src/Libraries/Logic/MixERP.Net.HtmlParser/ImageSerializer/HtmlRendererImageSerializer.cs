using MixERP.Net.Common.Helpers;
using System.Drawing;
using System.Threading;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace MixERP.Net.HtmlParser.ImageSerializer
{
    public sealed class HtmlRendererImageSerializer : HtmlImageSerializer
    {
        internal override void CreateImage(string imagePath)
        {
            Thread thread = new Thread(delegate()
            {
                string path = imagePath + ImageHelper.GetFileExtension(this.ImageFormat);

                Image image = HtmlRender.RenderToImage(this.Html);

                image.Save(path, ImageHelper.GetEncoder(this.ImageFormat), null);
                OnImageSaved(new ImageSavedEventArgs(path));
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            base.CreateImage(imagePath);
        }
    }
}