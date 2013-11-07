using MixERP.Net.Common;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using MixERP.Net.WebControls.StockTransactionView.Data.Models;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Data;


namespace MixERP.Net.WebControls.StockTransactionView.Data
{
    public static class ModelFactory
    {
        public static MergeModel GetMergeModel(Collection<int> ids, TranBook book, SubTranBook subBook)
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
            model.TransactionIdCollection = ids;

            model.Book = book;
            model.SubBook = subBook;

            Collection<ProductDetailsModel> products = new Collection<ProductDetailsModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(DBConnection.ConnectionString()))
            {
                using (NpgsqlCommand command = SalesQuotation.GetSalesQuotationViewCommand(ids))
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
                            model.ValueDate = Conversion.TryCastDate(reader["value_date"]);
                            model.PartyCode = Conversion.TryCastString(reader["party_code"]);
                            model.PriceTypeId = Conversion.TryCastInteger(reader["price_type_id"]);
                            model.ReferenceNumber = Conversion.TryCastString(reader["reference_number"]);
                            model.StatementReference = Conversion.TryCastString(reader["statement_reference"]);
                        }

                        Common.Models.Transactions.ProductDetailsModel product = new Common.Models.Transactions.ProductDetailsModel();

                        product.ItemCode = Conversion.TryCastString(reader["item_code"]);
                        product.ItemName = Conversion.TryCastString(reader["item_name"]);
                        product.Unit = Conversion.TryCastString(reader["unit_name"]);


                        product.Quantity = Conversion.TryCastInteger(reader["quantity"]);
                        product.Price = Conversion.TryCastDecimal(reader["price"]);
                        product.Amount = product.Quantity * product.Price;

                        product.Discount = Conversion.TryCastDecimal(reader["discount"]);
                        product.Subtotal = product.Amount - product.Discount;

                        product.Rate = Conversion.TryCastDecimal(reader["tax_rate"]);
                        product.Tax = Conversion.TryCastDecimal(reader["tax"]);
                        product.Total = product.Subtotal + product.Tax;

                        products.Add(product);

                        rowIndex++;
                    }

                    model.View = products;

                }
            }

            if (ids.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(model.StatementReference))
                {
                    model.StatementReference += Environment.NewLine;
                }

                model.StatementReference += "(" + Conversion.GetBookAcronym(book, subBook) + "# " + string.Join(",", ids) + ")";
            }

            return model;
        }
    }
}
