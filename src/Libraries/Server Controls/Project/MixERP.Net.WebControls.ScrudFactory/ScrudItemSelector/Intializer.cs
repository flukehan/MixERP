/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.WebControls.ScrudFactory.Data;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        private void Initialize(string catalog)
        {
            this.LoadParmeters(catalog);
            this.LoadGridView(catalog);
        }

        private void LoadGridView(string catalog)
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema()))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(this.GetView()))
            {
                return;
            }

            using (var table = FormHelper.GetTable(catalog, this.GetSchema(), this.GetView(), "", "", 10, "1"))
            {
                this.searchGridView.DataSource = table;
                this.searchGridView.DataBind();
            }
        }

        private void LoadParmeters(string catalog)
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema()))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(this.GetView()))
            {
                return;
            }

            this.filterSelect.DataSource = TableHelper.GetColumns(catalog, this.GetSchema(), this.GetView());
            this.filterSelect.DataBind();
        }
    }
}