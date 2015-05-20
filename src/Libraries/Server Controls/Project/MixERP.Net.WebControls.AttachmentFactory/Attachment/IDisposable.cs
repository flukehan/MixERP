using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.AttachmentFactory
{
    public sealed partial class Attachment
    {
        private bool disposed;

        private PlaceHolder placeHolder;
        private HtmlGenericControl warningLabel;

        public override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }


            if (this.placeHolder != null)
            {
                this.placeHolder.Dispose();
                this.placeHolder = null;
            }

            if (this.warningLabel != null)
            {
                this.warningLabel.Dispose();
                this.warningLabel = null;
            }

            this.disposed = true;
        }
    }
}