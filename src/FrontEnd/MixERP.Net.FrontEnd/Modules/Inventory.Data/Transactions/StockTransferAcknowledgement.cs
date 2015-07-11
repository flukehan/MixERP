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

using PetaPoco;

namespace MixERP.Net.Core.Modules.Inventory.Data.Transactions
{
    public static class StockTransferAcknowledgement
    {
        public static long Receive(string catalog, int officeId, int userId, long loginId, long requestId)
        {
            const string sql =
                "SELECT * FROM transactions.post_stock_transfer_acknowledgement(@0::integer, @1::integer, @2::bigint, @3::bigint);";
            return Factory.Scalar<long>(catalog, sql, officeId, userId, loginId, requestId);
        }
    }
}