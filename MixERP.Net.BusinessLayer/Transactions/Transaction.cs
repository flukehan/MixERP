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
