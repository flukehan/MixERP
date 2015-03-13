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
            context.MapRoute(
                "BackOffice_Default",
                "BackOffice/{controller}/{action}/{id}",
                new { controller = "BackOffice", action = "Index", id = UrlParameter.Optional },
                new string[] { "MixERP.Net.Web.UI.BackOffice.Controllers" });
        }
    }
}