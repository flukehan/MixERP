/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Core;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.BusinessLayer.Transactions;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using Resources;
using FormHelper = MixERP.Net.Common.Helpers.FormHelper;

namespace MixERP.Net.FrontEnd.Finance
{
    public partial class JournalVoucher : MixERPWebpage
    {
        protected void PostTransactionButton_Click(object sender, EventArgs e)
        {
            DateTime valueDate = Conversion.TryCastDate(this.ValueDateTextBox.Text);
            string referenceNumber = this.ReferenceNumberTextBox.Text;
            int costCenterId = Conversion.TryCastInteger(this.CostCenterDropDownList.SelectedItem.Value);

            long transactionId = Transaction.Add(valueDate, referenceNumber, costCenterId, this.TransactionGridView);

            if(transactionId > 0)
            {
                this.Response.Redirect("~/Finance/Confirmation/JournalVoucher.aspx?TranId=" + transactionId, true);
            }
        }
        protected void TransactionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton lb = e.Row.FindControl("DeleteImageButton") as ImageButton;
                var current = ScriptManager.GetCurrent(this.Page);

                if (current != null)
                {
                    current.RegisterAsyncPostBackControl(lb);
                }
            }
        }

        protected void TransactionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            Collection<JournalDetailsModel> table = this.GetTable();

            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            table.RemoveAt(index);
            this.Session[this.ID] = table;
            this.BindGridView();
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                if(this.Session[this.ID] != null)
                {
                    this.Session.Remove(this.ID);
                }
            }
            this.InitializeControls();
            this.BindGridView();
        }

        private void InitializeControls()
        {
            this.ValueDateLiteral.Text = HtmlControlHelper.GetLabel(this.ValueDateTextBox.ClientID,  Titles.ValueDate);
            this.ReferenceNumberLiteral.Text = HtmlControlHelper.GetLabel(this.ReferenceNumberTextBox.ClientID, Titles.ReferenceNumber);
            this.CostCenterLiteral.Text = HtmlControlHelper.GetLabel(this.CostCenterDropDownList.ClientID, Titles.CostCenter);
            this.DebitTotalLiteral.Text = HtmlControlHelper.GetLabel(this.DebitTotalTextBox.ClientID, Titles.DebitTotal);
            this.CreditTotalLiteral.Text = HtmlControlHelper.GetLabel(this.CreditTotalTextBox.ClientID, Titles.CreditTotal);
        }

        private Collection<JournalDetailsModel> GetTable()
        {
            if(this.Session[this.ID] != null)
            {
                return this.Session[this.ID] as Collection<JournalDetailsModel>;
            }

            return new Collection<JournalDetailsModel>();
        }

        private void AddRowToTable(string accountCode, string account, string cashRepository, string statementReference, decimal debit, decimal credit)
        {
            Collection<JournalDetailsModel> table = this.GetTable();

            JournalDetailsModel model = new JournalDetailsModel();
            model.AccountCode = accountCode;
            model.Account = account;
            model.CashRepository = cashRepository;
            model.StatementReference = statementReference;
            model.Debit = debit;
            model.Credit = credit;

            table.Add(model);
            this.Session[this.ID] = table;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBindControls();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            string accountCode = this.AccountCodeTextBox.Text;
            string account = this.AccountDropDownList.SelectedItem.Text;
            string statementReference = this.StatementReferenceTextBox.Text;
            string cashRepository = this.CashRepositoryDropDownList.SelectedItem.Text;
            decimal debit = Conversion.TryCastDecimal(this.DebitTextBox.Text);
            decimal credit = Conversion.TryCastDecimal(this.CreditTextBox.Text);

            #region Validation
            if(string.IsNullOrWhiteSpace(accountCode))
            {                
                FormHelper.MakeDirty(this.AccountCodeTextBox);
                return;
            }
            
            FormHelper.RemoveDirty(this.AccountCodeTextBox);

            if(string.IsNullOrWhiteSpace(account))
            {
                FormHelper.MakeDirty(this.AccountDropDownList);
                return;
            }
            
            FormHelper.RemoveDirty(this.AccountDropDownList);

            if(debit.Equals(0) && credit.Equals(0))
            {
                FormHelper.MakeDirty(this.DebitTextBox);
                FormHelper.MakeDirty(this.CreditTextBox);
                return;
            }
            
            if(debit > 0 && credit > 0)
            {
                FormHelper.MakeDirty(this.DebitTextBox);
                FormHelper.MakeDirty(this.CreditTextBox);
                return;
            }

            FormHelper.RemoveDirty(this.StatementReferenceTextBox);

            if(Accounts.IsCashAccount(accountCode))
            {
                if(string.IsNullOrEmpty(this.CashRepositoryDropDownList.SelectedItem.Value))
                {
                    FormHelper.MakeDirty(this.CashRepositoryDropDownList);
                    this.CashRepositoryDropDownList.Focus();
                    return;
                }
                
                FormHelper.RemoveDirty(this.CashRepositoryDropDownList);
            }

            Collection<JournalDetailsModel> table = this.GetTable();

            if(table.Count > 0)
            {
                foreach(JournalDetailsModel row in table)
                {
                    if(row.AccountCode.Equals(accountCode))
                    {
                        FormHelper.MakeDirty(this.AccountCodeTextBox);
                        FormHelper.MakeDirty(this.AccountDropDownList);
                        return;
                    }
                }
            }

            #endregion

            this.AddRowToTable(accountCode, account, cashRepository, statementReference, debit, credit);
            this.BindGridView();
            this.DebitTextBox.Text = "";
            this.CreditTextBox.Text = "";
            this.AccountCodeTextBox.Focus();
        }

        private void DataBindControls()
        {
            string displayField = ConfigurationHelper.GetDbParameter( "CostCenterDisplayField");
            DropDownListHelper.BindDropDownList(this.CostCenterDropDownList, "office", "cost_centers", "cost_center_id", displayField);
        }

        private void BindGridView()
        {
            Collection<JournalDetailsModel> table = this.GetTable();
            this.TransactionGridView.DataSource = table;
            this.TransactionGridView.DataBind();

            if(table.Count > 0)
            {
                decimal debit = 0;
                decimal credit = 0;

                foreach(JournalDetailsModel row in table)
                {
                    debit += row.Debit;
                    credit += row.Credit;
                }

                this.DebitTotalTextBox.Text = debit.ToString("G29", Thread.CurrentThread.CurrentCulture);
                this.CreditTotalTextBox.Text = credit.ToString("G29", Thread.CurrentThread.CurrentCulture);
            }

        }

    }
}