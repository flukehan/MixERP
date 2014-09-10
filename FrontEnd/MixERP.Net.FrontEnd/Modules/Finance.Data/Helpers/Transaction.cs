using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class Transaction
    {
        public static long Add(DateTime valueDate, string referenceNumber, int costCenterId, Collection<JournalDetailsModel> details, Collection<Common.Models.Core.Attachment> attachments)
        {
            long transactionMasterId = DatabaseLayer.Transactions.Transaction.Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, details, attachments);
            DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }

        public static decimal GetExchangeRate(int officeId, string currencyCode)
        {
            return DatabaseLayer.Transactions.Transaction.GetExchangeRate(officeId, currencyCode);
        }
    }
}