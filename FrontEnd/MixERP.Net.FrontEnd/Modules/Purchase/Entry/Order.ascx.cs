using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Purchase.Resources;
using System;

namespace MixERP.Net.Core.Modules.Purchase.Entry
{
    public partial class Order : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            PurchaseOrder.Text = Titles.PurchaseOrder;
            base.OnControlLoad(sender, e);
        }
    }
}