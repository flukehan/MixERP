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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.TransactionViewFactory;
using System;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.Core.Modules.Finance
{
    public partial class VoucherVerification : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (TransactionView view = new TransactionView())
            {
                view.DisplayFlagButton = true;
                view.DisplayApproveButton = true;
                view.DisplayRejectButton = true;
                view.DisplayPrintButton = true;

                view.GridViewCssClass = "ui table nowrap";
                view.Text = Titles.VoucherVerification;

                //Default Values
                view.DateFromFromFrequencyType = FrequencyType.Today;
                view.DateToFrequencyType = FrequencyType.Today;
                view.Status = "Unverified";

                view.OfficeName = AppUsers.GetCurrent().View.OfficeName;

                view.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
                view.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
                view.Catalog = AppUsers.GetCurrentUserDB();

                this.Controls.Add(view);
            }

            this.AddModal();
            
        }

        #region Modal
        private void AddActions(HtmlGenericControl container)
        {
            using (HtmlGenericControl actions = new HtmlGenericControl("div"))
            {
                actions.Attributes.Add("class", "actions");

                using (HtmlGenericControl buttons = new HtmlGenericControl("div"))
                {
                    buttons.Attributes.Add("class", "ui buttons");

                    using (HtmlInputButton cancelButton = new HtmlInputButton())
                    {
                        cancelButton.Attributes.Add("class", "ui red button");
                        cancelButton.Value = Titles.Cancel;

                        buttons.Controls.Add(cancelButton);
                    }

                    using (HtmlInputButton verifyButton = new HtmlInputButton())
                    {
                        verifyButton.ID = "VerifyButton";
                        verifyButton.Attributes.Add("class", "ui green button");
                        verifyButton.Value = Titles.Verify;
                        verifyButton.Attributes.Add("title", "CTRL + RETURN");
                        buttons.Controls.Add(verifyButton);
                    }

                    actions.Controls.Add(buttons);
                }

                container.Controls.Add(actions);
            }
        }

        private void AddContent(HtmlGenericControl container)
        {
            using (HtmlGenericControl content = new HtmlGenericControl("div"))
            {
                content.Attributes.Add("class", "ui inverted red content");

                using (HtmlGenericControl form = HtmlControlHelper.GetForm())
                {
                    using (HtmlGenericControl header = new HtmlGenericControl("div"))
                    {
                        header.Attributes.Add("class", "ui blue large dividing header");
                        form.Controls.Add(header);
                    }

                    using (HtmlGenericControl field = HtmlControlHelper.GetField())
                    {
                        using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.VerificationReason, "ReasonTextArea"))
                        {
                            field.Controls.Add(label);
                        }

                        using (HtmlTextArea textArea = new HtmlTextArea())
                        {
                            textArea.ID = "ReasonTextArea";
                            field.Controls.Add(textArea);
                        }
                        form.Controls.Add(field);
                    }

                    content.Controls.Add(form);
                }

                container.Controls.Add(content);
            }
        }

        private void AddHeader(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("div"))
            {
                header.Attributes.Add("class", "ui massive red header");
                container.Controls.Add(header);
            }
        }

        private void AddModal()
        {
            using (HtmlGenericControl modal = HtmlControlHelper.GetModal())
            {
                modal.ID = "ActionModal";
                this.AddHeader(modal);
                this.AddContent(modal);
                this.AddActions(modal);

                this.Controls.Add(modal);
            }
        }
        #endregion
    }
}