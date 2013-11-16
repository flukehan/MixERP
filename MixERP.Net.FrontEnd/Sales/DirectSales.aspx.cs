/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Sales
{
    public partial class DirectSales : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Sales_SaveButtonClick(object sender, EventArgs e)
        {
            bool isCredit = DirectSalesControl.GetForm.TransactionType.Equals(Resources.Titles.Credit); ;
            long transactionMasterId = MixERP.Net.BusinessLayer.Transactions.DirectSales.Add(DirectSalesControl.GetForm.Date, DirectSalesControl.GetForm.StoreId, isCredit, DirectSalesControl.GetForm.PartyCode, DirectSalesControl.GetForm.AgentId, DirectSalesControl.GetForm.PriceTypeId, DirectSalesControl.GetForm.Details, DirectSalesControl.GetForm.ShippingCompanyId, DirectSalesControl.GetForm.ShippingAddressCode, DirectSalesControl.GetForm.ShippingCharge, DirectSalesControl.GetForm.CashRepositoryId, DirectSalesControl.GetForm.CostCenterId, DirectSalesControl.GetForm.ReferenceNumber, DirectSalesControl.GetForm.StatementReference);
            if(transactionMasterId > 0)
            {
                Response.Redirect("~/Sales/Confirmation/DirectSales.aspx?TranId=" + transactionMasterId, true);
            }
        }

    }
}