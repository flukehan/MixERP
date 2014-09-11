using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Delivery : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, int agentId, int shipperId, string shippingAddressCode, decimal shippingCharge, int costCenterId, string transactionIds, string attachmentsJSON)
        {
            System.Collections.ObjectModel.Collection<Common.Models.Transactions.StockMasterDetailModel> details = WebControls.StockTransactionFactory.Helpers.CollectionHelper.GetStockMasterDetailCollection(data, storeId);
            System.Collections.ObjectModel.Collection<int> tranIds = new System.Collections.ObjectModel.Collection<int>();

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            System.Collections.ObjectModel.Collection<Common.Models.Core.Attachment> attachments = js.Deserialize<System.Collections.ObjectModel.Collection<Common.Models.Core.Attachment>>(attachmentsJSON);

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

            if (!string.IsNullOrWhiteSpace(transactionIds))
            {
                foreach (var transactionId in transactionIds.Split(','))
                {
                    tranIds.Add(Common.Conversion.TryCastInteger(transactionId));
                }
            }

            return Data.Helpers.Delivery.Add(valueDate, storeId, partyCode, priceTypeId, details, shipperId, shippingAddressCode, shippingCharge, costCenterId, referenceNumber, agentId, statementReference, tranIds, attachments);
        }
    }
}