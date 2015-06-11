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

using System.Web.Mvc;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Sales.Resources;
using MixERP.Net.Web.UI.Providers;
using System.Collections.Generic;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Web.UI.Sales.Controllers.Setup
{
    [RouteArea("Sales")]
    [RoutePrefix("setup/salespersons")]
    [Route("{action=index}")]
    public class SalespersonsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Sales/Views/Setup/Salespersons.cshtml";

            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig(); 
            {
                config.KeyColumn = "salesperson_id";
                config.TableSchema = "core";
                config.Table = "salespersons";
                config.ViewSchema = "core";
                config.View = "salesperson_scrud_view";

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();
                config.SelectedValues = GetSelectedValues();

                config.Text = Resources.Titles.SalesPersons;
                config.ResourceAssembly = typeof(SalespersonsController).Assembly;

                return config;

            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id",
                ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_teams.sales_team_id",
                ConfigurationHelper.GetDbParameter("SalesTeamDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_scrud_view");
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