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
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.TransactionChecklist.Helpers;
using System;
using System.Globalization;
using MixERP.Net.TransactionGovernor.Verification;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        private void EmailLinkButton_Click(object sender, EventArgs e)
        {
            string tranId = this.GetTranId();

            string emailTemplate = this.emailHidden.Value;

            if (string.IsNullOrWhiteSpace(tranId))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(emailTemplate))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(this.PartyEmailAddress))
            {
                return;
            }

            EmailHelper email = new EmailHelper(this.Catalog, emailTemplate, this.Text + " #" + tranId, this.PartyEmailAddress);
            email.SendEmail();
            this.subTitleLiteral.Text = string.Format(CultureInfo.CurrentCulture, Labels.EmailSentConfirmation, this.PartyEmailAddress);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (this.RestrictedTransactionMode)
            {
                this.messageLabel.Text = Labels.CannotWithdrawTransaction;
                this.messageLabel.CssClass = "ui block message red vpad12";
                return;
            }

            string tranId = this.GetTranId();

            if (string.IsNullOrWhiteSpace(tranId))
            {
                return;
            }

            if (this.IsNonGlTransaction)
            {
                this.messageLabel.Text = Labels.CannotWithdrawNotValidGLTransaction;
                this.messageLabel.CssClass = "ui block message red vpad12";
                return;
            }

            DateTime transactionDate = DateTime.Now;
            long transactionMasterId = Conversion.TryCastLong(tranId);

            Entities.Models.Transactions.Verification model = VerificationStatus.GetVerificationStatus(this.Catalog, transactionMasterId, this.IsStockTransferRequest);
            if (
                model.VerificationStatusId.Equals(0) //Awaiting verification
                ||
                model.VerificationStatusId.Equals(1) //Automatically Approved by Workflow
                )
            {
                //Withdraw this transaction.
                if (transactionMasterId > 0)
                {
                    if (Withdrawal.WithdrawTransaction(this.Catalog, this.IsStockTransferRequest, transactionMasterId, this.UserId, this.reasonTextBox.Text))
                    {
                        this.messageLabel.Text = string.Format(CultureInfo.CurrentCulture, Labels.TransactionWithdrawnMessage, transactionDate.ToShortDateString());
                        this.messageLabel.CssClass = "ui block message yellow vpad12";
                    }
                }
            }
            else
            {
                this.messageLabel.Text = Labels.CannotWithdrawTransaction;
                this.messageLabel.CssClass = "ui block message red vpad12";
            }

            this.ShowVerificationStatus(tranId, this.verificationLabel);
        }
    }
}