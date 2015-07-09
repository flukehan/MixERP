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

using MixERP.Net.Entities;
using System.Linq;

namespace MixERP.Net.TransactionGovernor.Data.Verification
{
    public static class VerificationStatus
    {
        public static Entities.Models.Transactions.Verification GetVerificationStatus(string catalog, long transactionMasterId, bool isStockTransferRequest)
        {
            string sql = @"SELECT 
                             verification_status_id, 
                             office.get_user_name_by_user_id(verified_by_user_id) AS verified_by_user_name,
                             verified_by_user_id,
                             last_verified_on, 
                             verification_reason
                         FROM transactions.transaction_master
                         WHERE transaction_master_id=@0;";

            if (isStockTransferRequest)
            {
                sql = @"SELECT 
                            CASE WHEN authorized THEN 1 ELSE CASE WHEN withdrawn THEN -1 ELSE 0 END END as verification_status_id,
                            office.get_user_name_by_user_id(CASE WHEN withdrawn THEN withdrawn_by_user_id ELSE authorized_by_user_id END) AS verified_by_user_name,
                            CASE WHEN withdrawn THEN withdrawn_by_user_id ELSE authorized_by_user_id END AS verified_by_user_id,
                            CASE WHEN withdrawn THEN withdrawn_on ELSE authorized_on END AS last_verified_on,
                            CASE WHEN withdrawn THEN withdrawal_reason ELSE '' END AS verification_reason
                        FROM transactions.inventory_transfer_requests
                        WHERE inventory_transfer_request_id = @0;";
            }


            return Factory.Get<Entities.Models.Transactions.Verification>(catalog, sql, transactionMasterId).SingleOrDefault();
        }
    }
}