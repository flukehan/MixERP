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
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;

namespace MixERP.Net.Core.Modules.Inventory.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Transfer : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, string referenceNumber, string statementReference, string data)
        {
            Collection<StockAdjustmentDetail> stockTransferModels = GetModels(data);

            foreach (var model in stockTransferModels)
            {
                if (model.TransferType == TransactionType.Credit)
                {
                    decimal existingQuantity = Data.Helpers.Items.CountItemInStock(model.ItemCode, model.UnitName, model.StoreName);

                    if (existingQuantity < model.Quantity)
                    {
                        throw new MixERPException(string.Format(CultureInfo.CurrentCulture, Errors.InsufficientStockWarning, Conversion.TryCastInteger(existingQuantity), model.UnitName, model.ItemName));
                    }
                }
            }

            int officeId = CurrentSession.GetOfficeId();
            int userId = CurrentSession.GetUserId();
            long loginId = CurrentSession.GetLoginId();

            return Data.Transactions.StockTransfer.Add(officeId, userId, loginId, valueDate, referenceNumber, statementReference, stockTransferModels);
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private static Collection<StockAdjustmentDetail> GetModels(string json)
        {
            Collection<StockAdjustmentDetail> models = new Collection<StockAdjustmentDetail>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                StockAdjustmentDetail detail = new StockAdjustmentDetail();
                TransactionType type = TransactionType.Credit;

                if (Conversion.TryCastString(item[0]).ToString().Equals("Dr"))
                {
                    type = TransactionType.Debit;
                }

                detail.TransferType = type;
                detail.StoreName = Conversion.TryCastString(item[1]);
                detail.ItemCode = Conversion.TryCastString(item[2]);
                detail.ItemName = Conversion.TryCastString(item[3]);
                detail.UnitName = Conversion.TryCastString(item[4]);
                detail.Quantity = Conversion.TryCastInteger(item[5]);

                models.Add(detail);
            }

            var results = from rows in models
                group rows by new {rows.ItemCode, rows.UnitName}
                into aggregate
                select new
                {
                    aggregate.Key.ItemCode,
                    aggregate.Key.UnitName,
                    Debit = aggregate.Where(row => row.TransferType.Equals(TransactionType.Debit)).Sum(row => row.Quantity),
                    Credit = aggregate.Where(row => row.TransferType.Equals(TransactionType.Credit)).Sum(row => row.Quantity)
                };

            if ((from query in results where query.Debit != query.Credit select query).Any())
            {
                throw new MixERPException(Errors.ReferencingSidesNotEqual);
            }

            return models;
        }
    }
}