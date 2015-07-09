using MixERP.Net.Common;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.TransactionViewFactory.Helpers;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private MixERPGridView transactionGridView;

        private void AddGridPanel(Control container)
        {
            using (HtmlGenericControl gridPanel = new HtmlGenericControl("div"))
            {
                gridPanel.Attributes.Add("style", "width:100%;overflow:auto");

                this.AddGridView(gridPanel);

                container.Controls.Add(gridPanel);
            }
        }

        private void AddGridView(HtmlGenericControl container)
        {
            this.transactionGridView = new MixERPGridView();
            this.transactionGridView.ID = "TransactionGridView";
            this.transactionGridView.GridLines = GridLines.None;
            this.transactionGridView.AutoGenerateColumns = false;
            this.transactionGridView.CssClass = this.GridViewCssClass;

            this.transactionGridView.Attributes.Add("style", "min-width:1200px;max-width:2000px;");

            this.transactionGridView.RowDataBound += this.TransactionGridView_RowDataBound;
            GridViewColumnHelper.AddColumns(this.transactionGridView);

            container.Controls.Add(this.transactionGridView);
        }

        private void BindGrid()
        {
            int userId = this.UserId;
            int officeId = this.OfficeId;

            DateTime from = Conversion.TryCastDate(this.dateFromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(this.dateToDateTextBox.Text);
            long tranId = Conversion.TryCastLong(this.tranIdInputText.Value);
            string tranCode = this.tranCodeInputText.Value;
            string book = this.bookInputText.Value;
            string referenceNumber = this.referenceNumberInputText.Value;
            string statementReference = this.statementReferenceInputText.Value;
            string postedBy = this.postedByInputText.Value;
            string office = this.officeInputText.Value;
            string status = this.statusInputText.Value;
            string verifiedBy = this.verifiedByInputText.Value;
            string reason = this.reasonInputText.Value;

            this.transactionGridView.DataSource = Data.Journal.GetJournalView(this.Catalog, userId, officeId, from, to, tranId, tranCode, book, referenceNumber, statementReference, postedBy, office, status, verifiedBy, reason);
            this.transactionGridView.DataBind();
        }
    }
}