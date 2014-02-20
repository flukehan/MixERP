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
    public partial class Currencies : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "currency_code";
                scrud.TableSchema = "core";
                scrud.Table = "currencies";
                scrud.ViewSchema = "core";
                scrud.View = "currencies";

                scrud.Text = Titles.Currencies;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }
    }
}