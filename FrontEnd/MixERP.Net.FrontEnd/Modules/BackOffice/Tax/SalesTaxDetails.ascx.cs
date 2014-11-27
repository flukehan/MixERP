using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;

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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MixERP.Net.Core.Modules.BackOffice.Tax
{
    public partial class SalesTaxDetails : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "sales_tax_detail_id";
                scrud.TableSchema = "core";
                scrud.Table = "sales_tax_details";
                scrud.ViewSchema = "core";
                scrud.View = "sales_tax_details";
                scrud.Text = Titles.SalesTaxDetails;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.ResourceAssembly = Assembly.GetAssembly(typeof(SalesTaxDetails));
                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.sales_tax_types.sales_tax_type_id", ConfigurationHelper.GetDbParameter("SalesTaxTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.state_sales_taxes.state_sales_tax_id", ConfigurationHelper.GetDbParameter("StateSalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_base_amount_types.tax_base_amount_type_code", ConfigurationHelper.GetDbParameter("TaxBaseAmountTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_rate_types.tax_rate_type_code", ConfigurationHelper.GetDbParameter("TaxRateTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.tax_authorities.tax_authority_id", ConfigurationHelper.GetDbParameter("TaxAuthorityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.rounding_methods.rounding_method_code", ConfigurationHelper.GetDbParameter("RoundingMethodCodeDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.sales_tax_types.sales_tax_type_id", "core.sales_tax_types");
            ScrudHelper.AddDisplayView(displayViews, "core.state_sales_taxes.state_sales_tax_id", "core.state_sales_taxes");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_base_amount_types.tax_base_amount_type_code", "core.tax_base_amount_types");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_rate_types.tax_rate_type_code", "core.tax_rate_types");
            ScrudHelper.AddDisplayView(displayViews, "core.tax_authorities.tax_authority_id", "core.tax_authorities");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.rounding_methods.rounding_method_code", "core.rounding_methods");
            return string.Join(",", displayViews);
        }
    }
}