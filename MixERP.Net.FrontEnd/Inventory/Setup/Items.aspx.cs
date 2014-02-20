/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Inventory.Setup
{
    public partial class Items : MixERPWebpage
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

                scrud.Width = 2000;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

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
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.item_groups.item_group_id", "core.item_groups");
            ScrudHelper.AddDisplayView(displayViews, "core.brands.brand_id", "core.brands");
            ScrudHelper.AddDisplayView(displayViews, "core.parties.party_id", "core.party_view");
            ScrudHelper.AddDisplayView(displayViews, "core.units.unit_id", "core.unit_view");
            ScrudHelper.AddDisplayView(displayViews, "core.taxes.tax_id", "core.tax_view");
            return string.Join(",", displayViews);
        }

    }
}