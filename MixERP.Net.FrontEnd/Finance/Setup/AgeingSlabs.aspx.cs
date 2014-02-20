/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using MixERP.Net.BusinessLayer;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Finance.Setup
{
    public partial class AgeingSlabs : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "ageing_slab_id";

                scrud.TableSchema = "core";
                scrud.Table = "ageing_slabs";
                scrud.ViewSchema = "core";
                scrud.View = "ageing_slabs";

                scrud.Text = Titles.AgeingSlabSetup;
                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }
    }
}