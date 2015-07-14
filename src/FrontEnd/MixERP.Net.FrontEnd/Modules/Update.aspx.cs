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
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.Updater;
using MixERP.Net.Updater.Api;

namespace MixERP.Net.FrontEnd.Modules
{
    public partial class Update : MixERPWebpage
    {
        protected string _downloadUrl = string.Empty;
        protected Release _release;

        protected async void Page_Init(object sender, EventArgs e)
        {
            try
            {
                UpdateManager updater = new UpdateManager();
                this._release = await updater.GetLatestRelease();
            }
            catch
            {
                this._release = new Release();
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