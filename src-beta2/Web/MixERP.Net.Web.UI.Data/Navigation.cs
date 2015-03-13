using System.Collections.Generic;
using System.Linq;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;

namespace MixERP.Net.Web.UI.Data
{
    public class Navigation
    {
        private Menu[] _menus;

        public Navigation(int userId, int officeId, string culture)
        {
            this.UserId = userId;
            this.OfficeId = officeId;
            this.Culture = culture;
        }

        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public string Culture { get; set; }

        public IEnumerable<NavigationMenu> GetMenus()
        {
            _menus = GetMenuCollection(this.OfficeId, this.UserId, this.Culture).ToArray();

            List<NavigationMenu> navs = _menus.Where(x => x.ParentMenuId == null).Select(menu => new NavigationMenu
            {
                Menu = menu
            }).ToList();


            if (navs.Count <= 0) return navs;

            foreach (NavigationMenu nav in navs)
            {
                nav.Children = GetChildren(nav);
            }

            return navs;
        }

        private IEnumerable<NavigationMenu> GetChildren(NavigationMenu nav)
        {
            List<NavigationMenu> children = new List<NavigationMenu>();
            int menuId = nav.Menu.MenuId;

            if (menuId.Equals(0))
            {
                return children;
            }

            children.AddRange(_menus.Where(m => m.ParentMenuId.Equals(menuId)).ToList().Select(menu => new NavigationMenu
            {
                Menu = menu
            }));

            if (children.Count <= 0) return children;

            foreach (NavigationMenu t in children)
            {
                t.Children = GetChildren(t);
            }
            return children;
        }

        private static IEnumerable<Menu> GetMenuCollection(int officeId, int userId, string culture)
        {
            return Factory.Get<Menu>("SELECT * FROM policy.get_menu(@0, @1, @2) ORDER BY menu_id;", userId, officeId,
                culture);
        }
    }

    public class NavigationMenu
    {
        public Menu Menu { get; set; }
        public IEnumerable<NavigationMenu> Children { get; set; }
    }
}