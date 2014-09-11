using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public static class Verification
    {
        public static VerificationModel GetVerificationStatus(long transactionMasterId)
        {
            return TransactionGovernor.Verification.VerificationStatus.GetVerificationStatus(transactionMasterId);
        }

        public static bool WithdrawTransaction(long transactionMasterId, int userId, string reason)
        {
            return TransactionGovernor.Verification.Withdrawal.WithdrawTransaction(transactionMasterId, userId, reason);
        }
    }
}