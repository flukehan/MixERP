using System;
using System.Linq;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Entities;

namespace MixERP.Net.Core.Api
{
    internal sealed class ApiAccessPolicy
    {
        public bool IsAuthorized;

        internal ApiAccessPolicy(Type type, string action)
        {
            this.Type = type;
            this.Action = action;

            this.LoginId = Conversion.TryCastLong(HttpContext.Current.User.Identity.Name);
        }

        private Type Type { get; set; }
        private long LoginId { get; set; }
        private string Action { get; set; }

        internal void Authorize()
        {
            bool isAuthenticated = HttpContext.Current.Request.IsAuthenticated;

            this.IsAuthorized = false;

            if (!isAuthenticated)
            {
                return;
            }

            if (this.Type == null)
            {
                return;
            }

            if (this.Action == null)
            {
                return;
            }

            if (this.LoginId <= 0)
            {
                return;
            }

            Entities.Policy.ApiAccessPolicy policy =
                Factory.Get<Entities.Policy.ApiAccessPolicy>(
                    "SELECT * FROM policy.api_access_policy " +
                    "WHERE user_id=audit.get_user_id_by_login_id(@0) " +
                    "AND office_id=audit.get_office_id_by_login_id(@0) " +
                    "AND poco_type_name=@1 " +
                    "AND http_action_code=@2 " +
                    "AND valid_till > NOW()",
                    this.LoginId,
                    this.Type.FullName,
                    this.Action).FirstOrDefault();


            if (policy != null)
            {
                this.IsAuthorized = true;
            }
        }
    }
}