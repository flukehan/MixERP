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

using System;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.WebControls.TransactionChecklist;

namespace MixERP.Net.Core.Modules.Purchase.Confirmation
{
    public partial class DirectPurchase : TransactionCheckListControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);

            using (TransactionChecklistForm checklist = new TransactionChecklistForm())
            {
                checklist.Text = Resources.Titles.DirectPurchase;
                checklist.ViewReportButtonText = Resources.Titles.ViewThisInvoice;
                checklist.EmailReportButtonText = Resources.Titles.EmailThisInvoice;
                checklist.PartyEmailAddress = Data.Helpers.Parties.GetEmailAddress(TranBook.Purchase, SubTranBook.Direct, transactionMasterId);
                checklist.AttachmentBookName = "transaction";
                checklist.OverridePath = "/Modules/Purchase/DirectPurchase.mix";
                checklist.DisplayWithdrawButton = true;
                checklist.DisplayViewReportButton = true;
                checklist.DisplayEmailReportButton = true;
                checklist.DisplayPrintGlEntryButton = true;
                checklist.DisplayAttachmentButton = true;
                checklist.ReportPath = "~/Modules/Purchase/Reports/DirectPurchaseInvoiceReport.mix";
                checklist.GlAdvicePath = "~/Modules/Finance/Reports/GLAdviceReport.mix";
                checklist.ViewPath = "/Modules/Purchase/DirectPurchase.mix";
                checklist.AddNewPath = "/Modules/Purchase/Entry/DirectPurchase.mix";
                checklist.UserId = CurrentUser.GetSignInView().UserId.ToInt();

                this.Placeholder1.Controls.Add(checklist);
            }
        }
    }
}