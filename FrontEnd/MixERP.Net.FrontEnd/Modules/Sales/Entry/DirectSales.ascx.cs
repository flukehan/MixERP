
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Sales.Entry
{
    public partial class DirectSales : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            DirectSalesControl.Text = Titles.DirectSales;
            base.OnControlLoad(sender, e);
        }
    }
}