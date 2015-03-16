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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Transactions;
using MixERP.Net.FrontEnd.Cache;
using Serilog;

namespace MixERP.Net.Core.Modules.BackOffice.Services.OTS
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class OpeningInventory : WebService
    {
        [WebMethod]
        public long Save(DateTime valueDate, string referenceNumber, string statementReference, string jsonDetails)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jsonDetails))
                {
                    return 0;
                }

                int userId = CurrentUser.GetSignInView().UserId.ToInt();
                int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
                long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

                Collection<OpeningStockType> details = this.GetStockDetails(jsonDetails);

                return Data.OneTimeSetup.OpeningInventory.Save(officeId, userId, loginId, valueDate, referenceNumber, statementReference, details);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save opening inventory entry. {Exception}", ex);
                throw;
            }
        }

        private Collection<OpeningStockType> GetStockDetails(string json)
        {
            Collection<OpeningStockType> details = new Collection<OpeningStockType>();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            if (result != null)
            {
                foreach (dynamic item in result)
                {
                    OpeningStockType detail = new OpeningStockType();
                    detail.ItemCode = item[0];
                    detail.StoreName = item[2];
                    detail.Quantity = Conversion.TryCastInteger(item[3]);
                    detail.UnitName = item[4];
                    detail.Amount = Conversion.TryCastDecimal(item[5]);

                    details.Add(detail);
                }
            }

            return details;
        }


    }
}