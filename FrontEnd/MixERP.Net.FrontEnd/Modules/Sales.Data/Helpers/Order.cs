using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Order
    {
        public static long Add(DateTime valueDate, string partyCode, int priceTypeId, Collection<StockMasterDetailModel> details, string referenceNumber, string statementReference, Collection<int> transactionIdCollection, Collection<Attachment> attachments)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.PriceTypeId = priceTypeId;

            long nonGlStockMasterId = DatabaseLayer.Transactions.NonGlStockTransaction.Add("Sales.Order", valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), referenceNumber, statementReference, stockMaster, details, transactionIdCollection, attachments);
            return nonGlStockMasterId;
        }
    }
}