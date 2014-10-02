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

using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Common.Helpers
{
    public static class TransactionBookHelper
    {
        public static string GetTransactionBookName(TranBook book, SubTranBook subBook)
        {
            string bookName = string.Empty;

            if (book == TranBook.Sales)
            {
                switch (subBook)
                {
                    case SubTranBook.Delivery:
                        bookName = "Sales.Delivery";
                        break;

                    case SubTranBook.Direct:
                        bookName = "Sales.Direct";
                        break;

                    case SubTranBook.Invoice:
                        bookName = "Sales.Invoice";
                        break;

                    case SubTranBook.Order:
                        bookName = "Sales.Order";
                        break;

                    case SubTranBook.Payment:
                        throw new InvalidOperationException(Warnings.InvalidSubTranBookSalesPayment);
                    case SubTranBook.Quotation:
                        bookName = "Sales.Quotation";
                        break;

                    case SubTranBook.Receipt:
                        bookName = "Sales.Receipt";
                        break;

                    case SubTranBook.Return:
                        bookName = "Sales.Return";
                        break;
                }
            }

            if (book == TranBook.Purchase)
            {
                switch (subBook)
                {
                    case SubTranBook.Delivery:
                        throw new InvalidOperationException(Warnings.InvalidSubTranBookPurchaseDelivery);
                    case SubTranBook.Direct:
                        bookName = "Purchase.Direct";
                        break;

                    case SubTranBook.Invoice:
                        bookName = "Purchase.Invoice";
                        break;

                    case SubTranBook.Order:
                        bookName = "Purchase.Order";
                        break;

                    case SubTranBook.Payment:
                        bookName = "Purchase.Payment";
                        break;

                    case SubTranBook.Quotation:
                        throw new InvalidOperationException(Warnings.InvalidSubTranBookPurchaseQuotation);
                    case SubTranBook.Receipt:
                        bookName = "Purchase.Receipt"; //Also known as GRN
                        break;

                    case SubTranBook.Return:
                        bookName = "Purchase.Return";
                        break;
                }
            }

            return bookName;
        }
    }
}