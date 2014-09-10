using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services
{
    /// <summary>
    /// Summary description for Currencies
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class Currencies : System.Web.Services.WebService
    {
        [WebMethod]
        public System.Collections.ObjectModel.Collection<System.Web.UI.WebControls.ListItem> GetCurrencies()
        {
            System.Collections.ObjectModel.Collection<System.Web.UI.WebControls.ListItem> values = new System.Collections.ObjectModel.Collection<System.Web.UI.WebControls.ListItem>();

            using (System.Data.DataTable table = BusinessLayer.Helpers.FormHelper.GetTable("core", "currencies"))
            {
                foreach (System.Data.DataRow dr in table.Rows)
                {
                    values.Add(new System.Web.UI.WebControls.ListItem(dr["currency_code"].ToString(), dr["currency_code"].ToString()));
                }
            }
            return values;
        }

        [WebMethod(EnableSession = true)]
        public decimal GetExchangeRate(string sourceCurrencyCode, string destinationCurrencyCode)
        {
            int officeId = Common.Helpers.SessionHelper.GetOfficeId();

            decimal exchangeRate = Data.Helpers.Transaction.GetExchangeRate(officeId, sourceCurrencyCode, destinationCurrencyCode);

            return exchangeRate;
        }
    }
}