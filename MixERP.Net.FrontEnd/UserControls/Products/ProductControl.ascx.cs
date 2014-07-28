using System.Collections.Generic;
using System.Data;
using System.Threading;
using MixERP.Net.BusinessLayer.Core;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.BusinessLayer.Office;
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

            if (this.ProductGridView != null)
            {
                if (this.ProductGridView.Rows.Count > 0)
                {
                    for (int i = 1; i < this.ProductGridView.Rows.Count; i++)
                    {
                        StockMasterDetailModel detail = new StockMasterDetailModel();

                        detail.ItemCode = this.ProductGridView.Rows[i].Cells[0].Text;
                        detail.Quantity = Conversion.TryCastInteger(this.ProductGridView.Rows[i].Cells[2].Text);
                        detail.UnitName = this.ProductGridView.Rows[i].Cells[3].Text;
                        detail.Price = Conversion.TryCastDecimal(this.ProductGridView.Rows[i].Cells[4].Text);
                        detail.Discount = Conversion.TryCastDecimal(this.ProductGridView.Rows[i].Cells[6].Text);
                        detail.TaxRate = Conversion.TryCastDecimal(this.ProductGridView.Rows[i].Cells[8].Text);
                        detail.Tax = Conversion.TryCastDecimal(this.ProductGridView.Rows[i].Cells[9].Text);

                        if (this.StoreDropDownList.SelectedItem != null)
                        {
                            detail.StoreId = Conversion.TryCastInteger(this.StoreDropDownList.SelectedItem.Value);
                        }

                        collection.AddDetail(detail);
                    }
                }
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

        /// <summary>
        /// This event will be raised on SaveButon's click event.
        /// </summary>
        public event EventHandler SaveButtonClick;

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //Validation Check Start

            if (this.ProductGridView.Rows.Count.Equals(0))
            {
                this.ErrorLabel.Text = Warnings.NoItemFound;
                return;
            }

            //If this is a purchase on cash transaction, we need to make sure
            //that the selected cash repository has enough balance for the credit transaction.
            //Remember: 
            //1. MixERP does not allow negative cash transaction.
            //2. Cash is maintained on LIFO principal.

            //The MixERP LIFO principal

            //LAST IN
            //The cash would be in at last --> Last In. 
            //This means that you would have to first approve a transaction which has cash on the debit side before it shows up in the effective balance. 
            //If you approve the transaction, cash is in-->Last In.
            //If you reject or ignore the transaction, there is no effect.

            //FIRST OUT
            //The cash would be out at first --> First Out.
            //This means that even when you have not approved a transaction which has cash on the credit side, it reduces the cash balance. 
            //So, if you approve the transaction, there is no effect since the cash was already out-->First Out. 
            //The actual cash balance is restored only when you reject the transaction.

            //If you are still not so sure what it means, don't worry. 
            //The calculation happens on the database level.
            //If anything goes wrong, throw stones to your DBAs.

            if (this.Book == TranBook.Purchase && this.CashRepositoryRow.Visible)
            {
                this.UpdateRepositoryBalance();

                decimal repositoryBalance = Conversion.TryCastDecimal(this.CashRepositoryBalanceTextBox.Text);
                decimal grandTotal = Conversion.TryCastDecimal(this.GrandTotalTextBox.Text);

                if (grandTotal > repositoryBalance)
                {
                    this.ErrorLabel.Text = Warnings.NotEnoughCash;
                    return;
                }
            }

            //Check if the shipping charge textbox has a value.
            if (!string.IsNullOrWhiteSpace(this.ShippingChargeTextBox.Text))
            {
                //Check if the value actually was a number.
                if (!Conversion.IsNumeric(this.ShippingChargeTextBox.Text))
                {
                    FormHelper.MakeDirty(this.ShippingChargeTextBox);
                    return;
                }
            }
            //Validation Check End
            //I am happy now.

            //Now exposing the button click event.
            this.OnSaveButtonClick(sender, e);
        }

        /// <summary>
        ///This method when called will raise a "SaveButtonClick" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (this.SaveButtonClick != null)
            {
                //Raise the event.
                this.SaveButtonClick(sender, e);
            }
        }

        protected void ProductGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.CommandName == "Delete")
            {
                this.DeleteRow(e);
            }
            if (e.CommandName == "Add")
            {
                this.AddRow();
            }
        }

        public void DeleteRow(GridViewCommandEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            //Get an instance of the collection of the products stored in the grid.
            Collection<ProductDetailsModel> dataSource = this.GetTable();

            //Get the instance of grid view row on which the the command was triggered.
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

            int index = row.RowIndex;
            dataSource.RemoveAt(index);

            this.Session[this.ID] = dataSource;
            this.BindGridView();
        }

        public void AddRow()
        {
            using (TextBox itemCodeTextBox = this.FindFooterControl("ItemCodeTextBox") as TextBox)
            {
                using (DropDownList itemDropDownList = this.FindFooterControl("ItemDropDownList") as DropDownList)
                {
                    using (HiddenField itemCodeHidden = this.UpdatePanel1.FindControl("ItemCodeHidden") as HiddenField)
                    {
                        using (TextBox quantityTextBox = this.FindFooterControl("QuantityTextBox") as TextBox)
                        {
                            using (TextBox priceTextBox = this.FindFooterControl("PriceTextBox") as TextBox)
                            {
                                using (TextBox discountTextBox = this.FindFooterControl("DiscountTextBox") as TextBox)
                                {
                                    using (TextBox taxRateTextBox = this.FindFooterControl("TaxRateTextBox") as TextBox)
                                    {
                                        using (TextBox taxTextBox = this.FindFooterControl("TaxTextBox") as TextBox)
                                        {
                                            using (
                                                DropDownList unitDropDownList =
                                                    this.FindFooterControl("UnitDropDownList") as DropDownList)
                                            {
                                                using (
                                                    HiddenField unitNameHidden =
                                                        this.UpdatePanel1.FindControl("UnitNameHidden") as HiddenField)
                                                {
                                                    using (
                                                        HiddenField unitIdHidden =
                                                            this.UpdatePanel1.FindControl("UnitIdHidden") as HiddenField)
                                                    {
                                                        if (itemCodeTextBox != null)
                                                        {
                                                            string itemCode = itemCodeTextBox.Text;

                                                            if (itemDropDownList != null)
                                                            {
                                                                if (itemCodeHidden != null)
                                                                {
                                                                    string itemName = itemCodeHidden.Value;
                                                                    if (quantityTextBox != null)
                                                                    {
                                                                        int quantity =
                                                                            Conversion.TryCastInteger(quantityTextBox.Text);
                                                                        if (unitNameHidden != null)
                                                                        {
                                                                            string unit = unitNameHidden.Value;

                                                                            if (unitIdHidden != null)
                                                                            {
                                                                                int unitId =
                                                                                    Conversion.TryCastInteger(
                                                                                        unitIdHidden.Value);
                                                                                if (priceTextBox != null)
                                                                                {
                                                                                    decimal price =
                                                                                        Conversion.TryCastDecimal(
                                                                                            priceTextBox.Text);
                                                                                    if (discountTextBox != null)
                                                                                    {
                                                                                        decimal discount =
                                                                                            Conversion.TryCastDecimal(
                                                                                                discountTextBox.Text);
                                                                                        if (taxRateTextBox != null)
                                                                                        {
                                                                                            decimal taxRate =
                                                                                                Conversion.TryCastDecimal(
                                                                                                    taxRateTextBox.Text);
                                                                                            if (taxTextBox != null)
                                                                                            {
                                                                                                decimal tax =
                                                                                                    Conversion
                                                                                                        .TryCastDecimal(
                                                                                                            taxTextBox.Text);
                                                                                                int storeId = 0;

                                                                                                if (
                                                                                                    this.StoreDropDownList
                                                                                                        .SelectedItem !=
                                                                                                    null)
                                                                                                {
                                                                                                    storeId =
                                                                                                        Conversion
                                                                                                            .TryCastInteger(
                                                                                                                this
                                                                                                                    .StoreDropDownList
                                                                                                                    .SelectedItem
                                                                                                                    .Value);
                                                                                                }

                                                                                                #region Validation

                                                                                                if (
                                                                                                    string
                                                                                                        .IsNullOrWhiteSpace(
                                                                                                            itemCode))
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        itemCodeTextBox);
                                                                                                    return;
                                                                                                }

                                                                                                FormHelper.RemoveDirty(
                                                                                                    itemCodeTextBox);

                                                                                                if (
                                                                                                    !Items.ItemExistsByCode(
                                                                                                        itemCode))
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        itemCodeTextBox);
                                                                                                    return;
                                                                                                }
                                                                                                FormHelper.RemoveDirty(
                                                                                                    itemCodeTextBox);

                                                                                                if (quantity < 1)
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        quantityTextBox);
                                                                                                    return;
                                                                                                }
                                                                                                FormHelper.RemoveDirty(
                                                                                                    quantityTextBox);

                                                                                                if (
                                                                                                    !Units.UnitExistsByName(
                                                                                                        unit))
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        unitDropDownList);
                                                                                                    return;
                                                                                                }
                                                                                                FormHelper.RemoveDirty(
                                                                                                    unitDropDownList);

                                                                                                if (price <= 0)
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        priceTextBox);
                                                                                                    return;
                                                                                                }
                                                                                                FormHelper.RemoveDirty(
                                                                                                    priceTextBox);

                                                                                                if (discount < 0)
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        discountTextBox);
                                                                                                    return;
                                                                                                }
                                                                                                if (discount >
                                                                                                    (price * quantity))
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        discountTextBox);
                                                                                                    return;
                                                                                                }
                                                                                                FormHelper.RemoveDirty(
                                                                                                    discountTextBox);

                                                                                                if (tax < 0)
                                                                                                {
                                                                                                    FormHelper.MakeDirty(
                                                                                                        taxTextBox);
                                                                                                    return;
                                                                                                }
                                                                                                FormHelper.RemoveDirty(
                                                                                                    taxTextBox);

                                                                                                if (this.VerifyStock)
                                                                                                {
                                                                                                    if (this.Book ==
                                                                                                        TranBook.Sales)
                                                                                                    {
                                                                                                        if (
                                                                                                            Items
                                                                                                                .IsStockItem
                                                                                                                (
                                                                                                                    itemCode))
                                                                                                        {
                                                                                                            decimal
                                                                                                                itemInStock
                                                                                                                    =
                                                                                                                    Items
                                                                                                                        .CountItemInStock
                                                                                                                        (itemCode,
                                                                                                                            unitId,
                                                                                                                            storeId);
                                                                                                            if (quantity >
                                                                                                                itemInStock)
                                                                                                            {
                                                                                                                FormHelper
                                                                                                                    .MakeDirty
                                                                                                                    (
                                                                                                                        quantityTextBox);
                                                                                                                this
                                                                                                                    .ErrorLabel
                                                                                                                    .Text =
                                                                                                                    String
                                                                                                                        .Format
                                                                                                                        (
                                                                                                                            Thread
                                                                                                                                .CurrentThread
                                                                                                                                .CurrentCulture,
                                                                                                                            Warnings
                                                                                                                                .InsufficientStockWarning,
                                                                                                                            itemInStock
                                                                                                                                .ToString
                                                                                                                                ("G29",
                                                                                                                                    Thread
                                                                                                                                        .CurrentThread
                                                                                                                                        .CurrentCulture),
                                                                                                                            unitNameHidden
                                                                                                                                .Value,
                                                                                                                            itemDropDownList
                                                                                                                                .SelectedItem
                                                                                                                                .Text);
                                                                                                                return;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }

                                                                                                #endregion

                                                                                                this.AddRowToTable(itemCode,
                                                                                                    itemName, quantity, unit,
                                                                                                    price, discount, taxRate,
                                                                                                    tax);

                                                                                                this.BindGridView();
                                                                                                itemCodeTextBox.Text = "";

                                                                                                quantityTextBox.Text =
                                                                                                    (1).ToString(
                                                                                                        CultureInfo
                                                                                                            .InvariantCulture);
                                                                                                priceTextBox.Text = "";
                                                                                                discountTextBox.Text = "";
                                                                                                taxTextBox.Text = "";

                                                                                                itemCodeTextBox.Focus();
                                                                                                this.LoadFooter();
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void ProductGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Conversion.TryCastInteger(e.Row.Cells[2].Text).Equals(0))
                {
                    e.Row.Visible = false;
                    return;
                }

                //Yeaaaaa! This is a data row. Yippee!!!!!

                ImageButton deleteImageButton = e.Row.FindControl("DeleteImageButton") as ImageButton;

                if (deleteImageButton != null)
                {
                    //Tell the script manager that this button should fire an asynchronous post-back event.
                    var scriptManager = ScriptManager.GetCurrent(this.Page);
                    if (scriptManager != null)
                    {
                        scriptManager.RegisterAsyncPostBackControl(deleteImageButton);
                    }
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
            this.AddUnitNameHidden();
            this.AddUnitIdHidden();
            this.AddItemCodeHidden();
            this.LoadFooter();
            var scriptManager = ScriptManager.GetCurrent(this.Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterAsyncPostBackControl(this.ProductGridView);
            }
        }

        private void AddUnitNameHidden()
        {
            using (HiddenField unitNameHidden = new HiddenField())
            {
                unitNameHidden.ID = "UnitNameHidden";
                unitNameHidden.ClientIDMode = ClientIDMode.Static;
                this.UpdatePanel1.ContentTemplateContainer.Controls.Add(unitNameHidden);
            }
        }

        private void AddUnitIdHidden()
        {
            using (HiddenField unitIdHidden = new HiddenField())
            {
                unitIdHidden.ID = "UnitIdHidden";
                unitIdHidden.ClientIDMode = ClientIDMode.Static;
                this.UpdatePanel1.ContentTemplateContainer.Controls.Add(unitIdHidden);
            }
        }

        private void AddItemCodeHidden()
        {
            using (HiddenField unitIdHidden = new HiddenField())
            {
                unitIdHidden.ID = "ItemCodeHidden";
                unitIdHidden.ClientIDMode = ClientIDMode.Static;
                this.UpdatePanel1.ContentTemplateContainer.Controls.Add(unitIdHidden);
            }
        }

        #region "GridView Footer"
        private void LoadFooter()
        {
            using (GridViewRow footer = this.ProductGridView.FooterRow)
            {
                if (footer == null)
                {
                    return;
                }

                AddItemCodeTextBox(footer, 0);
                this.AddItemDropDownList(footer, 1);
                AddQuantityTextBox(footer, 2);
                this.AddUnitDropDownList(footer, 3);
                AddPriceTextBox(footer, 4);
                AddAmountTextBox(footer, 5);
                AddDiscountTextBox(footer, 6);
                AddSubtotalTextBox(footer, 7);
                AddTaxRateTextBox(footer, 8);
                AddTaxTextBox(footer, 9);
                AddTotalTextBox(footer, 10);
                this.AddAddButton(footer, 11);
            }
        }

        private static void AddItemCodeTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox itemCodeTextBox = new TextBox())
            {
                itemCodeTextBox.ID = "ItemCodeTextBox";
                itemCodeTextBox.ClientIDMode = ClientIDMode.Static;
                itemCodeTextBox.Attributes.Add("onblur", "selectDropDownListByValue(this.id, 'ItemDropDownList');");
                itemCodeTextBox.ToolTip = Titles.AltC;
                itemCodeTextBox.Width = 60;

                AddControlToGridViewRow(row, itemCodeTextBox, index);
            }
        }

        private void AddItemDropDownList(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (DropDownList itemDropDownList = new DropDownList())
            {
                itemDropDownList.ID = "ItemDropDownList";
                itemDropDownList.ClientIDMode = ClientIDMode.Static;
                itemDropDownList.Attributes.Add("onchange", "document.getElementById('ItemCodeTextBox').value = this.options[this.selectedIndex].value;document.getElementById('ItemCodeHidden').value = this.options[this.selectedIndex].value;if(this.selectedIndex == 0) { return false };");
                itemDropDownList.Attributes.Add("onblur", "getPrice();");
                itemDropDownList.ToolTip = Titles.CtrlI;
                itemDropDownList.Width = 300;

                var scriptManager = ScriptManager.GetCurrent(this.Page);

                if (scriptManager != null)
                {
                    scriptManager.RegisterAsyncPostBackControl(itemDropDownList);
                }

                AddControlToGridViewRow(row, itemDropDownList, index);
                this.LoadItems();
            }
        }

        private static void AddQuantityTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox quantityTextBox = new TextBox())
            {
                quantityTextBox.ID = "QuantityTextBox";
                quantityTextBox.ClientIDMode = ClientIDMode.Static;
                quantityTextBox.Attributes.Add("onblur", "updateTax();calculateAmount();");
                quantityTextBox.CssClass = "right";
                quantityTextBox.ToolTip = Titles.CtrlQ;
                quantityTextBox.Width = 50;
                quantityTextBox.Text = (1).ToString(CultureInfo.InvariantCulture);

                AddControlToGridViewRow(row, quantityTextBox, index);
            }
        }

        private void AddUnitDropDownList(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (DropDownList unitDropDownList = new DropDownList())
            {
                unitDropDownList.ID = "UnitDropDownList";
                unitDropDownList.ClientIDMode = ClientIDMode.Static;
                unitDropDownList.Attributes.Add("onchange", "$('#UnitNameHidden').val($(this).children('option').filter(':selected').text());$('#UnitIdHidden').val($(this).children('option').filter(':selected').val());");
                unitDropDownList.Attributes.Add("onblur", "getPrice();");
                unitDropDownList.ToolTip = Titles.CtrlU;
                unitDropDownList.Width = 70;
                var scriptManager = ScriptManager.GetCurrent(this.Page);

                if (scriptManager != null)
                {
                    scriptManager.RegisterAsyncPostBackControl(unitDropDownList);
                }

                AddControlToGridViewRow(row, unitDropDownList, index);
            }
        }


        private static void AddPriceTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox priceTextBox = new TextBox())
            {
                priceTextBox.ID = "PriceTextBox";
                priceTextBox.ClientIDMode = ClientIDMode.Static;
                priceTextBox.Attributes.Add("onblur", "updateTax();calculateAmount();");
                priceTextBox.CssClass = "right number";
                priceTextBox.ToolTip = Titles.AltP;
                priceTextBox.Width = 65;

                AddControlToGridViewRow(row, priceTextBox, index);
            }
        }

        private static void AddAmountTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox amountTextBox = new TextBox())
            {
                amountTextBox.ID = "AmountTextBox";
                amountTextBox.ClientIDMode = ClientIDMode.Static;
                amountTextBox.CssClass = "right number";
                amountTextBox.ReadOnly = true;
                amountTextBox.Width = 70;

                AddControlToGridViewRow(row, amountTextBox, index);
            }
        }

        private static void AddDiscountTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox discountTextBox = new TextBox())
            {
                discountTextBox.ID = "DiscountTextBox";
                discountTextBox.ClientIDMode = ClientIDMode.Static;
                discountTextBox.CssClass = "right number";
                discountTextBox.Attributes.Add("onblur", "updateTax();calculateAmount();");
                discountTextBox.ToolTip = Titles.CtrlD;

                discountTextBox.Width = 50;

                AddControlToGridViewRow(row, discountTextBox, index);
            }
        }

        private static void AddSubtotalTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox subtotalTextBox = new TextBox())
            {
                subtotalTextBox.ID = "SubtotalTextBox";
                subtotalTextBox.ClientIDMode = ClientIDMode.Static;
                subtotalTextBox.CssClass = "right number";
                subtotalTextBox.ReadOnly = true;
                subtotalTextBox.Width = 70;

                AddControlToGridViewRow(row, subtotalTextBox, index);
            }
        }

        private static void AddTaxRateTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox taxRateTextBox = new TextBox())
            {
                taxRateTextBox.ID = "TaxRateTextBox";
                taxRateTextBox.ClientIDMode = ClientIDMode.Static;
                taxRateTextBox.Attributes.Add("onblur", "updateTax();calculateAmount();");
                taxRateTextBox.CssClass = "right";
                taxRateTextBox.Width = 40;

                AddControlToGridViewRow(row, taxRateTextBox, index);
            }
        }

        private static void AddTaxTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox taxTextBox = new TextBox())
            {
                taxTextBox.ID = "TaxTextBox";
                taxTextBox.ClientIDMode = ClientIDMode.Static;
                taxTextBox.Attributes.Add("onblur", "calculateAmount();");
                taxTextBox.CssClass = "right number";
                taxTextBox.Width = 50;
                taxTextBox.ToolTip = Titles.CtrlT;

                AddControlToGridViewRow(row, taxTextBox, index);
            }
        }

        private static void AddTotalTextBox(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (TextBox totalTextBox = new TextBox())
            {
                totalTextBox.ID = "TotalTextBox";
                totalTextBox.ClientIDMode = ClientIDMode.Static;
                totalTextBox.CssClass = "right number";
                totalTextBox.ReadOnly = true;
                totalTextBox.Width = 70;

                AddControlToGridViewRow(row, totalTextBox, index);
            }
        }

        private void AddAddButton(GridViewRow row, int index)
        {
            if (row == null)
            {
                return;
            }

            using (Button addButton = new Button())
            {
                addButton.ID = "AddButton";
                addButton.OnClientClick = "calculateAmount();";
                addButton.CommandName = "Add";
                addButton.Text = Titles.Add;
                addButton.ToolTip = Titles.CtrlReturn;

                var scriptManager = ScriptManager.GetCurrent(this.Page);

                if (scriptManager != null)
                {
                    scriptManager.RegisterAsyncPostBackControl(addButton);
                }

                AddControlToGridViewRow(row, addButton, index);
            }
        }

        private static void AddControlToGridViewRow(GridViewRow row, Control control, int columnIndex)
        {
            row.Cells[columnIndex].Controls.Add(control);
        }
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

            this.PartyHidden.Value = this.model.PartyCode.ToString(LocalizationHelper.GetCurrentCulture());
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
                    this.UpdateRepositoryBalance();
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

        private Control FindFooterControl(string controlId)
        {
            using (GridViewRow footer = this.ProductGridView.FooterRow)
            {
                if (footer == null)
                {
                    return null;
                }

                foreach (TableCell cell in footer.Cells)
                {
                    Control control = cell.FindControl(controlId);

                    if (control != null)
                    {
                        return control;
                    }
                }
            }

            return null;
        }

        private void LoadItems()
        {
            //using (DropDownList itemDropDownList = this.FindFooterControl("ItemDropDownList") as DropDownList)
            //{
            //    if (itemDropDownList == null)
            //    {
            //        return;
            //    }

            //    using (ItemData data = new ItemData())
            //    {
            //        if (this.Book == TranBook.Sales)
            //        {
            //            itemDropDownList.DataSource = data.GetItems();
            //        }
            //        else
            //        {
            //            itemDropDownList.DataSource = data.GetStockItems();
            //        }

            //        itemDropDownList.DataTextField = "Text";
            //        itemDropDownList.DataValueField = "Value";
            //        itemDropDownList.DataBind();
            //    }
            //}
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
            //if (this.Request.Form["__EVENTTARGET"] != null)
            //{
            //    Control c = this.Page.FindControl(this.Request.Form["__EVENTTARGET"]);
            //    if (c != null)
            //    {
            //        if (c.ID.Equals("UnitDropDownList"))
            //        {
            //            this.UnitDropDownList_SelectedIndexChanged();
            //        }
            //    }
            //}

            //Moved from Page_Init
            this.TitleLabel.Text = this.Text;
            this.Page.Title = this.Text;
            this.TransactionTypeLiteral.Visible = this.DisplayTransactionTypeRadioButtonList;
            this.TransactionTypeRadioButtonList.Visible = this.DisplayTransactionTypeRadioButtonList;

            this.SetControlStates();
        }

        private void BindGridView()
        {
            Collection<ProductDetailsModel> table = this.GetTable();

            this.ProductGridView.DataSource = table;
            this.ProductGridView.DataBind();

            this.ShowTotals();
        }

        protected void ShippingChargeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ShowTotals();

            if (this.CashRepositoryBalanceRow.Visible)
            {
                this.CashRepositoryDropDownList.Focus();
                return;
            }

            if (this.CostCenterRow.Visible)
            {
                this.CostCenterDropDownList.Focus();
                return;
            }

            this.StatementReferenceTextBox.Focus();
        }

        private void ShowTotals()
        {
            Collection<ProductDetailsModel> table = this.GetTable();

            this.RunningTotalTextBox.Text = (GetRunningTotalOfSubTotal(table) + Conversion.TryCastDecimal(this.ShippingChargeTextBox.Text)).ToString(Thread.CurrentThread.CurrentCulture);
            this.TaxTotalTextBox.Text = GetRunningTotalOfTax(table).ToString(Thread.CurrentThread.CurrentCulture);
            this.GrandTotalTextBox.Text = (GetRunningTotalOfTotal(table) + Conversion.TryCastDecimal(this.ShippingChargeTextBox.Text)).ToString(Thread.CurrentThread.CurrentCulture);
        }

        #region "Running Totals"
        private static decimal GetRunningTotalOfSubTotal(Collection<ProductDetailsModel> table)
        {
            decimal retVal = 0;

            if (table.Count > 0)
            {
                foreach (ProductDetailsModel model in table)
                {
                    retVal += Conversion.TryCastDecimal(model.Subtotal);
                }
            }

            return retVal;
        }

        private static decimal GetRunningTotalOfTax(Collection<ProductDetailsModel> table)
        {
            decimal retVal = 0;

            if (table.Count > 0)
            {
                foreach (ProductDetailsModel model in table)
                {
                    retVal += Conversion.TryCastDecimal(model.Tax);
                }
            }

            return retVal;
        }

        private static decimal GetRunningTotalOfTotal(Collection<ProductDetailsModel> productDetailsModelCollection)
        {
            decimal retVal = 0;

            if (productDetailsModelCollection.Count > 0)
            {
                foreach (ProductDetailsModel productDetailsModel in productDetailsModelCollection)
                {
                    retVal += Conversion.TryCastDecimal(productDetailsModel.Total);
                }
            }

            return retVal;
        }
        #endregion

        protected void CashRepositoryDropDownList_SelectIndexChanged(object sender, EventArgs e)
        {
            this.UpdateRepositoryBalance();
        }

        private void UpdateRepositoryBalance()
        {
            if (this.CashRepositoryBalanceRow.Visible)
            {
                if (this.CashRepositoryDropDownList.SelectedItem != null)
                {
                    this.CashRepositoryBalanceTextBox.Text = CashRepositories.GetBalance(Conversion.TryCastInteger(this.CashRepositoryDropDownList.SelectedItem.Value)).ToString(Thread.CurrentThread.CurrentCulture);
                }
            }
        }

        private void AddRowToTable(string itemCode, string itemName, int quantity, string unit, decimal price, decimal discount, decimal taxRate, decimal tax)
        {
            Collection<ProductDetailsModel> table = this.GetTable();

            decimal amount = price * quantity;
            decimal subTotal = amount - discount;
            decimal total = subTotal + tax;

            ProductDetailsModel row = new ProductDetailsModel();
            row.ItemCode = itemCode;
            row.ItemName = itemName;
            row.Quantity = quantity;
            row.Unit = unit;
            row.Price = price;
            row.Amount = amount;
            row.Discount = discount;
            row.Subtotal = subTotal;
            row.Rate = taxRate;
            row.Tax = tax;
            row.Total = total;

            table.Add(row);
            this.Session[this.ID] = table;
        }

        private Collection<ProductDetailsModel> GetTable()
        {
            Collection<ProductDetailsModel> productCollection = new Collection<ProductDetailsModel>();
            productCollection.Add(new ProductDetailsModel());

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

        private bool VerifyQuantity()
        {
            if (!this.VerifyStock)
            {
                return true;
            }

            if (this.Book != TranBook.Sales)
            {
                return true;
            }

            if (this.ProductGridView == null)
            {
                return true;
            }

            if (this.ProductGridView.Rows.Count.Equals(0))
            {
                return true;
            }

            int storeId = Conversion.TryCastInteger(this.StoreDropDownList.SelectedItem.Value);

            foreach (GridViewRow row in this.ProductGridView.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string itemCode = row.Cells[0].Text;
                    string itemName = row.Cells[1].Text;
                    int quantity = Conversion.TryCastInteger(row.Cells[2].Text);
                    string unitName = row.Cells[3].Text;

                    if (Items.IsStockItem(itemCode))
                    {
                        decimal itemInStock = Items.CountItemInStock(itemCode, unitName, storeId);

                        if (quantity > itemInStock)
                        {
                            this.ErrorLabel.Text = String.Format(Thread.CurrentThread.CurrentCulture, Warnings.InsufficientStockWarning, itemInStock.ToString("G29", Thread.CurrentThread.CurrentCulture), unitName, itemName);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            //Verify quantities of the already added items on the selected store.
            if (!this.VerifyQuantity())
            {
                return;
            }

            DateTime valueDate = DateTime.MinValue;
            int storeId = 0;
            string transactionType = string.Empty;
            string partyCode = this.PartyCodeTextBox.Text;

            if (this.DateTextBox != null)
            {
                valueDate = Conversion.TryCastDate(this.DateTextBox.Text);
            }

            if (this.StoreDropDownList.SelectedItem != null)
            {
                storeId = Conversion.TryCastInteger(this.StoreDropDownList.SelectedItem.Value);
            }

            if (this.TransactionTypeRadioButtonList.SelectedItem != null)
            {
                transactionType = this.TransactionTypeRadioButtonList.SelectedItem.Value;
            }


            if (string.IsNullOrWhiteSpace(partyCode))
            {
                FormHelper.MakeDirty(this.PartyCodeTextBox);
                FormHelper.MakeDirty(this.PartyDropDownList);
                this.PartyCodeTextBox.Focus();
                return;
            }

            if (valueDate.Equals(DateTime.MinValue))
            {
                this.ErrorLabelTop.Text = Warnings.InvalidDate;
                var dateTextBox = this.DateTextBox;

                if (dateTextBox != null)
                {
                    dateTextBox.CssClass = "dirty";
                    dateTextBox.Focus();
                }
                return;
            }

            if (this.Book == TranBook.Sales)
            {
                if (this.StoreDropDownList.Visible)
                {
                    if (!Stores.IsSalesAllowed(storeId))
                    {
                        this.ErrorLabelTop.Text = Warnings.SalesNotAllowedHere;
                        FormHelper.MakeDirty(this.StoreDropDownList);
                        return;
                    }
                }

                if (this.TransactionTypeRadioButtonList.Visible)
                {
                    if (transactionType.Equals(Titles.Credit))
                    {
                        if (!Parties.IsCreditAllowed(partyCode))
                        {
                            this.ErrorLabelTop.Text = Warnings.CreditNotAllowed;
                            return;
                        }
                    }
                }
            }

            this.ModeHiddenField.Value = "Started";
            this.SetControlStates();
            using (TextBox itemCodeTextBox = this.FindFooterControl("ItemCodeTextBox") as TextBox)
            {
                if (itemCodeTextBox != null)
                {
                    itemCodeTextBox.Focus();
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.ModeHiddenField.Value = "";

            this.Session[this.ID] = null;
            this.RunningTotalTextBox.Text = "";
            this.TaxTotalTextBox.Text = "";
            this.GrandTotalTextBox.Text = "";

            this.SetControlStates();
            this.BindGridView();
        }

        private void SetControlStates()
        {
            bool state = this.ModeHiddenField.Value.Equals("Started");

            this.FormPanel.Enabled = state;
            this.BottomPanel.Enabled = state;
            this.DateTextBox.Disabled = state;
            this.StoreDropDownList.Enabled = !state;
            this.TransactionTypeRadioButtonList.Enabled = !state;
            this.PartyCodeTextBox.Enabled = !state;
            this.PartyDropDownList.Enabled = !state;
            this.PriceTypeDropDownList.Enabled = !state;
            this.ReferenceNumberTextBox.Enabled = !state;
            this.OkButton.Enabled = !state;
            this.CancelButton.Enabled = state;

            if (this.TransactionTypeRadioButtonList.Visible)
            {
                if (this.TransactionTypeRadioButtonList.SelectedItem.Value.Equals(Titles.Credit))
                {
                    this.CashRepositoryRow.Visible = false;
                    this.CashRepositoryBalanceRow.Visible = false;
                }
            }

        }
    }
}