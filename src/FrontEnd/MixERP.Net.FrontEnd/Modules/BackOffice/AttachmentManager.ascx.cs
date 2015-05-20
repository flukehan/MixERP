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

using System;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.AttachmentFactory;

namespace MixERP.Net.Core.Modules.BackOffice
{
    public partial class AttachmentManager : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            if (!this.IsRestrictedMode)
            {
                this.TitleLiteral.Text = Titles.UploadAttachments;

                using (Attachment attachment = new Attachment())
                {
                    attachment.ShowSaveButton = true;
                    this.Placeholder1.Controls.Add(attachment);
                }
            }

            this.RegisterJavascript();
            this.SubTitleLiteral.Text = Titles.ViewAttachments;
            this.OKLiteral.Text = Titles.OK;
            this.SetOverridePath();
        }

        private void RegisterJavascript()
        {
            string script = JSUtility.GetVar("viewLocalized", Titles.View);
            script += JSUtility.GetVar("downloadLocalized", Titles.Download);
            script += JSUtility.GetVar("deleteLocalized", Titles.Delete);

            PageUtility.RegisterJavascript("AttachmentManager_Localized", script, this.Page, true);
        }

        private void SetOverridePath()
        {
            string overridePath = this.Page.Request.QueryString["OverridePath"];

            if (!string.IsNullOrWhiteSpace(overridePath))
            {
                this.OverridePath = overridePath;
            }
        }
    }
}