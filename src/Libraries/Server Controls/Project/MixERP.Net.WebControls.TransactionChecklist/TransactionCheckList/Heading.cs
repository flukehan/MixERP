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

using MixERP.Net.i18n.Resources;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionChecklist
{
    public partial class TransactionChecklistForm
    {
        private Literal subTitleLiteral;
        private Literal titleLiteral;
        private Label verificationLabel;

        private void AddBreak(HtmlGenericControl p)
        {
            using (HtmlGenericControl line = new HtmlGenericControl())
            {
                line.TagName = "br";
                p.Controls.Add(line);
            }
        }

        private void AddHeader(HtmlGenericControl p)
        {
            this.AddTitle(p);
            this.AddRuler(p);
            this.AddSubtitle(p);
            this.AddRuler(p);
            this.AddVerificationLabel(p);
            this.AddBreak(p);
        }

        private void AddRuler(HtmlGenericControl p)
        {
            using (HtmlGenericControl ruler = new HtmlGenericControl("hr"))
            {
                ruler.Attributes.Add("class", "ui divider");
                p.Controls.Add(ruler);
            }
        }

        private void AddSubtitle(HtmlGenericControl p)
        {
            using (HtmlGenericControl subTitleHeading = new HtmlGenericControl())
            {
                subTitleHeading.TagName = "h2";
                this.subTitleLiteral = new Literal();
                this.subTitleLiteral.Text = Labels.TransactionPostedSuccessfully;
                subTitleHeading.Controls.Add(this.subTitleLiteral);
                p.Controls.Add(subTitleHeading);
            }
        }

        private void AddTitle(HtmlGenericControl p)
        {
            using (HtmlGenericControl titleHeading = new HtmlGenericControl())
            {
                titleHeading.TagName = "h1";
                this.titleLiteral = new Literal();
                this.titleLiteral.Text = this.Text;
                titleHeading.Controls.Add(this.titleLiteral);
                p.Controls.Add(titleHeading);
            }
        }
        private void AddVerificationLabel(HtmlGenericControl p)
        {
            this.verificationLabel = new Label();
            this.ShowVerificationStatus(this.GetTranId(), this.verificationLabel);
            p.Controls.Add(this.verificationLabel);
        }
    }
}