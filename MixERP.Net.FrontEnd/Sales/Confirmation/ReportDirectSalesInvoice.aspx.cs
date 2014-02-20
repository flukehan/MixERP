using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Sales.Confirmation
{
    public partial class ReportDirectSalesInvoice : MixERPWebReportPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@transaction_master_id", this.Request["TranId"]));
            this.DirectSalesInvoiceReport.AddParameterToCollection(list);
            this.DirectSalesInvoiceReport.AddParameterToCollection(list);
        }
    }
}