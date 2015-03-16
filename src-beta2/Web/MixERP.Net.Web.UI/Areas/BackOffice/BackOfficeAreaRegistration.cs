using System.Web.Mvc;

namespace MixERP.Net.Web.UI.BackOffice
{
    public class BackOfficeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "BackOffice"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;

            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "BackOffice_Default",
                "back-office/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[]
                {
                    "MixERP.Net.Web.UI.BackOffice.Controllers"
                });
        }
    }
}