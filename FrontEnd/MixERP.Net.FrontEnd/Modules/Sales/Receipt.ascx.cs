using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Sales
{
    public partial class Receipt : MixERPUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string ExchangeRateLocalized()
        {
            return Resources.Titles.ExchangeRate;
        }
    }
}