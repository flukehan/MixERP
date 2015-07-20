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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MixERP.Net.FrontEnd
{
    public partial class MixERPMaster : MixERPMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CatalogLiteral.Text = AppUsers.GetCurrentUserDB();
            this.BranchNameLiteral.Text = AppUsers.GetCurrent().View.OfficeName;
            this.SignOutLiteral.Text = Titles.SignOut;
            this.UserGreetingLiteral.Text = String.Format(CultureInfo.CurrentCulture, Labels.UserGreeting,
                AppUsers.GetCurrent().View.UserName);
            this.ChangePasswordLiteral.Text = Titles.ChangePassword;
            this.ManageProfileLiteral.Text = Titles.ManageProfile;
            this.MixERPDocumentationLiteral.Text = Titles.MixERPDocumentation;
            this.NotificationLiteral.Text = Titles.Notifications;
            this.VersionLiteral.Text = GetProductVersion();
        }

        public static string GetProductVersion()
        {
            var attribute = (AssemblyInformationalVersionAttribute)Assembly
              .GetExecutingAssembly()
              .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true)
              .Single();
            return attribute.InformationalVersion;
        }
    }
}