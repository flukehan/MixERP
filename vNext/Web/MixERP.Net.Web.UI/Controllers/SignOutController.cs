using System.Web.Mvc;
using System.Web.Security;

namespace MixERP.Net.Web.UI.Controllers
{
    [RoutePrefix("sign-out")]
    [Route("{action=index}")]
    public class SignOutController : Controller
    {
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/sign-in/");
        }
    }
}