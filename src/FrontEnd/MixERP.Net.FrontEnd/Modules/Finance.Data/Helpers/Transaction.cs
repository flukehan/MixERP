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

using System;
using System.Collections.ObjectModel;
using System.Linq;
using MixERP.Net.Common;
using MixERP.Net.DbFactory;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.i18n;
using MixERP.Net.i18n.Resources;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class Transaction
    {
        public static decimal GetExchangeRate(string catalog, int officeId, string currencyCode)
        {
            const string sql = "SELECT transactions.get_exchange_rate(@OfficeId, @CurrencyCode);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@CurrencyCode", currencyCode);

                return Conversion.TryCastDecimal(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static long GetTranIdByTranCode(string catalog, string tranCode)
        {
            const string sql =
                "SELECT transaction_master_id FROM transactions.transaction_master WHERE transaction_code=@TranCode;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TranCode", tranCode);

                return Conversion.TryCastLong(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static bool Reconcile(string catalog, string tranCode, DateTime bookDate)
        {
            const string sql =
                "UPDATE transactions.transaction_master SET book_date=@0::date WHERE transaction_code=@1::national character varying(50);";
            Factory.NonQuery(catalog, sql, bookDate, tranCode);
            return true;
        }

        public static void Verify(string catalog, long tranId, int officeId, int userId, long loginId,
            short verificationStatusId, string reason)
        {
            const string sql =
                "SELECT * FROM transactions.verify_transaction(@TranId::bigint, @OfficeId::integer, @UserId::integer, @LoginId::bigint, @VerificationStatusId::smallint, @Reason::national character varying);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TranId", tranId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@LoginId", loginId);
                command.Parameters.AddWithValue("@VerificationStatusId", verificationStatusId);
                command.Parameters.AddWithValue("@Reason", reason);

                DbOperation.ExecuteNonQuery(catalog, command);
            }
        }

        public static long Add(string catalog, DateTime valueDate, DateTime bookDate, int officeId, int userId,
            long loginId, int costCenterId, string referenceNumber, Collection<JournalDetail> details,
            Collection<Attachment> attachments)
        {
            if (details == null)
            {
                throw new InvalidOperationException(Errors.NoTransactionToPost);
            }

            if (details.Count.Equals(0))
            {
                throw new InvalidOperationException(Errors.NoTransactionToPost);
            }

            decimal debitTotal = (from detail in details select detail.LocalCurrencyDebit).Sum();
            decimal creditTotal = (from detail in details select detail.LocalCurrencyCredit).Sum();

            if (debitTotal != creditTotal)
            {
                throw new InvalidOperationException(Errors.ReferencingSidesNotEqual);
            }

            var decimalPlaces = CurrentCulture.GetCurrencyDecimalPlaces();

            if ((from detail in details
                where
                    Decimal.Round(detail.Credit*detail.ExchangeRate, decimalPlaces) !=
                    Decimal.Round(detail.LocalCurrencyCredit, decimalPlaces) ||
                    Decimal.Round(detail.Debit*detail.ExchangeRate, decimalPlaces) !=
                    Decimal.Round(detail.LocalCurrencyDebit, decimalPlaces)
                select detail).Any())
            {
                throw new InvalidOperationException(Errors.ReferencingSidesNotEqual);
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.GetConnectionString(catalog)))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql =
                            "INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, book_date, user_id, login_id, office_id, cost_center_id, reference_number) SELECT nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), transactions.get_new_transaction_counter(@ValueDate), transactions.get_transaction_code(@ValueDate, @OfficeId, @UserId, @LoginId), @Book, @ValueDate, @BookDate, @UserId, @LoginId, @OfficeId, @CostCenterId, @ReferenceNumber;SELECT currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));";
                        long transactionMasterId;
                        using (NpgsqlCommand master = new NpgsqlCommand(sql, connection))
                        {
                            master.Parameters.AddWithValue("@ValueDate", valueDate);
                            master.Parameters.AddWithValue("@BookDate", bookDate);
                            master.Parameters.AddWithValue("@OfficeId", officeId);
                            master.Parameters.AddWithValue("@UserId", userId);
                            master.Parameters.AddWithValue("@LoginId", loginId);
                            master.Parameters.AddWithValue("@Book", "Journal");
                            master.Parameters.AddWithValue("@CostCenterId", costCenterId);
                            master.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);

                            transactionMasterId = Conversion.TryCastLong(master.ExecuteScalar());
                        }

                        foreach (JournalDetail model in details)
                        {
                            sql =
                                "INSERT INTO transactions.transaction_details(value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) " +
                                "SELECT @ValueDate, @TransactionMasterId, @TranType, core.get_account_id_by_account_number(@AccountNumber::text), @StatementReference, office.get_cash_repository_id_by_cash_repository_code(@CashRepositoryCode), @CurrencyCode, @AmountInCurrency, transactions.get_default_currency_code_by_office_id(@OfficeId), @Er, @AmountInLocalCurrency;";

                            if (model.Credit > 0 && model.Debit > 0)
                            {
                                throw new InvalidOperationException(Errors.BothSidesCannotHaveValue);
                            }

                            if (model.LocalCurrencyCredit > 0 && model.LocalCurrencyDebit > 0)
                            {
                                throw new InvalidOperationException(Errors.BothSidesCannotHaveValue);
                            }

                            decimal amountInCurrency;
                            decimal amountInLocalCurrency;

                            string tranType;

                            if (model.Credit.Equals(0) && model.Debit > 0)
                            {
                                tranType = "Dr";
                                amountInCurrency = model.Debit;
                                amountInLocalCurrency = model.LocalCurrencyDebit;
                            }
                            else
                            {
                                tranType = "Cr";
                                amountInCurrency = model.Credit;
                                amountInLocalCurrency = model.LocalCurrencyCredit;
                            }

                            using (NpgsqlCommand transactionDetail = new NpgsqlCommand(sql, connection))
                            {
                                transactionDetail.Parameters.AddWithValue("@ValueDate", valueDate);
                                transactionDetail.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                                transactionDetail.Parameters.AddWithValue("@TranType", tranType);
                                transactionDetail.Parameters.AddWithValue("@AccountNumber", model.AccountNumber);
                                transactionDetail.Parameters.AddWithValue("@StatementReference",
                                    model.StatementReference);
                                transactionDetail.Parameters.AddWithValue("@CashRepositoryCode",
                                    model.CashRepositoryCode);
                                transactionDetail.Parameters.AddWithValue("@CurrencyCode", model.CurrencyCode);
                                transactionDetail.Parameters.AddWithValue("@AmountInCurrency", amountInCurrency);
                                transactionDetail.Parameters.AddWithValue("@OfficeId", officeId);
                                transactionDetail.Parameters.AddWithValue("@Er", model.ExchangeRate);
                                transactionDetail.Parameters.AddWithValue("@AmountInLocalCurrency",
                                    amountInLocalCurrency);
                                transactionDetail.ExecuteNonQuery();
                            }
                        }

                        #region Attachment

                        if (attachments != null && attachments.Count > 0)
                        {
                            foreach (Attachment attachment in attachments)
                            {
                                sql =
                                    "INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment) SELECT @UserId, @Resource, @ResourceKey, @ResourceId, @OriginalFileName, @FileExtension, @FilePath, @Comment;";
                                using (NpgsqlCommand attachmentCommand = new NpgsqlCommand(sql, connection))
                                {
                                    attachmentCommand.Parameters.AddWithValue("@UserId", userId);
                                    attachmentCommand.Parameters.AddWithValue("@Resource",
                                        "transactions.transaction_master");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceKey", "transaction_master_id");
                                    attachmentCommand.Parameters.AddWithValue("@ResourceId", transactionMasterId);
                                    attachmentCommand.Parameters.AddWithValue("@OriginalFileName",
                                        attachment.OriginalFileName);
                                    attachmentCommand.Parameters.AddWithValue("@FileExtension",
                                        System.IO.Path.GetExtension(attachment.OriginalFileName));
                                    attachmentCommand.Parameters.AddWithValue("@FilePath", attachment.FilePath);
                                    attachmentCommand.Parameters.AddWithValue("@Comment", attachment.Comment);

                                    attachmentCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        #endregion Attachment

                        #region Auto Verification

                        sql = "SELECT * FROM transactions.auto_verify(@TranId::bigint, @OfficeId::integer);";
                        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@TranId", transactionMasterId);
                            command.Parameters.AddWithValue("@OfficeId", officeId);

                            command.ExecuteNonQuery();
                        }

                        #endregion

                        transaction.Commit();
                        return transactionMasterId;
                    }
                    catch (NpgsqlException ex)
                    {
                        Log.Warning(
                            @"Could not post transaction. ValueDate: {ValueDate}, OfficeId: {OfficeId}, UserId: {UserId}, LoginId: {LoginId}, CostCenterId:{CostCenterId}, ReferenceNumber: {ReferenceNumber}, Details: {Details}, Attachments: {Attachments}. {Exception}.",
                            valueDate, officeId, userId, loginId, costCenterId, referenceNumber, details, attachments,
                            ex);
                        transaction.Rollback();
                        throw;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Log.Warning(
                            @"Could not post transaction. ValueDate: {ValueDate}, OfficeId: {OfficeId}, UserId: {UserId}, LoginId: {LoginId}, CostCenterId:{CostCenterId}, ReferenceNumber: {ReferenceNumber}, Details: {Details}, Attachments: {Attachments}. {Exception}.",
                            valueDate, officeId, userId, loginId, costCenterId, referenceNumber, details, attachments,
                            ex);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}