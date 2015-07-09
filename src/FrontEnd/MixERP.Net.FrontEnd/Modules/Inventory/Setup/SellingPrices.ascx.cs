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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class SellingPrices : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "item_selling_price_id";
                scrud.TableSchema = "core";
                scrud.Table = "item_selling_prices";
                scrud.ViewSchema = "core";
                scrud.View = "item_selling_price_scrud_view";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Titles.ItemSellingPrices;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.items.item_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "ItemDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.party_types.party_type_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "PartyTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.price_types.price_type_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "PriceTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.units.unit_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "UnitDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.items.item_id", "core.item_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.party_types.party_type_id", "core.party_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.price_types.price_type_id", "core.price_type_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.units.unit_id", "core.unit_scrud_view");
            return string.Join(",", displayViews);
        }
    }
}