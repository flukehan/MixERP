using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Finanace
{
    public class CRMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Finanace"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.MapRoute(
                "Finance_Default",
                "Finance/{controller}/{action}/{id}",
                new {controller = "Finance", action = "Index", id = UrlParameter.Optional},
                new string[] { "MixERP.Net.Web.UI.Finanace.Controllers" });
        }
    }
}