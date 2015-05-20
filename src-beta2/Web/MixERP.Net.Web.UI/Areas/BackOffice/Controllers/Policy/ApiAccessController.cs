using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Providers;

namespace MixERP.Net.Web.UI.BackOffice.Controllers.Policy
{
    [RouteArea("BackOffice", AreaPrefix = "back-office")]
    [RoutePrefix("policy/api-access")]
    [Route("{action=index}")]
    public class ApiAccessController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/BackOffice/Views/Policy/ApiAccess.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            bool denyToNonAdmins = !CacheProvider.GetSignInView().IsAdmin.ToBool();

            Config config = ScrudProvider.GetScrudConfig();

            config.DenyAdd = denyToNonAdmins;
            config.DenyEdit = denyToNonAdmins;
            config.DenyDelete = denyToNonAdmins;

            config.KeyColumn = "api_access_policy_id";

            config.TableSchema = "policy";
            config.Table = "api_access_policy";
            config.ViewSchema = "policy";
            config.View = "api_access_policy";

            config.PageSize = 100;

            config.DisplayFields = GetDisplayFields();
            config.DisplayViews = GetDisplayViews();

            config.Text = "API Access Policy";
            config.ResourceAssembly = Assembly.GetAssembly(typeof (ApiAccessController));

            ViewData["Pocos"] = this.GetPocos();

            return config;
        }


        private string GetPocos()
        {
            Type type = typeof (IPoco);
            List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)).ToList();


            List<string> items = types.Select(t => "'" + t.FullName + "'").ToList();
            return string.Join(",", items);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.users.user_id",
                ConfigurationHelper.GetDbParameter("UserDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id",
                ConfigurationHelper.GetDbParameter("OfficeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "policy.http_actions.http_action_code", "http_action_code");
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.users.user_id", "office.user_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "office.offices.office_id", "office.office_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "policy.http_actions.http_action_code", "policy.http_actions");
            return string.Join(",", displayViews);
        }
    }
}