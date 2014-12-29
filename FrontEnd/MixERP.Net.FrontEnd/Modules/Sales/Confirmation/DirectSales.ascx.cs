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
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.TransactionChecklist;
using System;

namespace MixERP.Net.Core.Modules.Sales.Confirmation
{
    public partial class DirectSales : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);

            using (TransactionChecklistForm checklist = new TransactionChecklistForm())
            {
                checklist.ViewReportButtonText = Resources.Titles.ViewThisInvoice;
                checklist.EmailReportButtonText = Resources.Titles.EmailThisInvoice;
                checklist.CustomerReportButtonText = Resources.Titles.ViewCustomerCopy;
                checklist.Text = Resources.Titles.DirectSales;
                checklist.PartyEmailAddress = Data.Helpers.Parties.GetEmailAddress(TranBook.Sales, SubTranBook.Direct, transactionMasterId);
                checklist.AttachmentBookName = "transaction";
                checklist.OverridePath = "/Modules/Sales/DirectSales.mix";
                checklist.DisplayWithdrawButton = true;
                checklist.DisplayViewReportButton = true;
                checklist.DisplayEmailReportButton = true;
                checklist.DisplayCustomerReportButton = true;
                checklist.DisplayPrintReceiptButton = true;
                checklist.DisplayPrintGlEntryButton = true;
                checklist.DisplayAttachmentButton = true;
                checklist.ReportPath = "~/Modules/Sales/Reports/DirectSalesInvoiceReport.mix";
                checklist.CustomerReportPath = "~/Modules/Sales/Reports/CustomerInvoiceReport.mix";
                checklist.ReceiptAdvicePath = "~/Modules/Sales/Reports/ReceiptReport.mix";
                checklist.GlAdvicePath = "~/Modules/Finance/Reports/GLAdviceReport.mix";
                checklist.ViewPath = "/Modules/Sales/DirectSales.mix";
                checklist.AddNewPath = "/Modules/Sales/Entry/DirectSales.mix";

                this.Placeholder1.Controls.Add(checklist);
            }

            base.OnControlLoad(sender, e);
        }
    }
}