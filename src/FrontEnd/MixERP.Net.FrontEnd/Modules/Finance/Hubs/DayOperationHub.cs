using Microsoft.AspNet.SignalR;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Events;
using MixERP.Net.Common.Extensions;
using MixERP.Net.i18n.Resources;
using System;

namespace MixERP.Net.Core.Modules.Finance.Hubs
{
    [CLSCompliant(false)]
    public class DayOperationHub : Hub
    {
        public void PerformEOD()
        {
            if (!this.IsValidRequest())
            {
                this.Clients.Caller.getNotification(Warnings.AccessIsDenied, Warnings.AccessIsDenied);
                return;
            }

            Data.EODOperation operation = new Data.EODOperation();
            operation.NotificationReceived += this.EOD_NotificationReceived;

            string catalog = AppUsers.GetCurrentUserDB();

            operation.Perform(catalog, AppUsers.GetCurrent().View.LoginId.ToLong());
        }

        private void EOD_NotificationReceived(object sender, MixERPPGEventArgs e)
        {
            this.Clients.Caller.getNotification(e.AdditionalInformation, e.Condition);
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
    }
}