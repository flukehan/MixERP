using System;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Purchase.Entry
{
    public partial class GRN : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "~/Purchase/GRN.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}