/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.Common.Models.Transactions;
using System.Data;

namespace MixERP.Net.TransactionGovernor.Verification
{
    public static class VerificationStatus
    {
        public static VerificationModel GetVerificationStatus(long transactionMasterId)
        {
            if (transactionMasterId <= 0)
            {
                return null;
            }

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