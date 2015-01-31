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
using System;
using System.Globalization;
using System.Web.UI;

namespace MixERP.Net.FrontEnd
{
    public partial class MixERPMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BranchNameLiteral.Text = CurrentSession.GetOfficeName();
            this.SignOutLiteral.Text = Resources.Titles.SignOut;
            this.UserGreetingLiteral.Text = String.Format(CultureInfo.CurrentCulture, Resources.Labels.UserGreeting, CurrentSession.GetUserName());
            this.ChangePasswordLiteral.Text = Resources.Titles.ChangePassword;
            this.ManageProfileLiteral.Text = Resources.Titles.ManageProfile;
            this.MixERPDocumentationLiteral.Text = Resources.Titles.MixERPDocumentation;
            this.NotificationLiteral.Text = Resources.Titles.Notifications;
        }
    }
}