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
using System.Web.Services;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;

namespace MixERP.Net.Core.Modules.Purchase.Services
{
    /// <summary>
    ///     Summary description for PurchaseOrder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the
    // following line.
    [System.Web.Script.Services.ScriptService]
    public class Order : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, string partyCode, string referenceNumber, string data, string statementReference, string attachmentsJSON)
        {
            Collection<StockDetail> details = CollectionHelper.GetStockMasterDetailCollection(data, 0);

            Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);

            int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
            int userId = CurrentUser.GetSignInView().UserId.ToInt();
            long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

            return Data.Transactions.Order.Add(officeId, userId, loginId, "Purchase.Order", valueDate, partyCode, 0, details, referenceNumber, statementReference, null, attachments);
        }
    }
}