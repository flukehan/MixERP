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
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.FrontEnd.Cache;
using Serilog;

namespace MixERP.Net.Core.Modules.Inventory.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Adjustment : WebService
    {
        [WebMethod]
        public long Save(DateTime valueDate, string referenceNumber, string statementReference, string models)
        {
            try
            {
                Collection<StockAdjustmentDetail> stockAdjustmentModels = GetModels(models);
                foreach (StockAdjustmentDetail model in stockAdjustmentModels)
                {
                    if (model.TransferTypeEnum == TransactionTypeEnum.Credit)
                    {
                        decimal existingQuantity = Data.Helpers.Items.CountItemInStock(model.ItemCode, model.UnitName, model.StoreName);

                        if (existingQuantity < model.Quantity)
                        {
                            throw new MixERPException(string.Format(CultureInfo.CurrentCulture, Errors.InsufficientStockWarning, Conversion.TryCastInteger(existingQuantity), model.UnitName, model.ItemName));
                        }
                    }
                }

                int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
                int userId = CurrentUser.GetSignInView().UserId.ToInt();
                long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

                return Data.Transactions.StockAdjustment.Add(officeId, userId, loginId, valueDate, referenceNumber, statementReference, stockAdjustmentModels);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save inventory adjustment entry. {Exception}", ex);
                throw;
            }
        }

        private static Collection<StockAdjustmentDetail> GetModels(string json)
        {
            Collection<StockAdjustmentDetail> models = new Collection<StockAdjustmentDetail>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                StockAdjustmentDetail detail = new StockAdjustmentDetail();
                const TransactionTypeEnum typeEnum = TransactionTypeEnum.Credit;

                detail.TransferTypeEnum = typeEnum;
                detail.StoreName = Conversion.TryCastString(item["StoreName"]);
                detail.ItemCode = Conversion.TryCastString(item["ItemCode"]);
                detail.ItemName = Conversion.TryCastString(item["ItemName"]);
                detail.UnitName = Conversion.TryCastString(item["UnitName"]);
                detail.Quantity = Conversion.TryCastInteger(item["Quantity"]);

                models.Add(detail);
            }

            return models;
        }
    }
}