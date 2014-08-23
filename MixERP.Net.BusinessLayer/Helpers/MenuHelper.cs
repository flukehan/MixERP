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

                        menu += string.Format(Thread.CurrentThread.CurrentCulture, "<div class='panel panel-default'><div class='panel-heading'><h3 class='panel-title'>{0}</h3></div>", rootMenu.MenuText);

                        Collection<Menu> childMenus = Core.Menu.GetMenuCollection(rootMenu.MenuId, 2);

                        if(childMenus.Count > 0)
                        {
                            foreach (Menu childMenu in childMenus)
                            {
                                menu += string.Format(Thread.CurrentThread.CurrentCulture, "<a href='{0}' title='{1}' data-menucode='{2}' class='list-group-item'>{1}</a>", page.ResolveUrl(childMenu.Url), childMenu.MenuText, childMenu.MenuCode);
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
                string menu = "<div class='row'>";

                Collection<Menu> menuCollection = Core.Menu.GetMenuCollection(page.Request.Url.AbsolutePath, 1);

                if(menuCollection.Count > 0)
                {
                    foreach(Menu model in menuCollection)
                    {
                        menu += string.Format(Thread.CurrentThread.CurrentCulture, "<div class='col-md-2'><div class='panel panel-default'><div class='panel-heading'><h3 class='panel-title'>{0}</h3></div>", model.MenuText);

                        Collection<Menu> childMenus = Core.Menu.GetMenuCollection(model.MenuId, 2);

                        if(childMenus.Count > 0)
                        {
                            foreach(Menu childMenu in childMenus)
                            {
                                menu += string.Format(Thread.CurrentThread.CurrentCulture, "<a href='{0}' title='{1}' class='list-group-item'>{1}</a>", page.ResolveUrl(childMenu.Url), childMenu.MenuText);
                            }
                        }

                        menu += "</div></div>";
                    }
                }

                menu += "</div>";
                return menu;
            }

            return null;
        }
    }
}
