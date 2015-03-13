using System.Web.Mvc;
using MixERP.Net.Web.UI.Data;

namespace MixERP.Net.Web.UI.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult GetMenu()
        {
            int userId = 2;
            int officeId = 2;
            string culture = "en";

            Navigation nav = new Navigation(userId, officeId, culture);
            var menus = nav.GetMenus();

            return Json(menus, JsonRequestBehavior.AllowGet);
        }
    }
}