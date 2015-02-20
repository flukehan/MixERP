using System;
using System.Web;
using System.Web.Http.Controllers;
using MixERP.Net.Common;

namespace MixERP.Net.Core.Api
{
    internal sealed class ApiAccessPolicy
    {
        private Type Type { get; set; }
        private long LoginId { get; set; }
        private IActionHttpMethodProvider Action { get; set; }
        public bool IsAuthorized;

        internal ApiAccessPolicy(Type type, IActionHttpMethodProvider action)
        {
            this.Type = type;
            this.LoginId = Conversion.TryCastLong(HttpContext.Current.User.Identity.Name);
        }


        internal void Authorize()
        {
            bool isAuthenticated = HttpContext.Current.Request.IsAuthenticated;

            if (isAuthenticated)
            {
                if (this.LoginId > 0)
                {
                    //Todo
                    this.IsAuthorized = true;
                    return;
                }
            }

            this.IsAuthorized = false;
        }

    }
}