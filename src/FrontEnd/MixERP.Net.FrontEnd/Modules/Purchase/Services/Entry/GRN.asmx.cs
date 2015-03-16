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
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using Serilog;

namespace MixERP.Net.Core.Modules.Purchase.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class GRN : WebService
    {
        [WebMethod]
        public long Save(DateTime valueDate, int storeId, string partyCode, string referenceNumber, string data, string statementReference, int costCenterId, string transactionIds, string attachmentsJSON)
        {
            try
            {
                Collection<StockDetail> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);
                Collection<long> tranIds = new Collection<long>();

                Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);

                if (!string.IsNullOrWhiteSpace(transactionIds))
                {
                    foreach (string transactionId in transactionIds.Split(','))
                    {
                        tranIds.Add(Conversion.TryCastInteger(transactionId));
                    }
                }

                int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
                int userId = CurrentUser.GetSignInView().UserId.ToInt();
                long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

                return Data.Transactions.GRN.Add(officeId, userId, loginId, valueDate, storeId, partyCode, details, costCenterId, referenceNumber, statementReference, tranIds, attachments);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save GRN entry. {Exception}", ex);
                throw;
            }
        }
    }
}