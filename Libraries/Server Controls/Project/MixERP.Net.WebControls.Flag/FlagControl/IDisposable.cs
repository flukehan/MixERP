namespace MixERP.Net.WebControls.Flag
{
    public partial class FlagControl
    {
        private bool disposed;

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
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.container != null)
                    {
                        this.container.Dispose();
                        this.container = null;

                        this.flagDropDownlist.Dispose();
                        this.flagDropDownlist = null;

                        this.updateButton.Dispose();
                        this.updateButton = null;
                    }
                }

                this.disposed = true;
            }
        }
    }
}