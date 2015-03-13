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
            context.MapRoute(
                "Purchase_Default",
                "Purchase/{controller}/{action}/{id}",
                new { controller = "Purchase", action = "Index", id = UrlParameter.Optional },
                new string[] { "MixERP.Net.Web.UI.Purchase.Controllers" });
        }
    }
}