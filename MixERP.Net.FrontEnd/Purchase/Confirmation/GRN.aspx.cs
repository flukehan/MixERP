using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd.Purchase.Confirmation
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