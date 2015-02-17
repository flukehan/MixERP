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
using System.Globalization;
using System.Web.Services;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class DirectSales : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string transactionType, int paymentTermId, int salespersonId, int shipperId, string shippingAddressCode, decimal shippingCharge, int costCenterId, string transactionIds, string attachmentsJSON, bool nonTaxable)
        {
            Collection<StockDetail> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);

            Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);

            bool isCredit = transactionType != null && !transactionType.ToUpperInvariant().Equals("CASH");

            if (!Data.Helpers.Stores.IsSalesAllowed(storeId))
            {
                throw new InvalidOperationException("Sales is not allowed here.");
            }

            foreach (StockDetail model in details)
            {
                if (Data.Helpers.Items.IsStockItem(model.ItemCode))
                {
                    decimal available = Data.Helpers.Items.CountItemInStock(model.ItemCode, model.UnitName, model.StoreId);

                    if (available < model.Quantity)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.Warnings.InsufficientStockWarning, available, model.UnitName, model.ItemCode));
                    }
                }
            }

            int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
            int userId = CurrentUser.GetSignInView().UserId.ToInt();
            long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

            return Data.Transactions.DirectSales.Add(officeId, userId, loginId, valueDate, storeId, isCredit, paymentTermId, partyCode, salespersonId, priceTypeId, details, shipperId, shippingAddressCode, shippingCharge, costCenterId, referenceNumber, statementReference, attachments, nonTaxable);
        }
    }
}