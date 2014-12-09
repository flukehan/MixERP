using System.Globalization;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private void SetDefaultValues()
        {
            if (this.TranId > 0)
            {
                this.tranIdInputText.Value = this.TranId.ToString(CultureInfo.InvariantCulture);
            }

            this.tranCodeInputText.Value = this.TranCode;
            this.bookInputText.Value = this.Book;
            this.referenceNumberInputText.Value = this.ReferenceNumber;
            this.statementReferenceInputText.Value = this.StatementReference;
            this.postedByInputText.Value = this.PostedBy;
            this.officeInputText.Value = this.OfficeName;
            this.statusInputText.Value = this.Status;
            this.verifiedByInputText.Value = this.VerifiedBy;
            this.reasonInputText.Value = this.Reason;
        }
    }
}