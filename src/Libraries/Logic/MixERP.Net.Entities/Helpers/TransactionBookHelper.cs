using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using System.Diagnostics.CodeAnalysis;

namespace MixERP.Net.Entities.Helpers
{
    public static class TransactionBookHelper
    {
        public static string GetBookAcronym(TranBook book, SubTranBook subBook)
        {
            if (book == TranBook.Sales)
            {
                if (subBook == SubTranBook.Delivery)
                {
                    return ConfigurationHelper.GetParameter("SalesDeliveryAcronym");
                }

                if (subBook == SubTranBook.Direct)
                {
                    return ConfigurationHelper.GetParameter("SalesDirectAcronym");
                }

                if (subBook == SubTranBook.Invoice)
                {
                    return ConfigurationHelper.GetParameter("SalesInvoiceAcronym");
                }

                if (subBook == SubTranBook.Order)
                {
                    return ConfigurationHelper.GetParameter("SalesOrderAcronym");
                }

                if (subBook == SubTranBook.Quotation)
                {
                    return ConfigurationHelper.GetParameter("SalesQuotationAcronym");
                }

                if (subBook == SubTranBook.Receipt)
                {
                    return ConfigurationHelper.GetParameter("SalesReceiptAcronym");
                }

                if (subBook == SubTranBook.Return)
                {
                    return ConfigurationHelper.GetParameter("SaleReturnAcronym");
                }
            }

            if (book == TranBook.Purchase)
            {
                if (subBook == SubTranBook.Direct)
                {
                    return ConfigurationHelper.GetParameter("PurchaseDirectAcronym");
                }

                if (subBook == SubTranBook.Order)
                {
                    return ConfigurationHelper.GetParameter("PurchaseOrderAcronym");
                }

                if (subBook == SubTranBook.Payment)
                {
                    return ConfigurationHelper.GetParameter("PurchasePaymentAcronym");
                }

                if (subBook == SubTranBook.Receipt)
                {
                    return ConfigurationHelper.GetParameter("PurchaseGRNAcronym");
                }

                if (subBook == SubTranBook.Return)
                {
                    return ConfigurationHelper.GetParameter("PurchaseReturnAcronym");
                }
            }

            return string.Empty;
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public static string GetInvariantTransactionBookName(TranBook book, SubTranBook subBook)
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