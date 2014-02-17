/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using FormHelper = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void LoadGrid()
        {
            var showAll = (Conversion.TryCastString(this.Page.Request.QueryString["show"]).Equals("all"));

            this.BindGridView();
            this.formGridView.Width = this.GetWidth();
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

        private Unit GetWidth()
        {
            if (this.Width.Value.Equals(0))
            {
                var width = Conversion.TryCastInteger(ConfigurationHelper.GetScrudParameter("GridViewDefaultWidth"));

                if (width.Equals(0))
                {
                    return 1000;
                }

                return width;
            }

            return this.Width;
        }

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
    }
}
