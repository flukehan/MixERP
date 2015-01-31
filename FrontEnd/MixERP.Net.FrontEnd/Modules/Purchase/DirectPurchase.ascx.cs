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
using MixERP.Net.Core.Modules.Purchase.Resources;
using MixERP.Net.Entities;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.StockTransactionViewFactory;

namespace MixERP.Net.Core.Modules.Purchase
{
    public partial class DirectPurchase : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (StockTransactionView view = new StockTransactionView())
            {
                view.Text = Titles.DirectPurchase;
                view.Book = TranBook.Purchase;
                view.SubBook = SubTranBook.Direct;
                view.AddNewUrl = "~/Modules/Purchase/Entry/DirectPurchase.mix";
                view.PreviewUrl = "~/Modules/Purchase/Reports/DirectPurchaseInvoiceReport.mix";
                view.ChecklistUrl = "~/Modules/Purchase/Confirmation/DirectPurchase.mix";

                view.ShowReturnButton = true;
                view.ReturnButtonUrl = "~/Modules/Purchase/Entry/Return.mix";

                view.DbTableName = "transactions.stock_master";
                view.PrimaryKey = "stock_master_id";

                this.Placeholder1.Controls.Add(view);
            }
            base.OnControlLoad(sender, e);
        }
    }
}