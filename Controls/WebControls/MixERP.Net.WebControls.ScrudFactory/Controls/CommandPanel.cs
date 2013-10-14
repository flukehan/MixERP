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
    public class CommandPanel
    {
        public Button EditButton;
        public event EventHandler EditButtonClick;

        public Button DeleteButton;
        public event EventHandler DeleteButtonClick;

        public string ButtonCssClass { get; set; }

        public Panel GetCommandPanel()
        {
            commandPanel = new Panel();
            commandPanel.CssClass = "vpad16";
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
            HtmlInputButton showCompactButton = new HtmlInputButton();
            showCompactButton.Attributes.Add("class", this.ButtonCssClass);
            showCompactButton.Attributes.Add("title", "ALT + C");
            showCompactButton.Attributes.Add("onclick", "showCompact();");
            showCompactButton.Value = Resources.ScrudResource.ShowCompact;
            p.Controls.Add(showCompactButton);
        }

        private void AddShowAllButton(Panel p)
        {
            HtmlInputButton showAllButton = new HtmlInputButton();
            showAllButton.Attributes.Add("class", this.ButtonCssClass);
            showAllButton.Attributes.Add("title", "Ctrl + S");
            showAllButton.Attributes.Add("onclick", "showAll();");
            showAllButton.Value = Resources.ScrudResource.ShowAll;
            p.Controls.Add(showAllButton);
        }

        private void AddAddButton(Panel p)
        {
            HtmlInputButton addButton = new HtmlInputButton();
            addButton.Attributes.Add("class", this.ButtonCssClass);
            addButton.Attributes.Add("title", "Alt + A");
            addButton.Attributes.Add("onclick", "return(addNew());");
            addButton.Value = Resources.ScrudResource.AddNew;
            p.Controls.Add(addButton);
        }

        private void AddEditButton(Panel p)
        {
            EditButton = new Button();
            EditButton.Attributes.Add("class", this.ButtonCssClass);
            EditButton.Attributes.Add("title", "Ctrl + E");
            EditButton.Attributes.Add("onclick", "return(confirmAction());");
            EditButton.Text = Resources.ScrudResource.EditSelected;
            EditButton.Click += new EventHandler(OnEditButtonClick);
            p.Controls.Add(EditButton);
        }

        private void OnEditButtonClick(object sender, EventArgs e)
        {
            if(EditButtonClick != null)
            {
                EditButtonClick(this, e);
            }
        }

        private void AddDeleteButton(Panel p)
        {
            DeleteButton = new Button();
            DeleteButton.Attributes.Add("class", this.ButtonCssClass);
            DeleteButton.Attributes.Add("title", "Ctrl + D");
            DeleteButton.Attributes.Add("onclick", "return(confirmAction());");
            DeleteButton.Text = Resources.ScrudResource.DeleteSelected;
            DeleteButton.Click += new EventHandler(OnDeleteButtonClick);
            p.Controls.Add(DeleteButton);
        }

        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            if(DeleteButtonClick != null)
            {
                DeleteButtonClick(this, e);
            }
        }

        private void AddPrintButton(Panel p)
        {
            HtmlInputButton printButton = new HtmlInputButton();
            printButton.Attributes.Add("class", this.ButtonCssClass);
            printButton.Attributes.Add("title", "Ctrl + P");
            printButton.Attributes.Add("onclick", "printThis();");
            printButton.Value = Resources.ScrudResource.Print;
            p.Controls.Add(printButton);
        }

    }
}
