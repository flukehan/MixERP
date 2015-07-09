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
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Core.Modules.Inventory.Data.Helpers;
using MixERP.Net.Core.Modules.Inventory.Data.Transactions;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
        [WebMethod]
        public long Save(DateTime valueDate, string referenceNumber, string statementReference, string data)
        {
            try
            {
                Collection<StockAdjustmentDetail> stockTransferModels = GetModels(data);

                foreach (StockAdjustmentDetail model in stockTransferModels)
                {
                    if (model.TransferTypeEnum == TransactionTypeEnum.Credit)
                    {
                        decimal existingQuantity = Items.CountItemInStock(AppUsers.GetCurrentUserDB(), model.ItemCode,
                            model.UnitName, model.StoreName);

                        if (existingQuantity < model.Quantity)
                        {
                            throw new MixERPException(string.Format(CultureInfo.CurrentCulture,
                                Errors.InsufficientStockWarning, Conversion.TryCastInteger(existingQuantity),
                                model.UnitName, model.ItemName));
                        }
                    }
                }

                int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
                int userId = AppUsers.GetCurrent().View.UserId.ToInt();
                long loginId = AppUsers.GetCurrent().View.LoginId.ToLong();

                return StockTransfer.Add(AppUsers.GetCurrentUserDB(), officeId, userId, loginId, valueDate,
                    referenceNumber, statementReference, stockTransferModels);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save inventory transfer entry. {Exception}", ex);
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
                TransactionTypeEnum typeEnum = TransactionTypeEnum.Credit;

                if (Conversion.TryCastString(item[0]).ToString().Equals("Dr"))
                {
                    typeEnum = TransactionTypeEnum.Debit;
                }

                detail.TransferTypeEnum = typeEnum;
                detail.StoreName = Conversion.TryCastString(item[1]);
                detail.ItemCode = Conversion.TryCastString(item[2]);
                detail.ItemName = Conversion.TryCastString(item[3]);
                detail.UnitName = Conversion.TryCastString(item[4]);
                detail.Quantity = Conversion.TryCastInteger(item[5]);

                models.Add(detail);
            }

            return models;
        }
    }
}