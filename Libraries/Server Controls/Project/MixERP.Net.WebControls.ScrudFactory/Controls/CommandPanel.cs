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

using MixERP.Net.WebControls.ScrudFactory.Resources;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls
{
    public class CommandPanel : IDisposable
    {
        private Panel commandPanel;
        private bool disposed;

        public event EventHandler DeleteButtonClick;

        public event EventHandler EditButtonClick;

        public string AddButtonCssClass { get; set; }

        public string AllButtonCssClass { get; set; }

        public string ButtonCssClass { get; set; }

        public string CompactButtonCssClass { get; set; }

        public string CssClass { get; set; }

        public Button DeleteButton { get; private set; }

        public string DeleteButtonCssClass { get; set; }

        public Button EditButton { get; private set; }

        public string EditButtonCssClass { get; set; }

        public string PrintButtonCssClass { get; set; }

        public string SelectButtonCssClass { get; set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Panel GetCommandPanel(string controlSuffix)
        {
            this.commandPanel = new Panel();
            this.commandPanel.Attributes.Add("role", "toolbar");
            commandPanel.CssClass = this.CssClass;
            this.AddSelectButton(this.commandPanel);
            this.AddShowCompactButton(this.commandPanel);
            this.AddShowAllButton(this.commandPanel);
            this.AddAddButton(this.commandPanel);
            this.AddEditButtonHidden(this.commandPanel, controlSuffix);
            this.AddEditButtonVisible(this.commandPanel, controlSuffix);
            this.AddDeleteButtonHidden(this.commandPanel, controlSuffix);
            this.AddDeleteButtonVisible(this.commandPanel, controlSuffix);
            this.AddPrintButton(this.commandPanel);
            return this.commandPanel;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.commandPanel != null)
                    {
                        this.commandPanel.Dispose();
                        this.commandPanel = null;
                    }

                    if (this.EditButton != null)
                    {
                        this.EditButton.Click -= this.OnEditButtonClick;
                        this.EditButtonClick = null;
                        this.EditButton.Dispose();
                        this.EditButton = null;
                    }

                    if (this.DeleteButton != null)
                    {
                        this.DeleteButton.Click -= this.OnDeleteButtonClick;
                        this.DeleteButtonClick = null;
                        this.DeleteButton.Dispose();
                        this.DeleteButton = null;
                    }
                }

                this.disposed = true;
            }
        }

        private void AddAddButton(Panel p)
        {
            var addButton = this.GetInputButton("ALT + A", "return(scrudAddNew());", ScrudResource.AddNew, this.AddButtonCssClass);
            p.Controls.Add(addButton);
        }

        private void AddDeleteButtonHidden(Panel p, string controlSuffix)
        {
            this.DeleteButton = this.GetButton("CTRL + D", "return(scrudConfirmAction());", ScrudResource.DeleteSelected);
            this.DeleteButton.ID = "DeleteButton" + controlSuffix;
            this.DeleteButton.CssClass = "hidden";
            this.DeleteButton.Click += this.OnDeleteButtonClick;
            p.Controls.Add(this.DeleteButton);
        }

        private void AddDeleteButtonVisible(Panel p, string controlSuffix)
        {
            var deleteButton = this.GetInputButton("CTRL + E", "$('#DeleteButton" + controlSuffix + "').click();return false;", ScrudResource.DeleteSelected, this.DeleteButtonCssClass);
            p.Controls.Add(deleteButton);
        }

        private void AddEditButtonHidden(Panel p, string controlSuffix)
        {
            this.EditButton = this.GetButton("CTRL + E", "return(scrudConfirmAction());", ScrudResource.EditSelected);
            this.EditButton.Attributes.Add("role", "edit");
            this.EditButton.ID = "EditButton" + controlSuffix;
            this.EditButton.CssClass = "hidden";
            this.EditButton.Click += this.OnEditButtonClick;
            p.Controls.Add(this.EditButton);
        }

        private void AddEditButtonVisible(Panel p, string controlSuffix)
        {
            var editButton = this.GetInputButton("CTRL + E", "$('#EditButton" + controlSuffix + "').click();return false;", ScrudResource.EditSelected, this.EditButtonCssClass);
            p.Controls.Add(editButton);
        }

        private void AddPrintButton(Panel p)
        {
            var printButton = this.GetInputButton("CTRL + P", "scrudPrintGridView();", ScrudResource.Print, this.PrintButtonCssClass);
            p.Controls.Add(printButton);
        }

        private void AddSelectButton(Panel p)
        {
            if (this.IsModal())
            {
                var addSelectButton = this.GetInputButton("RETURN", "scrudSelectAndClose();", ScrudResource.Select, this.SelectButtonCssClass);
                p.Controls.Add(addSelectButton);
            }
        }

        private void AddShowAllButton(Panel p)
        {
            var showAllButton = this.GetInputButton("CTRL + S", "scrudShowAll();", ScrudResource.ShowAll, this.AllButtonCssClass);
            p.Controls.Add(showAllButton);
        }

        private void AddShowCompactButton(Panel p)
        {
            var showCompactButton = this.GetInputButton("ALT + C", "scrudShowCompact();", ScrudResource.ShowCompact, this.CompactButtonCssClass);
            p.Controls.Add(showCompactButton);
        }

        private Button GetButton(string toolTip, string onClientClick, string text)
        {
            using (var button = new Button())
            {
                button.CssClass = this.ButtonCssClass;
                button.ToolTip = toolTip;
                button.OnClientClick = onClientClick;
                button.Text = text;
                return button;
            }
        }

        private HtmlButton GetInputButton(string title, string onclick, string value, string cssClass)
        {
            using (var inputButton = new HtmlButton())
            {
                inputButton.Attributes.Add("class", this.ButtonCssClass);
                inputButton.Attributes.Add("type", "button");
                inputButton.Attributes.Add("title", title);
                inputButton.Attributes.Add("onclick", onclick);
                inputButton.InnerHtml = @"<span class='" + cssClass + "'></span> " + value;

                return inputButton;
            }
        }

        private bool IsModal()
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            if (page != null)
            {
                var modal = page.Request.QueryString["modal"];
                if (modal != null)
                {
                    if (modal.Equals("1"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            if (this.DeleteButtonClick != null)
            {
                this.DeleteButtonClick(this, e);
            }
        }

        private void OnEditButtonClick(object sender, EventArgs e)
        {
            if (this.EditButtonClick != null)
            {
                this.EditButtonClick(this, e);
            }
        }
    }
}