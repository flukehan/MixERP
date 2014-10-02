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
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MixERP.Net.Core.Modules.Purchase.Data.Helpers
{
    public static class GRN
    {
        public static long Add(DateTime valueDate, int storeId, string partyCode, Collection<StockMasterDetailModel> details, int costCenterId, string referenceNumber, string statementReference, Collection<int> transactionIdCollection, Collection<AttachmentModel> attachments)
        {
            StockMasterModel stockMaster = new StockMasterModel();

            stockMaster.PartyCode = partyCode;
            stockMaster.StoreId = storeId;
            stockMaster.IsCredit = true;

            if (!string.IsNullOrWhiteSpace(statementReference))
            {
                statementReference = statementReference.Replace("&nbsp;", " ").Trim();
            }

            long transactionMasterId = Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, statementReference, stockMaster, details, transactionIdCollection, attachments);
            MixERP.Net.TransactionGovernor.Autoverification.Autoverify.PassTransactionMasterId(transactionMasterId);
            return transactionMasterId;
        }

        private static long Add(DateTime valueDate, int officeId, int userId, long logOnId, int costCenterId, string referenceNumber, string statementReference, StockMasterModel stockMaster, Collection<StockMasterDetailModel> details, Collection<int> transactionIdCollection, Collection<AttachmentModel> attachments)
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

            decimal total = details.Sum(d => (d.Price * d.Quantity));
            decimal discountTotal = details.Sum(d => d.Discount);
            decimal taxTotal = details.Sum(d => d.Tax);

            const string creditInvariantParameter = "Purchase.Payables";
            const string purchaseInvariantParameter = "Purchase";
            const string purchaseTaxInvariantParamter = "Purchase.Tax";
            const string purchaseDiscountInvariantParameter = "Purchase.Discount";

            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString()))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        #region TransactionMaster

                        string sql = "INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) " +
                                     "SELECT nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), transactions.get_new_transaction_counter(@ValueDate), transactions.get_transaction_code(@ValueDate, @OfficeId, @UserId, @LogOnId), @Book, @ValueDate, @UserId, @LogOnId, @OfficeId, @CostCenterId, @ReferenceNumber, @StatementReference;" +
                                     "SELECT currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));";

                        long transactionMasterId;

                        using (NpgsqlCommand tm = new NpgsqlCommand(sql, connection))
                        {
                            tm.Parameters.AddWithValue("@ValueDate", valueDate);
                            tm.Parameters.AddWithValue("@OfficeId", officeId);
                            tm.Parameters.AddWithValue("@UserId", userId);
                            tm.Parameters.AddWithValue("@LogOnId", logOnId);
                            tm.Parameters.AddWithValue("@Book", "Purchase.Receipt");
                            tm.Parameters.AddWithValue("@CostCenterId", costCenterId);
                            tm.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                            tm.Parameters.AddWithValue("@StatementReference", statementReference);

                            transactionMasterId = Conversion.TryCastLong(tm.ExecuteScalar());
                        }

                        #region TransactionDetails

                        sql = "INSERT INTO transactions.transaction_details(transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) " +
                              "SELECT @TransactionMasterId, @TranType, core.get_account_id_by_parameter(@ParameterName), @StatementReference, @CashRepositoryId, transactions.get_default_currency_code_by_office_id(@OfficeId), @Amount, transactions.get_default_currency_code_by_office_id(@OfficeId), 1, @Amount;";

                        using (NpgsqlCommand purchaseRow = new NpgsqlCommand(sql, connection))
                        {
                            purchaseRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                            purchaseRow.Parameters.AddWithValue("@TranType", "Dr");
                            purchaseRow.Parameters.AddWithValue("@ParameterName", purchaseInvariantParameter);
                            purchaseRow.Parameters.AddWithValue("@StatementReference", statementReference);
                            purchaseRow.Parameters.AddWithValue("@CashRepositoryId", DBNull.Value);
                            purchaseRow.Parameters.AddWithValue("@OfficeId", officeId);
                            purchaseRow.Parameters.AddWithValue("@Amount", total);

                            purchaseRow.ExecuteNonQuery();
                        }

                        if (taxTotal > 0)
                        {
                            using (NpgsqlCommand taxRow = new NpgsqlCommand(sql, connection))
                            {
                                taxRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                                taxRow.Parameters.AddWithValue("@TranType", "Dr");
                                taxRow.Parameters.AddWithValue("@ParameterName", purchaseTaxInvariantParamter);
                                taxRow.Parameters.AddWithValue("@StatementReference", statementReference);
                                taxRow.Parameters.AddWithValue("@CashRepositoryId", DBNull.Value);
                                taxRow.Parameters.AddWithValue("@OfficeId", officeId);
                                taxRow.Parameters.AddWithValue("@Amount", taxTotal);
                                taxRow.ExecuteNonQuery();
                            }
                        }

                        if (discountTotal > 0)
                        {
                            using (NpgsqlCommand discountRow = new NpgsqlCommand(sql, connection))
                            {
                                discountRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                                discountRow.Parameters.AddWithValue("@TranType", "Cr");
                                discountRow.Parameters.AddWithValue("@ParameterName", purchaseDiscountInvariantParameter);
                                discountRow.Parameters.AddWithValue("@StatementReference", statementReference);
                                discountRow.Parameters.AddWithValue("@CashRepositoryId", DBNull.Value);
                                discountRow.Parameters.AddWithValue("@OfficeId", officeId);
                                discountRow.Parameters.AddWithValue("@Amount", discountTotal);
                                discountRow.ExecuteNonQuery();
                            }
                        }

                        using (NpgsqlCommand creditRow = new NpgsqlCommand(sql, connection))
                        {
                            creditRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                            creditRow.Parameters.AddWithValue("@TranType", "Cr");
                            creditRow.Parameters.AddWithValue("@ParameterName", creditInvariantParameter);
                            creditRow.Parameters.AddWithValue("@StatementReference", statementReference);
                            creditRow.Parameters.AddWithValue("@CashRepositoryId", DBNull.Value);
                            creditRow.Parameters.AddWithValue("@OfficeId", officeId);
                            creditRow.Parameters.AddWithValue("@Amount", total - discountTotal + taxTotal);
                            creditRow.ExecuteNonQuery();
                        }

                        #endregion TransactionDetails

                        #endregion TransactionMaster

                        #region StockMaster

                        sql = "INSERT INTO transactions.stock_master(stock_master_id, transaction_master_id, party_id, is_credit, store_id) " +
                              "SELECT nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id')), @TransactionMasterId, core.get_party_id_by_party_code(@PartyCode), true, @StoreId; " +
                              "SELECT currval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));";

                        long stockMasterId;
                        using (NpgsqlCommand stockMasterRow = new NpgsqlCommand(sql, connection))
                        {
                            stockMasterRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                            stockMasterRow.Parameters.AddWithValue("@PartyCode", stockMaster.PartyCode);
                            stockMasterRow.Parameters.AddWithValue("@StoreId", stockMaster.StoreId);

                            stockMasterId = Conversion.TryCastLong(stockMasterRow.ExecuteScalar());
                        }

                        #region StockDetails

                        sql = @"INSERT INTO
                                transactions.stock_details(stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax)
                                SELECT  @StockMasterId, @TranType, @StoreId, core.get_item_id_by_item_code(@ItemCode), @Quantity, core.get_unit_id_by_unit_name(@UnitName), core.get_base_quantity_by_unit_name(@UnitName, @Quantity), core.get_base_unit_id_by_unit_name(@UnitName), @Price, @Discount, @TaxRate, @Tax;";

                        foreach (StockMasterDetailModel model in details)
                        {
                            using (NpgsqlCommand stockMasterDetailRow = new NpgsqlCommand(sql, connection))
                            {
                                stockMasterDetailRow.Parameters.AddWithValue("@StockMasterId", stockMasterId);
                                stockMasterDetailRow.Parameters.AddWithValue("@TranType", "Dr");
                                stockMasterDetailRow.Parameters.AddWithValue("@StoreId", model.StoreId);
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

                        #endregion StockDetails

                        #endregion StockMaster

                        if (transactionIdCollection != null)
                        {
                            if (transactionIdCollection.Count > 0)
                            {
                                foreach (int tranId in transactionIdCollection)
                                {
                                    sql = "INSERT INTO transactions.stock_master_non_gl_relations(stock_master_id, non_gl_stock_master_id) SELECT @Id, @RelationId;";
                                    using (NpgsqlCommand relation = new NpgsqlCommand(sql, connection))
                                    {
                                        relation.Parameters.AddWithValue("@Id", stockMasterId);
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
                                    attachmentCommand.Parameters.AddWithValue("@Resource", "transactions.transaction_master");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceKey", "transaction_master_id");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceId", transactionMasterId);
                                    attachmentCommand.Parameters.AddWithValue("@OriginalFileName", attachment.OriginalFileName);
                                    attachmentCommand.Parameters.AddWithValue("@FileExtension", Path.GetExtension(attachment.OriginalFileName));
                                    attachmentCommand.Parameters.AddWithValue("@FilePath", attachment.FilePath);
                                    attachmentCommand.Parameters.AddWithValue("@Comment", attachment.Comment);

                                    attachmentCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        #endregion Attachment

                        transaction.Commit();
                        return transactionMasterId;
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