using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Sales.Resources;
using System;

namespace MixERP.Net.Core.Modules.Sales.Entry
{
    public partial class Quotation : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            SalesQuotationControl.Text = Titles.SalesQuotation;
            base.OnControlLoad(sender, e);
        }
    }
}