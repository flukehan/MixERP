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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.Framework.Controls;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MixERP.Net.Core.Modules.BackOffice.Policy
{
    public partial class ApiAccess : MixERPUserControl
    {
        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.AdminOnly; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                bool denyToNonAdmins = !AppUsers.GetCurrent().View.IsAdmin.ToBool();

                scrud.DenyAdd = denyToNonAdmins;
                scrud.DenyEdit = denyToNonAdmins;
                scrud.DenyDelete = denyToNonAdmins;

                scrud.KeyColumn = "api_access_policy_id";

                scrud.TableSchema = "policy";
                scrud.Table = "api_access_policy";
                scrud.ViewSchema = "policy";
                scrud.View = "api_access_policy";

                scrud.PageSize = 100;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.UseDisplayViewsAsParents = true;

                scrud.Text = "API Access Policy";

                this.ScrudPlaceholder.Controls.Add(scrud);
                this.RegisterJavascript();
            }
        }

        private void RegisterJavascript()
        {
            var script = "var pocos = [" + this.GetPocos() + "];";
            PageUtility.RegisterJavascript("ApiAccess_Vars", script, this.Page, true);
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
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "UserDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "OfficeDisplayField"));
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