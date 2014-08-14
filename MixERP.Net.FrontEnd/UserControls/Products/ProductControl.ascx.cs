using System.Collections.Generic;
using System.Data;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using MixERP.Net.WebControls.StockTransactionView.Data.Models;
using Resources;
using FormHelper = MixERP.Net.Common.Helpers.FormHelper;
using SessionHelper = MixERP.Net.BusinessLayer.Helpers.SessionHelper;
using System.Web.Script.Serialization;

namespace MixERP.Net.FrontEnd.UserControls.Products
{
    public class ControlData
    {
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
        public string TransactionType { get; set; }
        public string PartyCode { get; set; }
        public int PriceTypeId { get; set; }
        public string ReferenceNumber { get; set; }

        private readonly Collection<StockMasterDetailModel> details = new Collection<StockMasterDetailModel>();
        public Collection<StockMasterDetailModel> Details
        {
            get
            {
                return this.details;
            }
        }

        public void AddDetail(StockMasterDetailModel detail)
        {
            this.details.Add(detail);
        }

        public decimal RunningTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public string ShippingAddressCode { get; set; }
        public int ShippingCompanyId { get; set; }
        public decimal ShippingCharge { get; set; }
        public int CashRepositoryId { get; set; }
        public int CostCenterId { get; set; }
        public int AgentId { get; set; }
        public string StatementReference { get; set; }
    }

    /// <summary>
    /// Todo: This class is subject to be moved to a standalone server control class library.
    /// This UserControl provides a common interface for all transactions that are related to
    /// stock and/or inventory. Everything is handled in this class, except for the Save event.
    /// The save event is exposed to the page containing this control and should be handled there.
    /// </summary>
    public partial class ProductControl : UserControl
    {
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
        /// This property when set to true will display the RadioButtonList control which contains the transaction types.
        /// Transaction types are Cash and Credit.
        /// </summary>
        public bool DisplayTransactionTypeRadioButtonList { get; set; }

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
        /// This property when enabled will display cash repositories and their available balance.
        /// Not all available cash repositories will be displayed here but those which belong to the current (or logged in) branch office.
        /// This property must be enabled for transactions which have affect on cash ledger, namely "Direct Purchase" and "Direct Sales".
        /// </summary>
        public bool ShowCashRepository { get; set; }

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

        public string ErrorMessage
        {
            get
            {
                if (this.ErrorLabel != null)
                {
                    return this.ErrorLabelBottom.Text;
                }

                return string.Empty;
            }
            set
            {
                if (this.ErrorLabel != null)
                {
                    this.ErrorLabelBottom.Text = value;
                }
            }
        }

        private static string GetDropDownValue(ListControl listControl)
        {
            if (listControl != null)
            {
                if (listControl.SelectedItem != null)
                {
                    return listControl.SelectedItem.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// This function returns a new instance of ControlCollection class.
        /// The control collection is processed on the page which contains this UserControl
        /// for transaction posting.
        /// </summary>
        /// <returns></returns>
        private ControlData GetControls()
        {
            //Todo
            ControlData collection = new ControlData();
            collection.Date = Conversion.TryCastDate(this.DateTextBox.Text);

            collection.StoreId = Conversion.TryCastInteger(GetDropDownValue(this.StoreDropDownList));
            collection.TransactionType = this.TransactionTypeRadioButtonList.SelectedItem.Value;
            collection.PartyCode = this.PartyCodeTextBox.Text;
            collection.PriceTypeId = Conversion.TryCastInteger(GetDropDownValue(this.PriceTypeDropDownList));
            collection.ReferenceNumber = this.ReferenceNumberTextBox.Text;
            collection.RunningTotal = Conversion.TryCastDecimal(this.RunningTotalTextBox.Text);
            collection.TaxTotal = Conversion.TryCastDecimal(this.TaxTotalTextBox.Text);
            collection.GrandTotal = Conversion.TryCastDecimal(this.GrandTotalTextBox.Text);
            collection.ShippingAddressCode = this.ShippingAddressCodeHidden.Value;
            collection.ShippingCompanyId = Conversion.TryCastInteger(GetDropDownValue(this.ShippingCompanyDropDownList));
            collection.ShippingCharge = Conversion.TryCastDecimal(this.ShippingChargeTextBox.Text);
            collection.CashRepositoryId = Conversion.TryCastInteger(GetDropDownValue(this.CashRepositoryDropDownList));
            collection.CostCenterId = Conversion.TryCastInteger(GetDropDownValue(this.CostCenterDropDownList));
            collection.AgentId = Conversion.TryCastInteger(GetDropDownValue(this.SalespersonDropDownList));
            collection.StatementReference = this.StatementReferenceTextBox.Text;

            string json = ProductGridViewDataHidden.Value;

            var jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                StockMasterDetailModel detail = new StockMasterDetailModel();
                detail.ItemCode = item[0];
                detail.Quantity = Conversion.TryCastInteger(item[2]);
                detail.UnitName = item[3];
                detail.Price = Conversion.TryCastDecimal(item[4]);
                detail.Discount = Conversion.TryCastDecimal(item[6]);
                detail.TaxRate = Conversion.TryCastDecimal(item[8]);
                detail.Tax = Conversion.TryCastDecimal(item[9]);

                if (this.StoreDropDownList.SelectedItem != null)
                {
                    detail.StoreId = Conversion.TryCastInteger(this.StoreDropDownList.SelectedItem.Value);
                }

                collection.AddDetail(detail);
            }

            return collection;
        }

        /// <summary>
        /// This property provides a read-only access to all the controls of this UserControl.
        /// This property is accessed after the user clicks the "Save" button.
        /// The values of each control is read and then sent to the transaction posting engine.
        /// </summary>
        public ControlData GetForm
        {
            get
            {
                return this.GetControls();
            }
        }

        public Unit TopPanelWidth
        {
            get
            {
                if (this.TopPanel != null)
                {
                    return this.TopPanel.Width;
                }

                return new Unit(0);
            }
            set
            {
                if (this.TopPanel != null)
                {
                    this.TopPanel.Width = value;
                }
            }

        }


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


        #region "GridView Footer"

        #endregion

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

            if (this.PriceTypeDropDownList.SelectedItem != null)
            {
                DropDownListHelper.SetSelectedValue(this.PriceTypeDropDownList, this.model.PriceTypeId.ToString(SessionHelper.GetCulture()));
            }

            this.ReferenceNumberTextBox.Text = this.model.ReferenceNumber;
            this.StatementReferenceTextBox.Text = this.model.StatementReference;

            this.Session[this.ID] = this.model.View;
            this.Session["TranIdCollection"] = this.model.TransactionIdCollection;
            this.ClearSession("Product");
        }

        public Collection<int> GetTranIdCollection()
        {
            Collection<int> tranIdCollection = new Collection<int>();

            if (this.Session["TranIdCollection"] != null)
            {
                tranIdCollection = this.Session["TranIdCollection"] as Collection<int>;
            }

            return tranIdCollection;
        }

        private void ClearSession(string key)
        {
            if (this.Session[key] != null)
            {
                this.Session.Remove(key);
            }
        }

        private void LoadCostCenters()
        {
            if (this.SubBook == SubTranBook.Direct || this.SubBook == SubTranBook.Invoice || this.SubBook == SubTranBook.Delivery || this.SubBook == SubTranBook.Receipt)
            {
                string displayField = ConfigurationHelper.GetDbParameter("CostCenterDisplayField");
                DropDownListHelper.BindDropDownList(this.CostCenterDropDownList, "office", "cost_centers", "cost_center_id", displayField);
            }
            else
            {
                this.CostCenterRow.Visible = false;
            }
        }

        private void LoadStores()
        {
            if (this.SubBook == SubTranBook.Direct || this.SubBook == SubTranBook.Invoice || this.SubBook == SubTranBook.Delivery || this.SubBook == SubTranBook.Receipt)
            {
                string displayField = ConfigurationHelper.GetDbParameter("StoreDisplayField");
                DropDownListHelper.BindDropDownList(this.StoreDropDownList, "office", "stores", "store_id", displayField);
            }
            else
            {
                this.StoreLiteral.Visible = false;
                this.StoreDropDownList.Visible = false;
            }
        }

        private void LoadCashRepositories()
        {
            if (this.ShowCashRepository)
            {
                string officeId = Conversion.TryCastString(SessionHelper.GetOfficeId());

                using (DataTable table = BusinessLayer.Helpers.FormHelper.GetTable("office", "cash_repositories", "office_id", officeId))
                {
                    string displayField = ConfigurationHelper.GetDbParameter("CashRepositoryDisplayField");
                    DropDownListHelper.BindDropDownList(this.CashRepositoryDropDownList, table, "cash_repository_id", displayField);
                }
            }
            else
            {
                this.CashRepositoryRow.Visible = false;
                this.CashRepositoryBalanceRow.Visible = false;
            }
        }

        private void LoadLabels()
        {
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
        }

        private void LoadTransactionTypeLabel()
        {
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

        private void LoadPriceTypes()
        {
            if (this.Book == TranBook.Sales)
            {
                string displayField = ConfigurationHelper.GetDbParameter("PriceTypeDisplayField");
                DropDownListHelper.BindDropDownList(this.PriceTypeDropDownList, "core", "price_types", "price_type_id", displayField);
            }
            else
            {
                this.PriceTypeLiteral.Visible = false;
                this.PriceTypeDropDownList.Visible = false;

                this.ShippingAddressRow.Visible = false;
                this.ShippingChargeRow.Visible = false;
                this.ShippingCompanyRow.Visible = false;
            }

        }

        private void LoadSalesperson()
        {
            this.SalespersonRow.Visible = false;

            if (this.Book == TranBook.Sales)
            {
                string displayField = ConfigurationHelper.GetDbParameter("AgentDisplayField");
                DropDownListHelper.BindDropDownList(this.SalespersonDropDownList, "core", "agents", "agent_id", displayField);
                this.SalespersonRow.Visible = true;
            }
        }

        private void LoadShippers()
        {
            this.ShippingAddressRow.Visible = false;
            this.ShippingChargeRow.Visible = false;
            this.ShippingCompanyRow.Visible = false;

            if (this.Book == TranBook.Sales)
            {
                if (this.SubBook == SubTranBook.Direct || this.SubBook == SubTranBook.Delivery)
                {
                    string displayField = ConfigurationHelper.GetDbParameter("ShipperDisplayField");
                    DropDownListHelper.BindDropDownList(this.ShippingCompanyDropDownList, "core", "shippers", "shipper_id", displayField);

                    this.ShippingAddressRow.Visible = true;
                    this.ShippingChargeRow.Visible = true;
                    this.ShippingCompanyRow.Visible = true;
                }
            }
        }

        private void InitializeControls()
        {
            this.LoadLabels();
            this.LoadTransactionTypeLabel();
            this.LoadPriceTypes();
            this.LoadShippers();
            this.LoadCostCenters();
            this.LoadSalesperson();
            this.LoadStores();
            this.LoadCashRepositories();
            this.LoadParties();
        }

        //Todo:Remove this implementation
        private void LoadParties()
        {
            //using (PartyData data = new PartyData())
            //{
            //    this.PartyDropDownList.DataSource = data.GetParties();
            //    this.PartyDropDownList.DataTextField = "Text";
            //    this.PartyDropDownList.DataValueField = "Value";
            //    this.PartyDropDownList.DataBind();
            //    this.PartyDropDownList.Items.Insert(0, new ListItem(Titles.Select, string.Empty));
            //}
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Moved from Page_Init
            this.TitleLabel.Text = this.Text;
            this.Page.Title = this.Text;
            this.TransactionTypeLiteral.Visible = this.DisplayTransactionTypeRadioButtonList;
            this.TransactionTypeRadioButtonList.Visible = this.DisplayTransactionTypeRadioButtonList;
        }

        private void BindGridView()
        {
            //Todo:
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
    }
}