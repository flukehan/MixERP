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

namespace MixERP.Net.FrontEnd.Finance.Setup
{
    public partial class AgeingSlabs : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScrudForm scrud = new ScrudForm();

            scrud.KeyColumn = "ageing_slab_id";

            scrud.TableSchema = "core";
            scrud.Table = "ageing_slabs";
            scrud.ViewSchema = "core";
            scrud.View = "ageing_slabs";

            scrud.Text = Resources.Titles.AgeingSlabSetup;
            ToolkitScriptManager1.NamingContainer.Controls.Add(scrud);
        }
    }
}