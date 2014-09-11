using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Verification
{
    public static class Withdrawal
    {
        public static bool WithdrawTransaction(long transactionMasterId, int userId, string reason)
        {
            short status = VerificationDomain.GetVerification(VerificationType.Withdrawn);

            return Data.Verification.Withdrawal.WithdrawTransaction(transactionMasterId, userId, reason, status);
        }
    }
}