using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Purchase.Resources;
using System;

namespace MixERP.Net.Core.Modules.Purchase
{
    public partial class Order : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            ProductView1.Text = Titles.PurchaseOrder;
            base.OnControlLoad(sender, e);
        }
    }
}