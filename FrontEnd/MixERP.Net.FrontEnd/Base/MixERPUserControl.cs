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

using System;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using System.Web.UI;
using MixERP.Net.Common.Domains;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.FrontEnd.Base
{
    public class MixERPUserControl : MixERPUserControlBase
    {
        public string GetPageMenu(Page page)
        {
            if (page != null)
            {
                string relativePath = this.Page.Request.Url.AbsolutePath;
                return MixERPWebpage.GetContentPageMenu(this.Page, relativePath, relativePath);
            }

            return null;
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            if (this.AccessLevel.Equals(AccessLevel.AdminOnly) && !CurrentSession.IsAdmin())
            {
                this.Page.Server.Transfer("~/Site/AccessIsDenied.aspx");
            }

            bool isLocalHost = PageUtility.IsLocalhost(this.Page);

            if (this.AccessLevel.Equals(AccessLevel.LocalhostAdmin) && !isLocalHost)
            {
                this.Page.Server.Transfer("~/Site/AccessIsDenied.aspx");
            }


            base.OnControlLoad(sender, e);
        }

        

        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.PolicyBased; }
        }


    }
}