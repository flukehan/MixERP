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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System.Web.UI.WebControls;
using FormHelper = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void BindGridView()
        {
            var showAll = (Conversion.TryCastString(this.Page.Request.QueryString["show"]).Equals("all"));

            var limit = 10;
            var offset = 0;

            if (this.PageSize != 0)
            {
                limit = this.PageSize;
            }

            if (showAll)
            {
                limit = 1000;
            }

            if (this.Page.Request["page"] != null)
            {
                offset = (Conversion.TryCastInteger(this.Page.Request["page"]) - 1) * limit;
            }

            using (var table = FormHelper.GetView(this.ViewSchema, this.View, this.KeyColumn, limit, offset))
            {
                this.formGridView.DataSource = table;
                this.formGridView.DataBind();
            }
        }

        private Unit GetGridViewWidth()
        {
            if (this.GridViewWidth.Value.Equals(0))
            {
                var width = Conversion.TryCastUnit(ConfigurationHelper.GetScrudParameter("GridViewDefaultWidth"));

                return width;
            }

            return this.GridViewWidth;
        }

        private void LoadGrid()
        {
            var showAll = (Conversion.TryCastString(this.Page.Request.QueryString["show"]).Equals("all"));

            this.BindGridView();
            this.formGridView.Width = this.GetGridViewWidth();
            this.formGridView.Attributes.Add("style", "white-space: nowrap;");

            this.pager.RecordCount = FormHelper.GetTotalRecords(this.ViewSchema, this.View);
            this.pager.PageSize = 10;

            if (this.PageSize != 0)
            {
                this.pager.PageSize = this.PageSize;
            }

            if (showAll)
            {
                this.pager.PageSize = 1000;
            }

            var userNameSessionKey = ConfigurationHelper.GetScrudParameter("UserNameSessionKey");
            var officeCodeSessionKey = ConfigurationHelper.GetScrudParameter("OfficeCodeSessionKey");

            this.userIdHidden.Value = SessionHelper.GetSessionValueByKey(userNameSessionKey);
            this.officeCodeHidden.Value = SessionHelper.GetSessionValueByKey(officeCodeSessionKey);
        }
    }
}