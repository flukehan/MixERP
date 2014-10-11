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
using MixERP.Net.WebControls.TransactionChecklist.Resources;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        private void ShowVerificationStatus(string tranId, Label label)
        {
            if (this.IsNonGlTransaction)
            {
                return;
            }

            long transactionMasterId = Conversion.TryCastLong(tranId);
            VerificationModel model = Verification.GetVerificationStatus(transactionMasterId);

            switch (model.Verification)
            {
                case -3:
                    label.CssClass = "alert-danger";
                    label.Text =
                        string.Format(Labels.TransactionWithdrawnMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                    break;

                case -2:
                    label.CssClass = "alert-warning";
                    label.Text =
                        string.Format(Labels.TransactionClosedDetails, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                    break;

                case -1:
                    label.Text =
                        string.Format(Labels.TransactionWithdrawnDetails, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                    label.CssClass = "alert-warning";
                    break;

                case 0:
                    label.Text = Labels.TransactionAwaitingVerification;
                    label.CssClass = "alert-info";
                    break;

                case 1:
                    label.Text = string.Format(Labels.TransactionApprovedDetails, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()));
                    label.CssClass = "alert-success";
                    break;
                case 2:
                    label.Text = string.Format(Labels.TransactionAutoApprovedDetails, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()));
                    label.CssClass = "alert-success";
                    break;
            }
        }
    }
}