/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;

namespace MixERP.Net.FrontEnd.Finance
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