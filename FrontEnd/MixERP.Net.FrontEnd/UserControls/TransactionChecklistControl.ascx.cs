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
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DatabaseLayer.Transactions;
using Resources;
using System;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.UserControls
{
    /// This class is subject to be moved to a standalone server control class library.
    public partial class TransactionChecklistControl : UserControl
    {
        public bool DisplayWithdrawButton { get; set; }

        public bool DisplayViewInvoiceButton { get; set; }

        public bool DisplayEmailInvoiceButton { get; set; }

        public bool DisplayCustomerInvoiceButton { get; set; }

        public bool DisplayPrintReceiptButton { get; set; }

        public bool DisplayPrintGlEntryButton { get; set; }

        public bool DisplayAttachmentButton { get; set; }

        public bool IsNonGlTransaction { get; set; }

        public string InvoicePath { get; set; }

        public string CustomerInvoicePath { get; set; }

        public string GlAdvicePath { get; set; }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            DateTime transactionDate = DateTime.Now;
            long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);

            VerificationModel model = Verification.GetVerificationStatus(transactionMasterId);
            if (
                model.Verification.Equals(0) //Awaiting verification
                ||
                model.Verification.Equals(2) //Automatically Approved by Workflow
                )
            {
                //Withdraw this transaction.
                if (transactionMasterId > 0)
                {
                    if (Verification.WithdrawTransaction(transactionMasterId, SessionHelper.GetUserId(), this.ReasonTextBox.Text))
                    {
                        this.MessageLabel.Text = string.Format(Labels.TransactionWithdrawnMessage, transactionDate.ToShortDateString());
                        this.MessageLabel.CssClass = "success vpad12";
                    }
                }
            }
            else
            {
                this.MessageLabel.Text = Warnings.CannotWithdrawTransaction;
                this.MessageLabel.CssClass = "error vpad12";
            }

            this.ShowVerificationStatus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.WithdrawButton.Visible = this.DisplayWithdrawButton;
            this.ViewInvoiceButton.Visible = this.DisplayViewInvoiceButton;
            this.EmailInvoiceButton.Visible = this.DisplayEmailInvoiceButton;
            this.CustomerInvoiceButton.Visible = this.DisplayCustomerInvoiceButton;
            this.PrintReceiptButton.Visible = this.DisplayPrintReceiptButton;
            this.PrintGLButton.Visible = this.DisplayPrintGlEntryButton;
            this.AttachmentButton.Visible = this.DisplayAttachmentButton;

            string invoiceUrl = this.ResolveUrl(this.InvoicePath + "?TranId=" + this.Request["TranId"]);
            string customerInvoiceUrl = this.ResolveUrl(this.CustomerInvoicePath + "?TranId=" + this.Request["TranId"]);
            string glAdviceUrl = this.ResolveUrl(this.GlAdvicePath + "?TranId=" + this.Request["TranId"]);

            this.ViewInvoiceButton.Attributes.Add("onclick", "showWindow('" + invoiceUrl + "');return false;");
            this.CustomerInvoiceButton.Attributes.Add("onclick", "showWindow('" + customerInvoiceUrl + "');return false;");
            this.PrintGLButton.Attributes.Add("onclick", "showWindow('" + glAdviceUrl + "');return false;");

            this.ShowVerificationStatus();
        }

        private void ShowVerificationStatus()
        {
            long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);
            VerificationModel model = Verification.GetVerificationStatus(transactionMasterId);

            switch (model.Verification)
            {
                case -3:
                    this.VerificationLabel.CssClass = "alert-danger";
                    this.VerificationLabel.Text = string.Format(Labels.VerificationRejectedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                    break;

                case -2:
                    this.VerificationLabel.CssClass = "alert-warning";
                    this.VerificationLabel.Text = string.Format(Labels.VerificationClosedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                    break;

                case -1:
                    this.VerificationLabel.Text = string.Format(Labels.VerificationWithdrawnMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                    this.VerificationLabel.CssClass = "alert-warning";
                    break;

                case 0:
                    this.VerificationLabel.Text = Labels.VerificationAwaitingMessage;
                    this.VerificationLabel.CssClass = "alert-info";
                    break;

                case 1:
                case 2:
                    this.VerificationLabel.Text = string.Format(Labels.VerificationApprovedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()));
                    this.VerificationLabel.CssClass = "alert-success";
                    break;
            }
        }
    }
}