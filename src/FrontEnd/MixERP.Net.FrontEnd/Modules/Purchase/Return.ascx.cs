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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.StockTransactionViewFactory;
using System;

namespace MixERP.Net.Core.Modules.Purchase
{
    public partial class Return : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (StockTransactionView view = new StockTransactionView())
            {
                view.Text = Titles.PurchaseReturn;
                view.Book = TranBook.Purchase;
                view.SubBook = SubTranBook.Return;
                view.PreviewUrl = "~/Modules/Purchase/Reports/PurchaseReturnReport.mix";
                view.ChecklistUrl = "~/Modules/Purchase/Confirmation/Return.mix";

                view.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
                view.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
                view.Catalog = AppUsers.GetCurrentUserDB();

                this.Placeholder1.Controls.Add(view);
            }
            
        }
    }
}