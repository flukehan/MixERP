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
using System.Web.Services;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Cache;
using Serilog;

namespace MixERP.Net.Core.Modules.Sales.Services.Receipt
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class TransactionPosting : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(string partyCode, string currencyCode, decimal amount, decimal debitExchangeRate, decimal creditExchangeRate, string referenceNumber, string statementReference, int costCenterId, int cashRepositoryId, DateTime? postedDate, int bankAccountId, string bankInstrumentCode, string bankTransactionCode)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(partyCode))
                {
                    throw new ArgumentNullException("partyCode");
                }

                if (string.IsNullOrWhiteSpace(currencyCode))
                {
                    throw new ArgumentNullException("currencyCode");
                }

                if (amount <= 0)
                {
                    throw new ArgumentNullException("amount");
                }

                if (debitExchangeRate <= 0)
                {
                    throw new ArgumentNullException("debitExchangeRate");
                }

                if (creditExchangeRate <= 0)
                {
                    throw new ArgumentNullException("creditExchangeRate");
                }

                if (cashRepositoryId == 0 && bankAccountId == 0)
                {
                    throw new InvalidOperationException(Warnings.InvalidReceiptMode);
                }

                if (cashRepositoryId > 0 &&
                    (bankAccountId > 0 || !string.IsNullOrWhiteSpace(bankInstrumentCode) ||
                     !string.IsNullOrWhiteSpace(bankInstrumentCode)))
                {
                    throw new InvalidOperationException(Warnings.CashTransactionCannotContainBankInfo);
                }

                return PostTransaction(partyCode, currencyCode, amount, debitExchangeRate, creditExchangeRate,
                    referenceNumber, statementReference, costCenterId, cashRepositoryId, postedDate, bankAccountId,
                    bankInstrumentCode, bankTransactionCode);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save sales receipt. {Exception}", ex);
                throw;
            }
        }

        private static long PostTransaction(string partyCode, string currencyCode, decimal amount, decimal debitExchangeRate, decimal creditExchangeRate, string referenceNumber, string statementReference, int costCenterId, int cashRepositoryId, DateTime? postedDate, int bankAccountId, string bankInstrumentCode, string bankTransactionCode)
        {
            int userId = CurrentUser.GetSignInView().UserId.ToInt();
            int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
            long loginId = CurrentUser.GetSignInView().LoginId.ToLong();

            long transactionMasterID = Data.Transactions.Receipt.PostTransaction(userId, officeId, loginId, partyCode, currencyCode, amount, debitExchangeRate, creditExchangeRate, referenceNumber, statementReference, costCenterId, cashRepositoryId, postedDate, bankAccountId, bankInstrumentCode, bankTransactionCode);

            return transactionMasterID;
        }
    }
}