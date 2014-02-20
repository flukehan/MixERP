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
using Resources;

namespace MixERP.Net.FrontEnd.Sales
{
    public partial class DeliveryWithoutOrder : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SalesDeliveryControl_SaveButtonClick(object sender, EventArgs e)
        {
            Collection<int> tranIdCollection = this.SalesDeliveryControl.GetTranIdCollection();

            long transactionMasterId = SalesDelivery.Add(this.SalesDeliveryControl.GetForm.Date, this.SalesDeliveryControl.GetForm.StoreId, this.SalesDeliveryControl.GetForm.PartyCode, this.SalesDeliveryControl.GetForm.PriceTypeId, this.SalesDeliveryControl.GetForm.Details, this.SalesDeliveryControl.GetForm.ShippingCompanyId, this.SalesDeliveryControl.GetForm.ShippingCharge, this.SalesDeliveryControl.GetForm.CostCenterId, this.SalesDeliveryControl.GetForm.ReferenceNumber, this.SalesDeliveryControl.GetForm.AgentId, this.SalesDeliveryControl.GetForm.StatementReference, tranIdCollection);

            if (transactionMasterId > 0)
            {
                this.Response.Redirect("~/Sales/Confirmation/DeliveryWithoutOrder.aspx?TranId=" + transactionMasterId, true);
            }
            else
            {
                this.SalesDeliveryControl.ErrorMessage = Labels.UnknownError;
            }

        }
    }
}