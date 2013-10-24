/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;

namespace MixERP.Net.FrontEnd.Items.Setup
{
    public partial class ShippingAddresses : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScrudForm scrud = new ScrudForm();

            scrud.KeyColumn = "shipping_address_id";
            scrud.TableSchema = "core";
            scrud.Table = "shipping_addresses";
            scrud.ViewSchema = "core";
            scrud.View = "shipping_address_view";

            //Shipping address code will be automatically generated on the database.
            scrud.Exclude = "shipping_address_code";

            scrud.DisplayFields = GetDisplayFields();
            scrud.DisplayViews = GetDisplayViews();

            scrud.Text = Resources.Titles.ShippingAddressMaintenance;

            ToolkitScriptManager1.NamingContainer.Controls.Add(scrud);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.parties.party_id", ConfigurationHelper.GetDbParameter("PartyDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.parties.party_id", "core.party_view");
            return string.Join(",", displayViews);
        }


    }
}