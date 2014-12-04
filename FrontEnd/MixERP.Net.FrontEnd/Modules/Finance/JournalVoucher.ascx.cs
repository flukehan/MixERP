/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Flag;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance
{
    public partial class JournalVoucher : MixERPUserControl
    {
        public void AddColumns(GridView grid)
        {
            grid.Columns.Clear();

            //AddTemplateFields(grid);

            AddDataBoundControl(grid, "transaction_master_id", Titles.Id);
            AddDataBoundControl(grid, "transaction_code", Titles.TranCode);
            AddDataBoundControl(grid, "book", Titles.Book);
            AddDataBoundControl(grid, "value_date", Titles.ValueDate, "{0:d}");
            AddDataBoundControl(grid, "reference_number", Titles.ReferenceNumber);
            AddDataBoundControl(grid, "statement_reference", Titles.StatementReference);

            AddDataBoundControl(grid, "posted_by", Titles.PostedBy);
            AddDataBoundControl(grid, "office", Titles.Office);

            AddDataBoundControl(grid, "status", Titles.Status);
            AddDataBoundControl(grid, "verified_by", Titles.VerifiedBy);
            AddDataBoundControl(grid, "verified_on", Titles.VerifiedOn, "{0:g}");
            AddDataBoundControl(grid, "reason", Titles.Reason);
            AddDataBoundControl(grid, "transaction_ts", Titles.TransactionTimestamp, "{0:g}");
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (FlagControl flag = new FlagControl())
            {
                flag.ID = "FlagPopUnder";
                flag.AssociatedControlId = "FlagButton";
                flag.OnClientClick = "return getSelectedItems();";
                flag.CssClass = "ui segment initially hidden";

                flag.Updated += Flag_Updated;

                FlagPlaceholder.Controls.Add(flag);
            }

            this.BindGrid();
            base.OnControlLoad(sender, e);
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void TransactionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        private static void AddDataBoundControl(GridView grid, string dataField, string headerText, string dataFormatString = "")
        {
            BoundField field = new BoundField();
            field.DataField = dataField;
            field.HeaderText = headerText;
            field.DataFormatString = dataFormatString;

            grid.Columns.Add(field);
        }

        private void BindGrid()
        {
            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();

            DateTime from = Conversion.TryCastDate(DateFromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(DateToDateTextBox.Text);
            long tranId = Conversion.TryCastLong(TranIdInputText.Value);
            string tranCode = TranCodeInputText.Value;
            string book = BookInputText.Value;
            string referenceNumber = ReferenceNumberInputText.Value;
            string statementReference = StatementReferenceInputText.Value;
            string postedBy = PostedByInputText.Value;
            string office = OfficeInputText.Value;
            string status = StatusInputText.Value;
            string verifiedBy = VerifiedByInputText.Value;
            string reason = ReasonInputText.Value;

            this.AddColumns(TransactionGridView);

            using (DataTable table = Data.Journal.GetJournalView(userId, officeId, from, to, tranId, tranCode, book, referenceNumber, statementReference, postedBy, office, status, verifiedBy, reason))
            {
                this.TransactionGridView.DataSource = table;
                this.TransactionGridView.DataBind();
            }
        }

        private void Flag_Updated(object sender, FlagUpdatedEventArgs e)
        {
        }
    }
}