using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Sales.Resources;
using System;

namespace MixERP.Net.Core.Modules.Sales.Entry
{
    public partial class Delivery : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            ProductView1.Text = Titles.SalesDelivery;
            base.OnControlLoad(sender, e);
        }
    }
}