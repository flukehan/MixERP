/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        Label titleLabel;
        Label descriptionLabel;

        private void AddTitle(Panel p)
        {
            if (p == null)
            {
                return;
            }

            using (var heading = new HtmlGenericControl("h1"))
            {
                this.titleLabel = new Label();
                this.titleLabel.ID = "TitleLabel";
                heading.Controls.Add(this.titleLabel);
                p.Controls.Add(heading);
            }
        }

        private void AddDescription(Panel p)
        {
            this.descriptionLabel = new Label();
            p.Controls.Add(this.descriptionLabel);
        }

        private void LoadTitle()
        {
            this.titleLabel.Text = this.Text;
            this.Page.Title = this.Text;
        }

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

        private void LoadDescription()
        {
            if (!string.IsNullOrWhiteSpace(this.Description))
            {
                this.descriptionLabel.CssClass = "description";
                this.descriptionLabel.Text = this.Description;
            }
        }

    }
}
