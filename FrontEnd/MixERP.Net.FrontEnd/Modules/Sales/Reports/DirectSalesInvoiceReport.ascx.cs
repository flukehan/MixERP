using MixERP.Net.BusinessLayer;
using MixERP.Net.WebControls.ReportEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Sales.Reports
{
    public partial class DirectSalesInvoiceReport : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@transaction_master_id", this.Page.Request["TranId"]));

            using (Report report = new Report())
            {
                report.AddParameterToCollection(list);
                report.AddParameterToCollection(list);
                report.AutoInitialize = true;
                report.Path = "~/Modules/Sales/Reports/Source/Sales.View.Sales.Invoice.xml";

                this.Controls.Add(report);
            }

            base.OnControlLoad(sender, e);
        }
    }
}