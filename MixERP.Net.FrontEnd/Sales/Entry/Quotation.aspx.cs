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
    public partial class Quotation : MixERPWebpage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "~/Sales/Quotation.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}