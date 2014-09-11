using MixERP.Net.Common;
using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Verification
{
    public static class VerificationStatus
    {
        public static VerificationModel GetVerificationStatus(long transactionMasterId)
        {
            VerificationModel model = new VerificationModel();
            DataRow row = Data.Verification.VerificationStatus.GetVerificationStatusDataRow(transactionMasterId);

            model.Verification = Conversion.TryCastShort(row["verification_status_id"]);
            model.VerifierUserId = Conversion.TryCastInteger(row["verified_by_user_id"]);
            model.VerifierName = Conversion.TryCastString(row["verified_by_user_name"]);
            model.VerifiedDate = Conversion.TryCastDate(row["last_verified_on"]);
            model.VerificationReason = Conversion.TryCastString(row["verification_reason"]);

            return model;
        }
    }
}