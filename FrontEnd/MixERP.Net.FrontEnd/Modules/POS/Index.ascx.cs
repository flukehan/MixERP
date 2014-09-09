using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using System;

namespace MixERP.Net.Core.Modules.POS
{
    public partial class Index : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            string menu = MenuHelper.GetPageMenu(this.Page);
            this.MenuLiteral.Text = menu;

            base.OnControlLoad(sender, e);
        }
    }
}