using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;

namespace MixERP.Net.FrontEnd.Setup
{
    public partial class Index : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string menu = MenuHelper.GetPageMenu(this.Page);
            this.MenuLiteral.Text = menu;
        }
    }
}