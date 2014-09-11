
using MixERP.Net.Core.Modules.Purchase.Resources;
using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Purchase
{
    public partial class GRN : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            ProductView1.Text = Titles.GoodsReceiptNote;
            base.OnControlLoad(sender, e);
        }
    }
}