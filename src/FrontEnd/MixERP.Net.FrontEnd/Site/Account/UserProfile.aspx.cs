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

using MixERP.Net.FrontEnd.Base;
using System;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Site.Account
{
    public partial class UserProfile : MixERPWebpage
    {
        public LinkButton emailLinkButton;

        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsLandingPage = true;
        }

        //private void EmailReportLinkButton_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}