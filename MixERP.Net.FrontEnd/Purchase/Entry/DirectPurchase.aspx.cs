using System;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Purchase.Entry
{
    public partial class DirectPurchase : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "~/Purchase/DirectPurchase.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}