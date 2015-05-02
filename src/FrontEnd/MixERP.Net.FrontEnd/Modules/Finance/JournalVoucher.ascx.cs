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
using MixERP.Net.Entities;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.TransactionViewFactory;

namespace MixERP.Net.Core.Modules.Finance
{
    public partial class JournalVoucher : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (TransactionView view = new TransactionView())
            {
                view.DisplayAddButton = true;
                view.DisplayFlagButton = true;
                view.DisplayPrintButton = true;
                view.AddNewPath = "Entry/JournalVoucher.mix";
                view.GridViewCssClass = "ui table nowrap";
                view.Text = Titles.JournalVoucher;

                //Default Values
                view.DateFromFromFrequencyType = FrequencyType.FiscalYearStartDate;
                view.DateToFrequencyType = FrequencyType.FiscalYearEndDate;

                view.Book = "Journal";
                view.PostedBy = CurrentUser.GetSignInView().UserName;
                view.OfficeName = CurrentUser.GetSignInView().OfficeName;
                view.Status = "Approved";

                view.UserId = CurrentUser.GetSignInView().UserId.ToInt();
                view.OfficeId = CurrentUser.GetSignInView().OfficeId.ToInt();

                this.Controls.Add(view);
            }
        }
    }
}