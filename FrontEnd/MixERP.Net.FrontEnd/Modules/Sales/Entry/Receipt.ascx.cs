using MixERP.Net.FrontEnd.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Sales.Entry
{
    public partial class Receipt : MixERPUserControl
    {
        public string ExchangeRateLocalized()
        {
            return Resources.Titles.ExchangeRate;
        }
    }
}