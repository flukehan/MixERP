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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using MixERP.Net.WebControls.AttachmentFactory;
using MixERP.Net.WebControls.Common;
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

        #region IDisposable
        private DateTextBox dateTextBox;

        private bool disposed;
        private HiddenField itemCodeHidden;
        private HiddenField itemIdHidden;
        private HiddenField modeHiddenField;
        private HiddenField partyCodeHidden;
        private HtmlInputText partyCodeInputText;
        private HiddenField partyIdHidden;
        private HiddenField priceTypeIdHidden;
        private HiddenField productGridViewDataHidden;
        private HtmlInputText referenceNumberInputText;
        private HiddenField salesPersonIdHidden;
        private HiddenField shipperIdHidden;
        private HiddenField shippingAddressCodeHidden;
        private HiddenField storeIdHidden;
        private HtmlGenericControl title;
        private HiddenField tranIdCollectionHiddenField;
        private HiddenField unitIdHidden;
        private HiddenField unitNameHidden;
        //Work in progress
        public override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            //if (this.dateTextBox != null)
            //{
            //    this.dateTextBox.Dispose();
            //    this.dateTextBox = null;
            //}

            //Work in progress
        }

        #endregion

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
                this.CreateTitle(this.Placeholder1);
                this.CreateTopFormPanel(this.Placeholder1);

                if (!this.IsPostBack)
                {
                    this.ClearSession(this.ID);
                }

                this.CreateGrid(this.Placeholder1);
                this.CreateErrorLabel(this.Placeholder1);
                this.CreateAttachmentPanel(this.Placeholder1);
                this.CreateBottomFormPanel(this.Placeholder1);
                this.CreateHiddenFields(this.Placeholder1);
                this.CreateErrorLabelBottom(this.Placeholder1);
                this.RegisterJavascriptVariables(this.Placeholder1);
                this.LoadValuesFromSession();
                this.BindGridView();

                this.Page.Title = this.Text;
                this.initialized = true;
            }
        }


        #region Javascript Variables

        private void RegisterJavascriptVariables(Control container)
        {
            string javascript = "var isSales={0};var tranBook='{1}';var taxAfterDiscount={2};var verifyStock={3};";
            string isSales = (this.Book.Equals(TranBook.Sales)) ? "true" : "false";
            string tranBook = this.GetTranBook();
            string taxAfterDiscount = Switches.TaxAfterDiscount().ToString().ToUpperInvariant().Equals("TRUE")?"true":"false";
            string verifyStock = (this.VerifyStock) ? "true" : "false";

            javascript = string.Format(CultureInfo.InvariantCulture, javascript, isSales, tranBook, taxAfterDiscount, verifyStock);

            PageUtility.RegisterJavascript("StockTransactionFactory_Vars", javascript, this.Page, true);
        }

        #endregion

        #region HiddenFields

        private void AddHiddenField(Control container, HiddenField hidden, string id)
        {
            if (hidden == null)
            {
                hidden = new HiddenField();
            }

            hidden.ID = id;
            container.Controls.Add(hidden);
        }

        private void CreateHiddenFields(Control container)
        {
            this.AddHiddenField(container, this.itemCodeHidden, "ItemCodeHidden");
            this.AddHiddenField(container, this.itemIdHidden, "ItemIdHidden");
            this.AddHiddenField(container, this.modeHiddenField, "ModeHiddenField");
            this.AddHiddenField(container, this.partyCodeHidden, "PartyCodeHidden");
            this.AddHiddenField(container, this.partyIdHidden, "PartyIdHidden");
            this.AddHiddenField(container, this.productGridViewDataHidden, "ProductGridViewDataHidden");
            this.AddHiddenField(container, this.priceTypeIdHidden, "PriceTypeIdHidden");
            this.AddHiddenField(container, this.tranIdCollectionHiddenField, "TranIdCollectionHiddenField");
            this.AddHiddenField(container, this.unitIdHidden, "UnitIdHidden");
            this.AddHiddenField(container, this.unitNameHidden, "UnitNameHidden");
            this.AddHiddenField(container, this.storeIdHidden, "StoreIdHidden");
            this.AddHiddenField(container, this.shipperIdHidden, "ShipperIdHidden");
            this.AddHiddenField(container, this.shippingAddressCodeHidden, "ShippingAddressCodeHidden");
            this.AddHiddenField(container, this.salesPersonIdHidden, "SalesPersonIdHidden");

        }
        #endregion

        #region Error Labels
        private void CreateErrorLabel(Control container)
        {
            using (HtmlGenericControl errorLabel = new HtmlGenericControl("span"))
            {
                errorLabel.ID = "ErrorLabel";
                errorLabel.Attributes.Add("class", "big error");

                container.Controls.Add(errorLabel);
            }
        }

        private void CreateErrorLabelBottom(Control container)
        {
            using (HtmlGenericControl errorLabel = new HtmlGenericControl("span"))
            {
                errorLabel.ID = "ErrorLabelBottom";
                errorLabel.Attributes.Add("class", "big error");

                container.Controls.Add(errorLabel);
            }
        }

        #endregion
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

        private void CreateAttachmentPanel(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.ID = "attachmentToggler";
                header.InnerText = StockTransactionFactoryResourceHelper.GetResourceString("Titles", "AttachmentsPlus");
                container.Controls.Add(header);
            }

            using (HtmlGenericControl attachmentContainer = new HtmlGenericControl("div"))
            {
                attachmentContainer.ID = "attachment";
                attachmentContainer.Attributes.Add("class", "ui segment initially hidden");


                using (Attachment attachment = new Attachment())
                {
                    attachmentContainer.Controls.Add(attachment);
                }
                container.Controls.Add(attachmentContainer);
            }
        }

        #region Grid

        private void CreateGrid(Control container)
        {
            using (Table productGridView = new Table())
            {
                productGridView.ID = "ProductGridView";
                productGridView.CssClass = "ui table";
                productGridView.Style.Add("max-width", "2000px");

                this.CreateHeaderRow(productGridView);
                this.CreateFooterRow(productGridView);

                container.Controls.Add(productGridView);
            }
        }


        #region Header Row

        private void CreateHeaderCell(TableRow row, string text, string targetControlId)
        {
            using (TableHeaderCell cell = new TableHeaderCell())
            {
                if (targetControlId != null)
                {
                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(text, targetControlId))
                    {
                        cell.Controls.Add(label);
                    }
                }
                else
                {
                    cell.Text = text;
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateHeaderRow(Table grid)
        {
            using (TableRow header = new TableRow())
            {
                header.TableSection = TableRowSection.TableHeader;

                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ItemCode"), "ItemCodeInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ItemName"), "ItemSelect");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "QuantityAbbreviated"), "QuantityInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Unit"), "UnitSelect");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Price"), "PriceInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Amount"), "AmountInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Discount"), "DiscountInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingCharge"), "ShippingChargeInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SubTotal"), "SubTotalInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "TaxForm"), "TaxSelect");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Tax"), "TaxInputText");
                this.CreateHeaderCell(header, StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Action"), null);

                grid.Rows.Add(header);
            }

        }
        #endregion

        #region Footer Row
        private void CreateActionField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputButton addButton = new HtmlInputButton())
                {
                    addButton.ID = "AddButton";
                    addButton.Attributes.Add("class", "small ui button blue");
                    addButton.Value = StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Add");
                    addButton.Attributes.Add("title", "Ctrl + Return");

                    cell.Controls.Add(addButton);
                }
                row.Cells.Add(cell);
            }

        }

        private void CreateAmountField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText amountInputText = new HtmlInputText())
                {
                    amountInputText.ID = "AmountInputText";
                    amountInputText.Attributes.Add("class", "currency text-right");
                    amountInputText.Attributes.Add("readonly", "readonly");

                    cell.Controls.Add(amountInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateDiscountField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText discountInputText = new HtmlInputText())
                {
                    discountInputText.ID = "DiscountInputText";
                    discountInputText.Attributes.Add("title", "Ctrl + D");
                    discountInputText.Attributes.Add("class", "currency text-right");

                    cell.Controls.Add(discountInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateFooterRow(Table grid)
        {
            using (TableRow row = new TableRow())
            {
                row.TableSection = TableRowSection.TableBody;
                row.CssClass = "ui footer-row form";

                this.CreateItemCodeField(row);
                this.CreateItemField(row);
                this.CreateQuantityField(row);
                this.CreateUnitField(row);
                this.CreatePriceField(row);
                this.CreateAmountField(row);
                this.CreateDiscountField(row);
                this.CreateShippingChargeField(row);
                this.CreateSubTotalField(row);
                this.CreateTaxFormField(row);
                this.CreateTaxtField(row);
                this.CreateActionField(row);

                grid.Rows.Add(row);
            }
        }


        private void CreateItemCodeField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText itemCodeInputText = new HtmlInputText())
                {
                    itemCodeInputText.ID = "ItemCodeInputText";
                    itemCodeInputText.Attributes.Add("title", "ALT + C");

                    cell.Controls.Add(itemCodeInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateItemField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlSelect itemSelect = new HtmlSelect())
                {
                    itemSelect.ID = "ItemSelect";
                    itemSelect.Attributes.Add("title", "Ctrl + I");
                    cell.Controls.Add(itemSelect);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreatePriceField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText priceInputText = new HtmlInputText())
                {
                    priceInputText.ID = "PriceInputText";
                    priceInputText.Attributes.Add("title", "Alt + P");
                    priceInputText.Attributes.Add("class", "currency text-right");

                    cell.Controls.Add(priceInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateQuantityField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText quantityInputText = new HtmlInputText())
                {
                    quantityInputText.ID = "QuantityInputText";
                    quantityInputText.Attributes.Add("title", "Ctrl + Q");
                    quantityInputText.Attributes.Add("class", "integer text-right");

                    quantityInputText.Value = "1";

                    cell.Controls.Add(quantityInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateShippingChargeField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText shippingChargeInputText = new HtmlInputText())
                {
                    shippingChargeInputText.ID = "ShippingChargeInputText";
                    shippingChargeInputText.Attributes.Add("title", "Ctrl + D");
                    shippingChargeInputText.Attributes.Add("class", "currency text-right");

                    if (!this.ShowShippingInformation)
                    {
                        shippingChargeInputText.Attributes.Add("readonly", "readonly");
                    }


                    cell.Controls.Add(shippingChargeInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateSubTotalField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText subTotalInputText = new HtmlInputText())
                {
                    subTotalInputText.ID = "SubTotalInputText";
                    subTotalInputText.Attributes.Add("class", "currency text-right");
                    subTotalInputText.Attributes.Add("readonly", "readonly");

                    cell.Controls.Add(subTotalInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateTaxFormField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlSelect taxSelect = new HtmlSelect())
                {
                    taxSelect.ID = "TaxSelect";
                    taxSelect.Attributes.Add("title", "Ctrl + T");
                    cell.Controls.Add(taxSelect);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateTaxtField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText taxInputText = new HtmlInputText())
                {
                    taxInputText.ID = "TaxInputText";
                    taxInputText.Attributes.Add("class", "currency text-right");
                    taxInputText.Attributes.Add("readonly", "readonly");

                    cell.Controls.Add(taxInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private void CreateUnitField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlSelect unitSelect = new HtmlSelect())
                {
                    unitSelect.ID = "UnitSelect";
                    unitSelect.Attributes.Add("title", "Ctrl + U");
                    cell.Controls.Add(unitSelect);
                }

                row.Cells.Add(cell);
            }
        }
        #endregion

        #endregion
        private void CreateTitle(Control container)
        {
            this.title = new HtmlGenericControl("h1");
            this.title.InnerText = this.Text;
            container.Controls.Add(this.title);
        }

        #region Top Form Panel

        private void AddCashTransactionDivCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                if (this.ShowTransactionType)
                {
                    using (HtmlGenericControl toggleCheckBox = HtmlControlHelper.GetToggleCheckBox())
                    {
                        toggleCheckBox.ID = "CashTransactionDiv";
                        using (HtmlInputCheckBox cashTransactionInputCheckBox = new HtmlInputCheckBox())
                        {
                            cashTransactionInputCheckBox.ID = "CashTransactionInputCheckBox";
                            cashTransactionInputCheckBox.Attributes.Add("checked", "checked");
                            toggleCheckBox.Controls.Add(cashTransactionInputCheckBox);
                        }

                        using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CashTransaction")))
                        {
                            toggleCheckBox.Controls.Add(label);
                        }

                        cell.Controls.Add(toggleCheckBox);
                    }
                }

                row.Cells.Add(cell);
            }
        }

        private void AddCell(HtmlTableRow row, string text)
        {
            using (HtmlTableCell cell = new HtmlTableCell())
            {
                cell.InnerHtml = text;

                row.Cells.Add(cell);
            }
        }

        private void AddDateTextBoxCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                this.dateTextBox = new DateTextBox();

                this.dateTextBox.ID = "DateTextBox";
                this.dateTextBox.Mode = FrequencyType.Today;
                this.dateTextBox.CssClass = "date";

                cell.Controls.Add(this.dateTextBox);

                row.Cells.Add(cell);
            }
        }

        private void AddPartyCodeInputTextCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                this.partyCodeInputText = new HtmlInputText();
                this.partyCodeInputText.ID = "PartyCodeInputText";
                this.partyCodeInputText.Attributes.Add("title", "F2");

                cell.Controls.Add(this.partyCodeInputText);

                row.Cells.Add(cell);
            }
        }

        private void AddPartySelectCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                using (HtmlSelect partySelect = new HtmlSelect())
                {
                    partySelect.ID = "PartySelect";
                    partySelect.Attributes.Add("title", "F2");

                    cell.Controls.Add(partySelect);
                }
                row.Cells.Add(cell);
            }
        }

        private void AddPaymentTermSelectCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                if (this.ShowPaymentTerms)
                {
                    using (HtmlSelect paymentTermSelect = new HtmlSelect())
                    {
                        paymentTermSelect.ID = "PaymentTermSelect";
                        cell.Controls.Add(paymentTermSelect);
                    }
                }

                row.Cells.Add(cell);
            }
        }

        private void AddPriceTypeSelectCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                using (HtmlSelect priceTypeSelect = new HtmlSelect())
                {
                    priceTypeSelect.ID = "PriceTypeSelect";
                    cell.Controls.Add(priceTypeSelect);
                }

                row.Cells.Add(cell);
            }
        }

        private void AddReferenceNumberInputTextCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                this.referenceNumberInputText = new HtmlInputText();
                this.referenceNumberInputText.ID = "ReferenceNumberInputText";
                this.referenceNumberInputText.MaxLength = 24;

                cell.Controls.Add(this.referenceNumberInputText);
                row.Cells.Add(cell);
            }
        }

        private void AddStoreSelectCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetFieldCell())
            {
                using (HtmlSelect storeSelect = new HtmlSelect())
                {
                    storeSelect.ID = "StoreSelect";
                    cell.Controls.Add(storeSelect);
                }

                row.Cells.Add(cell);
            }
        }

        private void AddTopFormControls(HtmlTable table)
        {
            using (HtmlTableRow row = new HtmlTableRow())
            {
                this.AddDateTextBoxCell(row);

                if (this.ShowStore)
                {
                    this.AddStoreSelectCell(row);
                }

                this.AddPartyCodeInputTextCell(row);
                this.AddPartySelectCell(row);

                if (this.ShowPriceTypes)
                {
                    this.AddPriceTypeSelectCell(row);
                }

                this.AddReferenceNumberInputTextCell(row);
                this.AddCashTransactionDivCell(row);
                this.AddPaymentTermSelectCell(row);

                table.Controls.Add(row);
            }
        }

        private void AddTopFormLabels(HtmlTable table)
        {
            using (HtmlTableRow header = new HtmlTableRow())
            {
                this.AddCell(header, HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ValueDate"), "DateTextBox"));

                if (this.ShowStore)
                {
                    this.AddCell(header, HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SelectStore"), "StoreSelect"));
                }

                this.AddCell(header, HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SelectParty"), "PartyCodeInputText"));
                this.AddCell(header, string.Empty);

                if (this.ShowPriceTypes)
                {
                    this.AddCell(header, HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "PriceType"), "PriceTypeSelect"));
                }

                this.AddCell(header, HtmlControlHelper.GetLabelHtml(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ReferenceNumberAbbreviated"), "ReferenceNumberInputText"));
                this.AddCell(header, string.Empty);
                this.AddCell(header, string.Empty);

                table.Rows.Add(header);
            }
        }

        private void CreateTopFormPanel(Control container)
        {
            using (HtmlGenericControl segment = HtmlControlHelper.GetSegment())
            {
                using (HtmlTable table = new HtmlTable())
                {
                    table.Attributes.Add("class", "ui form");

                    this.AddTopFormLabels(table);
                    this.AddTopFormControls(table);
                    segment.Controls.Add(table);
                }

                container.Controls.Add(segment);
            }


            using (HtmlGenericControl form = HtmlControlHelper.GetForm())
            {
                using (HtmlGenericControl fields = HtmlControlHelper.GetFields("two fields"))
                {
                    if (this.ShowShippingInformation)
                    {
                        this.AddShippingAddressCompositeField(fields);
                    }

                    if (this.Book == TranBook.Sales && this.ShowSalesType)
                    {
                        this.AddSalesTypeField(fields);
                    }

                    form.Controls.Add(fields);
                }

                container.Controls.Add(form);
            }
        }

        #region Shipping Address Composite Field

        private void AddShippingAddressCompositeField(HtmlGenericControl container)
        {
            using (HtmlGenericControl shippingAddressInfoDiv = HtmlControlHelper.GetField())
            {
                shippingAddressInfoDiv.ID = "ShippingAddressInfoDiv";
                shippingAddressInfoDiv.Attributes.Add("style", "width:500px;");

                using (HtmlGenericControl fields = HtmlControlHelper.GetFields("two fields"))
                {
                    this.AddShippingCompanyField(fields);
                    this.AddShippingAddressField(fields);

                    shippingAddressInfoDiv.Controls.Add(fields);
                }


                container.Controls.Add(shippingAddressInfoDiv);
            }
        }

        private void AddShippingAddressField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingAddress"), "ShippingAddressSelect"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlSelect shippingAddressSelect = new HtmlSelect())
                {
                    shippingAddressSelect.ID = "ShippingAddressSelect";
                    field.Controls.Add(shippingAddressSelect);
                }

                container.Controls.Add(field);
            }
        }

        private void AddShippingCompanyField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingCompany"), "ShippingCompanySelect"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlSelect shippingCompanySelect = new HtmlSelect())
                {
                    shippingCompanySelect.ID = "ShippingCompanySelect";
                    field.Controls.Add(shippingCompanySelect);
                }

                container.Controls.Add(field);
            }
        }

        #endregion

        #region Sales Types

        private void AddSalesTypeField(HtmlGenericControl container)
        {
            using (HtmlGenericControl salesTypeDiv = HtmlControlHelper.GetField())
            {
                salesTypeDiv.ID = "SalesTypeDiv";
                salesTypeDiv.Attributes.Add("style", "width:200px");

                using (HtmlGenericControl field = HtmlControlHelper.GetField())
                {
                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "SalesType"), "SalesTypeSelect"))
                    {
                        field.Controls.Add(label);
                    }

                    using (HtmlSelect salesTypeSelect = new HtmlSelect())
                    {
                        salesTypeSelect.ID = "SalesTypeSelect";
                        salesTypeSelect.DataSource = this.GetSalesTypes();
                        salesTypeSelect.DataTextField = "Text";
                        salesTypeSelect.DataValueField = "Value";
                        salesTypeSelect.DataBind();

                        field.Controls.Add(salesTypeSelect);
                    }

                    salesTypeDiv.Controls.Add(field);
                }

                container.Controls.Add(salesTypeDiv);
            }
        }

        private IEnumerable<ListItem> GetSalesTypes()
        {
            Collection<ListItem> items = new Collection<ListItem>();

            items.Add(new ListItem(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "TaxableSales"), "1"));
            items.Add(new ListItem(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "NonTaxableSales"), "0"));


            if (this.model.NonTaxableSales)
            {
                items[1].Selected = true;
            }

            return items;
        }

        #endregion

        private HtmlTableCell GetFieldCell()
        {
            using (HtmlTableCell cell = new HtmlTableCell())
            {
                cell.Attributes.Add("class", "ui field");
                return cell;
            }
        }

        #endregion

        #region Bottom Form Panel

        private void AddCostCenterField(HtmlGenericControl container)
        {
            using (HtmlGenericControl costCenterDiv = HtmlControlHelper.GetField())
            {
                costCenterDiv.ID = "CostCenterDiv";

                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "CostCenter"), "CostCenterSelect"))
                {
                    costCenterDiv.Controls.Add(label);
                }

                using (HtmlSelect costCenterSelect = new HtmlSelect())
                {
                    costCenterSelect.ID = "CostCenterSelect";
                    costCenterDiv.Controls.Add(costCenterSelect);
                }

                container.Controls.Add(costCenterDiv);
            }
        }

        private void AddSalespersonField(HtmlGenericControl container)
        {
            using (HtmlGenericControl salespersonDiv = HtmlControlHelper.GetField())
            {
                salespersonDiv.ID = "SalespersonDiv";

                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Salesperson"), "SalesPersonSelect"))
                {
                    salespersonDiv.Controls.Add(label);
                }

                using (HtmlSelect salesPersonSelect = new HtmlSelect())
                {
                    salesPersonSelect.ID = "SalesPersonSelect";
                    salespersonDiv.Controls.Add(salesPersonSelect);
                }


                container.Controls.Add(salespersonDiv);
            }
        }

        private void AddSaveButton(HtmlGenericControl container)
        {
            using (HtmlInputButton saveButton = new HtmlInputButton())
            {
                saveButton.ID = "SaveButton";
                saveButton.Attributes.Add("class", "small ui red button");
                saveButton.Value = StockTransactionFactoryResourceHelper.GetResourceString("Titles", "Save");

                container.Controls.Add(saveButton);
            }
        }

        private void AddShippingAddressTextAreaField(HtmlGenericControl container)
        {
            using (HtmlGenericControl shippingAddressDiv = HtmlControlHelper.GetField())
            {
                shippingAddressDiv.ID = "ShippingAddressDiv";

                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "ShippingAddress"), "ShippingAddressTextArea"))
                {
                    shippingAddressDiv.Controls.Add(label);
                }

                using (HtmlTextArea shippingAddressTextArea = new HtmlTextArea())
                {
                    shippingAddressTextArea.ID = "ShippingAddressTextArea";
                    shippingAddressTextArea.Attributes.Add("readonly", "readonly");

                    shippingAddressDiv.Controls.Add(shippingAddressTextArea);
                }

                container.Controls.Add(shippingAddressDiv);
            }
        }

        private void AddStatementReferenceField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "StatementReference"), "StatementReferenceTextArea"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlTextArea statementReferenceTextArea = new HtmlTextArea())
                {
                    statementReferenceTextArea.ID = "StatementReferenceTextArea";

                    statementReferenceTextArea.Value = this.model.StatementReference;

                    field.Controls.Add(statementReferenceTextArea);
                }

                container.Controls.Add(field);
            }
        }

        private void CreateBottomFormPanel(Control container)
        {
            using (HtmlGenericControl formContainer = new HtmlGenericControl("div"))
            {
                formContainer.Attributes.Add("style", "width:500px");

                using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment()) //ui page form segment
                {
                    if (this.ShowShippingInformation)
                    {
                        this.AddShippingAddressTextAreaField(formSegment);
                    }

                    this.AddTotalFields(formSegment);

                    if (this.ShowCostCenter)
                    {
                        this.AddCostCenterField(formSegment);
                    }

                    if (this.ShowSalesAgents)
                    {
                        this.AddSalespersonField(formSegment);
                    }

                    this.AddStatementReferenceField(formSegment);
                    this.AddSaveButton(formSegment);

                    formContainer.Controls.Add(formSegment);
                }
                container.Controls.Add(formContainer);
            }
        }

        #region Total Fields

        private void AddGrandTotalField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "GrandTotal"), "GrandTotalInputTextInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText grandTotalInputTextInputText = new HtmlInputText())
                {
                    grandTotalInputTextInputText.ID = "GrandTotalInputText";
                    grandTotalInputTextInputText.Attributes.Add("class", "currency");
                    grandTotalInputTextInputText.Attributes.Add("readonly", "readonly");
                    field.Controls.Add(grandTotalInputTextInputText);
                }

                container.Controls.Add(field);
            }
        }

        private void AddRunningTotalField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "RunningTotal"), "RunningTotalInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText runningTotalInputText = new HtmlInputText())
                {
                    runningTotalInputText.ID = "RunningTotalInputText";
                    runningTotalInputText.Attributes.Add("class", "currency");
                    runningTotalInputText.Attributes.Add("readonly", "readonly");
                    field.Controls.Add(runningTotalInputText);
                }

                container.Controls.Add(field);
            }
        }

        private void AddTaxTotalField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(StockTransactionFactoryResourceHelper.GetResourceString("Titles", "TaxTotal"), "TaxTotalInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText taxTotalInputText = new HtmlInputText())
                {
                    taxTotalInputText.ID = "TaxTotalInputText";
                    taxTotalInputText.Attributes.Add("class", "currency");
                    taxTotalInputText.Attributes.Add("readonly", "readonly");
                    field.Controls.Add(taxTotalInputText);
                }

                container.Controls.Add(field);
            }
        }

        private void AddTotalFields(HtmlGenericControl container)
        {
            using (HtmlGenericControl fields = HtmlControlHelper.GetFields("three fields"))
            {
                this.AddRunningTotalField(fields);
                this.AddTaxTotalField(fields);
                this.AddGrandTotalField(fields);

                container.Controls.Add(fields);
            }
        }

        #endregion

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

            this.partyCodeHidden.Value = this.model.PartyCode.ToString(CultureInfo.InvariantCulture);
            this.partyCodeInputText.Value = this.model.PartyCode.ToString(CultureInfo.InvariantCulture);
            this.priceTypeIdHidden.Value = this.model.PriceTypeId.ToString(CultureInfo.InvariantCulture);
            this.storeIdHidden.Value = this.model.StoreId.ToString(CultureInfo.InvariantCulture);
            this.shipperIdHidden.Value = this.model.ShippingCompanyId.ToString(CultureInfo.InvariantCulture);
            this.shippingAddressCodeHidden.Value = this.model.ShippingAddressCode.ToString(CultureInfo.InvariantCulture);
            this.salesPersonIdHidden.Value = this.model.SalesPersonId.ToString(CultureInfo.InvariantCulture);

            this.referenceNumberInputText.Value = this.model.ReferenceNumber;

            this.Session[this.ID] = this.model.View;
            this.tranIdCollectionHiddenField.Value = string.Join(",", this.model.TransactionIdCollection);
            this.ClearSession("Product");
        }


        #region Grid Binding

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

        private void BindGridView()
        {
            Collection<ProductDetail> table = this.GetTable();

            if (table.Count > 0)
            {
                List<string[]> rowData = new List<string[]>();

                foreach (ProductDetail row in table)
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

                this.productGridViewDataHidden.Value = data;
            }
        }

        private Collection<ProductDetail> GetTable()
        {
            Collection<ProductDetail> productCollection = new Collection<ProductDetail>();

            if (this.Session[this.ID] != null)
            {
                //Get an instance of the ProductDetailsModel collection stored in session.
                productCollection = (Collection<ProductDetail>)this.Session[this.ID];

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