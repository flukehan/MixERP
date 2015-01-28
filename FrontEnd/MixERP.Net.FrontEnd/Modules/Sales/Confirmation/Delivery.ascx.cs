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
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.TransactionChecklist;

namespace MixERP.Net.Core.Modules.Sales.Confirmation
{
    public partial class Delivery : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);

            using (TransactionChecklistForm checklist = new TransactionChecklistForm())
            {
                checklist.ViewReportButtonText = Resources.Titles.ViewThisDelivery;
                checklist.EmailReportButtonText = Resources.Titles.EmailThisDelivery;
                checklist.CustomerReportButtonText = Resources.Titles.ViewCustomerCopy;
                checklist.Text = Resources.Titles.SalesDelivery;
                checklist.AttachmentBookName = "transaction";
                checklist.OverridePath = "/Modules/Sales/Delivery.mix";
                checklist.DisplayWithdrawButton = true;
                checklist.DisplayViewReportButton = true;
                checklist.DisplayEmailReportButton = true;
                checklist.DisplayCustomerReportButton = true;
                checklist.DisplayPrintReceiptButton = false;
                checklist.DisplayPrintGlEntryButton = true;
                checklist.DisplayAttachmentButton = true;
                checklist.ReportPath = "~/Modules/Sales/Reports/DeliveryReport.mix";
                checklist.CustomerReportPath = "~/Modules/Sales/Reports/DeliveryNoteReport.mix";
                checklist.GlAdvicePath = "~/Modules/Finance/Reports/GLAdviceReport.mix";
                checklist.ViewPath = "/Modules/Sales/Delivery.mix";
                checklist.AddNewPath = "/Modules/Sales/Entry/Delivery.mix";

                checklist.PartyEmailAddress = Data.Helpers.Parties.GetEmailAddress(TranBook.Sales, SubTranBook.Delivery, transactionMasterId);

                this.Placeholder1.Controls.Add(checklist);
            }

            base.OnControlLoad(sender, e);
        }
    }
}