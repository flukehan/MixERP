using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class ItemsPopup : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
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

            base.OnControlLoad(sender, e);
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