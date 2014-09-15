using MixERP.Net.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Receipt
    {
        public static long PostTransaction(int userId, int officeId, long loginId, string partyCode, string currencyCode, decimal amount, decimal debitExchangeRate, decimal creditExchangeRate, string referenceNumber, string statementReference, int costCenterId, int cashRepositoryId, DateTime? postedDate, int bankAccountId, string bankInstrumentCode, string bankTransactionCode)
        {
            const string sql = "SELECT transactions.post_receipt_function(@UserId, @OfficeId, @LoginId, @PartyCode, @CurrencyCode, @Amount, @DebitExchangeRate, @CreditExchangeRate, @ReferenceNumber, @StatementReference, @CostCenterId, @CashRepositoryId, @PostedDate, @BankAccountId, @BankInstrumentCode, @BankTransactionCode); ";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@LoginId", loginId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@DebitExchangeRate", debitExchangeRate);
                command.Parameters.AddWithValue("@CreditExchangeRate", creditExchangeRate);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);
                command.Parameters.AddWithValue("@CostCenterId", costCenterId);

                if (cashRepositoryId.Equals(0))
                {
                    command.Parameters.AddWithValue("@CashRepositoryId", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@CashRepositoryId", cashRepositoryId);
                }

                if (bankAccountId.Equals(0))
                {
                    command.Parameters.AddWithValue("@BankAccountId", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankAccountId);
                }

                if (postedDate == null)
                {
                    command.Parameters.AddWithValue("@PostedDate", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@PostedDate", postedDate);
                }

                command.Parameters.AddWithValue("@BankInstrumentCode", bankInstrumentCode);
                command.Parameters.AddWithValue("@BankTransactionCode", bankTransactionCode);

                return Conversion.TryCastLong(DBFactory.DbOperations.GetScalarValue(command));
            }
        }

        public static DataTable GetView(int userId, int officeId, DateTime dateFrom, DateTime dateTo, string office, string party, string user, string referenceNumber, string statementReference)
        {
            const string sql = "SELECT * FROM transactions.get_receipt_view(@UserId, @OfficeId, @DateFrom, @DateTo, @Office, @Party, @User, @ReferenceNumber, @StatementReference);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@DateFrom", dateFrom);
                command.Parameters.AddWithValue("@DateTo", dateTo);
                command.Parameters.AddWithValue("@Office", office);
                command.Parameters.AddWithValue("@Party", party);
                command.Parameters.AddWithValue("@User", user);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);

                return DBFactory.DbOperations.GetDataTable(command);
            }
        }
    }
}