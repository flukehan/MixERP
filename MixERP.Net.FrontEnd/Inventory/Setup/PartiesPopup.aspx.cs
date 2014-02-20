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
    public partial class PartiesPopup : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "party_id";
                scrud.TableSchema = "core";
                scrud.Table = "parties";
                scrud.ViewSchema = "core";
                scrud.View = "party_view";

                scrud.Text = Titles.PartyMaintenance;
                scrud.Description = Labels.PartyDescription;
                scrud.Width = 4000;

                //Party code will be automtically generated on the database.
                scrud.Exclude = "party_code";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.SelectedValues = GetSelectedValues();

                this.container.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.party_types.party_type_id", ConfigurationHelper.GetDbParameter("PartyTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id", ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.party_types.party_type_id", "core.party_types");
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequencies");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_view");
            return string.Join(",", displayViews);
        }

        private static string GetSelectedValues()
        {
            List<string> selectedValues = new List<string>();

            //Todo:
            //The default selected value of party receivable account
            //should be implemented via GL Mapping.
            ScrudHelper.AddSelectedValue(selectedValues, "core.accounts.account_id", "'10400 (Accounts Receivable)'");
            return string.Join(",", selectedValues);
        }

        public static string GetPartyNameParameter()
        {
            return Parameters.PartyNameFormat();
        }

    }
}