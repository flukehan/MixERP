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
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Inventory.Setup
{
    public partial class ItemsPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "item_id";
                scrud.TableSchema = "core";
                scrud.Table = "items";
                scrud.ViewSchema = "core";
                scrud.View = "items";

                scrud.Width = 3000;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.UseDisplayViewsAsParents = true;

                scrud.Text = Titles.Items;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.item_groups.item_group_id", ConfigurationHelper.GetDbParameter("ItemGroupDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.brands.brand_id", ConfigurationHelper.GetDbParameter("BrandDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.parties.party_id", ConfigurationHelper.GetDbParameter("PartyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.units.unit_id", ConfigurationHelper.GetDbParameter("UnitDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.taxes.tax_id", ConfigurationHelper.GetDbParameter("TaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.shipping_mail_types.shipping_mail_type_id", ConfigurationHelper.GetDbParameter("ShippingMailTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.shipping_package_shapes.shipping_package_shape_id", ConfigurationHelper.GetDbParameter("ShippingPackageShapeDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.item_groups.item_group_id", "core.item_groups");
            ScrudHelper.AddDisplayView(displayViews, "core.brands.brand_id", "core.brands");
            ScrudHelper.AddDisplayView(displayViews, "core.parties.party_id", "core.supplier_view");
            ScrudHelper.AddDisplayView(displayViews, "core.units.unit_id", "core.unit_view");
            ScrudHelper.AddDisplayView(displayViews, "core.taxes.tax_id", "core.tax_view");
            ScrudHelper.AddDisplayView(displayViews, "core.shipping_mail_types.shipping_mail_type_id", "core.shipping_mail_types");
            ScrudHelper.AddDisplayView(displayViews, "core.shipping_package_shapes.shipping_package_shape_id", "core.shipping_package_shapes");
            return string.Join(",", displayViews);
        }

    }
}