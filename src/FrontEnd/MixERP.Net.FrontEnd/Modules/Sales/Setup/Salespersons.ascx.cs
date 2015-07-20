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
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Sales.Setup
{
    public partial class Salespersons : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.LoadControl();
        }

        protected void LoadControl()
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "salesperson_id";
                scrud.TableSchema = "core";
                scrud.Table = "salespersons";
                scrud.ViewSchema = "core";
                scrud.View = "salesperson_scrud_view";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.UseDisplayViewsAsParents = true;

                scrud.SelectedValues = GetSelectedValues();

                scrud.Text = Titles.SalesPersons;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_teams.sales_team_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "SalesTeamDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id",
                "core.salesperson_account_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.sales_teams.sales_team_id", "core.sales_team_scrud_view");
            return string.Join(",", displayViews);
        }

        private static string GetSelectedValues()
        {
            List<string> selectedValues = new List<string>();

            //Todo:
            //The default selected value of agent account
            //should be implemented via GL Mapping.
            ScrudHelper.AddSelectedValue(selectedValues, "core.accounts.account_id", "'20100 (Accounts Payable)'");
            return string.Join(",", selectedValues);
        }
    }
}