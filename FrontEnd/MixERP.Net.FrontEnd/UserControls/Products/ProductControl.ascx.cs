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
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using MixERP.Net.WebControls.StockTransactionView.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.UserControls.Products
{
    /// <summary>
    /// Todo: This class is subject to be moved to a standalone server control class library. This
    ///       UserControl provides a common interface for all transactions that are related to stock
    ///       and/or inventory. Everything is handled in this class, except for the Save event. The
    ///       save event is exposed to the page containing this control and should be handled there.
    /// </summary>
    public partial class ProductControl : UserControl
    {
        #region Properties

        /// <summary>
        /// This property is used to temporarily store pre assigned instance of transactions for
        /// merging transactions and creating a batch transactions. Some cases: Multiple Sales
        /// Quotations --&gt; Sales Order. Multiple Sales Quotations --&gt; Sales Delivery.
        /// </summary>
        private MergeModel model = new MergeModel();

        /// <summary>
        /// Transaction book for products are Sales and Purchase.
        /// </summary>
        public TranBook Book { get; set; }

        /// <summary>
        /// This property when enabled will display cash repositories and their available balance.
        /// Not all available cash repositories will be displayed here but those which belong to the
        /// current (or logged in) branch office. This property must be enabled for transactions
        /// which have affect on cash ledger, namely "Direct Purchase" and "Direct Sales".
        /// </summary>
        public bool ShowCashRepository { get; set; }

        /// <summary>
        /// This property when enabled will display cost centers.
        /// </summary>
        public bool ShowCostCenter { get; set; }

        /// <summary>
        /// This property when set to true will display stores.
        /// </summary>
        public bool ShowPriceTypes { get; set; }

        /// <summary>
        /// This property when enabled will display sales agents.
        /// </summary>
        public bool ShowSalesAgents { get; set; }

        /// <summary>
        /// This property when enabled will display shipping information, such as shipping address,
        /// shipping company, and shipping costs.
        /// </summary>
        public bool ShowShippingInformation { get; set; }

        /// <summary>
        /// This property when set to true will display stores.
        /// </summary>
        public bool ShowStore { get; set; }

        /// <summary>
        /// This property when set to true will display transaction types. Transaction types are
        /// Cash and Credit.
        /// </summary>
        public bool ShowTransactionType { get; set; }

        /// <summary>
        /// Sub transaction books are maintained for breaking down the Purchase and Sales
        /// transaction into smaller steps such as Quotations, Orders, Deliveries, e.t.c.
        /// </summary>
        public SubTranBook SubBook { get; set; }

        /// <summary>
        /// The title displayed in the form.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// This property when set to true will verify the stock against the credit inventory
        /// transactions or "Sales". Since negative stock is not allowed, you will not be able to
        /// add a product to the grid. This property must be enabled for Sales transaction which
        /// affect the available inventory on hand. Please also note that even when this property is
        /// enabled, the products having the switch "Maintain Stock" set to "Off" will not be
        /// checked for stock availability. This property should be disabled or set to false for
        /// stock transactions that do not affect stock such as "Quotations", "Orders", e.t.c.
        /// </summary>
        public bool VerifyStock { get; set; }

        /// <summary>
        /// This class is a representation of the controls in this UserControl.
        /// </summary>

        #endregion Properties

        #region "Page Initialization"

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
            if (!initialized)
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
                initialized = true;
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
            this.DateLiteral.Text = HtmlControlHelper.GetLabel(this.DateTextBox.ClientID, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ValueDate"));
            this.StoreSelectLabel.Text = HtmlControlHelper.GetLabel("StoreSelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SelectStore"));

            this.PartyCodeInputTextLabel.Text = HtmlControlHelper.GetLabel("PartyCodeInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SelectParty"));
            this.PriceTypeSelectLabel.Text = HtmlControlHelper.GetLabel("PriceTypeSelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "PriceType"));
            this.ReferenceNumberInputTextLabel.Text = HtmlControlHelper.GetLabel("ReferenceNumberInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ReferenceNumberAbbreviated"));

            this.ItemCodeInputTextLabel.Text = HtmlControlHelper.GetLabel("ItemCodeInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ItemCode"));
            this.ItemSelectLabel.Text = HtmlControlHelper.GetLabel("ItemSelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ItemName"));
            this.QuantityInputTextLabel.Text = HtmlControlHelper.GetLabel("QuantityInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "QuantityAbbreviated"));
            this.UnitSelectLabel.Text = HtmlControlHelper.GetLabel("UnitSelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Unit"));
            this.PriceInputTextLabel.Text = HtmlControlHelper.GetLabel("PriceInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Price"));
            this.AmountInputTextLabel.Text = HtmlControlHelper.GetLabel("AmountInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Amount"));
            this.DiscountInputTextLabel.Text = HtmlControlHelper.GetLabel("DiscountInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Discount"));
            this.SubTotalInputTextLabel.Text = HtmlControlHelper.GetLabel("SubTotalInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SubTotal"));
            this.TaxRateInputTextLabel.Text = HtmlControlHelper.GetLabel("TaxRateInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Rate"));
            this.TaxInputTextLabel.Text = HtmlControlHelper.GetLabel("TaxInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Tax"));
            this.TotalAmountInputTextLabel.Text = HtmlControlHelper.GetLabel("TotalAmountInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Total"));

            this.ShippingAddressSelectLabel.Text = HtmlControlHelper.GetLabel("ShippingAddressSelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingAddress"));
            this.ShippingCompanySelectLabel.Text = HtmlControlHelper.GetLabel("ShippingCompanySelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingCompany"));
            this.ShippingChargeInputTextLabel.Text = HtmlControlHelper.GetLabel("ShippingChargeInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingCharge"));
            this.RunningTotalInputTextLabel.Text = HtmlControlHelper.GetLabel("RunningTotalInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "RunningTotal"));
            this.TaxTotalInputTextLabel.Text = HtmlControlHelper.GetLabel("TaxTotalInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "TaxTotal"));
            this.GrandTotalInputTextLabel.Text = HtmlControlHelper.GetLabel("GrandTotalInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "GrandTotal"));
            this.CashRepositorySelectLabel.Text = HtmlControlHelper.GetLabel("CashRepositorySelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CashRepository"));
            this.CashRepositoryBalanceInputTextLabel.Text = HtmlControlHelper.GetLabel("CashRepositoryBalanceInputText", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CashRepositoryBalance"));
            this.CostCenterSelectLabel.Text = HtmlControlHelper.GetLabel("CostCenterSelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CostCenter"));
            this.SalesPersonSelectLabel.Text = HtmlControlHelper.GetLabel("SalesPersonSelect", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Salesperson"));
            this.StatementReferenceTextAreaLabel.Text = HtmlControlHelper.GetLabel("StatementReferenceTextArea", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "StatementReference"));

            this.CashTransactionLiteral.Text = HtmlControlHelper.GetLabel("CashTransactionCheckBox", StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CashTransaction"));
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

            this.PartyCodeHidden.Value = this.model.PartyCode.ToString(LocalizationHelper.GetCurrentCulture());
            this.PartyCodeInputText.Value = this.model.PartyCode.ToString(LocalizationHelper.GetCurrentCulture());
            this.PriceTypeIdHidden.Value = this.model.PriceTypeId.ToString(SessionHelper.GetCulture());

            this.ReferenceNumberInputText.Value = this.model.ReferenceNumber;
            this.StatementReferenceTextArea.Value = this.model.StatementReference;

            this.Session[this.ID] = this.model.View;
            TranIdCollectionHiddenField.Value = string.Join(",", this.model.TransactionIdCollection);
            this.ClearSession("Product");
        }

        private void SetVisibleStates()
        {
            this.CashRepositoryDiv.Visible = this.ShowCashRepository;
            this.StoreDiv.Visible = this.ShowStore;
            this.CashTransactionDiv.Visible = this.ShowTransactionType;
            this.PriceTypeDiv.Visible = this.ShowPriceTypes;
            this.ShippingAddressDiv.Visible = this.ShowShippingInformation;
            this.ShippingCompanyDiv.Visible = this.ShowShippingInformation;
            this.ShippingChargeDiv.Visible = this.ShowShippingInformation;
            this.CostCenterDiv.Visible = this.ShowCostCenter;
            this.SalespersonDiv.Visible = this.ShowSalesAgents;
        }

        #region "JSON Grid Binding"

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
                    productInCollection = collection.FirstOrDefault(x => x.ItemCode == product.ItemCode && x.ItemName == product.ItemName && x.Unit == product.Unit && x.Price == product.Price && x.Rate == product.Rate);
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
                    string[] colData = new string[11];

                    colData[0] = row.ItemCode;
                    colData[1] = row.ItemName;
                    colData[2] = row.Quantity.ToString(CultureInfo.CurrentCulture);
                    colData[3] = row.Unit;
                    colData[4] = row.Price.ToString(CultureInfo.CurrentCulture);
                    colData[5] = row.Amount.ToString(CultureInfo.CurrentCulture);
                    colData[6] = row.Discount.ToString(CultureInfo.CurrentCulture);
                    colData[7] = row.Subtotal.ToString(CultureInfo.CurrentCulture);
                    colData[8] = row.Rate.ToString(CultureInfo.CurrentCulture);
                    colData[9] = row.Tax.ToString(CultureInfo.CurrentCulture);
                    colData[10] = row.Total.ToString(CultureInfo.CurrentCulture);

                    rowData.Add(colData);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string data = serializer.Serialize(rowData);

                ProductGridViewDataHidden.Value = data;
            }
        }

        private Collection<ProductDetailsModel> GetTable()
        {
            Collection<ProductDetailsModel> productCollection = new Collection<ProductDetailsModel>();

            if (this.Session[this.ID] != null)
            {
                //Get an instance of the ProductDetailsModel collection stored in session.
                productCollection = (Collection<ProductDetailsModel>)this.Session[this.ID];

                //Summate the collection.
                productCollection = SummateProducts(productCollection);

                //Store the summed table in session.
                this.Session[this.ID] = productCollection;
            }

            return productCollection;
        }

        #endregion "JSON Grid Binding"

        #endregion "Page Initialization"
    }
}