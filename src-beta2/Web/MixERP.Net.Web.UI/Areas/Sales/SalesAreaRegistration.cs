using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Sales
{
    public class CRMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Sales"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.MapRoute(
                "Sales_Default",
                "Sales/{controller}/{action}/{id}",
                new {controller = "Sales", action = "Index", id = UrlParameter.Optional},
                new string[] { "MixERP.Net.Web.UI.Sales.Controllers" });
        }
    }
}