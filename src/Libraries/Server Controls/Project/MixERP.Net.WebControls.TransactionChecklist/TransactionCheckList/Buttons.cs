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

using MixERP.Net.Common;
using MixERP.Net.i18n.Resources;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        private LinkButton emailLinkButton;

        private void AddAttachmentAnchor(HtmlGenericControl h)
        {
            if (this.DisplayAttachmentButton)
            {
                if (this.GetTranId().Equals("0"))
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.AttachmentBookName))
                {
                    return;
                }

                string overridePath = this.OverridePath;

                if (string.IsNullOrWhiteSpace(overridePath))
                {
                    overridePath = PageUtility.GetCurrentPageUrl(this.Page);
                }

                string attachmentUrl = string.Format(CultureInfo.InvariantCulture, "~/Modules/BackOffice/AttachmentManager.mix?OverridePath={0}&Book={1}&Id={2}", overridePath, this.AttachmentBookName, this.GetTranId());

                using (HtmlAnchor anchor = new HtmlAnchor())
                {
                    anchor.ID = "AttachmentAnchor";
                    anchor.InnerHtml = "<i class='icon cloud upload'></i>" + Titles.UploadAttachmentsForThisTransaction;
                    anchor.Attributes.Add("class", "item");
                    anchor.HRef = attachmentUrl;

                    h.Controls.Add(anchor);
                }
            }
        }

        private void AddBackAnchor(HtmlGenericControl h)
        {
            using (HtmlAnchor anchor = new HtmlAnchor())
            {
                anchor.InnerHtml = "<i class='icon backward'></i>" + Titles.Back;
                anchor.Attributes.Add("class", "item");
                anchor.HRef = "javascript:history.go(-1);";
                h.Controls.Add(anchor);
            }
        }

        private void AddButtons(HtmlGenericControl p)
        {
            using (HtmlGenericControl div = new HtmlGenericControl())
            {
                div.TagName = "div";
                div.Attributes.Add("class", "ui vertical menu");
                div.Attributes.Add("style", "width:" + this.GetWidth() + "px;");
                this.AddPanelHeader(div);
                this.AddPanelBody(div);

                p.Controls.Add(div);
            }
        }

        private void AddCustomerReportAnchor(HtmlGenericControl h)
        {
            if (this.DisplayCustomerReportButton)
            {
                if (string.IsNullOrWhiteSpace(this.CustomerReportPath))
                {
                    return;
                }

                if (this.GetTranId().Equals("0"))
                {
                    return;
                }

                string customerReportUrl = this.ResolveUrl(this.CustomerReportPath + "?" + TRAN_ID_PARAMETER_NAME + "=" + this.GetTranId());

                using (HtmlAnchor anchor = new HtmlAnchor())
                {
                    anchor.ID = "CustomerReportAnchor";
                    anchor.InnerHtml = "<i class='icon list layout'></i>" + this.CustomerReportButtonText;
                    anchor.Attributes.Add("class", "item");
                    anchor.Attributes.Add("onclick", "showWindow('" + customerReportUrl + "');");
                    anchor.HRef = "javascript:void(0);";
                    h.Controls.Add(anchor);
                }
            }
        }

        private void AddEmailReportLinkButton(HtmlGenericControl h)
        {
            if (this.DisplayEmailReportButton)
            {
                if (string.IsNullOrWhiteSpace(this.PartyEmailAddress))
                {
                    return;
                }

                this.emailLinkButton = new LinkButton();

                this.emailLinkButton.ID = "EmailReportLinkButton";
                this.emailLinkButton.Text = @"<i class='icon mail'></i>" + this.EmailReportButtonText;
                this.emailLinkButton.CausesValidation = false;
                this.emailLinkButton.Attributes.Add("class", "item");
                this.emailLinkButton.Click += this.EmailLinkButton_Click;
                h.Controls.Add(this.emailLinkButton);
            }
        }

        private void AddNewAnchor(HtmlGenericControl h)
        {
            if (string.IsNullOrWhiteSpace(this.AddNewPath))
            {
                return;
            }

            string addNewUrl = this.ResolveUrl(this.AddNewPath);

            using (HtmlAnchor anchor = new HtmlAnchor())
            {
                anchor.ID = "AddNewAnchor";
                anchor.InnerHtml = "<i class='icon add sign'></i>" + Titles.AddNew;
                anchor.Attributes.Add("class", "item");
                anchor.HRef = addNewUrl;
                h.Controls.Add(anchor);
            }
        }

        private void AddPanelBody(HtmlGenericControl div)
        {
            this.AddWidthdrawAnchor(div);
            this.AddViewReportAnchor(div);
            this.AddEmailReportLinkButton(div);
            this.AddCustomerReportAnchor(div);
            this.AddPrintReceiptAnchor(div);
            this.AddPrintGLAnchor(div);
            this.AddAttachmentAnchor(div);
            this.AddNewAnchor(div);
            this.AddViewAnchor(div);
            this.AddBackAnchor(div);
        }

        private void AddPanelHeader(HtmlGenericControl div)
        {
            using (HtmlGenericControl header = new HtmlGenericControl())
            {
                header.TagName = "div";
                header.Attributes.Add("class", "ui header inverted green item");
                header.InnerHtml = "<i class='user icon'></i>" + Titles.Checklists;

                div.Controls.Add(header);
            }
        }

        private void AddPrintGLAnchor(HtmlGenericControl h)
        {
            if (this.DisplayPrintGlEntryButton)
            {
                if (string.IsNullOrWhiteSpace(this.GlAdvicePath))
                {
                    return;
                }

                if (this.GetTranId().Equals("0"))
                {
                    return;
                }

                string glAdviceUrl = this.ResolveUrl(this.GlAdvicePath + "?" + TRAN_ID_PARAMETER_NAME + "=" + this.GetTranId());

                using (HtmlAnchor anchor = new HtmlAnchor())
                {
                    anchor.ID = "PrintGLAnchor";
                    anchor.InnerHtml = "<i class='icon print'></i>" + Titles.PrintGlEntry;
                    anchor.Attributes.Add("class", "item");
                    anchor.Attributes.Add("onclick", "showWindow('" + glAdviceUrl + "');");
                    anchor.HRef = "javascript:void(0);";
                    h.Controls.Add(anchor);
                }
            }
        }

        private void AddPrintReceiptAnchor(HtmlGenericControl h)
        {
            if (this.DisplayPrintReceiptButton)
            {
                if (string.IsNullOrWhiteSpace(this.ReceiptAdvicePath))
                {
                    return;
                }

                if (this.GetTranId().Equals("0"))
                {
                    return;
                }

                string receiptUrl = this.ResolveUrl(this.ReceiptAdvicePath + "?" + TRAN_ID_PARAMETER_NAME + "=" + this.GetTranId());

                using (HtmlAnchor anchor = new HtmlAnchor())
                {
                    anchor.ID = "PrintReceiptAnchor";
                    anchor.InnerHtml = "<i class='icon print'></i>" + Titles.PrintReceipt;
                    anchor.Attributes.Add("class", "item");
                    anchor.Attributes.Add("onclick", "showWindow('" + receiptUrl + "');");
                    anchor.HRef = "javascript:void(0);";
                    h.Controls.Add(anchor);
                }
            }
        }

        private void AddViewAnchor(HtmlGenericControl h)
        {
            if (string.IsNullOrWhiteSpace(this.ViewPath))
            {
                return;
            }

            string viewUrl = this.ResolveUrl(this.ViewPath);

            using (HtmlAnchor anchor = new HtmlAnchor())
            {
                anchor.ID = "ViewAnchor";
                anchor.InnerHtml = "<i class='icon circle left'></i>" + Titles.ReturnToView;
                anchor.Attributes.Add("class", "item");
                anchor.HRef = viewUrl;
                h.Controls.Add(anchor);
            }
        }

        private void AddViewReportAnchor(HtmlGenericControl h)
        {
            if (this.DisplayViewReportButton)
            {
                if (string.IsNullOrWhiteSpace(this.ReportPath))
                {
                    return;
                }

                if (this.GetTranId().Equals("0"))
                {
                    return;
                }

                string reportUrl = this.ResolveUrl(this.ReportPath + "?" + TRAN_ID_PARAMETER_NAME + "=" + this.GetTranId());

                using (HtmlAnchor anchor = new HtmlAnchor())
                {
                    anchor.ID = "ViewReportAnchor";
                    anchor.InnerHtml = "<i class='icon grid layout'></i>" + this.ViewReportButtonText;
                    anchor.Attributes.Add("class", "item");
                    anchor.Attributes.Add("onclick", "showWindow('" + reportUrl + "');");
                    anchor.HRef = "javascript:void(0);";
                    anchor.Attributes.Add("data-url", reportUrl);
                    h.Controls.Add(anchor);
                }
            }
        }

        private void AddWidthdrawAnchor(HtmlGenericControl h)
        {
            if (this.DisplayWithdrawButton)
            {
                using (HtmlAnchor anchor = new HtmlAnchor())
                {
                    anchor.ID = "WithdrawAnchor";
                    anchor.InnerHtml = "<i class='icon hide'></i>" + Titles.WithdrawTransaction;
                    anchor.Attributes.Add("class", "item");
                    anchor.HRef = "javascript:void(0);";
                    h.Controls.Add(anchor);
                }
            }
        }

        private double GetWidth()
        {
            if (this.Width.Value > 0)
            {
                return this.Width.Value;
            }

            return 400; //Todo: Can be parameterized on a config file.
        }
    }
}