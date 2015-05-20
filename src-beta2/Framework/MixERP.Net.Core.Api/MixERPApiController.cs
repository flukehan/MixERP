using System.Web;
using System.Web.Http;
using MixERP.Net.Common;

namespace MixERP.Net.Core.Api
{
    public class MixERPApiController : ApiController
    {
        protected long Page()
        {
            long page = Conversion.TryCastLong(HttpContext.Current.Request.RequestContext.RouteData.Values["page"]);
            if (page.Equals(0))
            {
                page = 1;
            }

            return page;
        }
    }
}