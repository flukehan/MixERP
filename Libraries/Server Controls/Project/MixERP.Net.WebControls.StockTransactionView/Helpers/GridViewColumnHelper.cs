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

        public class StockTransactionViewSelectTemplate : ITemplate, IDisposable
        {
            private bool disposed;
            private HtmlInputCheckBox checkBox;

            public void InstantiateIn(Control container)
            {
                using (HtmlGenericControl div = new HtmlGenericControl("div"))
                {
                    div.Attributes.Add("class", "ui toggle checkbox");
                    checkBox = new HtmlInputCheckBox();
                    checkBox.ID = "SelectCheckBox";
                    checkBox.ClientIDMode = ClientIDMode.Predictable;

                    div.Controls.Add(checkBox);

                    //Added for compatibility with Semantic UI
                    using (HtmlGenericControl label = new HtmlGenericControl("label"))
                    {
                        div.Controls.Add(label);
                    }

                    container.Controls.Add(div);
                }
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
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

        public class StockTransactionViewActionTemplate : ITemplate, IDisposable
        {
            private bool disposed;
            private HtmlAnchor checkListAnchor;
            private HtmlAnchor printAnchor;
            private HtmlAnchor goToTopAnchor;

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void InstantiateIn(Control container)
            {
                checkListAnchor = new HtmlAnchor();
                checkListAnchor.ID = "ChecklistAnchor";
                checkListAnchor.ClientIDMode = ClientIDMode.Predictable;
                checkListAnchor.Title = Labels.GoToChecklistWindow;
                checkListAnchor.InnerHtml = "<img src='" + PageUtility.ResolveUrl("~/Resource/Icons/checklist-16.png") + "' />";//Todo: embed these icons.

                printAnchor = new HtmlAnchor();
                printAnchor.ID = "PrintAnchor";
                printAnchor.ClientIDMode = ClientIDMode.Predictable;
                printAnchor.Title = Titles.Print;
                printAnchor.InnerHtml = "<img src='" + PageUtility.ResolveUrl("~/Resource/Icons/print-16.png") + "' />";

                goToTopAnchor = new HtmlAnchor();
                goToTopAnchor.Title = Labels.GoToTop;
                goToTopAnchor.Attributes.Add("onclick", "window.scroll(0);");
                goToTopAnchor.InnerHtml = "<img src='" + PageUtility.ResolveUrl("~/Resource/Icons/top-16.png") + "' />";

                container.Controls.Add(checkListAnchor);
                container.Controls.Add(printAnchor);
                container.Controls.Add(goToTopAnchor);
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

        private static void AddDataBoundControl(GridView grid, string dataField, string headerText, string dataFormatString)
        {
            BoundField field = new BoundField();
            field.DataField = dataField;
            field.HeaderText = headerText;
            field.DataFormatString = dataFormatString;

            grid.Columns.Add(field);
        }

        public static void AddColumns(GridView grid, SubTranBook book)
        {
            grid.Columns.Clear();

            AddTemplateFields(grid);

            AddDataBoundControl(grid, "id", Titles.Id, "");
            AddDataBoundControl(grid, "value_date", Titles.ValueDate, "{0:d}");
            AddDataBoundControl(grid, "office", Titles.Office, "");
            AddDataBoundControl(grid, "reference_number", Titles.ReferenceNumber, "");
            AddDataBoundControl(grid, "party", Titles.Party, "");

            if (book != SubTranBook.Receipt)
            {
                AddDataBoundControl(grid, "price_type", Titles.PriceType, "");
            }

            AddDataBoundControl(grid, "amount", Titles.Amount, "");
            AddDataBoundControl(grid, "transaction_ts", Titles.TransactionTimestamp, "{0:d}");
            AddDataBoundControl(grid, "user", Titles.User, "");
            AddDataBoundControl(grid, "statement_reference", Titles.StatementReference, "");

            if (book != SubTranBook.Receipt)
            {
                AddDataBoundControl(grid, "book", Titles.Book, "");
            }

            AddDataBoundControl(grid, "flag_background_color", Titles.FlagBackgroundColor, "");
            AddDataBoundControl(grid, "flag_foreground_color", Titles.FlagForegroundColor, "");
        }
    }
}