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
using MixERP.Net.WebControls.StockTransactionView.Data.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using SessionHelper = MixERP.Net.BusinessLayer.Helpers.SessionHelper;

namespace MixERP.Net.FrontEnd.UserControls.Products
{
    /// <summary>
    /// Todo: This class is subject to be moved to a standalone server control class library.
    /// This UserControl provides a common interface for all transactions that are related to
    /// stock and/or inventory. Everything is handled in this class, except for the Save event.
    /// The save event is exposed to the page containing this control and should be handled there.
    /// </summary>
    public partial class ProductControl : UserControl
    {
        #region Properties
        /// <summary>
        /// Transaction book for products are Sales and Purchase.
        /// </summary>
        public TranBook Book { get; set; }

        /// <summary>
        /// Sub transaction books are maintained for breaking down the Purchase and Sales transaction into smaller steps
        /// such as Quotations, Orders, Deliveries, e.t.c.
        /// </summary>
        public SubTranBook SubBook { get; set; }

        /// <summary>
        /// The title displayed in the form.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// This property when set to true will display transaction types.
        /// Transaction types are Cash and Credit.
        /// </summary>
        public bool ShowTransactionType { get; set; }

        /// <summary>
        /// This property when set to true will display stores.
        /// </summary>
        public bool ShowStore { get; set; }

        /// <summary>
        /// This property when set to true will display stores.
        /// </summary>
        public bool ShowPriceTypes { get; set; }


        /// <summary>
        /// This property when enabled will display cash repositories and their available balance.
        /// Not all available cash repositories will be displayed here but those which belong to the current (or logged in) branch office.
        /// This property must be enabled for transactions which have affect on cash ledger, namely "Direct Purchase" and "Direct Sales".
        /// </summary>
        public bool ShowCashRepository { get; set; }

        /// <summary>
        /// This property when enabled will display shipping information, such as shipping address, shipping company, and shipping costs.
        /// </summary>
        public bool ShowShippingInformation { get; set; }

        /// <summary>
        /// This property when enabled will display cost centers.
        /// </summary>
        public bool ShowCostCenter { get; set; }

        /// <summary>
        /// This property when enabled will display sales agents.
        /// </summary>
        public bool ShowSalesAgents { get; set; }

        /// <summary>
        /// This property when set to true will verify the stock against the credit inventory transactions or "Sales".
        /// Since negative stock is not allowed, you will not be able to add a product to the grid.
        /// This property must be enabled for Sales transaction which affect the available inventory on hand.
        /// Please also note that even when this property is enabled, the products having the switch "Maintain Stock" set to "Off"
        /// will not be checked for stock availability.
        /// This property should be disabled or set to false for stock transactions that do not affect stock such as "Quotations", "Orders", e.t.c.
        /// </summary>
        public bool VerifyStock { get; set; }


        /// <summary>
        /// This property is used to temporarily store pre assigned instance of transactions for merging transactions
        /// and creating a batch transactions.
        /// Some cases:
        /// Multiple Sales Quotations --> Sales Order.
        /// Multiple Sales Quotations --> Sales Delivery.
        /// </summary>
        private MergeModel model = new MergeModel();
        /// <summary>
        /// This class is a representation of the controls in this UserControl.
        /// </summary>
        #endregion

        #region "Page Initialization"
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ClearSession(this.ID);
            }

            this.InitializeControls();
            this.LoadValuesFromSession();
            this.BindGridView();
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
            this.PartyCodeTextBox.Text = this.model.PartyCode.ToString(LocalizationHelper.GetCurrentCulture());
            this.PriceTypeIdHidden.Value = this.model.PriceTypeId.ToString(SessionHelper.GetCulture());

            this.ReferenceNumberTextBox.Text = this.model.ReferenceNumber;
            this.StatementReferenceTextBox.Text = this.model.StatementReference;

            this.Session[this.ID] = this.model.View;
            TranIdCollectionHiddenField.Value = string.Join(",", this.model.TransactionIdCollection);
            this.ClearSession("Product");
        }


        #region "JSON Grid Binding"
        private void BindGridView()
        {
            Collection<ProductDetailsModel> table = this.GetTable();

            if (table.Count > 0)
            {
                List<string[]> rowData = new List<string[]>();

                foreach (var row in table)
                {
                    string[] colData = new string[11];

                    colData[0] = row.ItemCode;
                    colData[1] = row.ItemName;
                    colData[2] = row.Quantity.ToString(CultureInfo.CurrentUICulture);
                    colData[3] = row.Unit;
                    colData[4] = row.Price.ToString(CultureInfo.CurrentUICulture);
                    colData[5] = row.Amount.ToString(CultureInfo.CurrentUICulture);
                    colData[6] = row.Discount.ToString(CultureInfo.CurrentUICulture);
                    colData[7] = row.Subtotal.ToString(CultureInfo.CurrentUICulture);
                    colData[8] = row.Rate.ToString(CultureInfo.CurrentUICulture);
                    colData[9] = row.Tax.ToString(CultureInfo.CurrentUICulture);
                    colData[10] = row.Total.ToString(CultureInfo.CurrentUICulture);

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

                //Store the summated table in session.
                this.Session[this.ID] = productCollection;

            }

            return productCollection;
        }

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

        #endregion

        private void LoadLabels()
        {
            //Todo: Remove the inconsistency of this usage.

            this.DateLiteral.Text = HtmlControlHelper.GetLabel(this.DateTextBox.ClientID, Titles.ValueDate);
            this.StoreLiteral.Text = HtmlControlHelper.GetLabel(this.StoreDropDownList.ClientID, Titles.SelectStore);

            this.PartyLiteral.Text = HtmlControlHelper.GetLabel(this.PartyCodeTextBox.ClientID, Titles.SelectParty);
            this.PriceTypeLiteral.Text = HtmlControlHelper.GetLabel(this.PriceTypeDropDownList.ClientID, Titles.PriceType);
            this.ReferenceNumberLiteral.Text = HtmlControlHelper.GetLabel(this.ReferenceNumberTextBox.ClientID, Titles.ReferenceNumberAbbreviated);

            this.RunningTotalTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(this.RunningTotalTextBox.ClientID, Titles.RunningTotal);
            this.TaxTotalTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(this.TaxTotalTextBox.ClientID, Titles.TaxTotal);
            this.GrandTotalTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(this.GrandTotalTextBox.ClientID, Titles.GrandTotal);
            this.ShippingAddressDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(this.ShippingAddressDropDownList.ClientID, Titles.ShippingAddress);
            this.ShippingCompanyDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(this.ShippingCompanyDropDownList.ClientID, Titles.ShippingCompany);
            this.ShippingChargeTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(this.ShippingChargeTextBox.ClientID, Titles.ShippingCharge);
            this.CashRepositoryDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(this.CashRepositoryDropDownList.ClientID, Titles.CashRepository);
            this.CashRepositoryBalanceTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(this.CashRepositoryBalanceTextBox.ClientID, Titles.CashRepositoryBalance);
            this.CostCenterDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(this.CostCenterDropDownList.ClientID, Titles.CostCenter);
            this.SalespersonDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(this.SalespersonDropDownList.ClientID, Titles.Salesperson);
            this.StatementReferenceTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(this.StatementReferenceTextBox.ClientID, Titles.StatementReference);

            if (this.Book == TranBook.Sales)
            {
                this.TransactionTypeLiteral.Text = HtmlControlHelper.GetLabel(Titles.SalesType);
            }
            else
            {
                this.TransactionTypeLiteral.Text = HtmlControlHelper.GetLabel(Titles.PurchaseType);
            }
        }

        public string GetTranBook()
        {
            if (this.Book == TranBook.Sales)
            {
                return "Sales";
            }

            return "Purchase";
        }

        private void SetVisibleStates()
        {
            this.CashRepositoryRow.Visible = this.ShowCashRepository;
            this.CashRepositoryBalanceRow.Visible = this.ShowCashRepository;
            this.StoreDiv.Visible = this.ShowStore;
            this.TransactionTypeDiv.Visible = this.ShowTransactionType;
            this.PriceTypeDiv.Visible = this.ShowPriceTypes;
            this.ShippingAddressRow.Visible = this.ShowShippingInformation;
            this.ShippingCompanyRow.Visible = this.ShowShippingInformation;
            this.ShippingChargeRow.Visible = this.ShowShippingInformation;
            this.CostCenterRow.Visible = this.ShowCostCenter;
            this.SalespersonRow.Visible = this.ShowSalesAgents;
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TitleLabel.Text = this.Text;
            this.Page.Title = this.Text;
        }

    }
}