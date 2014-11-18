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
using MixERP.Net.Core.Modules.Inventory.Data.Helpers;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MixERP.Net.Core.Modules.Inventory.Data.Transactions
{
    public static class StockTransfer
    {
        public static long Add(int officeId, int userId, long loginId, DateTime valueDate, string referenceNumber, string statementReference, Collection<StockAdjustmentModel> details)
        {
            string detailParameter = ParameterHelper.CreateStockTransferModelParameter(details);
            string sql = string.Format("SELECT * FROM transactions.post_stock_journal(@OfficeId, @UserId, @LoginId, @ValueDate, @ReferenceNumber, @StatementReference, ARRAY[{0}]);", detailParameter);

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@LoginId", loginId);
                command.Parameters.AddWithValue("@ValueDate", valueDate);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);
                command.Parameters.AddRange(ParameterHelper.AddStockTransferModelParameter(details).ToArray());

                long tranId = Conversion.TryCastLong(DbOperations.GetScalarValue(command));
                TransactionGovernor.Autoverification.Autoverify.PassTransactionMasterId(tranId);
                return tranId;
            }
        }
    }
}