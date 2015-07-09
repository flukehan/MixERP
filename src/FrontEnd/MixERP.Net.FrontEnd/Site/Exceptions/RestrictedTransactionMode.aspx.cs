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

using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using System;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.FrontEnd.Site.Exceptions
{
    public partial class RestrictedTransactionMode : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsLandingPage = true;

            using (HtmlGenericControl header = new HtmlGenericControl("h1"))
            {
                header.InnerText = Titles.RestrictedTransactionMode;
                this.Placeholder1.Controls.Add(header);
            }

            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                this.Placeholder1.Controls.Add(divider);
            }

            using (HtmlGenericControl p = new HtmlGenericControl("p"))
            {
                p.InnerText = Warnings.RestrictedTransactionMode;
                this.Placeholder1.Controls.Add(p);
            }

            using (HtmlAnchor anchor = new HtmlAnchor())
            {
                anchor.InnerText = Titles.BackToPreviousPage;
                anchor.HRef = "javascript:history.go(-1);";
                anchor.Attributes.Add("class", "ui pink button");
                this.Placeholder1.Controls.Add(anchor);
            }
        }
    }
}