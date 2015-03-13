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
            context.MapRoute(
                "Production_Default",
                "Production/{controller}/{action}/{id}",
                new {controller = "Production", action = "Index", id = UrlParameter.Optional},
                new string[] { "MixERP.Net.Web.UI.Production.Controllers" });
        }
    }
}