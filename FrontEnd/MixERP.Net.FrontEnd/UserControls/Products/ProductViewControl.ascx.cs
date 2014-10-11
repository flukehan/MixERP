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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.WebControls.StockTransactionView.Data;
using MixERP.Net.WebControls.StockTransactionView.Data.Helpers;
using MixERP.Net.WebControls.StockTransactionView.Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.UserControls.Products
{
    /// This class is subject to be moved to a standalone server control class library.
    public partial class ProductViewControl : UserControl
    {
        public TranBook Book { get; set; }

        public SubTranBook SubBook { get; set; }

        public string Text
        {
            get { return this.TitleLiteral.Text; }
            set { this.TitleLiteral.Text = value; }
        }

        public string ChecklistUrl { get; set; }

        public string PreviewUrl { get; set; }

        public string AddNewUrl { get; set; }

        public bool ShowMergeToDeliveryButton
        {
            set { MergeToDeliveryButton.Visible = value; }
        }

        public bool ShowMergeToOrderButton
        {
            set { MergeToOrderButton.Visible = value; }
        }

        public bool ShowMergeToGRNButton
        {
            set { MergeToGRNButton.Visible = value; }
        }

        public bool ShowReturnButton
        {
            set { ReturnButton.Visible = value; }
        }

        protected void Page_Init()
        {
            this.BindFlagTypeDropDownList();
        }

        private void BindFlagTypeDropDownList()
        {
            Data.Helpers.DropDownListHelper.BindDropDownList(this.FlagDropDownList, "core", "flag_types", "flag_type_id", "flag_type_name", "flag_type_id");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetVisibleStates();
            this.LoadGridView();
            this.InitializePostBackUrls();
        }

        private void SetVisibleStates()
        {
            if (this.SubBook == SubTranBook.Receipt)
            {
                this.PriceTypeDiv.Visible = false;
            }
        }

        private void InitializePostBackUrls()
        {
            if (string.IsNullOrWhiteSpace(this.AddNewUrl))
            {
                this.AddNewButton.Visible = false;
                return;
            }

            this.AddNewButton.Attributes.Add("onclick", "window.location='" + ResolveUrl(this.AddNewUrl) + "'");
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

        protected void MergeToOrderButton_Click(object sender, EventArgs e)
        {
            Collection<int> values = this.GetSelectedValues();

            if (this.IsValid())
            {
                this.Merge(values, "~/Modules/Sales/Entry/Order.mix");
            }
        }

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

        protected void MergeToDeliveryButton_Click(object sender, EventArgs e)
        {
            Collection<int> values = this.GetSelectedValues();

            if (this.IsValid())
            {
                this.Merge(values, "~/Modules/Sales/Entry/Delivery.mix");
            }
        }

        protected void MergeToGRNButton_Click(object sender, EventArgs e)
        {
            Collection<int> values = this.GetSelectedValues();

            if (this.IsValid())
            {
                this.Merge(values, "~/Modules/Purchase/Entry/GRN.mix");
            }
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Collection<int> values = this.GetSelectedValues();

            if (this.Book == TranBook.Sales)
            {
                Response.Redirect("~/Modules/Sales/Entry/Return.mix?TranId=" + values[0]);
                return;
            }

            Response.Redirect("~/Modules/Purchase/Entry/Return.mix?TranId=" + values[0]);
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
            string bookName = TransactionBookHelper.GetTransactionBookName(this.Book, this.SubBook);

            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();

            WebControls.StockTransactionView.Helpers.GridViewColumnHelper.AddColumns(this.ProductViewGridView, this.SubBook);

            if (this.IsNonGlTransaction())
            {
                if (this.SubBook == SubTranBook.Receipt)
                {
                    using (DataTable table = CustomerReceipts.GetView(userId, officeId, dateFrom, dateTo, office, party, user, referenceNumber, statementReference))
                    {
                        this.ProductViewGridView.DataSource = table;
                        this.ProductViewGridView.DataBind();
                        return;
                    }
                }
                using (DataTable table = NonGlStockTransaction.GetView(bookName, dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference))
                {
                    this.ProductViewGridView.DataSource = table;
                    this.ProductViewGridView.DataBind();
                    return;
                }
            }

            using (DataTable table = GLStockTransaction.GetView(bookName, dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference))
            {
                this.ProductViewGridView.DataSource = table;
                this.ProductViewGridView.DataBind();
            }
        }

        private bool IsNonGlTransaction()
        {
            if (this.Book == TranBook.Sales)
            {
                switch (this.SubBook)
                {
                    case SubTranBook.Receipt:
                        return true;

                    case SubTranBook.Order:
                        return true;

                    case SubTranBook.Quotation:
                        return true;
                }
            }

            if (this.Book == TranBook.Purchase)
            {
                if (this.SubBook == SubTranBook.Order)
                {
                    return true;
                }
            }

            return false;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = e.Row.Cells[2].Text;
                //Todo: Fix 403 errors.
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (!string.IsNullOrWhiteSpace(this.PreviewUrl))
                    {
                        string popUpQuotationPreviewUrl = this.Page.ResolveUrl(this.PreviewUrl + "?TranId=" + id);

                        using (HtmlAnchor printAnchor = (HtmlAnchor) e.Row.Cells[0].FindControl("PrintAnchor"))
                        {
                            if (printAnchor != null)
                            {
                                printAnchor.Attributes.Add("onclick", "showWindow('" + popUpQuotationPreviewUrl + "');return false;");
                            }
                        }
                    }

                    using (HtmlAnchor checklistAnchor = (HtmlAnchor) e.Row.Cells[0].FindControl("ChecklistAnchor"))
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

            if (this.SubBook == SubTranBook.Receipt)
            {
                resource = "transactions.transaction_master";
                resourceKey = "transaction_master_id";
            }

            Collection<int> resourceIds = this.GetSelectedValues();

            int userId = SessionHelper.GetUserId();

            WebControls.StockTransactionView.Helpers.Flags.CreateFlag(userId, flagTypeId, resource, resourceKey, resourceIds);
            this.LoadGridView();
        }

        #region Validation

        private bool IsValid()
        {
            Collection<int> values = this.GetSelectedValues();

            if (values.Count.Equals(0))
            {
                this.ErrorLabel.Text = "Nothing selected.";
                return false;
            }

            if (!this.BelongToSameParty(values))
            {
                return false;
            }
            if (this.AreAlreadyMerged(values))
            {
                return false;
            }

            return true;
        }

        private bool BelongToSameParty(Collection<int> values)
        {
            bool belongToSameParty = NonGlStockTransaction.TransactionIdsBelongToSameParty(values);

            if (!belongToSameParty)
            {
                this.ErrorLabel.Text = "Cannot merge transactions of different parties into a single batch. Please try again.";
                return false;
            }

            return true;
        }

        private bool AreAlreadyMerged(Collection<int> values)
        {
            if (this.AreSalesQuotationsAlreadyMerged(values))
            {
                return true;
            }
            if (this.AreSalesOrdersAlreadyMerged(values))
            {
                return true;
            }

            return false;
        }

        private bool AreSalesQuotationsAlreadyMerged(Collection<int> values)
        {
            if (this.Book == TranBook.Sales && this.SubBook == SubTranBook.Quotation)
            {
                if (NonGlStockTransaction.AreSalesQuotationsAlreadyMerged(values))
                {
                    this.ErrorLabel.Text = "The selected transaction(s) contains item(s) which have already been merged. Please try again.";
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
                    this.ErrorLabel.Text = "The selected transaction(s) contains item(s) which have already been merged. Please try again.";
                    return true;
                }
            }

            return false;
        }

        #endregion Validation
    }
}