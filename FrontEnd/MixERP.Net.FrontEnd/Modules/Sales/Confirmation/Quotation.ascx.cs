using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Sales.Confirmation
{
    public partial class Quotation : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            TransactionChecklist1.ViewReportButtonText = Resources.Titles.ViewThisQuotation;
            TransactionChecklist1.EmailReportButtonText = Resources.Titles.EmailThisQuotation;

            base.OnControlLoad(sender, e);
        }
    }
}