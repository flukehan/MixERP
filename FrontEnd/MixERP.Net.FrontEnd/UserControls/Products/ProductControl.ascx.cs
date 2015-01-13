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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using MixERP.Net.WebControls.StockTransactionView.Data.Models;

namespace MixERP.Net.FrontEnd.UserControls.Products
{
    /// <summary>
    ///     Todo: This class is subject to be moved to a standalone server control class library. This
    ///     UserControl provides a common interface for all transactions that are related to stock
    ///     and/or inventory. Everything is handled in this class, except for the Save event. The
    ///     save event is exposed to the page containing this control and should be handled there.
    /// </summary>
    public partial class ProductControl : UserControl
    {
        #region Properties

        /// <summary>
        ///     This property is used to temporarily store pre assigned instance of transactions for
        ///     merging transactions and creating a batch transactions. Some cases: Multiple Sales
        ///     Quotations --&gt; Sales Order. Multiple Sales Quotations --&gt; Sales Delivery.
        /// </summary>
        private MergeModel model = new MergeModel();

        /// <summary>
        ///     Transaction book for products are Sales and Purchase.
        /// </summary>
        public TranBook Book { get; set; }

        /// <summary>
        ///     This property when enabled will display cost centers.
        /// </summary>
        public bool ShowCostCenter { get; set; }

        /// <summary>
        ///     This property when enabled will display payment terms.
        /// </summary>
        public bool ShowPaymentTerms { get; set; }

        /// <summary>
        ///     This property when set to true will display stores.
        /// </summary>
        public bool ShowPriceTypes { get; set; }

        /// <summary>
        ///     This property when enabled will display sales agents.
        /// </summary>
        public bool ShowSalesAgents { get; set; }

        /// <summary>
        ///     This property when set to true will display sales types, namely "Taxable" and "Nontaxable".
        /// </summary>
        public bool ShowSalesType { get; set; }

        /// <summary>
        ///     This property when enabled will display shipping information, such as shipping address,
        ///     shipping company, and shipping costs.
        /// </summary>
        public bool ShowShippingInformation { get; set; }

        /// <summary>
        ///     This property when set to true will display stores.
        /// </summary>
        public bool ShowStore { get; set; }

        /// <summary>
        ///     This property when set to true will display transaction types. Transaction types are
        ///     Cash and Credit.
        /// </summary>
        public bool ShowTransactionType { get; set; }

        /// <summary>
        ///     Sub transaction books are maintained for breaking down the Purchase and Sales
        ///     transaction into smaller steps such as Quotations, Orders, Deliveries, e.t.c.
        /// </summary>
        public SubTranBook SubBook { get; set; }

        /// <summary>
        ///     The title displayed in the form.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     This property when set to true will verify the stock against the credit inventory
        ///     transactions or "Sales". Since negative stock is not allowed, you will not be able to
        ///     add a product to the grid. This property must be enabled for Sales transaction which
        ///     affect the available inventory on hand. Please also note that even when this property is
        ///     enabled, the products having the switch "Maintain Stock" set to "Off" will not be
        ///     checked for stock availability. This property should be disabled or set to false for
        ///     stock transactions that do not affect stock such as "Quotations", "Orders", e.t.c.
        /// </summary>
        public bool VerifyStock { get; set; }

        /// <summary>
        ///     This class is a representation of the controls in this UserControl.
        /// </summary>

        #endregion Properties

        #region Page Initialization
        private bool initialized;

        public string GetTranBook()
        {
            if (this.Book == TranBook.Sales)
            {
                return "Sales";
            }

            return "Purchase";
        }

        public void Initialize()
        {
            if (!this.initialized)
            {
                if (!this.IsPostBack)
                {
                    this.ClearSession(this.ID);
                }

                this.InitializeControls();
                this.LoadValuesFromSession();
                this.BindGridView();

                this.TitleLiteral.Text = this.Text;
                this.Page.Title = this.Text;
                this.initialized = true;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Initialize();
        }

        private void ClearSession(string key)
        {
            if (this.Session[key] != null)
            {
                this.Session.Remove(key);
            }
        }

        private void InitializeControls()
        {
            this.LoadLabels();
            this.SetVisibleStates();
        }

        private void LoadLabels()
        {
            this.DateLiteral.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ValueDate"), "DateTextBox");
            this.StoreSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SelectStore"), "StoreSelect");

            this.PartyCodeInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SelectParty"), "PartyCodeInputText");

            this.PriceTypeSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "PriceType"), "PriceTypeSelect");
            this.ReferenceNumberInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ReferenceNumberAbbreviated"), "ReferenceNumberInputText");

            this.ItemCodeInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ItemCode"), "ItemCodeInputText");
            this.ItemSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ItemName"), "ItemSelect");
            this.QuantityInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "QuantityAbbreviated"), "QuantityInputText");
            this.UnitSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Unit"), "UnitSelect");
            this.PriceInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Price"), "PriceInputText");
            this.AmountInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Amount"), "AmountInputText");
            this.DiscountInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Discount"), "DiscountInputText");
            this.SubTotalInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SubTotal"), "SubTotalInputText");
            this.TaxSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "TaxForm"), "TaxSelect");
            this.TaxInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Tax"), "TaxInputText");

            this.ShippingAddressSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingAddress"), "ShippingAddressSelect");
            this.ShippingAddressTextAreaLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingAddress"), "ShippingAddressTextArea");
            this.ShippingCompanySelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingCompany"), "ShippingCompanySelect");
            this.ShippingChargeInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingCharge"), "ShippingChargeInputText");
            this.SalesTypeSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SalesType"), "SalesTypeSelect");

            this.SalesTypeSelect.Items.Add(new ListItem(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "TaxableSales"), "1"));
            this.SalesTypeSelect.Items.Add(new ListItem(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "NonTaxableSales"), "0"));

            this.RunningTotalInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "RunningTotal"), "RunningTotalInputText");
            this.TaxTotalInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "TaxTotal"), "TaxTotalInputText");
            this.GrandTotalInputTextInputTextLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "GrandTotal"), "GrandTotalInputTextInputText");
            this.CostCenterSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CostCenter"), "CostCenterSelect");
            this.SalesPersonSelectLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Salesperson"), "SalesPersonSelect");
            this.StatementReferenceTextAreaLabel.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "StatementReference"), "StatementReferenceTextArea");

            this.CashTransactionLiteral.Text = HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CashTransaction"));
        }

        private void LoadValuesFromSession()
        {
            if (this.Session["Product"] == null)
            {
                return;
            }

            this.model = this.Session["Product"] as MergeModel;

            if (this.model == null)
            {
                return;
            }

            this.PartyCodeHidden.Value = this.model.PartyCode.ToString(CultureInfo.InvariantCulture);
            this.PartyCodeInputText.Value = this.model.PartyCode.ToString(CultureInfo.InvariantCulture);
            this.PriceTypeIdHidden.Value = this.model.PriceTypeId.ToString(CultureInfo.InvariantCulture);
            this.StoreIdHidden.Value = this.model.StoreId.ToString(CultureInfo.InvariantCulture);
            this.ShipperIdHidden.Value = this.model.ShippingCompanyId.ToString(CultureInfo.InvariantCulture);
            this.ShippingAddressCodeHidden.Value = this.model.ShippingAddressCode.ToString(CultureInfo.InvariantCulture);
            this.SalesPersonIdHidden.Value = this.model.SalesPersonId.ToString(CultureInfo.InvariantCulture);

            this.ReferenceNumberInputText.Value = this.model.ReferenceNumber;
            this.StatementReferenceTextArea.Value = this.model.StatementReference;
            if (this.model.NonTaxableSales)
            {
                this.SalesTypeSelect.SelectedIndex = 1;
            }

            this.Session[this.ID] = this.model.View;
            this.TranIdCollectionHiddenField.Value = string.Join(",", this.model.TransactionIdCollection);
            this.ClearSession("Product");
        }

        private void SetVisibleStates()
        {
            this.StoreSelect.Visible = this.ShowStore;
            this.StoreSelectLabel.Visible = this.ShowStore;

            this.CashTransactionDiv.Visible = this.ShowTransactionType;

            this.PaymentTermSelect.Visible = this.ShowPaymentTerms;

            this.PriceTypeSelect.Visible = this.ShowPriceTypes;
            this.PriceTypeSelectLabel.Visible = this.ShowPriceTypes;

            this.ShippingAddressDiv.Visible = this.ShowShippingInformation;
            this.ShippingAddressInfoDiv.Visible = this.ShowShippingInformation;

            if (!this.ShowShippingInformation)
            {
                this.ShippingChargeInputText.Attributes.Add("readonly", "readonly");
            }

            this.SalesTypeDiv.Visible = (this.Book == TranBook.Sales && this.ShowSalesType);

            this.CostCenterDiv.Visible = this.ShowCostCenter;
            this.SalespersonDiv.Visible = this.ShowSalesAgents;
        }

        #region Grid Binding

        private static Collection<ProductDetailsModel> SummateProducts(IEnumerable<ProductDetailsModel> productCollection)
        {
            //Create a new collection of products.
            Collection<ProductDetailsModel> collection = new Collection<ProductDetailsModel>();

            //Iterate through the supplied product collection.
            foreach (ProductDetailsModel product in productCollection)
            {
                //Create a product
                ProductDetailsModel productInCollection = null;

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

        private void BindGridView()
        {
            Collection<ProductDetailsModel> table = this.GetTable();

            if (table.Count > 0)
            {
                List<string[]> rowData = new List<string[]>();

                foreach (ProductDetailsModel row in table)
                {
                    string[] colData = new string[12];

                    colData[0] = row.ItemCode;
                    colData[1] = row.ItemName;
                    colData[2] = row.Quantity.ToString(CultureInfo.CurrentCulture);
                    colData[3] = row.Unit;
                    colData[4] = row.Price.ToString(CultureInfo.CurrentCulture);
                    colData[5] = row.Amount.ToString(CultureInfo.CurrentCulture);
                    colData[6] = row.Discount.ToString(CultureInfo.CurrentCulture);
                    colData[7] = row.ShippingCharge.ToString(CultureInfo.CurrentCulture);
                    colData[8] = row.Subtotal.ToString(CultureInfo.CurrentCulture);
                    colData[9] = row.TaxCode.ToString(CultureInfo.CurrentCulture);
                    colData[10] = row.Tax.ToString(CultureInfo.CurrentCulture);
                    colData[11] = row.Total.ToString(CultureInfo.CurrentCulture);

                    rowData.Add(colData);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string data = serializer.Serialize(rowData);

                this.ProductGridViewDataHidden.Value = data;
            }
        }

        private Collection<ProductDetailsModel> GetTable()
        {
            Collection<ProductDetailsModel> productCollection = new Collection<ProductDetailsModel>();

            if (this.Session[this.ID] != null)
            {
                //Get an instance of the ProductDetailsModel collection stored in session.
                productCollection = (Collection<ProductDetailsModel>) this.Session[this.ID];

                //Summate the collection.
                productCollection = SummateProducts(productCollection);

                //Store the summed table in session.
                this.Session[this.ID] = productCollection;
            }

            return productCollection;
        }

        #endregion Grid Binding

        #endregion Page Initialization
    }
}