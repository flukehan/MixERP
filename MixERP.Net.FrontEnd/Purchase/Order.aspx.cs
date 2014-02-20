/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Transactions;

namespace MixERP.Net.FrontEnd.Purchase
{
    public partial class Order : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PurchaseOrder_SaveButtonClick(object sender, EventArgs e)
        {
            Collection<int> tranIdCollection = this.PurchaseOrder.GetTranIdCollection();

            long nonGlStockMasterId = NonGlStockTransaction.Add("Purchase.Order", this.PurchaseOrder.GetForm.Date, this.PurchaseOrder.GetForm.PartyCode, this.PurchaseOrder.GetForm.PriceTypeId, this.PurchaseOrder.GetForm.Details, this.PurchaseOrder.GetForm.ReferenceNumber, this.PurchaseOrder.GetForm.StatementReference, tranIdCollection);
            if(nonGlStockMasterId > 0)
            {
                this.Response.Redirect("~/Dashboard/Index.aspx?TranId=" + nonGlStockMasterId, true);
            }
        }
    }
}