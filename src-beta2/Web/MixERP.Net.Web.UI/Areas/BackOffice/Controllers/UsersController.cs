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

namespace MixERP.Net.Web.UI.BackOffice.Controllers
{
    [RouteArea("BackOffice", AreaPrefix = "back-office")]
    [RoutePrefix("users")]
    [Route("{action=index}")]

    public class UsersController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/BackOffice/Views/Users.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                config.Text = Titles.Users;
                config.TableSchema = "office";
                config.Table = "users";
                config.ViewSchema = "office";
                config.View = "user_view";
                config.KeyColumn = "user_id";

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();
                config.ExcludeEdit = "password, user_name";

                config.ResourceAssembly = Assembly.GetAssembly(typeof(UsersController));

                bool denyToNonAdmins = !CacheProvider.GetSignInView().IsAdmin.ToBool();

                config.DenyAdd = denyToNonAdmins;
                config.DenyEdit = denyToNonAdmins;
                config.DenyDelete = denyToNonAdmins;

                return config;
            }
        }
        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id", ConfigurationHelper.GetDbParameter("OfficeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.roles.role_id", ConfigurationHelper.GetDbParameter("RoleDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.departments.department_id", ConfigurationHelper.GetDbParameter("DepartmentDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.offices.office_id", "office.office_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.roles.role_id", "office.role_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.departments.deparment_id", "office.department_scrud_view");
            return string.Join(",", displayViews);
        }

    }
}