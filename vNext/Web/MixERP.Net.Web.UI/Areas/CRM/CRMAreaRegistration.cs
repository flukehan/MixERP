using System.Web.Mvc;

namespace MixERP.Net.Web.UI.CRM
{
    public class CRMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "CRM"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "CRM_Default",
                "crm/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] { "MixERP.Net.Web.UI.CRM.Controllers" });
        }
    }
}