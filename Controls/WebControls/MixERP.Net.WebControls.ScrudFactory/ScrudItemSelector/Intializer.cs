/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.WebControls.ScrudFactory.Data;

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

            using (var table = TableHelper.GetTable(this.GetSchema(), this.GetView(), ""))
            {
                this.filterDropDownList.DataSource = table;
                this.filterDropDownList.DataBind();
            }
        }

        private void LoadGridView()
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema())) return;
            if (string.IsNullOrWhiteSpace(this.GetView())) return;

            using (var table = FormHelper.GetTable(this.GetSchema(), this.GetView(), "", "", 10))
            {
                this.searchGridView.DataSource = table;
                this.searchGridView.DataBind();
            }
        }
    }
}
