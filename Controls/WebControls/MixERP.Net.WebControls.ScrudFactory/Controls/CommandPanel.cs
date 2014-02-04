/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(commandPanel);

                    if(editButton !=null)
                    { 
                        editButton.Click -= new EventHandler(OnEditButtonClick);
                    }

                    if (EditButtonClick != null)
                    {
                        EditButtonClick = null;
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(editButton);

                    if (deleteButton != null)
                    {
                        deleteButton.Click -= new EventHandler(OnDeleteButtonClick);
                    }

                    if (DeleteButtonClick != null)
                    {
                        DeleteButtonClick = null;
                    }
                    
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(deleteButton);
                }

                disposed = true;
            }
        }
        public Panel GetCommandPanel()
        {
            commandPanel = new Panel();
            commandPanel.CssClass = "vpad16"; //Parameterize in configuration file.

            this.AddShowCompactButton(commandPanel);
            this.AddShowAllButton(commandPanel);
            this.AddAddButton(commandPanel);
            this.AddEditButton(commandPanel);
            this.AddDeleteButton(commandPanel);
            this.AddPrintButton(commandPanel);
            return commandPanel;
        }

        private Panel commandPanel;

        private void AddShowCompactButton(Panel p)
        {
            HtmlInputButton showCompactButton = this.GetInputButton("ALT + C", "showCompact();", Resources.ScrudResource.ShowCompact);
            p.Controls.Add(showCompactButton);
        }

        private void AddShowAllButton(Panel p)
        {
            HtmlInputButton showAllButton = this.GetInputButton("CTRL + S", "showAll();", Resources.ScrudResource.ShowAll);
            p.Controls.Add(showAllButton);
        }

        private void AddAddButton(Panel p)
        {
            HtmlInputButton addButton = this.GetInputButton("ALT + A", "return(addNew());", Resources.ScrudResource.AddNew);
            p.Controls.Add(addButton);
        }

        private void AddEditButton(Panel p)
        {
            editButton = this.GetButton("CTRL + E", "return(confirmAction('Edit'));", Resources.ScrudResource.EditSelected);
            editButton.Click += new EventHandler(OnEditButtonClick);
            p.Controls.Add(editButton);
        }

        private void OnEditButtonClick(object sender, EventArgs e)
        {
            if (EditButtonClick != null)
            {
                EditButtonClick(this, e);
            }
        }

        private void AddDeleteButton(Panel p)
        {
            deleteButton = this.GetButton("CTRL + D", "return(confirmAction('Delete'));", Resources.ScrudResource.DeleteSelected);
            deleteButton.Click += new EventHandler(OnDeleteButtonClick);
            p.Controls.Add(deleteButton);
        }

        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            if (DeleteButtonClick != null)
            {
                DeleteButtonClick(this, e);
            }
        }

        private void AddPrintButton(Panel p)
        {
            HtmlInputButton printButton = this.GetInputButton("CTRL + P", "printThis();", Resources.ScrudResource.Print);
            p.Controls.Add(printButton);
        }

        private HtmlInputButton GetInputButton(string title, string onclick, string value)
        {
            using (HtmlInputButton inputButton = new HtmlInputButton())
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
            using (Button button = new Button())
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
