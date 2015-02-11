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
    public partial class Order : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);

            using (TransactionChecklistForm checklist = new TransactionChecklistForm())
            {
                checklist.ViewReportButtonText = Resources.Titles.ViewThisOrder;
                checklist.EmailReportButtonText = Resources.Titles.EmailThisOrder;
                checklist.Text = Resources.Titles.SalesOrder;
                checklist.PartyEmailAddress = Data.Helpers.Parties.GetEmailAddress(TranBook.Sales, SubTranBook.Order, transactionMasterId);
                checklist.AttachmentBookName = "non-gl-transaction";
                checklist.OverridePath = "/Modules/Sales/Order.mix";
                checklist.DisplayViewReportButton = true;
                checklist.DisplayEmailReportButton = true;
                checklist.DisplayAttachmentButton = true;
                checklist.IsNonGlTransaction = true;
                checklist.ReportPath = "~/Modules/Sales/Reports/SalesOrderReport.mix";
                checklist.ViewPath = "/Modules/Sales/Order.mix";
                checklist.AddNewPath = "/Modules/Sales/Entry/Order.mix";

                this.Placeholder1.Controls.Add(checklist);
            }
        }
    }
}