
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Sales
{
    public partial class Quotation : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            ProductView1.Text = Titles.SalesQuotation;
            base.OnControlLoad(sender, e);
        }
    }
}