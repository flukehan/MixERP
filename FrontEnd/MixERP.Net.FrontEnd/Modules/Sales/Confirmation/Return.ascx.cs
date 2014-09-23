using MixERP.Net.FrontEnd.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Sales.Confirmation
{
    public partial class Return : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            TransactionChecklist1.ViewReportButtonText = Resources.Titles.ViewThisReturn;
            TransactionChecklist1.EmailReportButtonText = Resources.Titles.EmailThisReturn;

            base.OnControlLoad(sender, e);
        }
    }
}