using System.Data;
using System.Web.UI;
using MixERP.Net.BusinessLayer.Core;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.BusinessLayer.Transactions;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionView.Data;
using MixERP.Net.WebControls.StockTransactionView.Data.Models;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Resources;

namespace MixERP.Net.FrontEnd.UserControls.Products
{
    /// This class is subject to be moved to a standalone server control class library.
    public partial class ProductViewControl : UserControl
    {
        public TranBook Book { get; set; }
        public SubTranBook SubBook { get; set; }
        public string Text
        {
            get
            {
                return this.TitleLiteral.Text;
            }
            set
            {
                this.TitleLiteral.Text = value;
            }
        }

        public string ChecklistUrl { get; set; }
        public string PreviewUrl { get; set; }

        protected void Page_Init()
        {
            this.BindFlagTypeDropDownList();
        }

        private void BindFlagTypeDropDownList()
        {
            DropDownListHelper.BindDropDownList(this.FlagDropDownList, "core", "flag_types", "flag_type_id", "flag_type_name");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadGridView();
            this.InitializePostBackUrls();
            this.InitializeLinkButtons();
        }

        private void InitializeLinkButtons()
        {
            if (this.Book == TranBook.Sales)
            {
                if (this.SubBook == SubTranBook.Order)
                {
                    this.MergeToDeliveryLinkButton.Visible = true;
                }

                if (this.SubBook == SubTranBook.Quotation)
                {
                    this.MergeToOrderLinkButton.Visible = true;
                    this.MergeToDeliveryLinkButton.Visible = true;
                }
            }
        }

        private void InitializePostBackUrls()
        {
            if (this.Book == TranBook.Sales)
            {
                switch (this.SubBook)
                {
                    case SubTranBook.Order:
                        this.AddNewLinkButton.PostBackUrl = "~/Sales/Entry/Order.aspx";
                        break;
                    case SubTranBook.Quotation:
                        this.AddNewLinkButton.PostBackUrl = "~/Sales/Entry/Quotation.aspx";
                        break;
                }
            }
        }

        private Collection<int> GetSelectedValues()
        {
            //Get the comma separated selected values.
            string selectedValues = this.SelectedValuesHidden.Value;

            //Check if something was selected.
            if (string.IsNullOrWhiteSpace(selectedValues))
            {
                return new Collection<int>();
            }

            //Create a collection object to store the IDs.
            Collection<int> values = new Collection<int>();

            //Iterate through each value in the selected values
            //and determine if each value is a number.
            foreach (string value in selectedValues.Split(','))
            {
                //Parse the value to integer.
                int val = Conversion.TryCastInteger(value);

                //If the object "val" has a greater than zero,
                //add it to the collection.
                if (val > 0)
                {
                    values.Add(val);
                }
            }

            return values;
        }

        protected void MergeToOrderLinkButton_Click(object sender, EventArgs e)
        {
            Collection<int> values = this.GetSelectedValues();

            if (this.IsValid())
            {
                this.Merge(values, "~/Sales/Entry/Order.aspx");
            }
        }

        #region Validation
        private bool IsValid()
        {
            Collection<int> values = this.GetSelectedValues();

            if (values.Count.Equals(0))
            {
                this.ErrorLabel.Text = Warnings.NothingSelectedPleaseTryAgain;
                return false;
            }

            if (!this.BelongToSameParty(values)) return false;
            if (this.AreAlreadyMerged(values)) return false;

            return true;
        }

        private bool BelongToSameParty(Collection<int> values)
        {
            bool belongToSameParty = NonGlStockTransaction.TransactionIdsBelongToSameParty(values);

            if (!belongToSameParty)
            {
                this.ErrorLabel.Text = Warnings.CannotMergeTransactionsOfDifferentParties;
                return false;
            }

            return true;
        }

        private bool AreAlreadyMerged(Collection<int> values)
        {
            if (this.AreSalesQuotationsAlreadyMerged(values)) return true;
            if (this.AreSalesOrdersAlreadyMerged(values)) return true;

            return false;
        }

        private bool AreSalesQuotationsAlreadyMerged(Collection<int> values)
        {
            if (this.Book == TranBook.Sales && this.SubBook == SubTranBook.Quotation)
            {
                if (NonGlStockTransaction.AreSalesQuotationsAlreadyMerged(values))
                {
                    this.ErrorLabel.Text = Labels.TransactionAlreadyMerged;
                    return true;
                }
            }

            return false;
        }

        private bool AreSalesOrdersAlreadyMerged(Collection<int> values)
        {
            if (this.Book == TranBook.Sales && this.SubBook == SubTranBook.Order)
            {
                if (NonGlStockTransaction.AreSalesOrdersAlreadyMerged(values))
                {
                    this.ErrorLabel.Text = Labels.TransactionAlreadyMerged;
                    return true;
                }
            }

            return false;
        }

        #endregion

        private void Merge(Collection<int> ids, string link)
        {
            MergeModel model = ModelFactory.GetMergeModel(ids, this.Book, this.SubBook);

            if (model.View == null)
            {
                return;
            }

            if (model.View.Count.Equals(0))
            {
                return;
            }

            HttpContext.Current.Session["Product"] = model;
            HttpContext.Current.Response.Redirect(link);
        }

        protected void MergeToDeliveryLinkButton_Click(object sender, EventArgs e)
        {
            Collection<int> values = this.GetSelectedValues();

            if (this.IsValid())
            {
                this.Merge(values, "~/Sales/DeliveryWithoutOrder.aspx");
            }
        }


        protected void ShowButton_Click(object sender, EventArgs e)
        {
            this.LoadGridView();
        }

        private void LoadGridView()
        {
            DateTime dateFrom = Conversion.TryCastDate(this.DateFromDateTextBox.Text);
            DateTime dateTo = Conversion.TryCastDate(this.DateToDateTextBox.Text);
            string office = this.OfficeTextBox.Text;
            string party = this.PartyTextBox.Text;
            string priceType = this.PriceTypeTextBox.Text;
            string user = this.UserTextBox.Text;
            string referenceNumber = this.ReferenceNumberTextBox.Text;
            string statementReference = this.StatementReferenceTextBox.Text;
            string bookName = this.GetTransactionBookName();

            if (this.IsNonGlTransaction())
            {
                using (DataTable table = NonGlStockTransaction.GetView(bookName, dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference))
                {
                    this.ProductViewGridView.DataSource = table;
                    this.ProductViewGridView.DataBind();
                }
            }
        }

        private string GetTransactionBookName()
        {
            string bookName = string.Empty;

            if (this.Book == TranBook.Sales)
            {
                switch (this.SubBook)
                {
                    case SubTranBook.Delivery:
                        bookName = "Sales.Delivery";
                        break;
                    case SubTranBook.Direct:
                        bookName = "Sales.Direct";
                        break;
                    case SubTranBook.Invoice:
                        bookName = "Sales.Invoice";
                        break;
                    case SubTranBook.Order:
                        bookName = "Sales.Order";
                        break;
                    case SubTranBook.Payment:
                        throw new InvalidOperationException(Errors.InvalidSubTranBookSalesPayment);
                    case SubTranBook.Quotation:
                        bookName = "Sales.Quotation";
                        break;
                    case SubTranBook.Receipt:
                        bookName = "Sales.Receipt";
                        break;
                    case SubTranBook.Return:
                        bookName = "Sales.Return";
                        break;
                }
            }


            if (this.Book == TranBook.Purchase)
            {
                switch (this.SubBook)
                {
                    case SubTranBook.Delivery:
                        throw new InvalidOperationException(Errors.InvalidSubTranBookPurchaseDelivery);
                    case SubTranBook.Direct:
                        bookName = "Purchase.Direct";
                        break;
                    case SubTranBook.Invoice:
                        bookName = "Purchase.Invoice";
                        break;
                    case SubTranBook.Order:
                        bookName = "Purchase.Order";
                        break;
                    case SubTranBook.Payment:
                        bookName = "Purchase.Payment";
                        break;
                    case SubTranBook.Quotation:
                        throw new InvalidOperationException(Errors.InvalidSubTranBookPurchaseQuotation);
                    case SubTranBook.Receipt:
                        throw new InvalidOperationException(Errors.InvalidSubTranBookPurchaseReceipt);
                    case SubTranBook.Return:
                        bookName = "Purchase.Return";
                        break;
                }
            }

            return bookName;
        }
        private bool IsNonGlTransaction()
        {
            //Todo
            bool isNonGlTransaction = false;

            if (this.Book == TranBook.Sales)
            {
                switch (this.SubBook)
                {
                    case SubTranBook.Order:
                        isNonGlTransaction = true;
                        break;
                    case SubTranBook.Quotation:
                        isNonGlTransaction = true;
                        break;
                }
            }

            if (this.Book == TranBook.Purchase)
            {
                if (this.SubBook == SubTranBook.Order)
                {
                    isNonGlTransaction = true;
                }
            }

            return isNonGlTransaction;
        }
        private string GetTransactionTableName()
        {
            string tableName = "transactions.stock_master";

            if (this.IsNonGlTransaction())
            {
                tableName = "transactions.non_gl_stock_master";
            }
            return tableName;
        }

        private string GetTransactionTablePrimaryKeyName()
        {
            string key = "stock_master_id";

            if (this.IsNonGlTransaction())
            {
                key = "non_gl_stock_master_id";
            }
            return key;
        }

        protected void ProductViewGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string cellText = e.Row.Cells[i].Text.Replace("&nbsp;", " ").Trim();

                    if (!string.IsNullOrWhiteSpace(cellText))
                    {
                        cellText = LocalizationHelper.GetResourceString("ScrudResource", cellText);
                        e.Row.Cells[i].Text = cellText;
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = e.Row.Cells[2].Text;
                //Todo: Fix 403 errors.
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (!string.IsNullOrWhiteSpace(this.PreviewUrl))
                    {
                        string popUpQuotationPreviewUrl = this.Page.ResolveUrl(this.PreviewUrl + "?TranId=" + id);

                        using (HtmlAnchor previewAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("PreviewAnchor"))
                        {
                            if (previewAnchor != null)
                            {
                                previewAnchor.HRef = popUpQuotationPreviewUrl;
                            }
                        }

                        using (HtmlAnchor printAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("PrintAnchor"))
                        {
                            if (printAnchor != null)
                            {
                                printAnchor.Attributes.Add("onclick", "showWindow('" + popUpQuotationPreviewUrl + "');return false;");
                            }
                        }
                    }

                    using (HtmlAnchor checklistAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("ChecklistAnchor"))
                    {
                        if (!string.IsNullOrWhiteSpace(this.ChecklistUrl))
                        {
                            if (checklistAnchor != null)
                            {
                                string checkListUrl = this.Page.ResolveUrl(this.ChecklistUrl + "?TranId=" + id);
                                checklistAnchor.HRef = checkListUrl;
                            }
                        }
                    }

                }
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int flagTypeId = Conversion.TryCastInteger(this.FlagDropDownList.SelectedValue);
            string resource = this.GetTransactionTableName();
            string resourceKey = this.GetTransactionTablePrimaryKeyName();
            Collection<int> resourceIds = this.GetSelectedValues();

            Flags.CreateFlag(flagTypeId, resource, resourceKey, resourceIds);
            this.LoadGridView();
        }
    }
}