/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.BackOffice.Resources;
using MixERP.Net.Web.UI.Providers;

namespace MixERP.Net.Web.UI.BackOffice.Controllers.Policy
{
    [RouteArea("BackOffice", AreaPrefix = "back-office")]
    [RoutePrefix("policy/auto-verification")]
    [Route("{action=index}")]

    public class AutoVerificationController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/BackOffice/Views/Policy/AutoVerification.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                bool denyToNonAdmins = !CacheProvider.GetSignInView().IsAdmin.ToBool();

                config.DenyAdd = denyToNonAdmins;
                config.DenyEdit = denyToNonAdmins;
                config.DenyDelete = denyToNonAdmins;

                config.KeyColumn = "user_id";

                config.TableSchema = "policy";
                config.Table = "auto_verification_policy";
                config.ViewSchema = "policy";
                config.View = "auto_verification_policy_scrud_view";

                config.PageSize = 100;

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();

                config.Text = Titles.AutoVerificationPolicy;
                config.ResourceAssembly = Assembly.GetAssembly(typeof(AutoVerificationController));

                return config;
            }
        }
        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.users.user_id",
                ConfigurationHelper.GetDbParameter("UserDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.users.user_id", "office.user_selector_view");
            return string.Join(",", displayViews);
        }

    }
}