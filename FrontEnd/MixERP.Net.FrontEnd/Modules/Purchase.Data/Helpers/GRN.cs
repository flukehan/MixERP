using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Purchase.Data.Helpers
{
    public static class GRN
    {
        public static long Add(DateTime valueDate, int storeId, string partyCode, Collection<StockMasterDetailModel> details, int costCenterId, string referenceNumber, string statementReference, Collection<int> transactionIdCollection, Collection<Attachment> attachments)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.StoreId = storeId;
            stockMaster.IsCredit = true;

            if (!string.IsNullOrWhiteSpace(statementReference))
            {
                statementReference = statementReference.Replace("&nbsp;", " ").Trim();
            }

            long transactionMasterId = DatabaseLayer.Transactions.GRN.Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details, transactionIdCollection, attachments);
            DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}