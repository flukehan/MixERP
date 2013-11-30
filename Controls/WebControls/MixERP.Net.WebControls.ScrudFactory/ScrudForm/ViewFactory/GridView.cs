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
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        private void LoadGrid()
        {
            bool showAll = (MixERP.Net.Common.Conversion.TryCastString(this.Page.Request.QueryString["show"]).Equals("all"));

            this.BindGridView();
            this.formGridView.Width = this.GetWidth();
            this.pager.RecordCount = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTotalRecords(this.ViewSchema, this.View);
            this.pager.PageSize = 10;


            if (this.PageSize != 0)
            {
                this.pager.PageSize = this.PageSize;
            }

            if (showAll)
            {
                this.pager.PageSize = 1000;
            }


            this.userIdHidden.Value = MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetUserName();
            this.officeCodeHidden.Value = MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetOfficeName();
        }

        private Unit GetWidth()
        {
            if (this.Width.Value.Equals(0))
            {
                int width = MixERP.Net.Common.Conversion.TryCastInteger(MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("GridViewDefaultWidth"));

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
            bool showAll = (MixERP.Net.Common.Conversion.TryCastString(this.Page.Request.QueryString["show"]).Equals("all"));

            int limit = 10;
            int offset = 0;

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
                offset = (MixERP.Net.Common.Conversion.TryCastInteger(this.Page.Request["page"]) - 1) * limit;
            }


            using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetView(this.ViewSchema, this.View, this.KeyColumn, limit, offset))
            {
                this.formGridView.DataSource = table;
                this.formGridView.DataBind();
            }
        }
    }
}
