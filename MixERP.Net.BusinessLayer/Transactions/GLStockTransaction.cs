using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixERP.Net.BusinessLayer.Helpers;

namespace MixERP.Net.BusinessLayer.Transactions
{
    public static class GLStockTransaction
    {
        public static DataTable GetView(string book, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return DatabaseLayer.Transactions.GLStockTransaction.GetView(SessionHelper.GetUserId(), book, SessionHelper.GetOfficeId(), dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }

    }
}
