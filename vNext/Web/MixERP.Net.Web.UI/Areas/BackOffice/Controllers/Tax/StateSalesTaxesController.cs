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
using MixERP.Net.Web.UI.BackOffice.Resources;
using MixERP.Net.Web.UI.Providers;
using System.Reflection;
using System.Collections.Generic;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Web.UI.BackOffice.Controllers.Tax
{
    [RouteArea("BackOffice", AreaPrefix = "back-office")]
    [RoutePrefix("tax/state-sales-taxes")]
    [Route("{action=index}")]

    public class StateSalesTaxesController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/BackOffice/Views/Tax/StateSalesTaxes.cshtml";
            return View(view, this.GetConfig());
        }
        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                config.KeyColumn = "state_sales_tax_id";
                config.TableSchema = "core";
                config.Table = "state_sales_taxes";
                config.ViewSchema = "core";
                config.View = "state_sales_tax_scrud_view";
                config.Text = Titles.StateSalesTaxes;

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();

                config.ResourceAssembly = typeof(StateSalesTaxesController).Assembly;
                return config;
            }
        }
        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.states.state_id", ConfigurationHelper.GetDbParameter("StateDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.entities.entity_id", ConfigurationHelper.GetDbParameter("EntityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.industries.industry_id", ConfigurationHelper.GetDbParameter("IndustryDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.item_groups.item_group_id", ConfigurationHelper.GetDbParameter("ItemGroupDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.states.state_id", "core.state_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.entities.entity_id", "core.entity_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.industries.industry_id", "core.industry_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.item_groups.item_group_id", "core.item_group_scrud_view");
            return string.Join(",", displayViews);
        }


    }
}