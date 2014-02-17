/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Controls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        UpdatePanel updatePanel;
        HiddenField userIdHidden;
        HiddenField officeCodeHidden;
        Label messageLabel;
        CommandPanel bottomCommandPanel;
        CommandPanel topCommandPanel;


        protected override void OnPreRender(EventArgs e)
        {
            this.AddScriptManager();
            base.OnPreRender(e);
        }

        private void AddScriptManager()
        {
            var scriptManager = ScriptManager.GetCurrent(this.Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterAsyncPostBackControl(this.saveButton);
                scriptManager.RegisterAsyncPostBackControl(this.cancelButton);
                scriptManager.RegisterAsyncPostBackControl(this.topCommandPanel.EditButton);
                scriptManager.RegisterAsyncPostBackControl(this.bottomCommandPanel.EditButton);
                scriptManager.RegisterAsyncPostBackControl(this.topCommandPanel.DeleteButton);
                scriptManager.RegisterAsyncPostBackControl(this.bottomCommandPanel.DeleteButton);
            }
        }

        private void AddUpdatePanel(Panel p)
        {
            this.messageLabel = new Label();

            this.updatePanel = new UpdatePanel();
            this.updatePanel.ID = "ScrudUpdatePanel";
            this.updatePanel.ChildrenAsTriggers = true;
            this.updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;

            this.updatePanel.ContentTemplateContainer.Controls.Add(this.topCommandPanel.GetCommandPanel());
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.messageLabel);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.gridPanel);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.formPanel);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.bottomCommandPanel.GetCommandPanel()); //Bottom command panel.

            this.userIdHidden = new HiddenField();
            this.officeCodeHidden = new HiddenField();

            this.updatePanel.ContentTemplateContainer.Controls.Add(this.userIdHidden);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.officeCodeHidden);
            p.Controls.Add(this.updatePanel);
        }


        private string GetCommandPanelButtonCssClass()
        {
            var cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CommandPanelButtonCssClass");
            }

            return cssClass;
        }

        private string GetGridViewCssClass()
        {
            var cssClass = this.GridViewCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewCssClass");
            }

            return cssClass;
        }

        private string GetResourceClassName()
        {
            var resourceClassName = this.ResourceClassName;

            if (string.IsNullOrWhiteSpace(resourceClassName))
            {
                resourceClassName = ConfigurationHelper.GetScrudParameter("ResourceClassName");
            }

            return resourceClassName;
        }

        private string GetItemSelectorPath()
        {
            var itemSelectorPath = this.ItemSelectorPath;

            if (string.IsNullOrWhiteSpace(itemSelectorPath))
            {
                itemSelectorPath = ConfigurationHelper.GetScrudParameter("ItemSelectorPath");
            }

            return itemSelectorPath;
        }

        private string GetButtonCssClass()
        {
            var cssClass = this.ButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("ButtonCssClass");
            }

            return cssClass;
        }

        private void CreateCommandPanels()
        {
            this.bottomCommandPanel = new CommandPanel();
            this.bottomCommandPanel.DeleteButtonClick += this.DeleteButton_Click;
            this.bottomCommandPanel.EditButtonClick += this.EditButton_Click;
            this.bottomCommandPanel.ButtonCssClass = this.GetCommandPanelButtonCssClass();

            this.topCommandPanel = new CommandPanel();
            this.topCommandPanel.DeleteButtonClick += this.DeleteButton_Click;
            this.topCommandPanel.EditButtonClick += this.EditButton_Click;
            this.topCommandPanel.ButtonCssClass = this.GetCommandPanelButtonCssClass();

        }
    }
}
