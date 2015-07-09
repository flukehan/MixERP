using Microsoft.AspNet.SignalR;
using MixER.Net.ApplicationState.Cache;
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
            if (this.Context == null)
            {
                this.Clients.Caller.getNotification(Warnings.AccessIsDenied);
                return;
            }

            long globalLoginId = Conversion.TryCastLong(this.Context.User.Identity.Name);

            if (globalLoginId <= 0)
            {
                this.Clients.Caller.getNotification(Warnings.AccessIsDenied);
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
    }
}