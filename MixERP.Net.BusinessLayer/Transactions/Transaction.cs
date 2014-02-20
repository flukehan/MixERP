/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Transactions;

namespace MixERP.Net.BusinessLayer.Transactions
{
    public static class Transaction
    {
        public static long Add(DateTime valueDate, string referenceNumber, int costCenterId, GridView grid)
        {
            Collection<TransactionDetailModel> details = new Collection<TransactionDetailModel>();

            if(grid != null)
            {
                if(grid.Rows.Count > 0)
                {
                    foreach(GridViewRow row in grid.Rows)
                    {
                        TransactionDetailModel detail = new TransactionDetailModel();
                        detail.AccountCode = row.Cells[0].Text;
                        detail.CashRepositoryName = row.Cells[2].Text;
                        detail.StatementReference = row.Cells[3].Text.Replace("&nbsp;", " ").Trim();
                        detail.Debit = Conversion.TryCastDecimal(row.Cells[4].Text);
                        detail.Credit = Conversion.TryCastDecimal(row.Cells[5].Text);

                        details.Add(detail);
                    }
                }
            }


            long transactionMasterId = DatabaseLayer.Transactions.Transaction.Add(valueDate, SessionHelper.GetOfficeId(), SessionHelper.GetUserId(), SessionHelper.GetLogOnId(), costCenterId, referenceNumber, details);
            DatabaseLayer.Transactions.Verification.CallAutoVerification(transactionMasterId);
            return transactionMasterId;
        }
    }
}
