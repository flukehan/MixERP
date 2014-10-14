using System;
using System.Drawing.Imaging;

namespace MixERP.Net.HtmlParser.ImageSerializer
{
    public interface IHtmlImageSerializer
    {
        string TempDirectory { get; set; }

        string Html { get; set; }

        ImageFormat ImageFormat { get; set; }

        event EventHandler<ImageSavedEventArgs> ImageSaved;

        void Serialize();
    }
}