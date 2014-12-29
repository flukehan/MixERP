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
using MixERP.Net.WebControls.StockTransactionView.Resources;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionView.Helpers
{
    public static class GridViewColumnHelper
    {
        public static void AddColumns(GridView grid, SubTranBook book)
        {
            grid.Columns.Clear();

            AddTemplateFields(grid);

            GridViewHelper.AddDataBoundControl(grid, "id", Titles.Id);
            GridViewHelper.AddDataBoundControl(grid, "value_date", Titles.ValueDate, "{0:d}");
            GridViewHelper.AddDataBoundControl(grid, "office", Titles.Office);
            GridViewHelper.AddDataBoundControl(grid, "reference_number", Titles.ReferenceNumber);
            GridViewHelper.AddDataBoundControl(grid, "party", Titles.Party);

            if (book != SubTranBook.Receipt)
            {
                GridViewHelper.AddDataBoundControl(grid, "price_type", Titles.PriceType);
            }

            GridViewHelper.AddDataBoundControl(grid, "amount", Titles.Amount);
            GridViewHelper.AddDataBoundControl(grid, "transaction_ts", Titles.TransactionTimestamp, "{0:d}");
            GridViewHelper.AddDataBoundControl(grid, "user", Titles.User);
            GridViewHelper.AddDataBoundControl(grid, "statement_reference", Titles.StatementReference);

            if (book != SubTranBook.Receipt)
            {
                GridViewHelper.AddDataBoundControl(grid, "book", Titles.Book, "");
            }

            GridViewHelper.AddDataBoundControl(grid, "flag_background_color", Titles.FlagBackgroundColor);
            GridViewHelper.AddDataBoundControl(grid, "flag_foreground_color", Titles.FlagForegroundColor);
        }

        private static void AddTemplateFields(GridView grid)
        {
            TemplateField actionTemplateField = new TemplateField();
            actionTemplateField.HeaderStyle.Width = 90;
            actionTemplateField.HeaderText = Titles.Actions;
            actionTemplateField.ItemTemplate = new StockTransactionViewActionTemplate();

            grid.Columns.Add(actionTemplateField);

            TemplateField checkBoxTemplateField = new TemplateField();
            checkBoxTemplateField.HeaderText = Titles.Select;
            checkBoxTemplateField.ItemTemplate = new StockTransactionViewSelectTemplate();
            grid.Columns.Add(checkBoxTemplateField);
        }

        public class StockTransactionViewActionTemplate : ITemplate, IDisposable
        {
            private HtmlAnchor checkListAnchor;
            private bool disposed;
            private HtmlAnchor goToTopAnchor;
            private HtmlAnchor printAnchor;

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void InstantiateIn(Control container)
            {
                this.checkListAnchor = new HtmlAnchor();
                this.checkListAnchor.ID = "ChecklistAnchor";
                this.checkListAnchor.ClientIDMode = ClientIDMode.Predictable;
                this.checkListAnchor.Title = Labels.GoToChecklistWindow;
                this.checkListAnchor.InnerHtml = "<img src='" + PageUtility.ResolveUrl("~/Resource/Icons/checklist-16.png") + "' />";//Todo: embed these icons.

                this.printAnchor = new HtmlAnchor();
                this.printAnchor.ID = "PrintAnchor";
                this.printAnchor.ClientIDMode = ClientIDMode.Predictable;
                this.printAnchor.Title = Titles.Print;
                this.printAnchor.InnerHtml = "<img src='" + PageUtility.ResolveUrl("~/Resource/Icons/print-16.png") + "' />";

                this.goToTopAnchor = new HtmlAnchor();
                this.goToTopAnchor.Title = Labels.GoToTop;
                this.goToTopAnchor.Attributes.Add("onclick", "window.scroll(0);");
                this.goToTopAnchor.InnerHtml = "<img src='" + PageUtility.ResolveUrl("~/Resource/Icons/top-16.png") + "' />";

                container.Controls.Add(this.checkListAnchor);
                container.Controls.Add(this.printAnchor);
                container.Controls.Add(this.goToTopAnchor);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        if (this.checkListAnchor != null)
                        {
                            this.checkListAnchor.Dispose();
                            this.checkListAnchor = null;
                        }

                        if (this.printAnchor != null)
                        {
                            this.printAnchor.Dispose();
                            this.printAnchor = null;
                        }

                        if (this.goToTopAnchor != null)
                        {
                            this.goToTopAnchor.Dispose();
                            this.goToTopAnchor = null;
                        }
                    }

                    this.disposed = true;
                }
            }
        }

        public class StockTransactionViewSelectTemplate : ITemplate, IDisposable
        {
            private HtmlInputCheckBox checkBox;
            private bool disposed;

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void InstantiateIn(Control container)
            {
                using (HtmlGenericControl div = new HtmlGenericControl("div"))
                {
                    div.Attributes.Add("class", "ui toggle checkbox");
                    this.checkBox = new HtmlInputCheckBox();
                    this.checkBox.ID = "SelectCheckBox";
                    this.checkBox.ClientIDMode = ClientIDMode.Predictable;

                    div.Controls.Add(this.checkBox);

                    //Added for compatibility with Semantic UI
                    using (HtmlGenericControl label = new HtmlGenericControl("label"))
                    {
                        div.Controls.Add(label);
                    }

                    container.Controls.Add(div);
                }
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        if (this.checkBox != null)
                        {
                            this.checkBox.Dispose();
                            this.checkBox = null;
                        }
                    }

                    this.disposed = true;
                }
            }
        }
    }
}