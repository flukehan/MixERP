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
    public partial class Order : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (StockTransactionView view = new StockTransactionView())
            {
                view.Text = Titles.PurchaseOrder;
                view.Book = TranBook.Purchase;
                view.SubBook = SubTranBook.Order;
                view.AddNewUrl = "~/Modules/Purchase/Entry/Order.mix";
                view.PreviewUrl = "~/Modules/Purchase/Reports/PurchaseOrderReport.mix";
                view.ChecklistUrl = "~/Modules/Purchase/Confirmation/Order.mix";

                view.IsNonGlTransaction = true;

                view.DbTableName = "transactions.non_gl_stock_master";
                view.PrimaryKey = "non_gl_stock_master_id";


                this.Placeholder1.Controls.Add(view);
            }

            base.OnControlLoad(sender, e);
        }
    }
}