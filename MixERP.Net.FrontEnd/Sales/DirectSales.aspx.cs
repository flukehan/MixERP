/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using MixERP.Net.BusinessLayer;
using Resources;

namespace MixERP.Net.FrontEnd.Sales
{
    public partial class DirectSales : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Sales_SaveButtonClick(object sender, EventArgs e)
        {
            bool isCredit = this.DirectSalesControl.GetForm.TransactionType.Equals(Titles.Credit);
            long transactionMasterId = BusinessLayer.Transactions.DirectSales.Add(this.DirectSalesControl.GetForm.Date, this.DirectSalesControl.GetForm.StoreId, isCredit, this.DirectSalesControl.GetForm.PartyCode, this.DirectSalesControl.GetForm.AgentId, this.DirectSalesControl.GetForm.PriceTypeId, this.DirectSalesControl.GetForm.Details, this.DirectSalesControl.GetForm.ShippingCompanyId, this.DirectSalesControl.GetForm.ShippingAddressCode, this.DirectSalesControl.GetForm.ShippingCharge, this.DirectSalesControl.GetForm.CashRepositoryId, this.DirectSalesControl.GetForm.CostCenterId, this.DirectSalesControl.GetForm.ReferenceNumber, this.DirectSalesControl.GetForm.StatementReference);
            if(transactionMasterId > 0)
            {
                this.Response.Redirect("~/Sales/Confirmation/DirectSales.aspx?TranId=" + transactionMasterId, true);
            }
        }

    }
}