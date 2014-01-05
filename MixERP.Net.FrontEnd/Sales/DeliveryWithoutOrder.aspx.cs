/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Sales
{
    public partial class DeliveryWithoutOrder : MixERP.Net.BusinessLayer.MixERPWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SalesDeliveryControl_SaveButtonClick(object sender, EventArgs e)
        {
            Collection<int> tranIdCollection = SalesDeliveryControl.GetTranIdCollection();

            long transactionMasterId = MixERP.Net.BusinessLayer.Transactions.SalesDelivery.Add(SalesDeliveryControl.GetForm.Date, SalesDeliveryControl.GetForm.StoreId, SalesDeliveryControl.GetForm.PartyCode, SalesDeliveryControl.GetForm.PriceTypeId, SalesDeliveryControl.GetForm.Details, SalesDeliveryControl.GetForm.ShippingCompanyId, SalesDeliveryControl.GetForm.ShippingCharge, SalesDeliveryControl.GetForm.CostCenterId, SalesDeliveryControl.GetForm.ReferenceNumber, SalesDeliveryControl.GetForm.AgentId, SalesDeliveryControl.GetForm.StatementReference, tranIdCollection);

            if (transactionMasterId > 0)
            {
                Response.Redirect("~/Sales/Confirmation/DeliveryWithoutOrder.aspx?TranId=" + transactionMasterId, true);
            }
            else
            {
                SalesDeliveryControl.ErrorMessage = Resources.Labels.UnknownError;
            }

        }
    }
}