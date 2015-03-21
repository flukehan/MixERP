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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.TransactionGovernor.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using Serilog;

namespace MixERP.Net.Core.Modules.Purchase.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Return : WebService
    {
        [WebMethod]
        public long Save(long tranId, DateTime valueDate, int storeId, string partyCode, int priceTypeId,
            string referenceNumber, string data, string statementReference, string attachmentsJSON)
        {
            try
            {
                if (!StockTransaction.IsValidStockTransactionByTransactionMasterId(tranId))
                {
                    throw new InvalidOperationException(Resources.Warnings.InvalidStockTransaction);
                }

                if (!StockTransaction.IsValidPartyByTransactionMasterId(tranId, partyCode))
                {
                    throw new InvalidOperationException(Resources.Warnings.InvalidParty);
                }

                Collection<StockDetail> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);


                Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);

                int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
                int userId = CurrentUser.GetSignInView().UserId.ToInt();
                long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

                return Data.Transactions.Return.PostTransaction(tranId, valueDate, officeId, userId, loginId, storeId,
                    partyCode, priceTypeId, referenceNumber, statementReference, details, attachments);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save purchase return entry. {Exception}", ex);
                throw;
            }
        }

    }
}