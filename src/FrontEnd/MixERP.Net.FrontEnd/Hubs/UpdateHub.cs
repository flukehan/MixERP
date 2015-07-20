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
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using MixERP.Net.Updater;
using MixERP.Net.Updater.Api;
using MixERP.Net.Updater.Installation;
using Serilog;

namespace MixERP.Net.FrontEnd.Hubs
{
    [CLSCompliant(false)]
    public class UpdateHub : Hub
    {
        public void InstallUpdates()
        {
            if (!this.IsValidRequest())
            {
                this.Notify("Cannot Update", Warnings.AccessIsDenied);
                return;
            }

            string downloadUrl = string.Empty;
            string[] toBackup =
            {
                DbConfig.GetAttachmentParameter(AppUsers.GetCurrentUserDB(), "AttachmentsDirectory"),
                ConfigurationHelper.GetDbServerParameter("DatabaseBackupDirectory")
            };

            Release release = new Release();

            try
            {
                UpdateManager updater = new UpdateManager();
                Task task = Task.Run(async () => { release = await updater.GetLatestRelease(); });
                task.Wait();
            }
            catch
            {
                this.Notify("Cannot Update", "Cannot start the update because no release was found.");
                return;
            }


            string keyword = Config.UpdateKeyword;

            Asset ass = release.Assets.FirstOrDefault(a => a.Name.ToUpperInvariant().Contains(keyword.ToUpperInvariant()));

            if (ass != null)
            {
                downloadUrl = ass.DownloadUrl;
            }

            if (string.IsNullOrWhiteSpace(downloadUrl))
            {
                this.Notify("Cannot Update", "Cannot start the update because no download was found on the release.");
                return;
            }

            this.InstallUpdate(downloadUrl, toBackup);
        }

        private bool IsValidRequest()
        {
            System.Threading.Thread.Sleep(2000);

            if (this.Context == null)
            {
                this.Clients.Caller.getNotification(Warnings.AccessIsDenied);
                return false;
            }

            long globalLoginId = Conversion.TryCastLong(this.Context.User.Identity.Name);

            if (globalLoginId <= 0)
            {
                this.Clients.Caller.getNotification(Warnings.AccessIsDenied);
                return false;
            }

            if (!AppUsers.GetCurrent(globalLoginId).View.IsAdmin.ToBool())
            {
                return false;
            }

            if (!AppUsers.GetCurrent(globalLoginId).View.Elevated.ToBool())
            {
                return false;
            }

            return true;
        }

        private void InstallUpdate(string downloadUrl, string[] toBackup)
        {
            UpdateInstaller installer = new UpdateInstaller(downloadUrl, toBackup);
            installer.Progress += installer_Progress;


            this.Notify("Update Started", "The update process has started.");
            try
            {
                installer.Update();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred. {Exception}.", ex);
                this.Notify(Titles.UnknownError, ex.Message);
            }
        }

        private void Notify(string description, string message)
        {
            this.Clients.All.updateInstallationNotification(DateTime.UtcNow, description, message);
        }

        private void installer_Progress(ProgressInfo progressInfo)
        {
            this.Clients.All.updateInstallationNotification(progressInfo.Timestamp, progressInfo.Description,
                progressInfo.Message);
        }
    }
}