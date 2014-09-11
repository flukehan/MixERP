
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class GLAdviceReport : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@transaction_master_id", this.Page.Request["TranId"]));

            this.Report1.AddParameterToCollection(list);
            this.Report1.AddParameterToCollection(list);
            this.Report1.RunningTotalText = Titles.RunningTotal;

            base.OnControlLoad(sender, e);
        }
    }
}