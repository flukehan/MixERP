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

namespace MixERP.Net.FrontEnd.Sales.Entry
{
    public partial class Order : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "~/Sales/Order.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void SalesOrder_SaveButtonClick(object sender, EventArgs e)
        {
            Collection<int> tranIdCollection = this.SalesOrder.GetTranIdCollection();
            long nonGlStockMasterId = NonGlStockTransaction.Add("Sales.Order", this.SalesOrder.GetForm.Date, this.SalesOrder.GetForm.PartyCode, this.SalesOrder.GetForm.PriceTypeId, this.SalesOrder.GetForm.Details, this.SalesOrder.GetForm.ReferenceNumber, this.SalesOrder.GetForm.StatementReference, tranIdCollection);
            if (nonGlStockMasterId > 0)
            {
                this.Response.Redirect("~/Sales/Order.aspx?TranId=" + nonGlStockMasterId, true);
            }
        }
    }
}