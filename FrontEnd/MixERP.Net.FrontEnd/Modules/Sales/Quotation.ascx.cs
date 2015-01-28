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
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.UserControls.Products;

namespace MixERP.Net.Core.Modules.Sales
{
    public partial class Quotation : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ProductViewControl productView = (ProductViewControl)this.Page.LoadControl("~/UserControls/Products/ProductViewControl.ascx"))
            {
                productView.Text = Titles.SalesQuotation;
                productView.Book = TranBook.Sales;
                productView.SubBook = SubTranBook.Quotation;
                productView.AddNewUrl = "~/Modules/Sales/Entry/Quotation.mix";
                productView.PreviewUrl = "~/Modules/Sales/Reports/SalesQuotationReport.mix";
                productView.ChecklistUrl = "~/Modules/Sales/Confirmation/Quotation.mix";
                productView.ShowMergeToOrderButton = true;
                productView.ShowMergeToDeliveryButton = true;
                productView.Initialize();

                this.Placeholder1.Controls.Add(productView);
            }

            base.OnControlLoad(sender, e);
        }
    }
}