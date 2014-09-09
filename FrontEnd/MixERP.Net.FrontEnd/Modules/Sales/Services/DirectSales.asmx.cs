using MixERP.Net.Common;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services
{
    /// <summary>
    /// Summary description for DirectSales
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class DirectSales : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string transactionType, int agentId, int shipperId, string shippingAddressCode, decimal shippingCharge, int cashRepositoryId, int costCenterId, string transactionIds, string attachmentsJSON)
        {
            Collection<StockMasterDetailModel> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);
            Collection<int> tranIds = new Collection<int>();

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<Attachment> attachments = js.Deserialize<Collection<Attachment>>(attachmentsJSON);

            if (!string.IsNullOrWhiteSpace(transactionIds))
            {
                foreach (var transactionId in transactionIds.Split(','))
                {
                    tranIds.Add(Conversion.TryCastInteger(transactionId));
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

            foreach (StockMasterDetailModel model in details)
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

            return Data.Helpers.DirectSales.Add(valueDate, storeId, isCredit, partyCode,
                agentId, priceTypeId, details, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId,
                costCenterId, referenceNumber, statementReference, attachments);
        }
    }
}