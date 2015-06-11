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
using MixERP.Net.Common.Helpers;
using System.Collections.Generic;

namespace MixERP.Net.Web.UI.BackOffice.Controllers.Tax
{
    [RouteArea("BackOffice", AreaPrefix = "back-office")]
    [RoutePrefix("tax/sales-tax-details")]
    [Route("{action=index}")]

    public class SalesTaxDetailsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/BackOffice/Views/Tax/SalesTaxDetails.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                config.KeyColumn = "sales_tax_detail_id";
                config.TableSchema = "core";
                config.Table = "sales_tax_details";
                config.ViewSchema = "core";
                config.View = "sales_tax_detail_scrud_view";
                config.Text = Titles.SalesTaxDetails;

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();

                config.ResourceAssembly = Assembly.GetAssembly(typeof (SalesTaxDetailsController));

                return config;
            }
        }
        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.sales_tax_types.sales_tax_type_id", ConfigurationHelper.GetDbParameter("SalesTaxTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_taxes.sales_tax_id", ConfigurationHelper.GetDbParameter("SalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.state_sales_taxes.state_sales_tax_id", ConfigurationHelper.GetDbParameter("StateSalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.county_sales_taxes.county_sales_tax_id", ConfigurationHelper.GetDbParameter("CountySalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_rate_types.tax_rate_type_code", ConfigurationHelper.GetDbParameter("TaxRateTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_authorities.tax_authority_id", ConfigurationHelper.GetDbParameter("TaxAuthorityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.rounding_methods.rounding_method_code", ConfigurationHelper.GetDbParameter("RoundingMethodCodeDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.sales_tax_types.sales_tax_type_id", "core.sales_tax_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.sales_taxes.sales_tax_id", "core.sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.state_sales_taxes.state_sales_tax_id", "core.state_sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.county_sales_taxes.county_sales_tax_id", "core.county_sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_rate_types.tax_rate_type_code", "core.tax_rate_type_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_authorities.tax_authority_id", "core.tax_authority_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.rounding_methods.rounding_method_code", "core.rounding_method_selector_view");
            return string.Join(",", displayViews);
        }

    }
}