using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.TransactionGovernor.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Return : System.Web.Services.WebService
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

            return Data.Helpers.Return.PostTransaction(tranId, valueDate, officeId, userId, loginId, storeId, partyCode, priceTypeId, referenceNumber, statementReference, details, attachments);
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