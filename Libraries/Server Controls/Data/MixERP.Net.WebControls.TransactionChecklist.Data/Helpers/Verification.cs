using MixERP.Net.Common.Models.Transactions;

namespace MixERP.Net.WebControls.TransactionChecklist.Data.Helpers
{
    public static class Verification
    {
        public static VerificationModel GetVerificationStatus(long transactionMasterId)
        {
            return DatabaseLayer.Transactions.Verification.GetVerificationStatus(transactionMasterId);
        }

        public static bool WithdrawTransaction(long transactionMasterId, int userId, string reason)
        {
            return DatabaseLayer.Transactions.Verification.WithdrawTransaction(transactionMasterId, userId, reason);
        }
    }
}