using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Setup
{
    public partial class Index : MixERP.Net.BusinessLayer.MixERPWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string menu = MixERP.Net.BusinessLayer.Helpers.MenuHelper.GetPageMenu(this.Page);
            MenuLiteral.Text = menu;
        }
    }
}