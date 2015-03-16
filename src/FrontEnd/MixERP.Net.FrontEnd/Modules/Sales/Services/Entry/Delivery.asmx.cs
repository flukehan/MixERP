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
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Services;
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
    [ToolboxItem(false)]
    [ScriptService]
    public class Delivery : WebService
    {
        [WebMethod]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, int paymentTermId, string referenceNumber, string data, string statementReference, int salespersonId, int shipperId, string shippingAddressCode, decimal shippingCharge, int costCenterId, string transactionIds, string attachmentsJSON, bool nonTaxable)
        {
            try
            {
                Collection<StockDetail> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);
                Collection<int> tranIds = new Collection<int>();

                Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);

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

                if (!string.IsNullOrWhiteSpace(transactionIds))
                {
                    foreach (string transactionId in transactionIds.Split(','))
                    {
                        tranIds.Add(Common.Conversion.TryCastInteger(transactionId));
                    }
                }

                int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
                int userId = CurrentUser.GetSignInView().UserId.ToInt();
                long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

                return Data.Transactions.Delivery.Add(officeId, userId, loginId, valueDate, storeId, partyCode, priceTypeId, paymentTermId, details, shipperId, shippingAddressCode, shippingCharge, costCenterId, referenceNumber, salespersonId, statementReference, tranIds, attachments, nonTaxable);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save sales delivery entry. {Exception}", ex);
                throw;
            }
        }
    }
}