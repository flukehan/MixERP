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
using MixERP.Net.Common.Extensions;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.TransactionChecklist;

namespace MixERP.Net.Core.Modules.Finance.Confirmation
{
    public partial class JournalVoucher : TransactionCheckListControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (TransactionChecklistForm checklist = new TransactionChecklistForm())
            {
                checklist.Text = Titles.JournalVoucherEntry;
                checklist.DisplayWithdrawButton = true;
                checklist.DisplayPrintGlEntryButton = true;
                checklist.DisplayAttachmentButton = true;
                checklist.AttachmentBookName = "transaction";
                checklist.OverridePath = this.OverridePath;

                checklist.ViewPath = "~/Modules/Finance/JournalVoucher.mix";
                checklist.AddNewPath = "~/Modules/Finance/Entry/JournalVoucher.mix";
                checklist.GlAdvicePath = "~/Modules/Finance/Reports/GLAdviceReport.mix";
                checklist.UserId = CurrentUser.GetSignInView().UserId.ToInt();
                checklist.RestrictedTransactionMode = this.IsRestrictedMode;

                this.Placeholder1.Controls.Add(checklist);
            }
        }
    }
}