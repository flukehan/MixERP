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

using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        public string AddNewPath { get; set; }
        public string AttachmentBookName { get; set; }
        public string Catalog { get; set; }
        public string CustomerReportButtonText { get; set; }
        public string CustomerReportPath { get; set; }
        public bool HideVerificationMessage { get; set; }
        public bool DisplayAttachmentButton { get; set; }
        public bool DisplayCustomerReportButton { get; set; }
        public bool DisplayEmailReportButton { get; set; }
        public bool DisplayPrintGlEntryButton { get; set; }
        public bool DisplayPrintReceiptButton { get; set; }
        public bool DisplayViewReportButton { get; set; }
        public bool DisplayWithdrawButton { get; set; }
        public string EmailReportButtonText { get; set; }
        public string GlAdvicePath { get; set; }
        public bool IsNonGlTransaction { get; set; }
        public bool IsStockTransferRequest { get; set; }
        public string TableName { get; set; }
        public string OverridePath { get; set; }
        public string PartyEmailAddress { get; set; }
        public string ReceiptAdvicePath { get; set; }
        public string ReportPath { get; set; }
        public string Text { get; set; }
        public string ViewPath { get; set; }
        public string ViewReportButtonText { get; set; }
        public new Unit Width { get; set; }
        public int UserId { get; set; }
        public bool RestrictedTransactionMode { get; set; }
    }
}