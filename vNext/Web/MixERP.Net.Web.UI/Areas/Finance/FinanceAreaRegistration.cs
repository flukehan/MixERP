using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Finance
{
    public class CRMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Finance"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Finance_Default",
                "finance/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] { "MixERP.Net.Web.UI.Finanace.Controllers" });
        }
    }
}