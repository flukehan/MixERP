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
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MixERP.Net.Core.Modules.BackOffice.Tax
{
    public partial class SalesTaxExemptDetails : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "sales_tax_exempt_detail_id";
                scrud.TableSchema = "core";
                scrud.Table = "sales_tax_exempt_details";
                scrud.ViewSchema = "core";
                scrud.View = "sales_tax_exempt_details";
                scrud.Text = Titles.SalesTaxExemptDetails;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.ResourceAssembly = Assembly.GetAssembly(typeof(SalesTaxExemptDetails));
                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.sales_tax_exempts.sales_tax_exempt_id", ConfigurationHelper.GetDbParameter("SalesTaxExemptDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.entities.entity_id", ConfigurationHelper.GetDbParameter("EntityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.industries.industry_id", ConfigurationHelper.GetDbParameter("IndustryDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.parties.party_id", ConfigurationHelper.GetDbParameter("PartyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.party_types.party_type_id", ConfigurationHelper.GetDbParameter("PartyTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.items.item_id", ConfigurationHelper.GetDbParameter("ItemDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.item_groups.item_group_id", ConfigurationHelper.GetDbParameter("ItemGroupDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.sales_tax_exempts.sales_tax_exempt_id", "core.sales_tax_exempts");
            ScrudHelper.AddDisplayView(displayViews, "core.entities.entity_id", "core.entities");
            ScrudHelper.AddDisplayView(displayViews, "core.industries.industry_id", "core.industries");
            ScrudHelper.AddDisplayView(displayViews, "core.party_types.party_type_id", "core.party_types");
            ScrudHelper.AddDisplayView(displayViews, "core.parties.party_id", "core.parties");
            ScrudHelper.AddDisplayView(displayViews, "core.items.item_id", "core.items");
            ScrudHelper.AddDisplayView(displayViews, "core.item_groups.item_group_id", "core.item_groups");
            return string.Join(",", displayViews);
        }
    }
}