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

using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Controls;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private CommandPanel bottomCommandPanel;
        private Label messageLabel;
        private HiddenField officeCodeHidden;
        private CommandPanel topCommandPanel;
        private UpdatePanel updatePanel;
        private HiddenField userIdHidden;

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
                scriptManager.RegisterAsyncPostBackControl(this.saveButton);
                scriptManager.RegisterAsyncPostBackControl(this.saveButton);
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

            this.updatePanel.ContentTemplateContainer.Controls.Add(this.topCommandPanel.GetCommandPanel("top"));
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.messageLabel);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.gridPanel);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.formPanel);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.bottomCommandPanel.GetCommandPanel("bottom"));

            //Bottom command panel.
            this.userIdHidden = new HiddenField();
            this.userIdHidden.ID = "UserIdHidden";
            this.userIdHidden.Value = this.UserId.ToString(CultureInfo.InvariantCulture);

            this.officeCodeHidden = new HiddenField();
            this.officeCodeHidden.ID = "OfficeCodeHidden";
            this.officeCodeHidden.Value = this.OfficeCode;

            this.updatePanel.ContentTemplateContainer.Controls.Add(this.userIdHidden);
            this.updatePanel.ContentTemplateContainer.Controls.Add(this.officeCodeHidden);
            p.Controls.Add(this.updatePanel);
        }

        private void CreateCommandPanels()
        {
            this.bottomCommandPanel = new CommandPanel();
            this.bottomCommandPanel.DeleteButtonClick += this.DeleteButton_Click;
            this.bottomCommandPanel.EditButtonClick += this.EditButton_Click;
            this.bottomCommandPanel.ButtonCssClass = this.GetCommandPanelButtonCssClass();
            this.bottomCommandPanel.CssClass = this.GetCommandPanelCssClass();

            this.bottomCommandPanel.SelectButtonIconCssClass = this.GetSelectButtonIconCssClass();
            this.bottomCommandPanel.CompactButtonIconCssClass = this.GetCompactButtonIconCssClass();
            this.bottomCommandPanel.AllButtonIconCssClass = this.GetAllButtonIconCssClass();
            this.bottomCommandPanel.AddButtonIconCssClass = this.GetAddButtonIconCssClass();
            this.bottomCommandPanel.EditButtonIconCssClass = this.GetEditButtonIconCssClass();
            this.bottomCommandPanel.DeleteButtonIconCssClass = this.GetDeleteButtonIconCssClass();
            this.bottomCommandPanel.PrintButtonIconCssClass = this.GetPrintButtonIconCssClass();

            this.topCommandPanel = new CommandPanel();
            this.topCommandPanel.DeleteButtonClick += this.DeleteButton_Click;
            this.topCommandPanel.EditButtonClick += this.EditButton_Click;
            this.topCommandPanel.ButtonCssClass = this.GetCommandPanelButtonCssClass();
            this.topCommandPanel.CssClass = this.GetCommandPanelCssClass();

            this.topCommandPanel.SelectButtonIconCssClass = this.GetSelectButtonIconCssClass();
            this.topCommandPanel.CompactButtonIconCssClass = this.GetCompactButtonIconCssClass();
            this.topCommandPanel.AllButtonIconCssClass = this.GetAllButtonIconCssClass();
            this.topCommandPanel.AddButtonIconCssClass = this.GetAddButtonIconCssClass();
            this.topCommandPanel.EditButtonIconCssClass = this.GetEditButtonIconCssClass();
            this.topCommandPanel.DeleteButtonIconCssClass = this.GetDeleteButtonIconCssClass();
            this.topCommandPanel.PrintButtonIconCssClass = this.GetPrintButtonIconCssClass();
        }

        private string GetItemSelectorPath()
        {
            string itemSelectorPath = this.ItemSelectorPath;

            if (string.IsNullOrWhiteSpace(itemSelectorPath))
            {
                itemSelectorPath = DbConfig.GetScrudParameter(this.Catalog, "ItemSelectorPath");
            }

            return itemSelectorPath;
        }

        private string GetResourceClassName()
        {
            string resourceClassName = this.ResourceClassName;

            if (string.IsNullOrWhiteSpace(resourceClassName))
            {
                resourceClassName = DbConfig.GetScrudParameter(this.Catalog, "ResourceClassName");
            }

            return resourceClassName;
        }
    }
}