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

            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Inventory_Default",
                "inventory/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "MixERP.Net.Web.UI.Inventory.Controllers" });
        }
    }
}