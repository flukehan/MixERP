using MixERP.Net.Common;
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
using MixERP.Net.Common.Helpers;
using System.Globalization;

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

        private Collection<StockMasterDetailModel> details = new Collection<StockMasterDetailModel>();
        public Collection<StockMasterDetailModel> Details
        {
            get
            {
                return details;
            }
        }

        public void AddDetail(StockMasterDetailModel detail)
        {
            details.Add(detail);
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
    public partial class ProductControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// Transaction book for products are Sales and Purchase.
        /// </summary>
        public MixERP.Net.Common.Models.Transactions.TranBook Book { get; set; }

        /// <summary>
        /// Sub transaction books are maintained for breaking down the Purchase and Sales transaction into smaller steps
        /// such as Quotations, Orders, Deliveries, e.t.c.
        /// </summary>
        public MixERP.Net.Common.Models.Transactions.SubTranBook SubBook { get; set; }

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
        private MixERP.Net.WebControls.StockTransactionView.Data.Models.MergeModel model = new MixERP.Net.WebControls.StockTransactionView.Data.Models.MergeModel();
        /// <summary>
        /// This class is a representation of the controls in this UserControl.
        /// </summary>

        public string ErrorMessage
        {
            get
            {
                return this.ErrorLabelBottom.Text;
            }
            set
            {
                this.ErrorLabelBottom.Text = value;
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
                    for (int i = 1; i < ProductGridView.Rows.Count; i++)
                    {
                        StockMasterDetailModel detail = new StockMasterDetailModel();

                        detail.ItemCode = ProductGridView.Rows[i].Cells[0].Text;
                        detail.Quantity = MixERP.Net.Common.Conversion.TryCastInteger(ProductGridView.Rows[i].Cells[2].Text);
                        detail.UnitName = ProductGridView.Rows[i].Cells[3].Text;
                        detail.Price = MixERP.Net.Common.Conversion.TryCastDecimal(ProductGridView.Rows[i].Cells[4].Text);
                        detail.Discount = MixERP.Net.Common.Conversion.TryCastDecimal(ProductGridView.Rows[i].Cells[6].Text);
                        detail.TaxRate = MixERP.Net.Common.Conversion.TryCastDecimal(ProductGridView.Rows[i].Cells[8].Text);
                        detail.Tax = MixERP.Net.Common.Conversion.TryCastDecimal(ProductGridView.Rows[i].Cells[9].Text);
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
                return this.TopPanel.Width;
            }
            set
            {
                this.TopPanel.Width = value;
            }

        }

        /// <summary>
        /// This event will be raised on SaveButon's click event.
        /// </summary>
        public event EventHandler SaveButtonClick;

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //Validation Check Start

            if (ProductGridView.Rows.Count.Equals(0))
            {
                ErrorLabel.Text = Resources.Warnings.NoItemFound;
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

            if (this.Book == Common.Models.Transactions.TranBook.Purchase && CashRepositoryRow.Visible)
            {
                this.UpdateRepositoryBalance();

                decimal repositoryBalance = MixERP.Net.Common.Conversion.TryCastDecimal(CashRepositoryBalanceTextBox.Text);
                decimal grandTotal = MixERP.Net.Common.Conversion.TryCastDecimal(GrandTotalTextBox.Text);

                if (grandTotal > repositoryBalance)
                {
                    ErrorLabel.Text = Resources.Warnings.NotEnoughCash;
                    return;
                }
            }

            //Check if the shipping charge textbox has a value.
            if (!string.IsNullOrWhiteSpace(ShippingChargeTextBox.Text))
            {
                //Check if the value actually was a number.
                if (!MixERP.Net.Common.Conversion.IsNumeric(ShippingChargeTextBox.Text))
                {
                    MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(ShippingChargeTextBox);
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
            if (SaveButtonClick != null)
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
            Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> dataSource = this.GetTable();

            //Get the instance of grid view row on which the the command was triggered.
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

            int index = row.RowIndex;
            dataSource.RemoveAt(index);

            Session[this.ID] = dataSource;
            this.BindGridView();
        }

        public void AddRow()
        {
            using (TextBox itemCodeTextBox = this.FindFooterControl("ItemCodeTextBox") as TextBox)
            {
                using (DropDownList itemDropDownList = this.FindFooterControl("ItemDropDownList") as DropDownList)
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
                                        using (DropDownList unitDropDownList = this.FindFooterControl("UnitDropDownList") as DropDownList)
                                        {
                                            using (HiddenField unitNameHidden = this.UpdatePanel1.FindControl("UnitNameHidden") as HiddenField)
                                            {
                                                using (HiddenField unitIdHidden = this.UpdatePanel1.FindControl("UnitIdHidden") as HiddenField)
                                                {
                                                    string itemCode = itemCodeTextBox.Text;
                                                    string itemName = itemDropDownList.SelectedItem.Text;
                                                    int quantity = MixERP.Net.Common.Conversion.TryCastInteger(quantityTextBox.Text);
                                                    string unit = unitNameHidden.Value;
                                                    int unitId = MixERP.Net.Common.Conversion.TryCastInteger(unitIdHidden.Value);
                                                    decimal itemInStock = 0;
                                                    decimal price = MixERP.Net.Common.Conversion.TryCastDecimal(priceTextBox.Text);
                                                    decimal discount = MixERP.Net.Common.Conversion.TryCastDecimal(discountTextBox.Text);
                                                    decimal taxRate = MixERP.Net.Common.Conversion.TryCastDecimal(taxRateTextBox.Text);
                                                    decimal tax = MixERP.Net.Common.Conversion.TryCastDecimal(taxTextBox.Text);
                                                    int storeId = 0;

                                                    if (StoreDropDownList.SelectedItem != null)
                                                    {
                                                        storeId = MixERP.Net.Common.Conversion.TryCastInteger(StoreDropDownList.SelectedItem.Value);
                                                    }

                                                    #region Validation

                                                    if (string.IsNullOrWhiteSpace(itemCode))
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(itemCodeTextBox);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.RemoveDirty(itemCodeTextBox);
                                                    }

                                                    if (!MixERP.Net.BusinessLayer.Core.Items.ItemExistsByCode(itemCode))
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(itemCodeTextBox);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.RemoveDirty(itemCodeTextBox);
                                                    }

                                                    if (quantity < 1)
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(quantityTextBox);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.RemoveDirty(quantityTextBox);
                                                    }

                                                    if (!MixERP.Net.BusinessLayer.Core.Units.UnitExistsByName(unit))
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(unitDropDownList);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.RemoveDirty(unitDropDownList);
                                                    }

                                                    if (price <= 0)
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(priceTextBox);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.RemoveDirty(priceTextBox);
                                                    }

                                                    if (discount < 0)
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(discountTextBox);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        if (discount > (price * quantity))
                                                        {
                                                            MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(discountTextBox);
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            MixERP.Net.BusinessLayer.Helpers.FormHelper.RemoveDirty(discountTextBox);
                                                        }
                                                    }

                                                    if (tax < 0)
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(taxTextBox);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        MixERP.Net.BusinessLayer.Helpers.FormHelper.RemoveDirty(taxTextBox);
                                                    }

                                                    if (this.VerifyStock)
                                                    {
                                                        if (this.Book == Common.Models.Transactions.TranBook.Sales)
                                                        {
                                                            if (MixERP.Net.BusinessLayer.Core.Items.IsStockItem(itemCode))
                                                            {
                                                                itemInStock = MixERP.Net.BusinessLayer.Core.Items.CountItemInStock(itemCode, unitId, storeId);
                                                                if (quantity > itemInStock)
                                                                {
                                                                    MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(quantityTextBox);
                                                                    ErrorLabel.Text = String.Format(System.Threading.Thread.CurrentThread.CurrentCulture, Resources.Warnings.InsufficientStockWarning, itemInStock.ToString("G29", System.Threading.Thread.CurrentThread.CurrentCulture), unitNameHidden.Value, itemDropDownList.SelectedItem.Text);
                                                                    return;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    #endregion

                                                    this.AddRowToTable(itemCode, itemName, quantity, unit, price, discount, taxRate, tax);

                                                    this.BindGridView();
                                                    itemCodeTextBox.Text = "";
                                                    quantityTextBox.Text = (1).ToString(CultureInfo.InvariantCulture);
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
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(deleteImageButton);
                }
            }
        }

        #region "Page Initialization"
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ClearSession(this.ID);
            }

            this.InitializeControls();
            this.LoadValuesFromSession();
            this.BindGridView();
            this.AddUnitNameHidden();
            this.AddUnitIdHidden();
            this.LoadFooter();
            ScriptManager1.RegisterAsyncPostBackControl(ProductGridView);
        }

        private void AddUnitNameHidden()
        {
            using (HiddenField unitNameHidden = new HiddenField())
            {
                unitNameHidden.ID = "UnitNameHidden";
                unitNameHidden.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                UpdatePanel1.ContentTemplateContainer.Controls.Add(unitNameHidden);
            }
        }

        private void AddUnitIdHidden()
        {
            using (HiddenField unitIdHidden = new HiddenField())
            {
                unitIdHidden.ID = "UnitIdHidden";
                unitIdHidden.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                UpdatePanel1.ContentTemplateContainer.Controls.Add(unitIdHidden);
            }
        }

        #region "GridView Footer"
        private void LoadFooter()
        {
            using (GridViewRow footer = ProductGridView.FooterRow)
            {
                if (footer == null)
                {
                    return;
                }

                AddItemCodeTextBox(footer, 0);
                AddItemDropDownList(footer, 1);
                AddQuantityTextBox(footer, 2);
                AddUnitDropDownList(footer, 3);
                AddPriceTextBox(footer, 4);
                AddAmountTextBox(footer, 5);
                AddDiscountTextBox(footer, 6);
                AddSubtotalTextBox(footer, 7);
                AddTaxRateTextBox(footer, 8);
                AddTaxTextBox(footer, 9);
                AddTotalTextBox(footer, 10);
                AddAddButton(footer, 11);
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
                itemCodeTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                itemCodeTextBox.Attributes.Add("onblur", "selectDropDownListByValue(this.id, 'ItemDropDownList');");
                itemCodeTextBox.ToolTip = Resources.Titles.AltC;
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
                itemDropDownList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                itemDropDownList.Attributes.Add("onchange", "document.getElementById('ItemCodeTextBox').value = this.options[this.selectedIndex].value;if(this.selectedIndex == 0) { return false };");
                itemDropDownList.Attributes.Add("onblur", "getPrice();");
                itemDropDownList.ToolTip = Resources.Titles.CtrlI;
                itemDropDownList.Width = 300;

                ScriptManager1.RegisterAsyncPostBackControl(itemDropDownList);
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
                quantityTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                quantityTextBox.Attributes.Add("onblur", "updateTax();calculateAmount();");
                quantityTextBox.CssClass = "right";
                quantityTextBox.ToolTip = Resources.Titles.CtrlQ;
                quantityTextBox.Width = 50;
                quantityTextBox.Text = (1).ToString(CultureInfo.InvariantCulture); ;

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
                unitDropDownList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                unitDropDownList.AutoPostBack = true;
                unitDropDownList.Attributes.Add("onchange", "$('#UnitNameHidden').val($(this).children('option').filter(':selected').text());$('#UnitIdHidden').val($(this).children('option').filter(':selected').val());");
                unitDropDownList.ToolTip = Resources.Titles.CtrlU;
                unitDropDownList.Width = 70;
                ScriptManager1.RegisterAsyncPostBackControl(unitDropDownList);
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
                priceTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                priceTextBox.Attributes.Add("onblur", "updateTax();calculateAmount();");
                priceTextBox.CssClass = "right number";
                priceTextBox.ToolTip = Resources.Titles.AltP;
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
                amountTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
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
                discountTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                discountTextBox.CssClass = "right number";
                discountTextBox.Attributes.Add("onblur", "updateTax();calculateAmount();");
                discountTextBox.ToolTip = Resources.Titles.CtrlD;

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
                subtotalTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
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
                taxRateTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
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
                taxTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                taxTextBox.Attributes.Add("onblur", "calculateAmount();");
                taxTextBox.CssClass = "right number";
                taxTextBox.Width = 50;
                taxTextBox.ToolTip = Resources.Titles.CtrlT;

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
                totalTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
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
                addButton.Text = Resources.Titles.Add;
                addButton.ToolTip = Resources.Titles.CtrlReturn;

                this.ScriptManager1.RegisterAsyncPostBackControl(addButton);
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
            if (Session["Product"] == null)
            {
                return;
            }

            model = Session["Product"] as MixERP.Net.WebControls.StockTransactionView.Data.Models.MergeModel;

            if (model == null)
            {
                return;
            }

            PartyDropDownList.SelectedItem.Value = model.PartyCode.ToString();

            if (PriceTypeDropDownList.SelectedItem != null)
            {
                MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.SetSelectedValue(PriceTypeDropDownList, model.PriceTypeId.ToString(MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetCulture()));
            }

            ReferenceNumberTextBox.Text = model.ReferenceNumber;
            StatementReferenceTextBox.Text = model.StatementReference;

            Session[this.ID] = model.View;
            Session["TranIdCollection"] = model.TransactionIdCollection;
            this.ClearSession("Product");
        }

        public Collection<int> GetTranIdCollection()
        {
            Collection<int> tranIdCollection = new Collection<int>();

            if (Session["TranIdCollection"] != null)
            {
                tranIdCollection = Session["TranIdCollection"] as Collection<int>;
            }

            return tranIdCollection;
        }

        private void ClearSession(string key)
        {
            if (Session[key] != null)
            {
                Session.Remove(key);
            }
        }

        private void LoadCostCenters()
        {
            if (this.SubBook == Common.Models.Transactions.SubTranBook.Direct || this.SubBook == Common.Models.Transactions.SubTranBook.Invoice || this.SubBook == Common.Models.Transactions.SubTranBook.Delivery || this.SubBook == Common.Models.Transactions.SubTranBook.Receipt)
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("CostCenterDisplayField");
                MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.BindDropDownList(CostCenterDropDownList, "office", "cost_centers", "cost_center_id", displayField);
            }
            else
            {
                CostCenterRow.Visible = false;
            }
        }

        private void LoadStores()
        {
            if (this.SubBook == Common.Models.Transactions.SubTranBook.Direct || this.SubBook == Common.Models.Transactions.SubTranBook.Invoice || this.SubBook == Common.Models.Transactions.SubTranBook.Delivery || this.SubBook == Common.Models.Transactions.SubTranBook.Receipt)
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("StoreDisplayField");
                MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.BindDropDownList(StoreDropDownList, "office", "stores", "store_id", displayField);
            }
            else
            {
                StoreLiteral.Visible = false;
                StoreDropDownList.Visible = false;
            }
        }

        private void LoadCashRepositories()
        {
            if (this.ShowCashRepository)
            {
                using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Office.CashRepositories.GetCashRepositories(MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetOfficeId()))
                {
                    string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("CashRepositoryDisplayField");
                    MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.BindDropDownList(CashRepositoryDropDownList, table, "cash_repository_id", displayField);
                    this.UpdateRepositoryBalance();
                }
            }
            else
            {
                CashRepositoryRow.Visible = false;
                CashRepositoryBalanceRow.Visible = false;
            }
        }

        private void LoadLabels()
        {
            DateLiteral.Text = HtmlControlHelper.GetLabel(DateTextBox.ClientID, Resources.Titles.ValueDate);
            StoreLiteral.Text = HtmlControlHelper.GetLabel(StoreDropDownList.ClientID, Resources.Titles.SelectStore);

            PartyLiteral.Text = HtmlControlHelper.GetLabel(PartyCodeTextBox.ClientID, Resources.Titles.SelectParty);
            PriceTypeLiteral.Text = HtmlControlHelper.GetLabel(PriceTypeDropDownList.ClientID, Resources.Titles.PriceType);
            ReferenceNumberLiteral.Text = HtmlControlHelper.GetLabel(ReferenceNumberTextBox.ClientID, Resources.Titles.ReferenceNumberAbbreviated);

            RunningTotalTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(RunningTotalTextBox.ClientID, Resources.Titles.RunningTotal);
            TaxTotalTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(TaxTotalTextBox.ClientID, Resources.Titles.TaxTotal);
            GrandTotalTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(GrandTotalTextBox.ClientID, Resources.Titles.GrandTotal);
            ShippingAddressDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(ShippingAddressDropDownList.ClientID, Resources.Titles.ShippingAddress);
            ShippingCompanyDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(ShippingCompanyDropDownList.ClientID, Resources.Titles.ShippingCompany);
            ShippingChargeTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(ShippingChargeTextBox.ClientID, Resources.Titles.ShippingCharge);
            CashRepositoryDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(CashRepositoryDropDownList.ClientID, Resources.Titles.CashRepository);
            CashRepositoryBalanceTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(CashRepositoryBalanceTextBox.ClientID, Resources.Titles.CashRepositoryBalance);
            CostCenterDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(CostCenterDropDownList.ClientID, Resources.Titles.CostCenter);
            SalespersonDropDownListLabelLiteral.Text = HtmlControlHelper.GetLabel(SalespersonDropDownList.ClientID, Resources.Titles.Salesperson);
            StatementReferenceTextBoxLabelLiteral.Text = HtmlControlHelper.GetLabel(StatementReferenceTextBox.ClientID, Resources.Titles.StatementReference);
        }

        private void LoadTransactionTypeLabel()
        {
            if (this.Book == Common.Models.Transactions.TranBook.Sales)
            {
                TransactionTypeLiteral.Text = HtmlControlHelper.GetLabel(Resources.Titles.SalesType);
            }
            else
            {
                TransactionTypeLiteral.Text = HtmlControlHelper.GetLabel(Resources.Titles.PurchaseType);
            }
        }

        private Control FindFooterControl(string controlId)
        {
            using (GridViewRow footer = ProductGridView.FooterRow)
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
            using (DropDownList itemDropDownList = FindFooterControl("ItemDropDownList") as DropDownList)
            {
                if (itemDropDownList == null)
                {
                    return;
                }

                using (Services.ItemData data = new Services.ItemData())
                {
                    if (this.Book == Common.Models.Transactions.TranBook.Sales)
                    {
                        itemDropDownList.DataSource = data.GetItems();
                    }
                    else
                    {
                        itemDropDownList.DataSource = data.GetStockItems();
                    }

                    itemDropDownList.DataTextField = "Text";
                    itemDropDownList.DataValueField = "Value";
                    itemDropDownList.DataBind();
                }
            }
        }

        private void LoadPriceTypes()
        {
            if (this.Book == Common.Models.Transactions.TranBook.Sales)
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("PriceTypeDisplayField");
                MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.BindDropDownList(PriceTypeDropDownList, "core", "price_types", "price_type_id", displayField);
            }
            else
            {
                PriceTypeLiteral.Visible = false;
                PriceTypeDropDownList.Visible = false;

                ShippingAddressRow.Visible = false;
                ShippingChargeRow.Visible = false;
                ShippingCompanyRow.Visible = false;
            }

        }

        private void LoadSalesperson()
        {
            SalespersonRow.Visible = false;

            if (this.Book == Common.Models.Transactions.TranBook.Sales)
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("AgentDisplayField");
                MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.BindDropDownList(SalespersonDropDownList, "core", "agents", "agent_id", displayField);
                SalespersonRow.Visible = true;
            }
        }

        private void LoadShippers()
        {
            ShippingAddressRow.Visible = false;
            ShippingChargeRow.Visible = false;
            ShippingCompanyRow.Visible = false;

            if (this.Book == Common.Models.Transactions.TranBook.Sales)
            {
                if (this.SubBook == Common.Models.Transactions.SubTranBook.Direct || this.SubBook == Common.Models.Transactions.SubTranBook.Delivery)
                {
                    string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("ShipperDisplayField");
                    MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.BindDropDownList(ShippingCompanyDropDownList, "core", "shippers", "shipper_id", displayField);

                    ShippingAddressRow.Visible = true;
                    ShippingChargeRow.Visible = true;
                    ShippingCompanyRow.Visible = true;
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

        private void LoadParties()
        {
            using (MixERP.Net.FrontEnd.Services.PartyData data = new Services.PartyData())
            {
                PartyDropDownList.DataSource = data.GetParties();
                PartyDropDownList.DataTextField = "Text";
                PartyDropDownList.DataValueField = "Value";
                PartyDropDownList.DataBind();
                PartyDropDownList.Items.Insert(0, new ListItem(Resources.Titles.Select, string.Empty));
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] != null)
            {
                Control c = this.Page.FindControl(Request.Form["__EVENTTARGET"]);
                if (c != null)
                {
                    if (c.ID.Equals("UnitDropDownList"))
                    {
                        UnitDropDownList_SelectedIndexChanged(c, e);
                    }
                }
            }

            //Moved from Page_Init
            this.TitleLabel.Text = this.Text;
            this.Page.Title = this.Text;
            TransactionTypeLiteral.Visible = this.DisplayTransactionTypeRadioButtonList;
            TransactionTypeRadioButtonList.Visible = this.DisplayTransactionTypeRadioButtonList;

            this.SetControlStates();
        }

        private void BindGridView()
        {
            Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> table = this.GetTable();

            ProductGridView.DataSource = table;
            ProductGridView.DataBind();

            this.ShowTotals();
        }

        protected void ShippingChargeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ShowTotals();

            if (CashRepositoryBalanceRow.Visible)
            {
                CashRepositoryDropDownList.Focus();
                return;
            }

            if (CostCenterRow.Visible)
            {
                CostCenterDropDownList.Focus();
                return;
            }

            StatementReferenceTextBox.Focus();
        }

        private void ShowTotals()
        {
            Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> table = this.GetTable();

            RunningTotalTextBox.Text = (GetRunningTotalOfSubTotal(table) + MixERP.Net.Common.Conversion.TryCastDecimal(ShippingChargeTextBox.Text)).ToString(System.Threading.Thread.CurrentThread.CurrentCulture);
            TaxTotalTextBox.Text = GetRunningTotalOfTax(table).ToString(System.Threading.Thread.CurrentThread.CurrentCulture);
            GrandTotalTextBox.Text = (GetRunningTotalOfTotal(table) + MixERP.Net.Common.Conversion.TryCastDecimal(ShippingChargeTextBox.Text)).ToString(System.Threading.Thread.CurrentThread.CurrentCulture);
        }

        #region "Running Totals"
        private static decimal GetRunningTotalOfSubTotal(Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> table)
        {
            decimal retVal = 0;

            if (table.Count > 0)
            {
                foreach (MixERP.Net.Common.Models.Transactions.ProductDetailsModel model in table)
                {
                    retVal += MixERP.Net.Common.Conversion.TryCastDecimal(model.Subtotal);
                }
            }

            return retVal;
        }

        private static decimal GetRunningTotalOfTax(Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> table)
        {
            decimal retVal = 0;

            if (table.Count > 0)
            {
                foreach (ProductDetailsModel model in table)
                {
                    retVal += MixERP.Net.Common.Conversion.TryCastDecimal(model.Tax);
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
                    retVal += MixERP.Net.Common.Conversion.TryCastDecimal(productDetailsModel.Total);
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
            if (CashRepositoryBalanceRow.Visible)
            {
                if (CashRepositoryDropDownList.SelectedItem != null)
                {
                    CashRepositoryBalanceTextBox.Text = MixERP.Net.BusinessLayer.Office.CashRepositories.GetBalance(MixERP.Net.Common.Conversion.TryCastInteger(CashRepositoryDropDownList.SelectedItem.Value)).ToString(System.Threading.Thread.CurrentThread.CurrentCulture);
                }
            }
        }

        private void AddRowToTable(string itemCode, string itemName, int quantity, string unit, decimal price, decimal discount, decimal taxRate, decimal tax)
        {
            Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> table = this.GetTable();

            decimal amount = price * quantity;
            decimal subTotal = amount - discount;
            decimal total = subTotal + tax;

            MixERP.Net.Common.Models.Transactions.ProductDetailsModel row = new Common.Models.Transactions.ProductDetailsModel();
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
            Session[this.ID] = table;
        }

        private Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> GetTable()
        {
            Collection<ProductDetailsModel> productCollection = new Collection<ProductDetailsModel>();
            productCollection.Add(new ProductDetailsModel());

            if (Session[this.ID] != null)
            {
                //Get an instance of the ProductDetailsModel collection stored in session.
                productCollection = (Collection<ProductDetailsModel>)Session[this.ID];

                //Summate the collection.
                productCollection = SummateProducts(productCollection);

                //Store the summated table in session.
                Session[this.ID] = productCollection;

            }

            return productCollection;
        }

        private static Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> SummateProducts(Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> productCollection)
        {
            //Create a new collection of products.
            Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> collection = new Collection<Common.Models.Transactions.ProductDetailsModel>();

            //Iterate through the supplied product collection.
            foreach (MixERP.Net.Common.Models.Transactions.ProductDetailsModel product in productCollection)
            {
                //Create a product
                MixERP.Net.Common.Models.Transactions.ProductDetailsModel productInCollection = null;

                if (collection.Count > 0)
                {
                    productInCollection = collection.Where(x => x.ItemCode == product.ItemCode && x.ItemName == product.ItemName && x.Unit == product.Unit && x.Price == product.Price && x.Rate == product.Rate).FirstOrDefault();
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

        void UnitDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayPrice();
            using (TextBox priceTextBox = this.FindFooterControl("PriceTextBox") as TextBox)
            {
                priceTextBox.Focus();
            }
        }

        private void DisplayPrice()
        {
            using (DropDownList itemDropDownList = this.FindFooterControl("ItemDropDownList") as DropDownList)
            {
                using (TextBox discountTextBox = this.FindFooterControl("DiscountTextBox") as TextBox)
                {
                    using (TextBox priceTextBox = this.FindFooterControl("PriceTextBox") as TextBox)
                    {
                        using (TextBox taxRateTextBox = this.FindFooterControl("TaxRateTextBox") as TextBox)
                        {
                            using (TextBox taxTextBox = this.FindFooterControl("TaxTextBox") as TextBox)
                            {
                                using (TextBox quantityTextBox = this.FindFooterControl("QuantityTextBox") as TextBox)
                                {
                                    using (TextBox amountTextBox = this.FindFooterControl("AmountTextBox") as TextBox)
                                    {
                                        using (HiddenField unitIdHidden = this.UpdatePanel1.FindControl("UnitIdHidden") as HiddenField)
                                        {
                                            string itemCode = itemDropDownList.SelectedItem.Value;
                                            string party = string.Empty;

                                            int unitId = MixERP.Net.Common.Conversion.TryCastInteger(unitIdHidden.Value);

                                            decimal price = 0;

                                            if (this.Book == Common.Models.Transactions.TranBook.Sales)
                                            {
                                                party = PartyDropDownList.SelectedItem.Value;
                                                short priceTypeId = MixERP.Net.Common.Conversion.TryCastShort(PriceTypeDropDownList.SelectedItem.Value);
                                                price = MixERP.Net.BusinessLayer.Core.Items.GetItemSellingPrice(itemCode, party, priceTypeId, unitId);
                                            }
                                            else
                                            {
                                                party = PartyDropDownList.SelectedItem.Value;
                                                price = MixERP.Net.BusinessLayer.Core.Items.GetItemCostPrice(itemCode, party, unitId);
                                            }

                                            decimal discount = MixERP.Net.Common.Conversion.TryCastDecimal(discountTextBox.Text);
                                            decimal taxRate = MixERP.Net.BusinessLayer.Core.Items.GetTaxRate(itemCode);


                                            priceTextBox.Text = price.ToString(System.Threading.Thread.CurrentThread.CurrentCulture);

                                            taxRateTextBox.Text = taxRate.ToString(System.Threading.Thread.CurrentThread.CurrentCulture);
                                            taxTextBox.Text = (((price - discount) * taxRate) / 100.00m).ToString("#.##", System.Threading.Thread.CurrentThread.CurrentCulture);

                                            decimal amount = price * MixERP.Net.Common.Conversion.TryCastInteger(quantityTextBox.Text);

                                            amountTextBox.Text = amount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool VerifyQuantity()
        {
            if (!this.VerifyStock)
            {
                return true;
            }

            if (this.Book != Common.Models.Transactions.TranBook.Sales)
            {
                return true;
            }

            if (ProductGridView == null)
            {
                return true;
            }

            if (ProductGridView.Rows == null)
            {
                return true;
            }

            if (ProductGridView.Rows.Count.Equals(0))
            {
                return true;
            }

            string itemCode = string.Empty;
            string itemName = string.Empty;
            int quantity = 0;
            string unitName = string.Empty;
            int storeId = Conversion.TryCastInteger(StoreDropDownList.SelectedItem.Value);
            decimal itemInStock = 0;

            foreach (GridViewRow row in ProductGridView.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    itemCode = row.Cells[0].Text;
                    itemName = row.Cells[1].Text;
                    quantity = Conversion.TryCastInteger(row.Cells[2].Text);
                    unitName = row.Cells[3].Text;

                    if (MixERP.Net.BusinessLayer.Core.Items.IsStockItem(itemCode))
                    {
                        itemInStock = MixERP.Net.BusinessLayer.Core.Items.CountItemInStock(itemCode, unitName, storeId);

                        if (quantity > itemInStock)
                        {
                            ErrorLabel.Text = String.Format(System.Threading.Thread.CurrentThread.CurrentCulture, Resources.Warnings.InsufficientStockWarning, itemInStock.ToString("G29", System.Threading.Thread.CurrentThread.CurrentCulture), unitName, itemName);
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
            string partyCode = PartyCodeTextBox.Text;

            if (DateTextBox != null)
            {
                valueDate = MixERP.Net.Common.Conversion.TryCastDate(DateTextBox.Text);
            }

            if (StoreDropDownList.SelectedItem != null)
            {
                storeId = MixERP.Net.Common.Conversion.TryCastInteger(StoreDropDownList.SelectedItem.Value);
            }

            if (TransactionTypeRadioButtonList.SelectedItem != null)
            {
                transactionType = TransactionTypeRadioButtonList.SelectedItem.Value;
            }


            if (string.IsNullOrWhiteSpace(partyCode))
            {
                MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(PartyCodeTextBox);
                MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(PartyDropDownList);
                PartyCodeTextBox.Focus();
                return;
            }

            if (valueDate.Equals(DateTime.MinValue))
            {
                ErrorLabelTop.Text = Resources.Warnings.InvalidDate;
                DateTextBox.CssClass = "dirty";
                DateTextBox.Focus();
                return;
            }

            if (this.Book == Common.Models.Transactions.TranBook.Sales)
            {
                if (StoreDropDownList.Visible)
                {
                    if (!MixERP.Net.BusinessLayer.Office.Stores.IsSalesAllowed(storeId))
                    {
                        ErrorLabelTop.Text = Resources.Warnings.SalesNotAllowedHere;
                        MixERP.Net.BusinessLayer.Helpers.FormHelper.MakeDirty(StoreDropDownList);
                        return;
                    }
                }

                if (TransactionTypeRadioButtonList.Visible)
                {
                    if (transactionType.Equals(Resources.Titles.Credit))
                    {
                        if (!MixERP.Net.BusinessLayer.Core.Parties.IsCreditAllowed(partyCode))
                        {
                            ErrorLabelTop.Text = Resources.Warnings.CreditNotAllowed;
                            return;
                        }
                    }
                }
            }

            ModeHiddenField.Value = "Started";
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
            ModeHiddenField.Value = "";

            Session[this.ID] = null;
            RunningTotalTextBox.Text = "";
            TaxTotalTextBox.Text = "";
            GrandTotalTextBox.Text = "";

            this.SetControlStates();
            this.BindGridView();
        }

        private void SetControlStates()
        {
            bool state = ModeHiddenField.Value.Equals("Started");

            FormPanel.Enabled = state;
            BottomPanel.Enabled = state;
            DateTextBox.Disabled = state;
            StoreDropDownList.Enabled = !state;
            TransactionTypeRadioButtonList.Enabled = !state;
            PartyCodeTextBox.Enabled = !state;
            PartyDropDownList.Enabled = !state;
            PriceTypeDropDownList.Enabled = !state;
            ReferenceNumberTextBox.Enabled = !state;
            OkButton.Enabled = !state;
            CancelButton.Enabled = state;

            if (TransactionTypeRadioButtonList.Visible)
            {
                if (TransactionTypeRadioButtonList.SelectedItem.Value.Equals(Resources.Titles.Credit))
                {
                    CashRepositoryRow.Visible = false;
                    CashRepositoryBalanceRow.Visible = false;
                }
            }

        }
    }
}