using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;

namespace MixERP.Net.FrontEnd.Services.Purchase
{
    /// <summary>
    /// Summary description for PurchaseOrder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Order : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, string partyCode, string referenceNumber, string data, string statementReference, string attachmentsJSON)
        {
            Collection<StockMasterDetailModel> details = CollectionHelper.GetStockMasterDetailCollection(data, 0);

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<Attachment> attachments = js.Deserialize<Collection<Attachment>>(attachmentsJSON);

            return BusinessLayer.Transactions.NonGlStockTransaction.Add("Purchase.Order", valueDate, partyCode, 0, details, referenceNumber, statementReference, null, attachments);
        }

    }
}
