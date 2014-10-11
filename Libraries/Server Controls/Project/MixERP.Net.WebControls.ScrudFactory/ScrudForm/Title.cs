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

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private Label descriptionLabel;
        private Label titleLabel;

        private static void AddRuler(Panel p)
        {
            if (p == null)
            {
                return;
            }

            using (var ruler = new HtmlGenericControl("hr"))
            {
                ruler.Attributes.Add("class", "hr");
                p.Controls.Add(ruler);
            }
        }

        private void AddDescription(Panel p)
        {
            this.descriptionLabel = new Label();
            p.Controls.Add(this.descriptionLabel);
        }

        private void AddTitle(Panel p)
        {
            if (p == null)
            {
                return;
            }

            using (HtmlGenericControl titleDiv = new HtmlGenericControl())
            {
                titleDiv.TagName = "div";
                this.titleLabel = new Label();
                this.titleLabel.ID = "TitleLabel";
                titleDiv.Attributes.Add("class", this.GetTitleLabelCssClass());

                titleDiv.Controls.Add(titleLabel);
                p.Controls.Add(titleDiv);
            }
        }

        private void LoadDescription()
        {
            if (!string.IsNullOrWhiteSpace(this.Description))
            {
                this.descriptionLabel.CssClass = this.GetDescriptionCssClass();
                this.descriptionLabel.Text = this.Description;
            }
        }

        private void LoadTitle()
        {
            this.titleLabel.Text = this.Text;
            this.Page.Title = this.Text;
        }
    }
}