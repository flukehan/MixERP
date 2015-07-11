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
using System.Collections.Generic;
using System.Linq;
using MixERP.Net.Entities.Core;
using PetaPoco;

namespace MixERP.Net.FrontEnd.Models
{
    [Serializable]
    public class Navigation
    {
        private Menu[] _menus;

        public Navigation(string catalog, int userId, int officeId, string culture)
        {
            this.Catalog = catalog;
            this.UserId = userId;
            this.OfficeId = officeId;
            this.Culture = culture;
        }

        public string Catalog { get; private set; }
        public int UserId { get; private set; }
        public int OfficeId { get; private set; }
        public string Culture { get; private set; }

        public List<NavigationMenu> GetMenus()
        {
            _menus = GetMenuCollection(this.Catalog, this.OfficeId, this.UserId, this.Culture).ToArray();

            List<NavigationMenu> navs = _menus.Where(x => x.ParentMenuId == null).Select(menu => new NavigationMenu
            {
                Menu = menu
            }).ToList();


            if (navs.Count <= 0)
            {
                return navs;
            }

            foreach (NavigationMenu nav in navs)
            {
                nav.Children = GetChildren(nav);
            }

            return navs;
        }

        private List<NavigationMenu> GetChildren(NavigationMenu nav)
        {
            List<NavigationMenu> children = new List<NavigationMenu>();
            int menuId = nav.Menu.MenuId;

            if (menuId.Equals(0))
            {
                return children;
            }

            children.AddRange(
                _menus.Where(m => m.ParentMenuId.Equals(menuId)).ToList().Select(menu => new NavigationMenu
                {
                    Menu = menu
                }));

            if (children.Count <= 0)
            {
                return children;
            }

            foreach (NavigationMenu t in children)
            {
                t.Children = GetChildren(t);
            }
            return children;
        }

        private static IEnumerable<Menu> GetMenuCollection(string catalog, int officeId, int userId, string culture)
        {
            return Factory.Get<Menu>(catalog, "SELECT * FROM policy.get_menu(@0, @1, @2) ORDER BY menu_id;", userId,
                officeId,
                culture);
        }
    }
}