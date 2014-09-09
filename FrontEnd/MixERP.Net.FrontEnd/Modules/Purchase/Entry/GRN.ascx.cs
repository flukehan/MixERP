using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Purchase.Resources;
using System;

namespace MixERP.Net.Core.Modules.Purchase.Entry
{
    public partial class GRN : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            GoodReceiptNote.Text = Titles.GoodsReceiptNote;

            base.OnControlLoad(sender, e);
        }
    }
}