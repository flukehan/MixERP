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
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class PartiesPopup : MixERPUserControl
    {
        public static string GetPartyNameParameter()
        {
            return Parameters.GetParameter("PartyName");
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "party_id";
                scrud.TableSchema = "core";
                scrud.Table = "parties";
                scrud.ViewSchema = "core";
                scrud.View = "party_scrud_view";

                //Party code will be automatically generated on the database.
                scrud.Exclude = "party_code, account_id";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Description = Labels.PartyDescription;
                scrud.Text = Titles.Parties;
                scrud.ResourceAssembly = Assembly.GetAssembly(typeof (PartiesPopup));
                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.party_types.party_type_id", ConfigurationHelper.GetDbParameter("PartyTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id", ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.currencies.currency_code", ConfigurationHelper.GetDbParameter("CurrencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.countries.country_id", ConfigurationHelper.GetDbParameter("CountryDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.states.state_id", ConfigurationHelper.GetDbParameter("StateDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.entities.entity_id", ConfigurationHelper.GetDbParameter("EntityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.industries.industry_id", ConfigurationHelper.GetDbParameter("IndustryDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.party_types.party_type_id", "core.party_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequency_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.currencies.currency_code", "core.currency_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.countries.country_id", "core.country_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.states.state_id", "core.state_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.entities.entity_id", "core.entity_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.industries.industry_id", "core.industry_scrud_view");

            return string.Join(",", displayViews);
        }
    }
}