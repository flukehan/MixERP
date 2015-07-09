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
using MixERP.Net.Entities;
using MixERP.Net.Entities.Helpers;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.StockTransactionViewFactory.Data.Helpers;
using MixERP.Net.WebControls.StockTransactionViewFactory.Helpers;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private void AddGridView(Control container)
        {
            using (HtmlGenericControl gridSegment = new HtmlGenericControl("div"))
            {
                gridSegment.Attributes.Add("style", "width:100%;overflow:auto;");

                this.productViewGridView = new MixERPGridView();
                this.productViewGridView.ID = "ProductViewGridView";
                this.productViewGridView.GridLines = GridLines.None;
                this.productViewGridView.CssClass = "ui nowrap table";
                this.productViewGridView.AutoGenerateColumns = false;
                this.productViewGridView.RowDataBound += this.ProductViewGridView_RowDataBound;

                gridSegment.Controls.Add(this.productViewGridView);


                container.Controls.Add(gridSegment);
            }
        }

        private void ProductViewGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = e.Row.Cells[2].Text;
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (!string.IsNullOrWhiteSpace(this.PreviewUrl))
                    {
                        string popUpQuotationPreviewUrl = this.Page.ResolveUrl(this.PreviewUrl + "?TranId=" + id);

                        using (HtmlAnchor printAnchor = (HtmlAnchor) e.Row.Cells[0].FindControl("PrintAnchor"))
                        {
                            if (printAnchor != null)
                            {
                                printAnchor.Attributes.Add("onclick",
                                    "showWindow('" + popUpQuotationPreviewUrl + "');return false;");
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

        private void LoadGridView()
        {
            DateTime dateFrom = Conversion.TryCastDate(this.dateFromDateTextBox.Text);
            DateTime dateTo = Conversion.TryCastDate(this.dateToDateTextBox.Text);
            string office = this.officeInputText.Value;
            string party = this.partyInputText.Value;
            string priceType = string.Empty;
            string user = this.userInputText.Value;
            string referenceNumber = this.referenceNumberInputText.Value;
            string statementReference = this.statementReferenceInputText.Value;
            string bookName = TransactionBookHelper.GetInvariantTransactionBookName(this.Book, this.SubBook);

            if (this.priceTypeInputText != null)
            {
                priceType = this.priceTypeInputText.Value;
            }

            int userId = this.UserId;
            int officeId = this.OfficeId;

            GridViewColumnHelper.AddColumns(this.productViewGridView, this.SubBook);

            if (this.IsNonGlTransaction)
            {
                this.productViewGridView.DataSource = NonGlStockTransaction.GetView(this.Catalog, userId, bookName, officeId, dateFrom,
                    dateTo, office, party, priceType, user, referenceNumber, statementReference);
                this.productViewGridView.DataBind();
                return;
            }

            if (this.Book == TranBook.Sales && this.SubBook == SubTranBook.Receipt)
            {
                this.productViewGridView.DataSource = CustomerReceipts.GetView(this.Catalog, userId, officeId, dateFrom, dateTo,
                    office, party, user, referenceNumber, statementReference);
                this.productViewGridView.DataBind();
                return;
            }


            this.productViewGridView.DataSource = GLStockTransaction.GetView(this.Catalog, userId, bookName, officeId, dateFrom,
                dateTo, office, party, priceType, user, referenceNumber, statementReference);
            this.productViewGridView.DataBind();
        }
    }
}