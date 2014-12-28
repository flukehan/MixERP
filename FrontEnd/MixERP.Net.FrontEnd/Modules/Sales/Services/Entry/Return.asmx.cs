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

using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.TransactionGovernor.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using CollectionHelper = MixERP.Net.WebControls.StockTransactionFactory.Helpers.CollectionHelper;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Return : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(long tranId, DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string attachmentsJSON)
        {
            if (!StockTransaction.IsValidStockTransactionByTransactionMasterId(tranId))
            {
                throw new InvalidOperationException(Resources.Warnings.InvalidStockTransaction);
            }

            if (!StockTransaction.IsValidPartyByTransactionMasterId(tranId, partyCode))
            {
                throw new InvalidOperationException(Resources.Warnings.InvalidParty);
            }

            Collection<StockMasterDetailModel> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);

            if (!this.ValidateDetails(details, tranId))
            {
                return 0;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<AttachmentModel> attachments = js.Deserialize<Collection<AttachmentModel>>(attachmentsJSON);

            int officeId = SessionHelper.GetOfficeId();
            int userId = SessionHelper.GetUserId();
            long loginId = SessionHelper.GetLogOnId();

            return Data.Transactions.Return.PostTransaction(tranId, valueDate, officeId, userId, loginId, storeId, partyCode, priceTypeId, referenceNumber, statementReference, details, attachments);
        }

        private bool ValidateDetails(IEnumerable<StockMasterDetailModel> details, long stockMasterId)
        {
            foreach (var model in details)
            {
                return StockTransaction.ValidateItemForReturn(stockMasterId, model.StoreId, model.ItemCode, model.UnitName, model.Quantity, model.Price);
            }

            return false;
        }
    }
}