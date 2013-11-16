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

namespace MixERP.Net.FrontEnd.POS.Setup
{
    public partial class Stores : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScrudForm scrud = new ScrudForm();

            scrud.KeyColumn = "store_id";

            scrud.TableSchema = "office";
            scrud.Table = "stores";
            scrud.ViewSchema = "office";
            scrud.View = "stores";

            scrud.DisplayFields = GetDisplayFields();
            scrud.DisplayViews = GetDisplayViews();

            scrud.Text = Resources.Titles.Stores;

            ScriptManager1.NamingContainer.Controls.Add(scrud);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.store_types.store_type_id", ConfigurationHelper.GetDbParameter("StoreTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id", ConfigurationHelper.GetDbParameter("OfficeDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.store_types.store_type_id", "office.store_types");
            ScrudHelper.AddDisplayView(displayViews, "office.offices.office_id", "office.office_view");
            return string.Join(",", displayViews);
        }

    }
}