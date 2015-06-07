using System.Collections.Generic;
using System.Web.Mvc;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Office;
using MixERP.Net.Web.UI.Data;
using Newtonsoft.Json;

namespace MixERP.Net.Web.UI.Controllers
{
    public class NavigationController : Controller
    {
        public JsonResult GetMenu()
        {
            SignInView view = Providers.CacheProvider.GetSignInView();

            int userId = view.UserId.ToInt();
            int officeId = view.OfficeId.ToInt();
            string culture = view.Culture;

            Navigation nav = new Navigation(userId, officeId, culture);
            IEnumerable<NavigationMenu> menus = nav.GetMenus();

            return Json(menus, JsonRequestBehavior.AllowGet);
        }
    }
}