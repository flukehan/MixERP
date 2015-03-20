/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).
This file is part of MixERP.
MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with MixERP. If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System.Web.Mvc;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Sales.Resources;
using MixERP.Net.Web.UI.Providers;
using System.Reflection;
using System.Collections.Generic;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Web.UI.Sales.Controllers.Setup
{
    [RouteArea("Sales")]
    [RoutePrefix("setup/bonus-slab-assignment")]
    [Route("{action=index}")]
    public class BonusSlabAssignmentController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Sales/Views/Setup/BonusSlabAssignment.cshtml";

            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();

            config.KeyColumn = "salesperson_bonus_setup_id";
            config.TableSchema = "core";
            config.Table = "salesperson_bonus_setups";
            config.ViewSchema = "core";
            config.View = "salesperson_bonus_setup_scrud_view";
            config.DisplayFields = GetDisplayFields();
            config.DisplayViews = GetDisplayViews();
            config.Text = Titles.AgentBonusSlabAssignment;
            config.ResourceAssembly = typeof(BonusSlabAssignmentController).Assembly;


            return config;
        }


        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.bonus_slabs.bonus_slab_id", ConfigurationHelper.GetDbParameter("BonusSlabDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.salespersons.salesperson_id", ConfigurationHelper.GetDbParameter("SalespersonDisplayField"));
            return string.Join(",", displayFields);
        }
        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.bonus_slabs.bonus_slab_id", "core.bonus_slab_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.salespersons.salesperson_id", "core.salesperson_scrud_view");
            return string.Join(",", displayViews);
        }

    }
}