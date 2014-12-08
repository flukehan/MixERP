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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Office;
using System.Collections.ObjectModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Finance.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class CurrencyData : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public Collection<Currency> GetExchangeCurrencies()
        {
            int officeId = SessionHelper.GetOfficeId();

            Collection<Currency> currencies = new Collection<Currency>();

            using (DataTable table = Data.Helpers.Currencies.GetExchangeCurrencies(officeId))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        currencies.Add(new Currency(Conversion.TryCastString(row["currency_code"]), Conversion.TryCastString(row["currency_symbol"]), Conversion.TryCastString(row["currency_name"]), Conversion.TryCastString(row["hundredth_name"])));
                    }
                }
            }

            return currencies;
        }
    }
}