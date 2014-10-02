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
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Purchase.Data.Helpers
{
    public static class NonGlStockTransaction
    {
        public static long Add(string book, DateTime valueDate, int officeId, int userId, long logOnId, string referenceNumber, string statementReference, StockMasterModel stockMaster, Collection<StockMasterDetailModel> details, Collection<int> transactionIdCollection, Collection<AttachmentModel> attachments)
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

            //decimal total = details.Sum(d => (d.Price * d.Quantity));
            //decimal discountTotal = details.Sum(d => d.Discount);
            //decimal taxTotal = details.Sum(d => d.Tax);

            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString()))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        #region NonGLStockMaster

                        string sql = "INSERT INTO transactions.non_gl_stock_master(non_gl_stock_master_id, value_date, book, party_id, price_type_id, login_id, user_id, office_id, reference_number, statement_reference) SELECT nextval(pg_get_serial_sequence('transactions.non_gl_stock_master', 'non_gl_stock_master_id')), @ValueDate, @Book, core.get_party_id_by_party_code(@PartyCode), @PriceTypeId, @LoginId, @UserId, @OfficeId, @ReferenceNumber, @StatementReference; SELECT currval(pg_get_serial_sequence('transactions.non_gl_stock_master', 'non_gl_stock_master_id'));";

                        long nonGlStockMasterId;
                        using (NpgsqlCommand stockMasterRow = new NpgsqlCommand(sql, connection))
                        {
                            stockMasterRow.Parameters.AddWithValue("@ValueDate", valueDate);
                            stockMasterRow.Parameters.AddWithValue("@Book", book);
                            stockMasterRow.Parameters.AddWithValue("@PartyCode", stockMaster.PartyCode);

                            if (stockMaster.PriceTypeId.Equals(0))
                            {
                                stockMasterRow.Parameters.AddWithValue("@PriceTypeId", DBNull.Value);
                            }
                            else
                            {
                                stockMasterRow.Parameters.AddWithValue("@PriceTypeId", stockMaster.PriceTypeId);
                            }

                            stockMasterRow.Parameters.AddWithValue("@LoginId", logOnId);
                            stockMasterRow.Parameters.AddWithValue("@UserId", userId);
                            stockMasterRow.Parameters.AddWithValue("@OfficeId", officeId);
                            stockMasterRow.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);

                            stockMasterRow.Parameters.AddWithValue("@StatementReference", statementReference);

                            nonGlStockMasterId = Conversion.TryCastLong(stockMasterRow.ExecuteScalar());
                        }

                        #region NonGLStockDetails

                        sql = @"INSERT INTO
                                transactions.non_gl_stock_details(non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax)
                                SELECT  @NonGlStockMasterId, core.get_item_id_by_item_code(@ItemCode), @Quantity, core.get_unit_id_by_unit_name(@UnitName), core.get_base_quantity_by_unit_name(@UnitName, @Quantity), core.get_base_unit_id_by_unit_name(@UnitName), @Price, @Discount, @TaxRate, @Tax;";

                        foreach (StockMasterDetailModel model in details)
                        {
                            using (NpgsqlCommand stockMasterDetailRow = new NpgsqlCommand(sql, connection))
                            {
                                stockMasterDetailRow.Parameters.AddWithValue("@NonGlStockMasterId", nonGlStockMasterId);
                                stockMasterDetailRow.Parameters.AddWithValue("@ItemCode", model.ItemCode);
                                stockMasterDetailRow.Parameters.AddWithValue("@Quantity", model.Quantity);
                                stockMasterDetailRow.Parameters.AddWithValue("@UnitName", model.UnitName);
                                stockMasterDetailRow.Parameters.AddWithValue("@Price", model.Price);
                                stockMasterDetailRow.Parameters.AddWithValue("@Discount", model.Discount);
                                stockMasterDetailRow.Parameters.AddWithValue("@TaxRate", model.TaxRate);
                                stockMasterDetailRow.Parameters.AddWithValue("@Tax", model.Tax);

                                stockMasterDetailRow.ExecuteNonQuery();
                            }
                        }

                        #endregion NonGLStockDetails

                        if (transactionIdCollection != null)
                        {
                            if (transactionIdCollection.Count > 0)
                            {
                                foreach (int tranId in transactionIdCollection)
                                {
                                    sql = "INSERT INTO transactions.non_gl_stock_master_relations(order_non_gl_stock_master_id, quotation_non_gl_stock_master_id) SELECT @Id, @RelationId;";
                                    using (NpgsqlCommand relation = new NpgsqlCommand(sql, connection))
                                    {
                                        relation.Parameters.AddWithValue("@Id", nonGlStockMasterId);
                                        relation.Parameters.AddWithValue("@RelationId", tranId);
                                        relation.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        #region Attachment

                        if (attachments != null && attachments.Count > 0)
                        {
                            foreach (AttachmentModel attachment in attachments)
                            {
                                sql = "INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment) SELECT @UserId, @Resource, @ResourceKey, @ResourceId, @OriginalFileName, @FileExtension, @FilePath, @Comment;";
                                using (NpgsqlCommand attachmentCommand = new NpgsqlCommand(sql, connection))
                                {
                                    attachmentCommand.Parameters.AddWithValue("@UserId", userId);
                                    attachmentCommand.Parameters.AddWithValue("@Resource", "transactions.non_gl_stock_master");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceKey", "non_gl_stock_master_id");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceId", nonGlStockMasterId);
                                    attachmentCommand.Parameters.AddWithValue("@OriginalFileName", attachment.OriginalFileName);
                                    attachmentCommand.Parameters.AddWithValue("@FileExtension", Path.GetExtension(attachment.OriginalFileName));
                                    attachmentCommand.Parameters.AddWithValue("@FilePath", attachment.FilePath);
                                    attachmentCommand.Parameters.AddWithValue("@Comment", attachment.Comment);

                                    attachmentCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        #endregion Attachment

                        #endregion NonGLStockMaster

                        transaction.Commit();
                        return nonGlStockMasterId;
                    }
                    catch (NpgsqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}