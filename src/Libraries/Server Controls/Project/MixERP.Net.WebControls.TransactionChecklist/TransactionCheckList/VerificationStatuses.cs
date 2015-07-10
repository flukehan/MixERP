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
using MixERP.Net.i18n;
using MixERP.Net.i18n.Resources;
using System.Globalization;
using System.Web.UI.WebControls;
using MixERP.Net.TransactionGovernor.Verification;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        private void ShowVerificationStatus(string tranId, Label label)
        {
            if (this.HideVerificationMessage)
            {
                return;
            }

            if (this.IsNonGlTransaction)
            {
                return;
            }

            long transactionMasterId = Conversion.TryCastLong(tranId);

            if (transactionMasterId <= 0)
            {
                return;
            }

            Entities.Models.Transactions.Verification model = VerificationStatus.GetVerificationStatus(this.Catalog, transactionMasterId, this.IsStockTransferRequest);

            switch (model.VerificationStatusId)
            {
                case -3:
                    label.CssClass = "ui block message red";
                    label.Text = string.Format(CultureInfo.CurrentCulture, Labels.TransactionRejectedDetails, model.VerifierName, model.VerifiedDate.ToString(CurrentCulture.GetCurrentUICulture()), model.VerificationReason);
                    break;

                case -2:
                    label.CssClass = "ui block message yellow";
                    label.Text = string.Format(CultureInfo.CurrentCulture, Labels.TransactionClosedDetails, model.VerifierName, model.VerifiedDate.ToString(CurrentCulture.GetCurrentUICulture()), model.VerificationReason);
                    break;

                case -1:
                    label.Text = string.Format(CultureInfo.CurrentCulture, Labels.TransactionWithdrawnDetails, model.VerifierName, model.VerifiedDate.ToString(CurrentCulture.GetCurrentUICulture()), model.VerificationReason);
                    label.CssClass = "ui block message yellow";
                    break;

                case 0:
                    label.Text = Labels.TransactionAwaitingVerification;
                    label.CssClass = "ui block message blue";
                    break;

                case 1:
                    label.Text = string.Format(CultureInfo.CurrentCulture, Labels.TransactionAutoApprovedDetails, model.VerifierName, model.VerifiedDate.ToString(CurrentCulture.GetCurrentUICulture()));
                    label.CssClass = "ui block message green";
                    break;

                case 2:
                    label.Text = string.Format(CultureInfo.CurrentCulture, Labels.TransactionApprovedDetails, model.VerifierName, model.VerifiedDate.ToString(CurrentCulture.GetCurrentUICulture()));
                    label.CssClass = "ui block message green";
                    break;
            }
        }
    }
}