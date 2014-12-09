namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
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
                    if (this.panel != null)
                    {
                        this.panel.Dispose();
                        this.panel = null;
                    }

                    if (this.transactionGridView != null)
                    {
                        this.transactionGridView.Dispose();
                        this.transactionGridView = null;
                    }

                    if (this.selectedValuesHidden != null)
                    {
                        this.selectedValuesHidden.Dispose();
                        this.selectedValuesHidden = null;
                    }
                    if (this.bookInputText != null)
                    {
                        this.bookInputText.Dispose();
                        this.bookInputText = null;
                    }
                    if (this.dateFromDateTextBox != null)
                    {
                        this.dateFromDateTextBox.Dispose();
                        this.dateFromDateTextBox = null;
                    }
                    if (this.dateToDateTextBox != null)
                    {
                        this.dateToDateTextBox.Dispose();
                        this.dateToDateTextBox = null;
                    }
                    if (this.officeInputText != null)
                    {
                        this.officeInputText.Dispose();
                        this.officeInputText = null;
                    }
                    if (this.postedByInputText != null)
                    {
                        this.postedByInputText.Dispose();
                        this.postedByInputText = null;
                    }
                    if (this.reasonInputText != null)
                    {
                        this.reasonInputText.Dispose();
                        this.reasonInputText = null;
                    }
                    if (this.referenceNumberInputText != null)
                    {
                        this.referenceNumberInputText.Dispose();
                        this.referenceNumberInputText = null;
                    }
                    if (this.showButton != null)
                    {
                        this.showButton.Dispose();
                        this.showButton = null;
                    }
                    if (this.statementReferenceInputText != null)
                    {
                        this.statementReferenceInputText.Dispose();
                        this.statementReferenceInputText = null;
                    }
                    if (this.statusInputText != null)
                    {
                        this.statusInputText.Dispose();
                        this.statusInputText = null;
                    }
                    if (this.tranCodeInputText != null)
                    {
                        this.tranCodeInputText.Dispose();
                        this.tranCodeInputText = null;
                    }
                    if (this.tranIdInputText != null)
                    {
                        this.tranIdInputText.Dispose();
                        this.tranIdInputText = null;
                    }
                    if (this.verifiedByInputText != null)
                    {
                        this.verifiedByInputText.Dispose();
                        this.verifiedByInputText = null;
                    }
                }

                this.disposed = true;
            }
        }
    }
}