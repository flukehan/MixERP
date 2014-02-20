/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Web.UI;
using Menu = MixERP.Net.Common.Models.Core.Menu;

namespace MixERP.Net.FrontEnd
{
    public partial class MixERPMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadMenu();
        }

        private void LoadMenu()
        {
            string menu = string.Empty;

            Collection<Menu> collection = BusinessLayer.Core.Menu.GetMenuCollection(0, 0);
            
            if (collection == null)
            {
                return;
            }

            if(collection.Count > 0)
            {
                foreach(Menu model in collection)
                {
                    string menuText = model.MenuText;
                    string url = model.Url;
                    menu += string.Format(Thread.CurrentThread.CurrentCulture, "<a href='{0}' title='{1}'>{1}</a>", this.ResolveUrl(url), menuText);
                }
            }

            this.MenuLiteral.Text = menu;
        }
    }
}