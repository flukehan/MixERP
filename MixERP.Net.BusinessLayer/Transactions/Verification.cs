/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.Common.Models.Transactions;

namespace MixERP.Net.BusinessLayer.Transactions
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
