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
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.Core.Modules.Inventory.Resources;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

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
            Collection<StockAdjustmentModel> stockTransferModels = this.GetModels(data);

            foreach (var model in stockTransferModels)
            {
                if (model.TransferType.ToLower(CultureInfo.InvariantCulture).Equals("cr"))
                {
                    decimal existingQuantity = Data.Helpers.Items.CountItemInStock(model.ItemCode, model.UnitName, model.StoreName);

                    if (existingQuantity < model.Quantity)
                    {
                        throw new MixERPException(string.Format(Errors.InsufficientStockWarning, Conversion.TryCastInteger(existingQuantity), model.UnitName, model.ItemName));
                    }
                }
            }

            int officeId = SessionHelper.GetOfficeId();
            int userId = SessionHelper.GetUserId();
            long loginId = SessionHelper.GetLogOnId();

            return Inventory.Data.Helpers.StockTransfer.Add(officeId, userId, loginId, valueDate, referenceNumber, statementReference, stockTransferModels);
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private Collection<StockAdjustmentModel> GetModels(string json)
        {
            Collection<StockAdjustmentModel> models = new Collection<StockAdjustmentModel>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                StockAdjustmentModel model = new StockAdjustmentModel();

                model.TransferType = Conversion.TryCastString(item[0]);
                model.StoreName = Conversion.TryCastString(item[1]);
                model.ItemCode = Conversion.TryCastString(item[2]);
                model.ItemName = Conversion.TryCastString(item[3]);
                model.UnitName = Conversion.TryCastString(item[4]);
                model.Quantity = Conversion.TryCastInteger(item[5]);

                models.Add(model);
            }

            var results = from rows in models
                          group rows by new { rows.ItemCode, rows.UnitName }
                              into aggregate
                              select new
                              {
                                  aggregate.Key.ItemCode,
                                  aggregate.Key.UnitName,
                                  Debit = aggregate.Where(row => row.TransferType.ToLower().Equals("dr")).Sum(row => row.Quantity),
                                  Credit = aggregate.Where(row => row.TransferType.ToLower().Equals("cr")).Sum(row => row.Quantity)
                              };

            if ((from query in results where query.Debit != query.Credit select query).Any())
            {
                throw new MixERPException(Errors.ReferencingSidesNotEqual);
            }

            return models;
        }
    }
}