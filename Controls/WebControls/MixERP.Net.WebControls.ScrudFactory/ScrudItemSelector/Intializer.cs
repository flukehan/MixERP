/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        private void Initialize()
        {
            this.LoadParmeters();
            this.LoadGridView();
        }

        private void LoadParmeters()
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema())) return;
            if (string.IsNullOrWhiteSpace(this.GetView())) return;

            using (System.Data.DataTable table = MixERP.Net.WebControls.ScrudFactory.Data.TableHelper.GetTable(this.GetSchema(), this.GetView(), ""))
            {
                filterDropDownList.DataSource = table;
                filterDropDownList.DataBind();
            }
        }

        private void LoadGridView()
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema())) return;
            if (string.IsNullOrWhiteSpace(this.GetView())) return;

            using (System.Data.DataTable table = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper.GetTable(this.GetSchema(), this.GetView(), "", "", 10))
            {
                searchGridView.DataSource = table;
                searchGridView.DataBind();
            }
        }
    }
}
