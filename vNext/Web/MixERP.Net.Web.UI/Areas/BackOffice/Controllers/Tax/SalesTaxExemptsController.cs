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
using MixERP.Net.Common.Helpers;
using System.Collections.Generic;
using System.Reflection;

namespace MixERP.Net.Web.UI.BackOffice.Controllers.Tax
{
    [RouteArea("BackOffice", AreaPrefix = "back-office")]
    [RoutePrefix("tax/sales-tax-exempts")]
    [Route("{action=index}")]

    public class SalesTaxExemptsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/BackOffice/Views/Tax/SalesTaxExempts.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                config.KeyColumn = "sales_tax_exempt_id";
                config.TableSchema = "core";
                config.Table = "sales_tax_exempts";
                config.ViewSchema = "core";
                config.View = "sales_tax_exempt_scrud_view";
                config.Text = Titles.SalesTaxExempts;

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();

                config.ResourceAssembly = Assembly.GetAssembly(typeof(SalesTaxExemptsController));

                this.AddScrudCustomValidatorErrorMessages();

                return config;
            }
        }
        private void AddScrudCustomValidatorErrorMessages()
        {
            ViewData["DateErrorMessage"] = Warnings.DateErrorMessage;
            ViewData["ComparePriceErrorMessage"] = Warnings.ComparePriceErrorMessage;

        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.tax_master.tax_master_id", ConfigurationHelper.GetDbParameter("TaxMasterDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_exempt_types.tax_exempt_type_id", ConfigurationHelper.GetDbParameter("TaxExemptTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_taxes.sales_tax_id", ConfigurationHelper.GetDbParameter("SalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.stores.store_id", ConfigurationHelper.GetDbParameter("StoreDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.tax_master.tax_master_id", "core.tax_master_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_exempt_types.tax_exempt_type_id", "core.tax_exempt_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.sales_taxes.sales_tax_id", "core.sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.stores.store_id", "office.store_scrud_view");

            return string.Join(",", displayViews);
        }

    }
}