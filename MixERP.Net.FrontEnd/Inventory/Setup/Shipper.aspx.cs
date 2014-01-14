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

namespace MixERP.Net.FrontEnd.Inventory.Setup
{
    public partial class Shipper : MixERP.Net.BusinessLayer.MixERPWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "shipper_id";
                scrud.TableSchema = "core";
                scrud.Table = "shippers";
                scrud.ViewSchema = "core";
                scrud.View = "shippers";
                scrud.Width = 5000;

                //The following fields will be automatically generated on the database server.
                scrud.Exclude = "shipper_code, shipper_name";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.SelectedValues = GetSelectedValues();

                scrud.Text = Resources.Titles.Shipper;

                ScriptManager1.NamingContainer.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_view");
            return string.Join(",", displayViews);
        }


        private static string GetSelectedValues()
        {
            List<string> selectedValues = new List<string>();

            //Todo:
            //The default selected value of shipping payable account
            //should be done via GL Mapping.
            ScrudHelper.AddSelectedValue(selectedValues, "core.accounts.account_id", "'20110 (Shipping Charge Payable)'");
            return string.Join(",", selectedValues);
        }

    }
}