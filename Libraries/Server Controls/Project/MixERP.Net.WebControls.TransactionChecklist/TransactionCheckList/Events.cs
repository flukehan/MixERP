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
using MixERP.Net.WebControls.TransactionChecklist.Helpers;
using MixERP.Net.WebControls.TransactionChecklist.Resources;
using System;
using System.Globalization;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        private void EmailLinkButton_Click(object sender, EventArgs e)
        {
            string tranId = this.GetTranId();

            string emailTemplate = emailHidden.Value;

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

            EmailHelper email = new EmailHelper(emailTemplate, this.Text + " #" + tranId, this.PartyEmailAddress);
            email.SendEmail();
            subTitleLiteral.Text = string.Format(CultureInfo.CurrentCulture, Labels.EmailSentConfirmation, this.PartyEmailAddress);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            string tranId = this.GetTranId();

            if (string.IsNullOrWhiteSpace(tranId))
            {
                return;
            }

            if (this.IsNonGlTransaction)
            {
                messageLabel.Text = Labels.CannotWithdrawNotValidGLTransaction;
                messageLabel.CssClass = "ui block message red vpad12";
                return;
            }

            DateTime transactionDate = DateTime.Now;
            long transactionMasterId = Conversion.TryCastLong(tranId);

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
                    if (Verification.WithdrawTransaction(transactionMasterId, SessionHelper.GetUserId(), reasonTextBox.Text))
                    {
                        messageLabel.Text = string.Format(CultureInfo.CurrentCulture, Labels.TransactionWithdrawnMessage, transactionDate.ToShortDateString());
                        messageLabel.CssClass = "ui block message yellow vpad12";
                    }
                }
            }
            else
            {
                messageLabel.Text = Labels.CannotWithdrawTransaction;
                messageLabel.CssClass = "ui block message red vpad12";
            }

            this.ShowVerificationStatus(tranId, this.verificationLabel);
        }
    }
}