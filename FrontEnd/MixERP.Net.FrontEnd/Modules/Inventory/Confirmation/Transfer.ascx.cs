using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.TransactionChecklist;
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

namespace MixERP.Net.Core.Modules.Inventory.Confirmation
{
    public partial class Transfer : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (TransactionChecklistForm checklist = new TransactionChecklistForm())
            {
                checklist.ViewReportButtonText = Resources.Titles.ViewThisTransfer;
                checklist.Text = Resources.Titles.StockTransferJournal;
                checklist.AttachmentBookName = "transaction";
                checklist.OverridePath = "/Modules/Inventory/Transfer.mix";
                checklist.DisplayWithdrawButton = true;
                checklist.DisplayViewReportButton = true;
                checklist.DisplayAttachmentButton = true;
                checklist.ReportPath = "~/Modules/Inventory/Reports/InventoryTransferReport.mix";
                checklist.ViewPath = "/Modules/Inventory/Transfer.mix";
                checklist.AddNewPath = "/Modules/Inventory/Entry/Transfer.mix";

                Placeholder1.Controls.Add(checklist);
            }

            base.OnControlLoad(sender, e);
        }
    }
}