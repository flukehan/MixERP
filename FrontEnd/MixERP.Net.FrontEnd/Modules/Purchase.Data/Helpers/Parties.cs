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
using MixERP.Net.DbFactory;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using Npgsql;

namespace MixERP.Net.Core.Modules.Purchase.Data.Helpers
{
    public static class Parties
    {
        public static string GetEmailAddress(TranBook tranBook, SubTranBook subTranBook, long tranId)
        {
            string sql = "SELECT core.get_email_address_by_party_id(party_id) FROM transactions.non_gl_stock_master WHERE non_gl_stock_master_id=@TranId;";

            if (subTranBook == SubTranBook.Direct || subTranBook == SubTranBook.Receipt || subTranBook == SubTranBook.Invoice || subTranBook == SubTranBook.Return)
            {
                sql = "SELECT core.get_email_address_by_party_id(party_id) FROM transactions.stock_master WHERE transaction_master_id=@TranId;";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TranId", tranId);

                return Conversion.TryCastString(DbOperation.GetScalarValue(command));
            }
        }
    }
}