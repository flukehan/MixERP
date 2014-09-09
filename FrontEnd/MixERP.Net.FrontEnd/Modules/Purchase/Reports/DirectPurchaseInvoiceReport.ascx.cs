using MixERP.Net.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Purchase.Reports
{
    public partial class DirectPurchaseInvoiceReport : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@transaction_master_id", this.Page.Request["TranId"]));

            this.Report1.AddParameterToCollection(list);
            this.Report1.AddParameterToCollection(list);

            base.OnControlLoad(sender, e);
        }
    }
}