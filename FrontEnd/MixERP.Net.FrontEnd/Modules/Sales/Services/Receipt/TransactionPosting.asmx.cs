using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MixERP.Net.Core.Modules.Sales.Services.Receipt
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class TransactionPosting : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(string partyCode, string currencyCode, decimal amount, decimal debitExchangeRate, decimal creditExchangeRate, string referenceNumber, string statementReference, int costCenterId, int cashRepositoryId, DateTime? postedDate, int bankAccountId, string bankInstrumentCode, string bankTransactionCode)
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
                throw new InvalidOperationException("Unknown mode of receipt.");
            }

            if (cashRepositoryId > 0 && (bankAccountId > 0 || !string.IsNullOrWhiteSpace(bankInstrumentCode) || !string.IsNullOrWhiteSpace(bankInstrumentCode)))
            {
                throw new InvalidOperationException("A cash transaction cannot contain bank detail information.");
            }

            return this.PostTransaction(partyCode, currencyCode, amount, debitExchangeRate, creditExchangeRate, referenceNumber, statementReference, costCenterId, cashRepositoryId, postedDate, bankAccountId, bankInstrumentCode, bankTransactionCode);
        }

        private long PostTransaction(string partyCode, string currencyCode, decimal amount, decimal debitExchangeRate, decimal creditExchangeRate, string referenceNumber, string statementReference, int costCenterId, int cashRepositoryId, DateTime? postedDate, int bankAccountId, string bankInstrumentCode, string bankTransactionCode)
        {
            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();
            long loginId = SessionHelper.GetLogOnId();

            long transactionMasterID = Data.Helpers.Receipt.PostTransaction(userId, officeId, loginId, partyCode, currencyCode, amount, debitExchangeRate, creditExchangeRate, referenceNumber, statementReference, costCenterId, cashRepositoryId, postedDate, bankAccountId, bankInstrumentCode, bankTransactionCode);
            TransactionGovernor.Autoverification.Autoverify.Pass(transactionMasterID);

            return transactionMasterID;
        }
    }
}