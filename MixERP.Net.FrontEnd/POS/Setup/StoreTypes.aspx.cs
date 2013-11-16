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
using MixERP.Net.WebControls.ScrudFactory;

namespace MixERP.Net.FrontEnd.POS.Setup
{
    public partial class StoreTypes : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScrudForm scrud = new ScrudForm();

            scrud.KeyColumn = "store_type_id";

            scrud.TableSchema = "office";
            scrud.Table = "store_types";
            scrud.ViewSchema = "office";
            scrud.View = "store_types";

            scrud.Text = Resources.Titles.StoreTypes;

            ScriptManager1.NamingContainer.Controls.Add(scrud);
        }
    }
}