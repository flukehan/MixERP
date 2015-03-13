using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Inventory
{
    public class InventoryAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Inventory"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.MapRoute(
                "Inventory_Default",
                "Inventory/{controller}/{action}/{id}",
                new { controller = "Inventory", action = "Index", id = UrlParameter.Optional },
                new string[] { "MixERP.Net.Web.UI.Inventory.Controllers" });
        }
    }
}