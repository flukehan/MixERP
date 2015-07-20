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

namespace MixERP.Net.Core.Modules.BackOffice.Tax
{
    public partial class SalesTaxDetails : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "sales_tax_detail_id";
                scrud.TableSchema = "core";
                scrud.Table = "sales_tax_details";
                scrud.ViewSchema = "core";
                scrud.View = "sales_tax_detail_scrud_view";
                scrud.Text = Titles.SalesTaxDetails;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.sales_tax_types.sales_tax_type_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "SalesTaxTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_taxes.sales_tax_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "SalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.state_sales_taxes.state_sales_tax_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "StateSalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.county_sales_taxes.county_sales_tax_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "CountySalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_rate_types.tax_rate_type_code",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "TaxRateTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_authorities.tax_authority_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "TaxAuthorityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.rounding_methods.rounding_method_code",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "RoundingMethodCodeDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.sales_tax_types.sales_tax_type_id",
                "core.sales_tax_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.sales_taxes.sales_tax_id", "core.sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.state_sales_taxes.state_sales_tax_id",
                "core.state_sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.county_sales_taxes.county_sales_tax_id",
                "core.county_sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_rate_types.tax_rate_type_code",
                "core.tax_rate_type_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_authorities.tax_authority_id",
                "core.tax_authority_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.rounding_methods.rounding_method_code",
                "core.rounding_method_selector_view");
            return string.Join(",", displayViews);
        }
    }
}