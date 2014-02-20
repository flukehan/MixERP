/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Collections.ObjectModel;
using System.Threading;
using System.Web.UI;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Core;

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class MenuHelper
    {
        public static string GetContentPageMenu(Control page, string path)
        {
            if(page != null)
            {
                string menu = string.Empty;
                string relativePath = Conversion.GetRelativePath(path);
                Collection<Menu> rootMenus = Core.Menu.GetRootMenuCollection(relativePath);

                if (rootMenus == null)
                {
                    return string.Empty;
                }

                if(rootMenus.Count > 0)
                {
                    foreach(Menu rootMenu in rootMenus)
                    {

                        menu += string.Format(Thread.CurrentThread.CurrentCulture, "<div class='sub-menu'><div class='menu-title'>{0}</div>", rootMenu.MenuText);

                        Collection<Menu> childMenus = Core.Menu.GetMenuCollection(rootMenu.MenuId, 2);

                        if(childMenus.Count > 0)
                        {
                            foreach (Menu childMenu in childMenus)
                            {
                                menu += string.Format(Thread.CurrentThread.CurrentCulture, "<a href='{0}' title='{1}' data-menucode='{2}' class='sub-menu-anchor'>{1}</a>", page.ResolveUrl(childMenu.Url), childMenu.MenuText, childMenu.MenuCode);
                            }
                        }

                        menu += "</div>";
                    }
                }

                return menu;
            }

            return null;
        }

        public static string GetPageMenu(Page page)
        {
            if(page != null)
            {
                string menu = string.Empty;

                Collection<Menu> menuCollection = Core.Menu.GetMenuCollection(page.Request.Url.AbsolutePath, 1);

                if(menuCollection.Count > 0)
                {
                    foreach(Menu model in menuCollection)
                    {
                        menu += string.Format(Thread.CurrentThread.CurrentCulture, "<div class='menu-panel'><div class='menu-header'>{0}</div><ul>", model.MenuText);

                        Collection<Menu> childMenus = Core.Menu.GetMenuCollection(model.MenuId, 2);

                        if(childMenus.Count > 0)
                        {
                            foreach(Menu childMenu in childMenus)
                            {
                                menu += string.Format(Thread.CurrentThread.CurrentCulture, "<li><a href='{0}' title='{1}'>{1}</a></li>", page.ResolveUrl(childMenu.Url), childMenu.MenuText);
                            }
                        }

                        menu += "</ul></div>";
                    }
                }

                menu += "<div style='clear:both;'></div>";
                return menu;
            }

            return null;
        }
    }
}
