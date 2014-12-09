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

using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.TransactionViewFactory;
using System;

namespace MixERP.Net.Core.Modules.Finance
{
    public partial class VoucherVerification : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (TransactionView view = new TransactionView())
            {
                view.DisplayFlagButton = true;
                view.DisplayApproveButton = true;
                view.DisplayRejectButton = true;
                view.DisplayPrintButton = true;

                view.GridViewCssClass = "ui table nowrap";
                view.Text = Titles.VoucherVerification;

                //Default Values
                view.DateFromFromFrequency = Frequency.Today;
                view.DateToFrequency = Frequency.Today;
                view.Status = "Unverified";

                view.OfficeName = SessionHelper.GetOfficeName();

                this.Controls.Add(view);
            }

            base.OnControlLoad(sender, e);
        }
    }
}