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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        Label titleLabel;
        Label descriptionLabel;

        private void AddTitle(Panel p)
        {
            HtmlGenericControl heading = new HtmlGenericControl("h1");
            titleLabel = new Label();
            titleLabel.ID = "TitleLabel";
            heading.Controls.Add(titleLabel);
            p.Controls.Add(heading);
        }

        private void AddDescription(Panel p)
        {
            descriptionLabel = new Label();
            p.Controls.Add(descriptionLabel);
        }

        private void LoadTitle()
        {
            titleLabel.Text = this.Text;
            this.Page.Title = this.Text;
        }

        private void AddRuler(Panel p)
        {
            HtmlGenericControl ruler = new HtmlGenericControl("hr");
            ruler.Attributes.Add("class", "hr");
            p.Controls.Add(ruler);
        }

        private void LoadDescription()
        {
            if (!string.IsNullOrWhiteSpace(this.Description))
            {
                descriptionLabel.CssClass = "description";
                descriptionLabel.Text = this.Description;
            }
        }

    }
}
