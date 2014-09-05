using System;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Purchase.Entry
{
    public partial class Order : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "~/Purchase/Order.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}