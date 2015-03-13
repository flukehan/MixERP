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
            context.MapRoute(
                "CRM_Default",
                "CRM/{controller}/{action}/{id}",
                new {controller = "CRM", action = "Index", id = UrlParameter.Optional},
                new string[] { "MixERP.Net.Web.UI.CRM.Controllers" });
        }
    }
}