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
using System.Collections.Generic;
using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Inventory.Controllers.Setup
{
    [RouteArea("Inventory")]
    [RoutePrefix("setup/items")]
    [Route("{action=index}")]
    public class ItemsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Inventory/Views/Setup/Items.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();

            config.KeyColumn = "item_id";
            config.TableSchema = "core";
            config.Table = "items";
            config.ViewSchema = "core";
            config.View = "item_scrud_view";
            config.DisplayFields = GetDisplayFields();
            config.DisplayViews = GetDisplayViews();
            config.UseDisplayViewsAsParents = true;
            config.ExcludeEdit = "item_code, maintain_stock, item_group_id";

            config.Text = Titles.Items;
            config.ResourceAssembly = typeof(ItemsController).Assembly;

            return config;
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.item_types.item_type_id", ConfigurationHelper.GetDbParameter("ItemTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.item_groups.item_group_id", ConfigurationHelper.GetDbParameter("ItemGroupDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.brands.brand_id", ConfigurationHelper.GetDbParameter("BrandDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.parties.party_id", ConfigurationHelper.GetDbParameter("PartyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.units.unit_id", ConfigurationHelper.GetDbParameter("UnitDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_taxes.sales_tax_id", ConfigurationHelper.GetDbParameter("SalesTaxDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.shipping_mail_types.shipping_mail_type_id", ConfigurationHelper.GetDbParameter("ShippingMailTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.shipping_package_shapes.shipping_package_shape_id", ConfigurationHelper.GetDbParameter("ShippingPackageShapeDisplayField"));

            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();

            ScrudHelper.AddDisplayView(displayViews, "core.item_types.item_type_id", "core.item_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.item_groups.item_group_id", "core.item_group_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.brands.brand_id", "core.brand_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.parties.party_id", "core.supplier_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.units.unit_id", "core.unit_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.sales_taxes.sales_tax_id", "core.sales_tax_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.shipping_mail_types.shipping_mail_type_id", "core.shipping_mail_type_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.shipping_package_shapes.shipping_package_shape_id", "core.shipping_package_shape_selector_view");

            return string.Join(",", displayViews);
        }
    }
}