using MixERP.Net.Common;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Return
    {
        public static long PostTransaction(long transactionMasterId, DateTime valueDate, int officeId, int userId, long loginId, int storeId, string partyCode, int priceTypeId, string referenceNumber, string statementReference, Collection<StockMasterDetailModel> details, Collection<AttachmentModel> attachments)
        {
            string detail = CreateStockMasterDetailParameter(details);
            string attachment = CreateAttachmentModelParameter(attachments);

            string sql = string.Format("SELECT * FROM transactions.post_sales_return(@TransactionMasterId, @OfficeId, @UserId, @LoginId, @ValueDate, @StoreId, @PartyCode, @PriceTypeId, @ReferenceNumber, @StatementReference, ARRAY[{0}], ARRAY[{1}]);", detail, attachment);

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@LoginId", loginId);
                command.Parameters.AddWithValue("@ValueDate", valueDate);
                command.Parameters.AddWithValue("@StoreId", storeId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                command.Parameters.AddWithValue("@PriceTypeId", priceTypeId);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);

                command.Parameters.AddRange(AddStockMasterDetailParameter(details).ToArray());
                command.Parameters.AddRange(AddAttachmentParameter(attachments).ToArray());

                long tranId = Conversion.TryCastLong(DbOperations.GetScalarValue(command));
                MixERP.Net.TransactionGovernor.Autoverification.Autoverify.Pass(tranId);
                return tranId;
            }
        }

        private static IEnumerable<NpgsqlParameter> AddStockMasterDetailParameter(Collection<StockMasterDetailModel> details)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (details != null)
            {
                for (int i = 0; i < details.Count; i++)
                {
                    collection.Add(new NpgsqlParameter("@StoreId" + i, details[i].StoreId));
                    collection.Add(new NpgsqlParameter("@ItemCode" + i, details[i].ItemCode));
                    collection.Add(new NpgsqlParameter("@Quantity" + i, details[i].Quantity));
                    collection.Add(new NpgsqlParameter("@UnitName" + i, details[i].UnitName));
                    collection.Add(new NpgsqlParameter("@Price" + i, details[i].Price));
                    collection.Add(new NpgsqlParameter("@Discount" + i, details[i].Discount));
                    collection.Add(new NpgsqlParameter("@TaxRate" + i, details[i].TaxRate));
                    collection.Add(new NpgsqlParameter("@Tax" + i, details[i].Tax));
                }
            }

            return collection;
        }

        private static IEnumerable<NpgsqlParameter> AddAttachmentParameter(Collection<AttachmentModel> attachments)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (attachments != null)
            {
                for (int i = 0; i < attachments.Count; i++)
                {
                    collection.Add(new NpgsqlParameter("@Comment" + i, attachments[i].Comment));
                    collection.Add(new NpgsqlParameter("@FilePath" + i, attachments[i].FilePath));
                    collection.Add(new NpgsqlParameter("@OriginalFileName" + i, attachments[i].OriginalFileName));
                }
            }

            return collection;
        }

        private static string CreateStockMasterDetailParameter(Collection<StockMasterDetailModel> details)
        {
            if (details == null)
            {
                return "NULL::stock_detail_type";
            }

            Collection<string> detailCollection = new Collection<string>();
            for (int i = 0; i < details.Count; i++)
            {
                detailCollection.Add(string.Format("ROW(@StoreId{0}, @ItemCode{0}, @Quantity{0}, @UnitName{0},@Price{0}, @Discount{0}, @TaxRate{0}, @Tax{0})::stock_detail_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", detailCollection);
        }

        private static string CreateAttachmentModelParameter(Collection<AttachmentModel> attachments)
        {
            if (attachments == null)
            {
                return "NULL::attachment_type";
            }

            Collection<string> attachmentCollection = new Collection<string>();

            for (int i = 0; i < attachments.Count; i++)
            {
                attachmentCollection.Add(string.Format("ROW(@Comment{0}, @FilePath{0}, @OriginalFileName{0})::attachment_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", attachmentCollection);
        }
    }
}