

using MixERP.Net.Common.Models.Core;
using MixERP.Net.FrontEnd.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Web.UI;

namespace MixERP.Net.Core.Modules.POS
{
    public partial class Index : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            string menu = GetPageMenu(this.Page);
            this.MenuLiteral.Text = menu;

            base.OnControlLoad(sender, e);
        }
    }
}