using MixERP.Net.Common.Base;
using MixERP.Net.Common.Models.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.Base
{
    public class MixERPUserControl : MixERPUserControlBase
    {
        public string GetPageMenu(Page page)
        {
            if (page != null)
            {
                string menu = "<div class='row'>";

                Collection<Menu> menuCollection = Data.Core.Menu.GetMenuCollection(page.Request.Url.AbsolutePath, 1);

                if (menuCollection.Count > 0)
                {
                    foreach (Menu model in menuCollection)
                    {
                        menu += string.Format(Thread.CurrentThread.CurrentCulture, "<div class='col-md-2'><div class='panel panel-default'><div class='panel-heading'><h3 class='panel-title'>{0}</h3></div>", model.MenuText);

                        Collection<Menu> childMenus = Data.Core.Menu.GetMenuCollection(model.MenuId, 2);

                        if (childMenus.Count > 0)
                        {
                            foreach (Menu childMenu in childMenus)
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