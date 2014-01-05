/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Purchase
{
    public partial class Order : MixERP.Net.BusinessLayer.MixERPWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PurchaseOrder_SaveButtonClick(object sender, EventArgs e)
        {
            Collection<int> tranIdCollection = PurchaseOrder.GetTranIdCollection();

            long nonGlStockMasterId = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.Add("Purchase.Order", PurchaseOrder.GetForm.Date, PurchaseOrder.GetForm.PartyCode, PurchaseOrder.GetForm.PriceTypeId, PurchaseOrder.GetForm.Details, PurchaseOrder.GetForm.ReferenceNumber, PurchaseOrder.GetForm.StatementReference, tranIdCollection);
            if(nonGlStockMasterId > 0)
            {
                Response.Redirect("~/Dashboard/Index.aspx?TranId=" + nonGlStockMasterId, true);
            }
        }
    }
}