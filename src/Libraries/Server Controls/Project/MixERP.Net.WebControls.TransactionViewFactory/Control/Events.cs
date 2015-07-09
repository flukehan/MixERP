using MixERP.Net.WebControls.Flag;
using System;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private void Flag_Updated(object sender, FlagUpdatedEventArgs e)
        {
            int flagTypeId = e.FlagId;

            const string resource = "transactions.transaction_master";
            const string resourceKey = "transaction_master_id";

            int userId = this.UserId;

            TransactionGovernor.Flags.CreateFlag(this.Catalog, userId, flagTypeId, resource, resourceKey, this.GetSelectedValues());

            this.BindGrid();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        private void TransactionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}