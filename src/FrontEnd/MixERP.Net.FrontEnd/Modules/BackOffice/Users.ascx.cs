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
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework.Controls;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.BackOffice
{
    public partial class Users : MixERPUserControl
    {
        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.LocalhostAdmin; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.Text = Titles.Users;
                scrud.TableSchema = "office";
                scrud.Table = "users";
                scrud.ViewSchema = "office";
                scrud.View = "user_view";
                scrud.KeyColumn = "user_id";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.ExcludeEdit = "password, user_name";

                bool denyToNonAdmins = !AppUsers.GetCurrent().View.IsAdmin.ToBool();

                scrud.DenyAdd = denyToNonAdmins;
                scrud.DenyEdit = denyToNonAdmins;
                scrud.DenyDelete = denyToNonAdmins;

                this.Placeholder1.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "OfficeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.roles.role_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "RoleDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.departments.department_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "DepartmentDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.stores.store_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "StoreDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.offices.office_id", "office.office_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.roles.role_id", "office.role_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.departments.deparment_id", "office.department_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.stores.store_id", "office.store_scrud_view");
            return string.Join(",", displayViews);
        }
    }
}