using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using MixERP.Net.BusinessLayer.Transactions;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;

namespace MixERP.Net.FrontEnd.Services.Sales
{
    /// <summary>
    /// Summary description for DirectSales
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DirectSales : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string transactionType, int agentId, int shipperId, string shippingAddressCode, decimal shippingCharge, int cashRepositoryId, int costCenterId, string transactionIds, string attachmentsJSON)
        {
            Collection<StockMasterDetailModel> details = this.GetDetails(data, storeId);
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

            bool isCredit = !transactionType.Equals("Cash");

            return BusinessLayer.Transactions.DirectSales.Add(valueDate, storeId, isCredit, partyCode,
                agentId, priceTypeId, details, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId,
                costCenterId, referenceNumber, statementReference, attachments);
        }

        public Collection<StockMasterDetailModel> GetDetails(string json, int storeId)
        {
            Collection<StockMasterDetailModel> details = new Collection<StockMasterDetailModel>();
            var jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                StockMasterDetailModel detail = new StockMasterDetailModel();
                detail.ItemCode = item[0];
                detail.Quantity = Conversion.TryCastInteger(item[2]);
                detail.UnitName = item[3];
                detail.Price = Conversion.TryCastDecimal(item[4]);
                detail.Discount = Conversion.TryCastDecimal(item[6]);
                detail.TaxRate = Conversion.TryCastDecimal(item[8]);
                detail.Tax = Conversion.TryCastDecimal(item[9]);
                detail.StoreId = storeId;

                details.Add(detail);
            }

            return details;
        }

    }
}
