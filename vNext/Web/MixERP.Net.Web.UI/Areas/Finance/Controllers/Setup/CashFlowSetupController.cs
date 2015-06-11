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
using System.Web.Mvc;
using MixERP.Net.Common.Helpers;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Finance.Resources;
using MixERP.Net.Web.UI.Providers;

namespace MixERP.Net.Web.UI.Finance.Controllers.Setup
{
    [RouteArea("Finance")]
    [RoutePrefix("setup/cash-flow-setup")]
    [Route("{action=index}")]
    public class CashFlowSetupController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Finance/Views/Setup/CashFlowSetup.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                config.KeyColumn = "cash_flow_setup_id";
                config.TableSchema = "core";
                config.Table = "cash_flow_setup";
                config.ViewSchema = "core";
                config.View = "cash_flow_setup_scrud_view";
                config.Text = Titles.CashFlowSetup;

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();

                config.ResourceAssembly =(typeof(CashFlowSetupController)).Assembly;
                return config;

            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.cash_flow_headings.cash_flow_heading_id", ConfigurationHelper.GetDbParameter("CashFlowHeadingDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.account_masters.account_master_id", ConfigurationHelper.GetDbParameter("AccountMasterDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.cash_flow_headings.cash_flow_heading_id", "core.cash_flow_heading_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.account_masters.account_master_id", "core.account_master_selector_view");
            return string.Join(",", displayViews);
        }
    }
}