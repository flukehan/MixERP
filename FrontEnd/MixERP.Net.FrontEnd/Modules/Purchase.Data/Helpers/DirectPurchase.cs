using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Purchase.Data.Helpers
{
    public static class DirectPurchase
    {
        public static long Add(DateTime valueDate, int storeId, bool isCredit, string partyCode, Collection<StockMasterDetailModel> details, int cashRepositoryId, int costCenterId, string referenceNumber, string statementReference, Collection<Attachment> attachments)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.StoreId = storeId;
            stockMaster.CashRepositoryId = cashRepositoryId;
            stockMaster.IsCredit = isCredit;

            if (!string.IsNullOrWhiteSpace(statementReference))
            {
                statementReference = statementReference.Replace("&nbsp;", " ").Trim();
            }

            long transactionMasterId = DatabaseLayer.Transactions.DirectPurchase.Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details, attachments);
            DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}