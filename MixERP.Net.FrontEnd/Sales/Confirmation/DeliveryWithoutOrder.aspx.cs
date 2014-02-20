/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Sales.Confirmation
{
    public partial class DeliveryWithoutOrder : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "~/Sales/DeliveryWithoutOrder.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}