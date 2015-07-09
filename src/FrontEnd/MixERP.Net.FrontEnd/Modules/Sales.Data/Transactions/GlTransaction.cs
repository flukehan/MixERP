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
using MixERP.Net.Core.Modules.Sales.Data.Data;
using MixERP.Net.DbFactory;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace MixERP.Net.Core.Modules.Sales.Data.Transactions
{
    internal static class GlTransaction
    {
        public static long Add(string catalog, string bookName, DateTime valueDate, int officeId, int userId, long loginId, int costCenterId, string referenceNumber, string statementReference, StockMaster stockMaster, Collection<StockDetail> details, Collection<Attachment> attachments, bool nonTaxable, Collection<long> tranIds)
        {
            if (stockMaster == null)
            {
                return 0;
            }

            if (details == null)
            {
                return 0;
            }

            if (details.Count.Equals(0))
            {
                return 0;
            }

            string detail = StockMasterDetailHelper.CreateStockMasterDetailParameter(details);
            string attachment = AttachmentHelper.CreateAttachmentModelParameter(attachments);
            string ids = "NULL::bigint";

            if (tranIds != null && tranIds.Count > 0)
            {
                ids = string.Join(",", tranIds);
            }

            string sql = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM transactions.post_sales(@BookName::national character varying(48), @OfficeId::integer, @UserId::integer, @LoginId::bigint, @ValueDate::date, @CostCenterId::integer, @ReferenceNumber::national character varying(24), @StatementReference::text, @IsCredit::boolean, @PaymentTermId::integer, @PartyCode::national character varying(12), @PriceTypeId::integer, @SalespersonId::integer, @ShipperId::integer, @ShippingAddressCode::national character varying(12), @StoreId::integer, @NonTaxable::boolean, ARRAY[{0}], ARRAY[{1}], ARRAY[{2}])", detail, attachment, ids);

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@BookName", bookName);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@LoginId", loginId);
                command.Parameters.AddWithValue("@ValueDate", valueDate);
                command.Parameters.AddWithValue("@CostCenterId", costCenterId);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);

                command.Parameters.AddWithValue("@IsCredit", stockMaster.IsCredit);

                if (stockMaster.PaymentTermId.Equals(0))
                {
                    command.Parameters.AddWithValue("@PaymentTermId", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@PaymentTermId", stockMaster.PaymentTermId);
                }

                command.Parameters.AddWithValue("@PartyCode", stockMaster.PartyCode);
                command.Parameters.AddWithValue("@PriceTypeId", stockMaster.PriceTypeId);
                command.Parameters.AddWithValue("@SalespersonId", stockMaster.SalespersonId);
                command.Parameters.AddWithValue("@ShipperId", stockMaster.ShipperId);
                command.Parameters.AddWithValue("@ShippingAddressCode", stockMaster.ShippingAddressCode);
                command.Parameters.AddWithValue("@StoreId", stockMaster.StoreId);
                command.Parameters.AddWithValue("@NonTaxable", nonTaxable);

                command.Parameters.AddRange(StockMasterDetailHelper.AddStockMasterDetailParameter(details).ToArray());
                command.Parameters.AddRange(AttachmentHelper.AddAttachmentParameter(attachments).ToArray());

                long tranId = Conversion.TryCastLong(DbOperation.GetScalarValue(catalog, command));
                return tranId;
            }
        }
    }
}