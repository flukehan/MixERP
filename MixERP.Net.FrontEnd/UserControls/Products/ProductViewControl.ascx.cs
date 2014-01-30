using MixERP.Net.BusinessLayer.Transactions;
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

namespace MixERP.Net.FrontEnd.UserControls.Products
{
    /// This class is subject to be moved to a standalone server control class library.
    public partial class ProductViewControl : System.Web.UI.UserControl
    {
        public MixERP.Net.Common.Models.Transactions.TranBook Book { get; set; }
        public MixERP.Net.Common.Models.Transactions.SubTranBook SubBook { get; set; }
        public string Text
        {
            get
            {
                return TitleLiteral.Text;
            }
            set
            {
                TitleLiteral.Text = value;
            }
        }

        protected void Page_Init()
        {
            this.BindFlagTypeDropDownList();
        }

        private void BindFlagTypeDropDownList()
        {
            MixERP.Net.BusinessLayer.Helpers.DropDownListHelper.BindDropDownList(FlagDropDownList, "core", "flag_types", "flag_type_id", "flag_type_name");
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
                    MergeToDeliveryLinkButton.Visible = true;
                }

                if (this.SubBook == SubTranBook.Quotation)
                {
                    MergeToOrderLinkButton.Visible = true;
                    MergeToDeliveryLinkButton.Visible = true;
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
                        AddNewLinkButton.PostBackUrl = "~/Sales/Entry/Order.aspx";
                        break;
                    case SubTranBook.Quotation:
                        AddNewLinkButton.PostBackUrl = "~/Sales/Entry/Quotation.aspx";
                        break;
                }
            }
        }

        private Collection<int> GetSelectedValues()
        {
            //Get the comma separated selected values.
            string selectedValues = SelectedValuesHidden.Value;

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
                int val = MixERP.Net.Common.Conversion.TryCastInteger(value);

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
                ErrorLabel.Text = Resources.Warnings.NothingSelectedPleaseTryAgain;
                return false;
            }

            if (!this.BelongToSameParty(values)) return false;
            if (this.AreAlreadyMerged(values)) return false;

            return true;
        }

        private bool BelongToSameParty(Collection<int> values)
        {
            bool belongToSameParty = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.TransactionIdsBelongToSameParty(values);

            if (!belongToSameParty)
            {
                ErrorLabel.Text = Resources.Warnings.CannotMergeTransactionsOfDifferentParties;
                return false;
            }

            return true;
        }

        private bool AreAlreadyMerged(Collection<int> values)
        {
            if (AreSalesQuotationsAlreadyMerged(values)) return true;
            if (AreSalesOrdersAlreadyMerged(values)) return true;

            return false;
        }

        private bool AreSalesQuotationsAlreadyMerged(Collection<int> values)
        {
            if (this.Book == TranBook.Sales && this.SubBook == SubTranBook.Quotation)
            {
                if (NonGLStockTransaction.AreSalesQuotationsAlreadyMerged(values))
                {
                    ErrorLabel.Text = Resources.Labels.TransactionAlreadyMerged;
                    return true;
                }
            }

            return false;
        }

        private bool AreSalesOrdersAlreadyMerged(Collection<int> values)
        {
            if (this.Book == TranBook.Sales && this.SubBook == SubTranBook.Order)
            {
                if (NonGLStockTransaction.AreSalesOrdersAlreadyMerged(values))
                {
                    ErrorLabel.Text = Resources.Labels.TransactionAlreadyMerged;
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
            DateTime dateFrom = MixERP.Net.Common.Conversion.TryCastDate(DateFromDateTextBox.Text);
            DateTime dateTo = MixERP.Net.Common.Conversion.TryCastDate(DateToDateTextBox.Text);
            string office = OfficeTextBox.Text;
            string party = PartyTextBox.Text;
            string priceType = PriceTypeTextBox.Text;
            string user = UserTextBox.Text;
            string referenceNumber = ReferenceNumberTextBox.Text;
            string statementReference = StatementReferenceTextBox.Text;
            string bookName = this.GetTransactionBookName();

            if (this.IsNonGlTransaction())
            {
                using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.GetView(bookName, dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference))
                {
                    ProductViewGridView.DataSource = table;
                    ProductViewGridView.DataBind();
                }
            }
            else
            {
                //Todo
                //using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.GetView(bookName, dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference))
                //{
                //    ProductViewGridView.DataSource = table;
                //    ProductViewGridView.DataBind();
                //}            
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
                        throw new InvalidOperationException("Invalid SubTranBook 'Sales Payment'");
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
                        throw new InvalidOperationException("Invalid SubTranBook 'Purchase Receipt'");
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
                        throw new InvalidOperationException("Invalid SubTranBook 'Purchase Quotation'");
                    case SubTranBook.Receipt:
                        throw new InvalidOperationException("Invalid SubTranBook 'Purchase Receipt'");
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
                if(this.SubBook == SubTranBook.Order)
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
                        cellText = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("ScrudResource", cellText);
                        e.Row.Cells[i].Text = cellText;
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = e.Row.Cells[2].Text;

                if (!string.IsNullOrWhiteSpace(id))
                {
                    string popUpQuotationPreviewUrl = this.Page.ResolveUrl("~/Sales/Confirmation/ReportSalesQuotation.aspx?TranId=" + id);

                    HtmlAnchor previewAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("PreviewAnchor");
                    if (previewAnchor != null)
                    {
                        previewAnchor.HRef = popUpQuotationPreviewUrl;
                    }

                    HtmlAnchor printAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("PrintAnchor");
                    if (printAnchor != null)
                    {
                        printAnchor.Attributes.Add("onclick", "showWindow('" + popUpQuotationPreviewUrl + "');return false;");
                    }
                }
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int flagTypeId = MixERP.Net.Common.Conversion.TryCastInteger(FlagDropDownList.SelectedValue);
            string resource = this.GetTransactionTableName();
            string resourceKey = this.GetTransactionTablePrimaryKeyName();
            Collection<int> resourceIds = this.GetSelectedValues();

            MixERP.Net.BusinessLayer.Core.Flags.CreateFlag(flagTypeId, resource, resourceKey, resourceIds);
            this.LoadGridView();
        }
    }
}