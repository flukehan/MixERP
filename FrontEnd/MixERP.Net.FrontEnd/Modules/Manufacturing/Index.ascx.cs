

using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Manufacturing
{
    public partial class Index : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            string menu = this.GetPageMenu(this.Page);
            this.MenuLiteral.Text = menu;

            base.OnControlLoad(sender, e);
        }
    }
}