using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Production
{
    public class CRMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Production"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Production_Default",
                "production/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] { "MixERP.Net.Web.UI.Production.Controllers" });
        }
    }
}