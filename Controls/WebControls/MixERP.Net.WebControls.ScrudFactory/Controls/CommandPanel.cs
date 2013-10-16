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
            EditButton = this.GetButton("CTRL + E", "return(confirmAction());", Resources.ScrudResource.EditSelected);
            EditButton.Click += new EventHandler(OnEditButtonClick);
            p.Controls.Add(EditButton);
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
            DeleteButton = this.GetButton("CTRL + D", "return(confirmAction());", Resources.ScrudResource.DeleteSelected);
            DeleteButton.Click += new EventHandler(OnDeleteButtonClick);
            p.Controls.Add(DeleteButton);
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
            HtmlInputButton inputButton = new HtmlInputButton();
            inputButton.Attributes.Add("class", this.ButtonCssClass);
            inputButton.Attributes.Add("title", title);
            inputButton.Attributes.Add("onclick", onclick);
            inputButton.Value = value;

            return inputButton;
        }

        private Button GetButton(string toolTip, string onClientClick, string text)
        {
            Button button = new Button();
            button.CssClass = this.ButtonCssClass;
            button.ToolTip = toolTip;
            button.OnClientClick = onClientClick;
            button.Text = text;

            return button;
        }


    }
}
