/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
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
                    menu += string.Format(Thread.CurrentThread.CurrentCulture, "<li><a href='{0}' title='{1}'>{1}</a></li>", this.ResolveUrl(url), menuText);
                }
            }

            this.MenuLiteral.Text = menu;
        }
    }
}