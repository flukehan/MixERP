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
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MixERP.Net.DatabaseLayer.Transactions
{
    public static class Transaction
    {
        public static long Add(DateTime valueDate, int officeId, int userId, long logOnId, int costCenterId, string referenceNumber, Collection<JournalDetailsModel> details)
        {
            if (details == null)
            {
                throw new InvalidOperationException(LocalizationHelper.GetResourceString("Errors", "NoTransactionToPost"));
            }

            if (details.Count.Equals(0))
            {
                throw new InvalidOperationException(LocalizationHelper.GetResourceString("Errors", "NoTransactionToPost"));
            }


            decimal debitTotal = (from detail in details select detail.LocalCurrencyDebit).Sum();
            decimal creditTotal = (from detail in details select detail.LocalCurrencyCredit).Sum();

            if (debitTotal != creditTotal)
            {
                throw new InvalidOperationException(LocalizationHelper.GetResourceString("Errors", "ReferencingSidesNotEqual"));
            }

            var decimalPlaces = LocalizationHelper.GetCurrencyDecimalPlaces();

            if ((from detail in details
                 where Decimal.Round(detail.Credit * detail.ExchangeRate, decimalPlaces) != Decimal.Round(detail.LocalCurrencyCredit, decimalPlaces) || Decimal.Round(detail.Debit * detail.ExchangeRate, decimalPlaces) != Decimal.Round(detail.LocalCurrencyDebit, decimalPlaces)
                 select detail).Any())
            {
                throw new InvalidOperationException(LocalizationHelper.GetResourceString("Errors", "ReferencingSidesNotEqual"));
            }




            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString()))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {

                        string sql = "INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number) SELECT nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), transactions.get_new_transaction_counter(@ValueDate), transactions.get_transaction_code(@ValueDate, @OfficeId, @UserId, @LogOnId), @Book, @ValueDate, @UserId, @LogOnId, @OfficeId, @CostCenterId, @ReferenceNumber;SELECT currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));";
                        long transactionMasterId;
                        using (NpgsqlCommand master = new NpgsqlCommand(sql, connection))
                        {
                            master.Parameters.AddWithValue("@ValueDate", valueDate);
                            master.Parameters.AddWithValue("@OfficeId", officeId);
                            master.Parameters.AddWithValue("@UserId", userId);
                            master.Parameters.AddWithValue("@LogOnId", logOnId);
                            master.Parameters.AddWithValue("@Book", "Journal");
                            master.Parameters.AddWithValue("@CostCenterId", costCenterId);
                            master.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);

                            transactionMasterId = Conversion.TryCastLong(master.ExecuteScalar());
                        }

                        foreach (JournalDetailsModel model in details)
                        {
                            sql = "INSERT INTO transactions.transaction_details(transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) " +
                                  "SELECT @TransactionMasterId, @TranType, core.get_account_id_by_account_code(@AccountCode::text), @StatementReference, office.get_cash_repository_id_by_cash_repository_code(@CashRepositoryCode), @CurrencyCode, @AmountInCurrency, transactions.get_default_currency_code_by_office_id(@OfficeId), @Er, @AmountInLocalCurrency;";

                            if (model.Credit > 0 && model.Debit > 0)
                            {
                                throw new InvalidOperationException(LocalizationHelper.GetResourceString("Warnings", "BothSidesHaveValue"));
                            }

                            if (model.LocalCurrencyCredit > 0 && model.LocalCurrencyDebit > 0)
                            {
                                throw new InvalidOperationException(LocalizationHelper.GetResourceString("Warnings", "BothSidesHaveValue"));
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
                                transactionDetail.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                                transactionDetail.Parameters.AddWithValue("@TranType", tranType);
                                transactionDetail.Parameters.AddWithValue("@AccountCode", model.AccountCode);
                                transactionDetail.Parameters.AddWithValue("@StatementReference", model.StatementReference);
                                transactionDetail.Parameters.AddWithValue("@CashRepositoryCode", model.CashRepositoryCode);
                                transactionDetail.Parameters.AddWithValue("@CurrencyCode", model.CurrencyCode);
                                transactionDetail.Parameters.AddWithValue("@AmountInCurrency", amountInCurrency);
                                transactionDetail.Parameters.AddWithValue("@OfficeId", officeId);
                                transactionDetail.Parameters.AddWithValue("@Er", model.ExchangeRate);
                                transactionDetail.Parameters.AddWithValue("@AmountInLocalCurrency", amountInLocalCurrency);
                                transactionDetail.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return transactionMasterId;
                    }
                    catch (NpgsqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    catch (InvalidOperationException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public static decimal GetExchangeRate(int officeId, string currencyCode)
        {
            const string sql = "SELECT transactions.get_exchange_rate(@OfficeId, @CurrencyCode);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@CurrencyCode", currencyCode);

                return Conversion.TryCastDecimal(DbOperations.GetScalarValue(command));
            }

        }
    }
}
