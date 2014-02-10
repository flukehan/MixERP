using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Sales.Confirmation
{
    public partial class ReportDirectSalesInvoice : MixERP.Net.BusinessLayer.MixERPWebReportPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@transaction_master_id", this.Request["TranId"]));
            DirectSalesInvoiceReport.AddParameterToCollection(list);
            DirectSalesInvoiceReport.AddParameterToCollection(list);
        }
    }
}