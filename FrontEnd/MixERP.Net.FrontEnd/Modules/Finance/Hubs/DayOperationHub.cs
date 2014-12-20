using Microsoft.AspNet.SignalR;
using MixERP.Net.Common;
using MixERP.Net.Core.Modules.Finance.Resources;

namespace MixERP.Net.Core.Modules.Finance.Hubs
{
    public class DayOperationHub : Hub
    {
        public void PerformEOD()
        {
            if (Context == null)
            {
                Clients.Caller.getNotification(Errors.AccessIsDenied);
                return;
            }

            long loginId = Conversion.TryCastLong(Context.User.Identity.Name);
            if (loginId <= 0)
            {
                Clients.Caller.getNotification(Errors.AccessIsDenied);
                return;
            }

            Data.EODOperation operation = new Data.EODOperation();
            operation.NotificationReceived += EOD_NotificationReceived;
            operation.Perform(loginId);
        }

        private void EOD_NotificationReceived(object sender, Common.Events.MixERPPGEventArgs e)
        {
            Clients.Caller.getNotification(e.AdditionalInformation);
        }
    }
}