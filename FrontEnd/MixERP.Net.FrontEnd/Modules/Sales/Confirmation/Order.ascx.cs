using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Sales.Confirmation
{
    public partial class Order : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            TransactionChecklist1.ViewReportButtonText = Resources.Titles.ViewThisOrder;
            TransactionChecklist1.EmailReportButtonText = Resources.Titles.EmailThisOrder;

            base.OnControlLoad(sender, e);
        }
    }
}