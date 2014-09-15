using MixERP.Net.TransactionGovernor.Transactions;
using System;
using System.ComponentModel;
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
        [WebMethod]
        public long Save(long tranId, DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string attachmentsJSON)
        {
            if (!StockTransaction.IsValidStockTransaction(tranId))
            {
                throw new InvalidOperationException(Resources.Warnings.AccessIsDenied);
            }

            if (!StockTransaction.IsValidPartyByTransactionMasterId(tranId, partyCode))
            {
                throw new InvalidOperationException(Resources.Warnings.InvalidParty);
            }

            return tranId;
        }
    }
}