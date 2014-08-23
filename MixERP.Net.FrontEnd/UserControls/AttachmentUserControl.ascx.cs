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
using System.Configuration;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.UserControls
{
    public partial class AttachmentUserControl : UserControl
    {
        public string GetAllowedExtensions()
        {
            return ConfigurationManager.AppSettings["AllowedExtensions"];
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckPermission();
        }

        private void CheckPermission()
        {
            var folder = ConfigurationManager.AppSettings["AttachmentsDirectory"];
            var permission = new FileIOPermission(FileIOPermissionAccess.Write, Server.MapPath(folder));
            var permissionSet = new PermissionSet(PermissionState.None);
            permissionSet.AddPermission(permission);

            if (!permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
            {
                WarningLabel.Text = String.Format(CultureInfo.CurrentUICulture, "The directory \"{0}\" is write protected", folder);
                //AttachmentPanel.Enabled = false;
            }

        }
    }
}