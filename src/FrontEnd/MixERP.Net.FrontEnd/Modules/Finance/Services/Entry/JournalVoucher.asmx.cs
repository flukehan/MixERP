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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Core.Modules.Finance.Data.Helpers;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Finance.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class JournalVoucher : WebService
    {
        [WebMethod]
        public long Save(DateTime valueDate, DateTime bookDate, string referenceNumber, string data, int costCenterId, string attachmentsJSON)
        {
            try
            {
                Collection<JournalDetail> details = GetJournalDetailCollection(data);

                Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);

                foreach (JournalDetail model in details)
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

                    if (!AccountHelper.AccountNumberExists(AppUsers.GetCurrentUserDB(), model.AccountNumber))
                    {
                        throw new InvalidOperationException("Invalid account " + model.AccountNumber);
                    }

                    if (model.Credit > 0)
                    {
                        if (AccountHelper.IsCashAccount(AppUsers.GetCurrentUserDB(), model.AccountNumber))
                        {
                            if (
                                !CashRepositories.CashRepositoryCodeExists(AppUsers.GetCurrentUserDB(),
                                    model.CashRepositoryCode))
                            {
                                throw new InvalidOperationException("Invalid cash repository " +
                                                                    model.CashRepositoryCode);
                            }

                            if (
                                CashRepositories.GetBalance(AppUsers.GetCurrentUserDB(), model.CashRepositoryCode,
                                    model.CurrencyCode) < model.Credit)
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

                int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
                int userId = AppUsers.GetCurrent().View.UserId.ToInt();
                long loginId = AppUsers.GetCurrent().View.LoginId.ToLong();

                return Transaction.Add(AppUsers.GetCurrentUserDB(), valueDate, bookDate, officeId, userId, loginId, costCenterId,
                    referenceNumber, details, attachments);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save transaction. {Exception}", ex);
                throw;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private static Collection<JournalDetail> GetJournalDetailCollection(string json)
        {
            Collection<JournalDetail> details = new Collection<JournalDetail>();
            var jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                JournalDetail detail = new JournalDetail();
                detail.StatementReference = item[0];
                detail.AccountNumber = item[1];
                detail.Account = item[2];
                detail.CashRepositoryCode = item[3];
                detail.CurrencyCode = item[4];
                detail.Debit = Conversion.TryCastDecimal(item[5]);
                detail.Credit = Conversion.TryCastDecimal(item[6]);
                detail.ExchangeRate = Conversion.TryCastDecimal(item[7]);
                detail.LocalCurrencyDebit = Conversion.TryCastDecimal(item[8]);
                detail.LocalCurrencyCredit = Conversion.TryCastDecimal(item[9]);
                details.Add(detail);
            }

            return details;
        }
    }
}