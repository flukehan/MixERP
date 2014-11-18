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
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    /// <summary>
    /// Summary description for DirectSales
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the
    // following line.
    [System.Web.Script.Services.ScriptService]
    public class DirectSales : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string transactionType, int agentId, int shipperId, string shippingAddressCode, decimal shippingCharge, int cashRepositoryId, int costCenterId, string transactionIds, string attachmentsJSON)
        {
            System.Collections.ObjectModel.Collection<Common.Models.Transactions.StockMasterDetailModel> details = WebControls.StockTransactionFactory.Helpers.CollectionHelper.GetStockMasterDetailCollection(data, storeId);
            System.Collections.ObjectModel.Collection<int> tranIds = new System.Collections.ObjectModel.Collection<int>();

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            System.Collections.ObjectModel.Collection<Common.Models.Core.AttachmentModel> attachments = js.Deserialize<System.Collections.ObjectModel.Collection<Common.Models.Core.AttachmentModel>>(attachmentsJSON);

            if (!string.IsNullOrWhiteSpace(transactionIds))
            {
                foreach (string transactionId in transactionIds.Split(','))
                {
                    tranIds.Add(Common.Conversion.TryCastInteger(transactionId));
                }
            }

            bool isCredit = !transactionType.ToLower().Equals("cash");

            if (isCredit && cashRepositoryId > 0)
            {
                throw new InvalidOperationException("Invalid cash repository specified in credit transaction.");
            }

            if (!Data.Helpers.Stores.IsSalesAllowed(storeId))
            {
                throw new InvalidOperationException("Sales is not allowed here.");
            }

            foreach (Common.Models.Transactions.StockMasterDetailModel model in details)
            {
                if (Data.Helpers.Items.IsStockItem(model.ItemCode))
                {
                    decimal available = Data.Helpers.Items.CountItemInStock(model.ItemCode, model.UnitName, model.StoreId);

                    if (available < model.Quantity)
                    {
                        throw new InvalidOperationException(string.Format(Resources.Warnings.InsufficientStockWarning, available, model.UnitName, model.ItemCode));
                    }
                }
            }

            return Data.Transactions.DirectSales.Add(valueDate, storeId, isCredit, partyCode, agentId, priceTypeId, details, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId, costCenterId, referenceNumber, statementReference, attachments);
        }
    }
}