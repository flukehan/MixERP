/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Sales.Confirmation
{
    public partial class ReportSalesQuotation : MixERPWebReportPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@non_gl_stock_master_id", this.Request["TranId"]));

            this.SalesQuotationReport.AddParameterToCollection(list);
            this.SalesQuotationReport.AddParameterToCollection(list);
        }
    }
}