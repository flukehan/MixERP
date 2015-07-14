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

using MixERP.Net.Common;
using MixERP.Net.DbFactory;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.StockTransactionViewFactory.Data.Models;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.WebControls.StockTransactionViewFactory.Data
{
    public static class ModelFactory
    {
        public static object Reader(this IDataRecord dataRecord, string columnName)
        {
            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                if (dataRecord.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return dataRecord[columnName];
            }
            return string.Empty;
        }
        public static MergeModel GetMergeModel(string catalog, Collection<long> ids, TranBook tranBook, SubTranBook subTranBook)
        {
            int rowIndex = 0;

            if (ids == null)
            {
                return new MergeModel();
            }

            if (ids.Count.Equals(0))
            {
                return new MergeModel();
            }

            MergeModel model = new MergeModel();

            foreach (long tranId in ids)
            {
                model.AddTransactionIdToCollection(tranId);
            }

            model.Book = tranBook;
            model.SubBook = subTranBook;

            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.GetConnectionString(catalog)))
            {
                using (NpgsqlCommand command = GetViewCommand(tranBook, subTranBook, ids))
                {
                    command.Connection = connection;
                    command.Connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    if (!reader.HasRows)
                    {
                        return new MergeModel();
                    }

                    while (reader.Read())
                    {
                        if (rowIndex.Equals(0))
                        {
                            model.ValueDate = Conversion.TryCastDate(Reader(reader,"value_date"));
                            model.PartyCode = Conversion.TryCastString(Reader(reader,"party_code"));
                            model.PriceTypeId = Conversion.TryCastInteger(Reader(reader,"price_type_id"));
                            model.ReferenceNumber = Conversion.TryCastString(Reader(reader,"reference_number"));
                            model.NonTaxableSales = Conversion.TryCastBoolean(Reader(reader, "non_taxable"));
                            model.ShippingCompanyId = Conversion.TryCastInteger(Reader(reader,"shipper_id"));
                            model.SalesPersonId = Conversion.TryCastInteger(Reader(reader,"salesperson_id"));
                            model.StoreId = Conversion.TryCastInteger(Reader(reader,"store_id"));
                            model.ShippingAddressCode = Conversion.TryCastString(Reader(reader,"shipping_address_code"));
                            model.StatementReference = Conversion.TryCastString(Reader(reader,"statement_reference"));
                        }

                        ProductDetail product = new ProductDetail();

                        product.ItemCode = Conversion.TryCastString(Reader(reader,"item_code"));
                        product.ItemName = Conversion.TryCastString(Reader(reader,"item_name"));
                        product.Unit = Conversion.TryCastString(Reader(reader,"unit_name"));

                        product.Quantity = Conversion.TryCastInteger(Reader(reader,"quantity"));
                        product.Price = Conversion.TryCastDecimal(Reader(reader,"price"));
                        product.Amount = product.Quantity * product.Price;

                        product.Discount = Conversion.TryCastDecimal(Reader(reader,"discount"));
                        product.ShippingCharge = Conversion.TryCastDecimal(Reader(reader,"shipping_charge"));
                        product.Subtotal = product.Amount - product.Discount - product.ShippingCharge;

                        product.TaxCode = Conversion.TryCastString(Reader(reader,"tax_code"));
                        product.Tax = Conversion.TryCastDecimal(Reader(reader,"tax"));
                        product.Total = product.Subtotal + product.Tax;

                        model.AddViewToCollection(product);

                        rowIndex++;
                    }
                }
            }

            if (ids.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(model.StatementReference))
                {
                    model.StatementReference += Environment.NewLine;
                }

                model.StatementReference += "(" +
                                            Entities.Helpers.TransactionBookHelper.GetBookAcronym(tranBook, subTranBook) +
                                            "# " + string.Join(",", ids) + ")";
            }

            return model;
        }

        private static NpgsqlCommand GetViewCommand(TranBook tranBook, SubTranBook subTranBook, Collection<long> ids)
        {
            if (tranBook == TranBook.Sales)
            {
                switch (subTranBook)
                {
                    case SubTranBook.Delivery:
                        break;

                    case SubTranBook.Direct:
                        break;

                    case SubTranBook.Invoice:
                        break;

                    case SubTranBook.Order:
                        return SalesOrder.GetSalesOrderViewCommand(ids);

                    case SubTranBook.Payment:
                        throw new InvalidOperationException(Errors.InvalidSubTranBookSalesPayment);
                    case SubTranBook.Quotation:
                        return SalesQuotation.GetSalesQuotationViewCommand(ids);

                    case SubTranBook.Receipt:
                        break;

                    case SubTranBook.Return:
                        break;
                }
            }

            switch (subTranBook)
            {
                case SubTranBook.Delivery:
                    throw new InvalidOperationException(Errors.InvalidSubTranBookPurchaseDelivery);
                case SubTranBook.Direct:
                    break;

                case SubTranBook.Invoice:
                    break;

                case SubTranBook.Order:
                    return PurchaseOrder.GetPurchaseOrderViewCommand(ids);

                case SubTranBook.Payment:
                    break;

                case SubTranBook.Quotation:
                    throw new InvalidOperationException(Errors.InvalidSubTranBookPurchaseQuotation);
                case SubTranBook.Receipt:
                    throw new InvalidOperationException(Errors.InvalidSubTranBookPurchaseReceipt);
            }

            return null;
        }
    }
}