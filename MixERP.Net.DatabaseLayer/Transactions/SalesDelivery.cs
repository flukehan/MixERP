/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using Npgsql;
using MixERP.Net.Common.Models.Core;

namespace MixERP.Net.DatabaseLayer.Transactions
{
    public static class SalesDelivery
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public static long Add(DateTime valueDate, int officeId, int userId, long logOnId, int costCenterId, string referenceNumber, string statementReference, StockMasterModel stockMaster, Collection<StockMasterDetailModel> details, Collection<int> transactionIdCollection, Collection<Attachment> attachments)
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

            if (stockMaster.AgentId.Equals(0))
            {
                return 0;
            }

            decimal total = details.Sum(d => (d.Price * d.Quantity));
            decimal discountTotal = details.Sum(d => d.Discount);
            decimal taxTotal = details.Sum(d => d.Tax);

            const string creditInvariantParameter = "Sales.Receivables";
            const string salesInvariantParameter = "Sales";
            const string salesTaxInvariantParamter = "Sales.Tax";
            const string salesDiscountInvariantParameter = "Sales.Discount";


            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString()))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {

                        #region TransactionMaster
                        string sql = "INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) SELECT nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), transactions.get_new_transaction_counter(@ValueDate), transactions.get_transaction_code(@ValueDate, @OfficeId, @UserId, @LogOnId), @Book, @ValueDate, @UserId, @LogOnId, @OfficeId, @CostCenterId, @ReferenceNumber, @StatementReference;SELECT currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));";
                        long transactionMasterId;
                        using (NpgsqlCommand tm = new NpgsqlCommand(sql, connection))
                        {
                            tm.Parameters.AddWithValue("@ValueDate", valueDate);
                            tm.Parameters.AddWithValue("@OfficeId", officeId);
                            tm.Parameters.AddWithValue("@UserId", userId);
                            tm.Parameters.AddWithValue("@LogOnId", logOnId);
                            tm.Parameters.AddWithValue("@Book", "Sales.Delivery");
                            tm.Parameters.AddWithValue("@CostCenterId", costCenterId);
                            tm.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                            tm.Parameters.AddWithValue("@StatementReference", statementReference);

                            transactionMasterId = Conversion.TryCastLong(tm.ExecuteScalar());
                        }

                        #region TransactionDetails
                        sql = "INSERT INTO transactions.transaction_details(transaction_master_id, tran_type, account_id, statement_reference, amount) SELECT @TransactionMasterId, @TranType, core.get_account_id_by_parameter(@ParameterName), @StatementReference, @Amount;";

                        using (NpgsqlCommand salesRow = new NpgsqlCommand(sql, connection))
                        {
                            salesRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                            salesRow.Parameters.AddWithValue("@TranType", "Cr");
                            salesRow.Parameters.AddWithValue("@ParameterName", salesInvariantParameter);
                            salesRow.Parameters.AddWithValue("@StatementReference", statementReference);
                            salesRow.Parameters.AddWithValue("@Amount", total);

                            salesRow.ExecuteNonQuery();
                        }

                        if (taxTotal > 0)
                        {
                            using (NpgsqlCommand taxRow = new NpgsqlCommand(sql, connection))
                            {
                                taxRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                                taxRow.Parameters.AddWithValue("@TranType", "Cr");
                                taxRow.Parameters.AddWithValue("@ParameterName", salesTaxInvariantParamter);
                                taxRow.Parameters.AddWithValue("@StatementReference", statementReference);
                                taxRow.Parameters.AddWithValue("@Amount", taxTotal);
                                taxRow.ExecuteNonQuery();
                            }
                        }

                        if (discountTotal > 0)
                        {
                            using (NpgsqlCommand discountRow = new NpgsqlCommand(sql, connection))
                            {
                                discountRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                                discountRow.Parameters.AddWithValue("@TranType", "Dr");
                                discountRow.Parameters.AddWithValue("@ParameterName", salesDiscountInvariantParameter);
                                discountRow.Parameters.AddWithValue("@StatementReference", statementReference);
                                discountRow.Parameters.AddWithValue("@Amount", discountTotal);
                                discountRow.ExecuteNonQuery();
                            }
                        }

                        using (NpgsqlCommand creditRow = new NpgsqlCommand(sql, connection))
                        {
                            creditRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                            creditRow.Parameters.AddWithValue("@TranType", "Dr");
                            creditRow.Parameters.AddWithValue("@ParameterName", creditInvariantParameter);
                            creditRow.Parameters.AddWithValue("@StatementReference", statementReference);
                            creditRow.Parameters.AddWithValue("@Amount", total - discountTotal + taxTotal + stockMaster.ShippingCharge);
                            creditRow.ExecuteNonQuery();
                        }

                        if (stockMaster.ShippingCharge > 0)
                        {
                            sql = "INSERT INTO transactions.transaction_details(transaction_master_id, tran_type, account_id, statement_reference, amount) SELECT @TransactionMasterId, @TranType, core.get_account_id_by_shipper_id(@ShipperId), @StatementReference, @Amount;";

                            using (NpgsqlCommand shippingChargeRow = new NpgsqlCommand(sql, connection))
                            {
                                shippingChargeRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                                shippingChargeRow.Parameters.AddWithValue("@TranType", "Cr");
                                shippingChargeRow.Parameters.AddWithValue("@ShipperId", stockMaster.ShipperId);
                                shippingChargeRow.Parameters.AddWithValue("@StatementReference", statementReference);
                                shippingChargeRow.Parameters.AddWithValue("@Amount", stockMaster.ShippingCharge);

                                shippingChargeRow.ExecuteNonQuery();
                            }
                        }
                        #endregion
                        #endregion

                        #region StockMaster

                        sql = "INSERT INTO transactions.stock_master(stock_master_id, transaction_master_id, party_id, agent_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id) SELECT nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id')), @TransactionMasterId, core.get_party_id_by_party_code(@PartyCode), @AgentId, @PriceTypeId, @IsCredit, @ShipperId, core.get_shipping_address_id_by_shipping_address_code(@ShippingAddressCode, core.get_party_id_by_party_code(@PartyCode)), @ShippingCharge, @StoreId; SELECT currval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));";

                        long stockMasterId;
                        using (NpgsqlCommand stockMasterRow = new NpgsqlCommand(sql, connection))
                        {
                            stockMasterRow.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                            stockMasterRow.Parameters.AddWithValue("@PartyCode", stockMaster.PartyCode);
                            stockMasterRow.Parameters.AddWithValue("@AgentId", stockMaster.AgentId);
                            stockMasterRow.Parameters.AddWithValue("@PriceTypeId", stockMaster.PriceTypeId);
                            stockMasterRow.Parameters.AddWithValue("@IsCredit", true);

                            if (stockMaster.ShipperId.Equals(0))
                            {
                                stockMasterRow.Parameters.AddWithValue("@ShipperId", DBNull.Value);
                            }
                            else
                            {
                                stockMasterRow.Parameters.AddWithValue("@ShipperId", stockMaster.ShipperId);
                            }

                            stockMasterRow.Parameters.AddWithValue("@ShippingAddressCode", stockMaster.ShippingAddressCode);
                            stockMasterRow.Parameters.AddWithValue("@ShippingCharge", stockMaster.ShippingCharge);

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
                                stockMasterDetailRow.Parameters.AddWithValue("@TranType", "Cr");
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

                        #endregion

                        #endregion

                        if (transactionIdCollection != null)
                        {
                            if (transactionIdCollection.Count > 0)
                            {
                                foreach (int tranId in transactionIdCollection)
                                {
                                    sql = "INSERT INTO transactions.stock_master_non_gl_relations(stock_master_id, non_gl_stock_master_id) SELECT @Id, @RelationId;";
                                    using (NpgsqlCommand relation = new NpgsqlCommand(sql, connection))
                                    {
                                        relation.Parameters.AddWithValue("@Id", transactionMasterId);
                                        relation.Parameters.AddWithValue("@RelationId", tranId);
                                        relation.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        #region Attachment

                        if (attachments != null && attachments.Count > 0)
                        {
                            foreach (Attachment attachment in attachments)
                            {
                                sql = "INSERT INTO transactions.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment) SELECT @UserId, @Resource, @ResourceKey, @ResourceId, @OriginalFileName, @FileExtension, @FilePath, @Comment;";
                                using (NpgsqlCommand attachmentCommand = new NpgsqlCommand(sql, connection))
                                {
                                    attachmentCommand.Parameters.AddWithValue("@UserId", userId);
                                    attachmentCommand.Parameters.AddWithValue("@Resource", "transactions.transaction_master");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceKey", "transaction_master_id");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceId", stockMasterId);
                                    attachmentCommand.Parameters.AddWithValue("@OriginalFileName", attachment.OriginalFileName);
                                    attachmentCommand.Parameters.AddWithValue("@FileExtension", Path.GetExtension(attachment.OriginalFileName));
                                    attachmentCommand.Parameters.AddWithValue("@FilePath", attachment.FilePath);
                                    attachmentCommand.Parameters.AddWithValue("@Comment", attachment.Comment);

                                    attachmentCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        #endregion

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