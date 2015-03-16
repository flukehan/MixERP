using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Purchase
{
    public class CRMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Purchase"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Purchase_Default",
                "purchase/{controller}/{action}/{id}",
                new { controller = "Purchase", action = "Index", id = UrlParameter.Optional },
                new[] { "MixERP.Net.Web.UI.Purchase.Controllers" });
        }
    }
}