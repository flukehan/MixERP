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

using MixERP.Net.Common.Helpers;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Inventory.Resources;
using MixERP.Net.Web.UI.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Inventory.Controllers.Setup
{
    [RouteArea("Inventory")]
    [RoutePrefix("setup/stores")]
    [Route("{action=index}")]
    public class StoresController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Inventory/Views/Setup/Stores.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();

            config.KeyColumn = "store_id";

            config.TableSchema = "office";
            config.Table = "stores";
            config.ViewSchema = "office";
            config.View = "store_scrud_view";

            config.DisplayFields = GetDisplayFields();
            config.DisplayViews = GetDisplayViews();

            config.Text = Titles.Stores;
            config.ResourceAssembly = typeof(StoresController).Assembly;

            return config;
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.store_types.store_type_id", ConfigurationHelper.GetDbParameter("StoreTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id", ConfigurationHelper.GetDbParameter("OfficeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.cash_repositories.cash_repository_id", ConfigurationHelper.GetDbParameter("CashRepositoryDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_taxes.sales_tax_id", ConfigurationHelper.GetDbParameter("SalesTaxDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.store_types.store_type_id", "office.store_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.offices.office_id", "office.office_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.cash_repositories.cash_repository_id", "office.cash_repository_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.sales_taxes.sales_tax_id", "core.sales_tax_scrud_view");

            return string.Join(",", displayViews);
        }
    }
}