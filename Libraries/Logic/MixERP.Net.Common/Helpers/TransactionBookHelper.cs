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

using MixERP.Net.Common.Base;
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
                        throw new MixERPException(Warnings.InvalidSubTranBookSalesPayment);
                    case SubTranBook.Quotation:
                        bookName = "Sales.Quotation";
                        break;

                    case SubTranBook.Receipt:
                        bookName = "Sales.Receipt";
                        break;

                    case SubTranBook.Return:
                        bookName = "Sales.Return";
                        break;
                    case SubTranBook.Transfer:
                        throw new MixERPException(Warnings.InvalidSubTranBookSalesTransfer);
                    case SubTranBook.Suspense:
                        throw new MixERPException(Warnings.InvalidSubTranBookSalesSuspense);
                }
            }

            if (book == TranBook.Purchase)
            {
                switch (subBook)
                {
                    case SubTranBook.Delivery:
                        throw new MixERPException(Warnings.InvalidSubTranBookPurchaseDelivery);
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
                        throw new MixERPException(Warnings.InvalidSubTranBookPurchaseQuotation);
                    case SubTranBook.Receipt:
                        bookName = "Purchase.Receipt"; //Also known as GRN
                        break;

                    case SubTranBook.Return:
                        bookName = "Purchase.Return";
                        break;
                    case SubTranBook.Transfer:
                        throw new MixERPException(Warnings.InvalidSubTranBookPurchaseTransfer);
                    case SubTranBook.Suspense:
                        throw new MixERPException(Warnings.InvalidSubTranBookPurchaseSuspense);
                }
            }

            if (book == TranBook.Inventory)
            {
                switch (subBook)
                {
                    case SubTranBook.Delivery:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryDelivery);
                    case SubTranBook.Direct:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryDirect);
                    case SubTranBook.Invoice:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryInvoice);
                    case SubTranBook.Order:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryOrder);
                    case SubTranBook.Payment:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryPayment);
                    case SubTranBook.Quotation:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryQuotation);
                    case SubTranBook.Receipt:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryReceipt);
                    case SubTranBook.Return:
                        throw new MixERPException(Warnings.InvalidSubTranBookInventoryReturn);
                    case SubTranBook.Transfer:
                        bookName = "Inventory.Transfer";
                        break;
                    case SubTranBook.Suspense:
                        bookName = "Inventory.Suspense";
                        break;
                }
            }

            return bookName;
        }
    }
}