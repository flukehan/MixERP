namespace MixERP.Net.HtmlParser.ImageSerializer
{
    public class ImageSavedEventArgs : System.EventArgs
    {
        public ImageSavedEventArgs(string imagePath)
        {
            this.ImagePath = imagePath;
        }

        public string ImagePath { get; private set; }
    }
}