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
using System.Linq;
using System.Threading;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.Updater;
using MixERP.Net.Updater.Api;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using Serilog;

namespace MixERP.Net.FrontEnd.Modules
{
    public partial class Update : MixERPWebpage
    {
        protected string _downloadUrl = string.Empty;
        protected Release _release = new Release();

        protected async void Page_Init(object sender, EventArgs e)
        {
            string userName = AppUsers.GetCurrent().View.UserName;
            string ipAddress = AppUsers.GetCurrent().View.IpAddress;

            bool isDevelopmentMode = DbConfig.GetMixERPParameter(AppUsers.GetCurrentUserDB(), "Mode").ToUpperInvariant().Equals("DEVELOPMENT");
            bool isLocalHost = PageUtility.IsLocalhost(this.Page);
            bool isAdmin = AppUsers.GetCurrent().View.IsAdmin.ToBool();

            bool hasAccess = false;

            if (isAdmin)
            {
                if (isDevelopmentMode && isLocalHost)
                {
                    hasAccess = true;
                }

                if (!isDevelopmentMode)
                {
                    hasAccess = true;
                }
            }

            if (!hasAccess)
            {
                Log.Information("Access to {Page} is denied to {User} from {IP}.", this,
                    userName, ipAddress);

                this.Page.Server.Execute("~/Site/AccessIsDenied.aspx", false);
                return;
            }

            try
            {
                UpdateManager updater = new UpdateManager();
                this._release = await updater.GetLatestRelease();
            }
            catch
            {
                this.ReleasePanel.Visible = false;
                this.UpToDatePanel.Visible = true;
                return;
            }


            string keyword = Config.UpdateKeyword;

            Asset ass =
                this._release.Assets.FirstOrDefault(a => a.Name.ToUpperInvariant().Contains(keyword.ToUpperInvariant()));

            if (ass != null)
            {
                this._downloadUrl = ass.DownloadUrl;
            }

            if (string.IsNullOrWhiteSpace(this._downloadUrl))
            {
                this.ErrorLabel.Text = "This release does not contain any update.";
            }

            this.OverridePath = "/Dashboard/Index.aspx";
        }
    }
}