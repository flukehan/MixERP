/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MixERP.Net.FrontEnd.Purchase.Confirmation
{
    public partial class DirectPurchaseInvoice : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@transaction_master_id", this.Request["TranId"]));

            DirectPurchaseInvoiceReport.AddParameterToCollection(list);
            DirectPurchaseInvoiceReport.AddParameterToCollection(list);
        }
    }
}