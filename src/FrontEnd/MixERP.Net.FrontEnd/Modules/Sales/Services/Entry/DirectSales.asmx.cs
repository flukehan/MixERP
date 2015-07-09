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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class DirectSales : WebService
    {
        [WebMethod]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber,
            string data, string statementReference, string transactionType, int paymentTermId, int salespersonId,
            int shipperId, string shippingAddressCode, decimal shippingCharge, int costCenterId, string transactionIds,
            string attachmentsJSON, bool nonTaxable)
        {
            try
            {
                Collection<StockDetail> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);

                Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);

                bool isCredit = transactionType != null && !transactionType.ToUpperInvariant().Equals("CASH");

                if (!Data.Helpers.Stores.IsSalesAllowed(AppUsers.GetCurrentUserDB(), storeId))
                {
                    throw new InvalidOperationException("Sales is not allowed here.");
                }

                foreach (StockDetail model in details)
                {
                    if (Data.Helpers.Items.IsStockItem(AppUsers.GetCurrentUserDB(), model.ItemCode))
                    {
                        decimal available = Data.Helpers.Items.CountItemInStock(AppUsers.GetCurrentUserDB(),
                            model.ItemCode, model.UnitName, model.StoreId);

                        if (available < model.Quantity)
                        {
                            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                                Warnings.InsufficientStockWarning, available, model.UnitName, model.ItemCode));
                        }
                    }
                }

                int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
                int userId = AppUsers.GetCurrent().View.UserId.ToInt();
                long loginId = AppUsers.GetCurrent().View.LoginId.ToLong();

                return Data.Transactions.DirectSales.Add(AppUsers.GetCurrentUserDB(), officeId, userId, loginId,
                    valueDate, storeId, isCredit, paymentTermId, partyCode, salespersonId, priceTypeId, details,
                    shipperId, shippingAddressCode, shippingCharge, costCenterId, referenceNumber, statementReference,
                    attachments, nonTaxable);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save direct sales entry. {Exception}", ex);
                throw;
            }
        }
    }
}