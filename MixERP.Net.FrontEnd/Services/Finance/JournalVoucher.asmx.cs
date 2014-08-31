using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using MixERP.Net.BusinessLayer.Core;
using MixERP.Net.BusinessLayer.Office;
using MixERP.Net.BusinessLayer.Transactions;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using SessionHelper = MixERP.Net.BusinessLayer.Helpers.SessionHelper;

namespace MixERP.Net.FrontEnd.Services.Finance
{
    /// <summary>
    /// Summary description for JournalVoucher
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class JournalVoucher : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, string referenceNumber, string data, int costCenterId, string attachmentsJSON)
        {
            Collection<JournalDetailsModel> details = CollectionHelper.GetJournalDetailCollection(data);

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<Attachment> attachments = js.Deserialize<Collection<Attachment>>(attachmentsJSON);


            foreach (JournalDetailsModel model in details)
            {

                if (model.Debit > 0 && model.Credit > 0)
                {
                    throw new InvalidOperationException("Invalid data");
                }

                if (model.Debit == 0 && model.Credit == 0)
                {
                    throw new InvalidOperationException("Invalid data");
                }

                if (model.Credit < 0 || model.Debit < 0)
                {
                    throw new InvalidOperationException("Invalid data");
                }

                if (!Accounts.AccountCodeExists(model.AccountCode))
                {
                    throw new InvalidOperationException("Invalid account " + model.AccountCode);
                }


                if (model.Credit > 0)
                {
                    if (Accounts.IsCashAccount(model.AccountCode))
                    {
                        if (!CashRepositories.CashRepositoryCodeExists(model.CashRepositoryCode))
                        {
                            throw new InvalidOperationException("Invalid cash repository " + model.CashRepositoryCode);
                        }

                        if (BusinessLayer.Office.CashRepositories.GetBalance(model.CashRepositoryCode) < model.Credit)
                        {
                            throw new InvalidOperationException("Insufficient balance in cash repository.");
                        }
                    }
                }
            }

            decimal drTotal = (from detail in details select detail.LocalCurrencyDebit).Sum();
            decimal crTotal = (from detail in details select detail.LocalCurrencyCredit).Sum();

            if (drTotal != crTotal)
            {
                throw new InvalidOperationException("Referencing sides are not equal.");
            }

            return Transaction.Add(valueDate, referenceNumber, costCenterId, details);

        }

        [WebMethod(EnableSession = true)]
        public decimal GetExchangeRate(string currencyCode)
        {
            int officeId = SessionHelper.GetOfficeId();
            decimal exchangeRate = Transaction.GetExchangeRate(officeId, currencyCode);

            return exchangeRate;
        }
    }
}
