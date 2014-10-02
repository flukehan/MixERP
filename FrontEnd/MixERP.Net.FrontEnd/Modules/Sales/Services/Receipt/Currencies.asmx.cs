/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services.Receipt
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

            using (System.Data.DataTable table = Data.Helpers.Currencies.GetCurrencyDataTable())
            {
                foreach (System.Data.DataRow dr in table.Rows)
                {
                    values.Add(new System.Web.UI.WebControls.ListItem(dr["currency_code"].ToString(), dr["currency_code"].ToString()));
                }
            }
            return values;
        }

        [WebMethod(EnableSession = true)]
        public string GetHomeCurrency()
        {
            int officeId = Common.Helpers.SessionHelper.GetOfficeId();
            return Data.Helpers.Currencies.GetHomeCurrency(officeId);
        }

        [WebMethod(EnableSession = true)]
        public decimal GetExchangeRate(string sourceCurrencyCode, string destinationCurrencyCode)
        {
            if (sourceCurrencyCode.Equals(destinationCurrencyCode))
            {
                return 1;
            }

            int officeId = Common.Helpers.SessionHelper.GetOfficeId();

            decimal exchangeRate = Data.Helpers.Transaction.GetExchangeRate(officeId, sourceCurrencyCode, destinationCurrencyCode);

            return exchangeRate;
        }
    }
}