using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.TransactionViewFactory.Helpers;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.Common;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private MixERPGridView transactionGridView;

        private void AddGridPanel(Control container)
        {
            using (HtmlGenericControl gridPanel = new HtmlGenericControl("div"))
            {
                gridPanel.Attributes.Add("style", "overflow:auto");

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
            this.transactionGridView.RowDataBound += this.TransactionGridView_RowDataBound;
            GridViewColumnHelper.AddColumns(this.transactionGridView);

            container.Controls.Add(this.transactionGridView);
        }

        private void BindGrid()
        {
            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();

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

            using (DataTable table = Data.Journal.GetJournalView(userId, officeId, from, to, tranId, tranCode, book, referenceNumber, statementReference, postedBy, office, status, verifiedBy, reason))
            {
                this.transactionGridView.DataSource = table;
                this.transactionGridView.DataBind();
            }
        }
    }
}