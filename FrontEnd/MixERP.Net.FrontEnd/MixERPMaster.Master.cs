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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
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
            BranchNameLiteral.Text = SessionHelper.GetOfficeName();
            SignOutLiteral.Text = Resources.Titles.SignOut;
            UserGreetingLiteral.Text = String.Format(Resources.Labels.UserGreeting, SessionHelper.GetUserName());
            ChangePasswordLiteral.Text = Resources.Titles.ChangePassword;
            ManageProfileLiteral.Text = Resources.Titles.ManageProfile;
            MixERPDocumentationLiteral.Text = Resources.Titles.MixERPDocumentation;
            NotificationLiteral.Text = Resources.Titles.Notifications;
            this.LoadMenu();
        }

        private void LoadMenu()
        {
            //Do not load the root menu items if the ContentMenu was already set on the base page.
            string contentMenu = ContentMenuLiteral.Text;

            if (!string.IsNullOrWhiteSpace(contentMenu))
            {
                return;
            }

            string menu = "<ul class=\"menu\">";

            Collection<Menu> collection = Data.Core.Menu.GetMenuCollection(0, 0);

            if (collection == null)
            {
                return;
            }

            if (collection.Count > 0)
            {
                foreach (Menu model in collection)
                {
                    string menuText = model.MenuText;
                    string menuCode = model.MenuCode;
                    string url = model.Url;
                    menu += string.Format(Thread.CurrentThread.CurrentCulture, "<li><a href='{0}' title='{1}' data-menucode='{2}'>{1}</a></li>", this.ResolveUrl(url), menuText, menuCode);
                }
            }

            menu += "</ul>";

            this.MenuLiteral.Text = menu;
        }
    }
}