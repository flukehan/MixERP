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

using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionViewFactory.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private const string sessionKey = "StockTransactionFactory";
        private const string mergeModelSessionKey = "Product";

        private void LoadValuesFromSession()
        {
            if (this.Page == null || this.Page.Session[mergeModelSessionKey] == null)
            {
                return;
            }

            this.model = this.Page.Session[mergeModelSessionKey] as MergeModel;

            if (this.model == null)
            {
                return;
            }

            this.partyCodeHidden.Value = this.model.PartyCode.ToString(CultureInfo.InvariantCulture);
            this.partyCodeInputText.Value = this.model.PartyCode.ToString(CultureInfo.InvariantCulture);
            this.priceTypeIdHidden.Value = this.model.PriceTypeId.ToString(CultureInfo.InvariantCulture);
            this.storeIdHidden.Value = this.model.StoreId.ToString(CultureInfo.InvariantCulture);
            this.shipperIdHidden.Value = this.model.ShippingCompanyId.ToString(CultureInfo.InvariantCulture);
            this.shippingAddressCodeHidden.Value = this.model.ShippingAddressCode.ToString(CultureInfo.InvariantCulture);
            this.salesPersonIdHidden.Value = this.model.SalesPersonId.ToString(CultureInfo.InvariantCulture);

            this.referenceNumberInputText.Value = this.model.ReferenceNumber;
            this.statementReferenceTextArea.Value = this.model.StatementReference;

            this.Page.Session[sessionKey] = this.model.View;
            this.tranIdCollectionHidden.Value = string.Join(",", this.model.TransactionIdCollection);
        }

        private void ClearSession()
        {
            SessionHelper.RemoveSessionKey(mergeModelSessionKey);
            SessionHelper.RemoveSessionKey(sessionKey);
        }

        private Collection<ProductDetail> GetTable()
        {
            Collection<ProductDetail> productCollection = new Collection<ProductDetail>();

            if (this.Page != null && this.Page.Session[sessionKey] != null)
            {
                //Get an instance of the ProductDetailsModel collection stored in session.
                productCollection = (Collection<ProductDetail>)this.Page.Session[sessionKey];

                //Summate the collection.
                productCollection = SummateProducts(productCollection);

                //Store the summated table in session.
                this.Page.Session[sessionKey] = productCollection;
            }

            return productCollection;
        }

        private static Collection<ProductDetail> SummateProducts(IEnumerable<ProductDetail> productCollection)
        {
            //Create a new collection of products.
            Collection<ProductDetail> collection = new Collection<ProductDetail>();

            //Iterate through the supplied product collection.
            foreach (ProductDetail product in productCollection)
            {
                //Create a product
                ProductDetail productInCollection = null;

                if (collection.Count > 0)
                {
                    productInCollection = collection.FirstOrDefault(x => x.ItemCode == product.ItemCode && x.ItemName == product.ItemName && x.Unit == product.Unit && x.Price == product.Price && x.TaxCode == product.TaxCode);
                }

                if (productInCollection == null)
                {
                    collection.Add(product);
                }
                else
                {
                    productInCollection.Quantity += product.Quantity;
                    productInCollection.Amount += product.Amount;
                    productInCollection.Discount += product.Discount;
                    productInCollection.Subtotal += product.Subtotal;
                    productInCollection.ShippingCharge += product.ShippingCharge;
                    productInCollection.Tax += product.Tax;
                    productInCollection.Total += product.Total;
                }
            }

            return collection;
        }
    }
}