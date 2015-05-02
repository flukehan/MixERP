using System;
using Microsoft.AspNet.SignalR;
using MixERP.Net.Common;
using MixERP.Net.Common.Events;
using MixERP.Net.i18n.Resources;

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

            long loginId = Conversion.TryCastLong(this.Context.User.Identity.Name);
            if (loginId <= 0)
            {
                this.Clients.Caller.getNotification(Warnings.AccessIsDenied);
                return;
            }

            Data.EODOperation operation = new Data.EODOperation();
            operation.NotificationReceived += this.EOD_NotificationReceived;
            operation.Perform(loginId);
        }

        private void EOD_NotificationReceived(object sender, MixERPPGEventArgs e)
        {            
            this.Clients.Caller.getNotification(e.AdditionalInformation, e.Condition);
        }
    }
}