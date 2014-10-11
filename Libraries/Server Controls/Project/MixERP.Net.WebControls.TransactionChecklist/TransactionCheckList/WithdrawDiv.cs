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

using MixERP.Net.WebControls.TransactionChecklist.Resources;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        private HtmlButton okButton;
        private TextBox reasonTextBox;

        private void AddWidthdrawDiv(HtmlGenericControl p)
        {
            if (this.DisplayWithdrawButton)
            {
                using (HtmlGenericControl withdrawDiv = new HtmlGenericControl())
                {
                    withdrawDiv.TagName = "div";
                    withdrawDiv.ID = "WithdrawDiv";
                    withdrawDiv.Attributes.Add("class", "panel panel-default panel-warning");
                    withdrawDiv.Attributes.Add("style", "max-width:" + this.GetMaxWidth() + "px;");

                    this.AddWithdrawHeader(withdrawDiv);
                    this.AddWithdrawPanelBody(withdrawDiv);
                    p.Controls.Add(withdrawDiv);
                }
            }
        }

        private void AddWithdrawHeader(HtmlGenericControl div)
        {
            using (HtmlGenericControl header = new HtmlGenericControl())
            {
                header.TagName = "div";
                header.Attributes.Add("class", "panel-heading");
                this.AddWithdrawHeading(header);
                div.Controls.Add(header);
            }
        }

        private void AddWithdrawHeading(HtmlGenericControl div)
        {
            using (HtmlGenericControl heading = new HtmlGenericControl())
            {
                heading.TagName = "h3";
                heading.InnerText = Titles.WithdrawTransaction;
                heading.Attributes.Add("class", "panel-title");
                div.Controls.Add(heading);
            }
        }

        private void AddWithdrawPanelBody(HtmlGenericControl div)
        {
            using (HtmlGenericControl body = new HtmlGenericControl())
            {
                body.TagName = "div";
                body.Attributes.Add("class", "panel-body");

                using (HtmlGenericControl paragraph = new HtmlGenericControl())
                {
                    paragraph.TagName = "p";
                    paragraph.InnerText = Questions.WithdrawalReason;
                    body.Controls.Add(paragraph);
                }

                using (HtmlGenericControl paragraph = new HtmlGenericControl())
                {
                    paragraph.TagName = "p";
                    this.AddReasonTextBox(paragraph);
                    body.Controls.Add(paragraph);
                }

                using (HtmlGenericControl paragraph = new HtmlGenericControl())
                {
                    paragraph.TagName = "p";
                    this.AddOkButton(paragraph);
                    this.AddSpace(paragraph);
                    this.AddCancelButton(paragraph);
                    body.Controls.Add(paragraph);
                }

                div.Controls.Add(body);
            }
        }

        private void AddSpace(HtmlGenericControl p)
        {
            using (HtmlGenericControl space = new HtmlGenericControl())
            {
                space.InnerHtml = "&nbsp;";

                p.Controls.Add(space);
            }
        }

        private void AddReasonTextBox(HtmlGenericControl p)
        {
            reasonTextBox = new TextBox();
            reasonTextBox.ID = "ReasonTextBox";
            reasonTextBox.ClientIDMode = ClientIDMode.Static;
            reasonTextBox.TextMode = TextBoxMode.MultiLine;
            reasonTextBox.CssClass = "form-control input-sm";
            reasonTextBox.Rows = 5;

            p.Controls.Add(reasonTextBox);

            using (RequiredFieldValidator reasonTextBoxRequired = new RequiredFieldValidator())
            {
                reasonTextBoxRequired.ID = "ReasonTextBoxRequired";
                reasonTextBoxRequired.ControlToValidate = reasonTextBox.ID;
                reasonTextBoxRequired.EnableClientScript = true;
                reasonTextBoxRequired.ErrorMessage = Labels.ThisFieldIsRequired;
                reasonTextBoxRequired.CssClass = "error-message";
                reasonTextBoxRequired.Display = ValidatorDisplay.Dynamic;
                p.Controls.Add(reasonTextBoxRequired);
            }
        }

        private void AddOkButton(HtmlGenericControl p)
        {
            okButton = new HtmlButton();
            okButton.CausesValidation = true;
            okButton.InnerText = Titles.OK;
            okButton.Attributes.Add("class", "btn btn-sm btn-warning");
            okButton.ServerClick += OkButton_Click;

            p.Controls.Add(okButton);
        }

        private void AddCancelButton(HtmlGenericControl p)
        {
            using (HtmlInputButton button = new HtmlInputButton())
            {
                button.ID = "CancelButton";
                button.CausesValidation = false;
                button.Value = Titles.Cancel;
                button.Attributes.Add("class", "btn btn-default btn-sm");

                p.Controls.Add(button);
            }
        }
    }
}