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
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Reflection;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        UpdatePanel updatePanel;
        HiddenField userIdHidden;
        HiddenField officeCodeHidden;
        Label messageLabel = new Label();


        protected override void OnPreRender(EventArgs e)
        {
            this.AddScriptManager();
            base.OnPreRender(e);
        }

        private void AddScriptManager()
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterAsyncPostBackControl(saveButton);
                scriptManager.RegisterAsyncPostBackControl(cancelButton);
                scriptManager.RegisterAsyncPostBackControl(topCommandPanel.EditButton);
                scriptManager.RegisterAsyncPostBackControl(bottomCommandPanel.EditButton);
                scriptManager.RegisterAsyncPostBackControl(topCommandPanel.DeleteButton);
                scriptManager.RegisterAsyncPostBackControl(bottomCommandPanel.DeleteButton);
            }
        }

        private void AddUpdatePanel(Panel p)
        {
            updatePanel = new UpdatePanel();
            updatePanel.ID = "ScrudUpdatePanel";
            updatePanel.ChildrenAsTriggers = true;
            updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;

            updatePanel.ContentTemplateContainer.Controls.Add(this.topCommandPanel.GetCommandPanel());
            updatePanel.ContentTemplateContainer.Controls.Add(this.messageLabel);
            updatePanel.ContentTemplateContainer.Controls.Add(this.gridPanel);
            updatePanel.ContentTemplateContainer.Controls.Add(this.formPanel);
            updatePanel.ContentTemplateContainer.Controls.Add(this.bottomCommandPanel.GetCommandPanel()); //Bottom command panel.

            userIdHidden = new HiddenField();
            officeCodeHidden = new HiddenField();

            updatePanel.ContentTemplateContainer.Controls.Add(userIdHidden);
            updatePanel.ContentTemplateContainer.Controls.Add(officeCodeHidden);
            p.Controls.Add(updatePanel);
        }

        MixERP.Net.WebControls.ScrudFactory.Controls.CommandPanel bottomCommandPanel;
        MixERP.Net.WebControls.ScrudFactory.Controls.CommandPanel topCommandPanel;

        private string GetCommandPanelButtonCssClass()
        {
            string cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter( "CommandPanelButtonCssClass");
            }

            return cssClass;
        }

        private string GetButtonCssClass()
        {
            string cssClass = this.ButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter( "ButtonCssClass");
            }

            return cssClass;
        }

        private void CreateCommandPanels()
        {
            bottomCommandPanel = new Controls.CommandPanel();
            bottomCommandPanel.DeleteButtonClick += DeleteButton_Click;
            bottomCommandPanel.EditButtonClick += EditButton_Click;
            bottomCommandPanel.ButtonCssClass = this.GetCommandPanelButtonCssClass();

            topCommandPanel = new Controls.CommandPanel();
            topCommandPanel.DeleteButtonClick += DeleteButton_Click;
            topCommandPanel.EditButtonClick += EditButton_Click;
            topCommandPanel.ButtonCssClass = this.GetCommandPanelButtonCssClass();

        }
    }
}
