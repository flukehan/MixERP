/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Controls
{
    public class CommandPanel : IDisposable
    {
        private bool disposed;
        
        private Button editButton;        
        public Button EditButton 
        {
            get
            {
                return this.editButton;
            }
        }

        public event EventHandler EditButtonClick;

        private Button deleteButton;
        public Button DeleteButton
        {
            get
            {
                return this.deleteButton;
            }
        }

        public event EventHandler DeleteButtonClick;

        public string ButtonCssClass { get; set; }

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
                    if (this.commandPanel != null)
                    {
                        this.commandPanel.Dispose();
                        this.commandPanel = null;
                    }

                    if(this.editButton !=null)
                    {
                        this.editButton.Click -= new EventHandler(this.OnEditButtonClick);
                        
                        if (this.EditButtonClick != null)
                        {
                            this.EditButtonClick = null;
                        }

                        this.editButton.Dispose();
                        this.editButton = null;
                    }


                    if (this.deleteButton != null)
                    {
                        this.deleteButton.Click -= new EventHandler(this.OnDeleteButtonClick);
                        
                        if (this.DeleteButtonClick != null)
                        {
                            this.DeleteButtonClick = null;
                        }

                        this.deleteButton.Dispose();
                        this.deleteButton = null;
                    }                    
                }

                this.disposed = true;
            }
        }
        public Panel GetCommandPanel()
        {
            this.commandPanel = new Panel {CssClass = "vpad16"};

            this.AddShowCompactButton(this.commandPanel);
            this.AddShowAllButton(this.commandPanel);
            this.AddAddButton(this.commandPanel);
            this.AddEditButton(this.commandPanel);
            this.AddDeleteButton(this.commandPanel);
            this.AddPrintButton(this.commandPanel);
            return this.commandPanel;
        }

        private Panel commandPanel;

        private void AddShowCompactButton(Panel p)
        {
            var showCompactButton = this.GetInputButton("ALT + C", "showCompact();", ScrudResource.ShowCompact);
            p.Controls.Add(showCompactButton);
        }

        private void AddShowAllButton(Panel p)
        {
            var showAllButton = this.GetInputButton("CTRL + S", "showAll();", ScrudResource.ShowAll);
            p.Controls.Add(showAllButton);
        }

        private void AddAddButton(Panel p)
        {
            var addButton = this.GetInputButton("ALT + A", "return(addNew());", ScrudResource.AddNew);
            p.Controls.Add(addButton);
        }

        private void AddEditButton(Panel p)
        {
            this.editButton = this.GetButton("CTRL + E", "return(confirmAction());", ScrudResource.EditSelected);
            this.editButton.Click += new EventHandler(this.OnEditButtonClick);
            p.Controls.Add(this.editButton);
        }

        private void OnEditButtonClick(object sender, EventArgs e)
        {
            if (this.EditButtonClick != null)
            {
                this.EditButtonClick(this, e);
            }
        }

        private void AddDeleteButton(Panel p)
        {
            this.deleteButton = this.GetButton("CTRL + D", "return(confirmAction());", ScrudResource.DeleteSelected);
            this.deleteButton.Click += new EventHandler(this.OnDeleteButtonClick);
            p.Controls.Add(this.deleteButton);
        }

        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            if (this.DeleteButtonClick != null)
            {
                this.DeleteButtonClick(this, e);
            }
        }

        private void AddPrintButton(Panel p)
        {
            var printButton = this.GetInputButton("CTRL + P", "printThis();", ScrudResource.Print);
            p.Controls.Add(printButton);
        }

        private HtmlInputButton GetInputButton(string title, string onclick, string value)
        {
            using (var inputButton = new HtmlInputButton())
            {
                inputButton.Attributes.Add("class", this.ButtonCssClass);
                inputButton.Attributes.Add("title", title);
                inputButton.Attributes.Add("onclick", onclick);
                inputButton.Value = value;

                return inputButton;
            }
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
    }
}
