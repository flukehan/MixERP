using MixERP.Net.Common;
using MixERP.Net.FrontEnd.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Sales.Entry
{
    public partial class Return : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            long tranId = Conversion.TryCastLong(this.Request.QueryString["TranId"]);

            if (tranId <= 0)
            {
                Response.Redirect("~/Modules/Sales/Return.mix");
            }

            base.OnControlLoad(sender, e);
        }
    }
}