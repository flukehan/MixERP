/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Helpers;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        UpdateProgress updateProgress;

        private void AddUpdateProgress(Panel p)
        {
            this.updateProgress = new UpdateProgress();
            this.updateProgress.ID = "ScrudUpdateProgress";

            this.updateProgress.ProgressTemplate = new AjaxProgressTemplate(this.GetUpdateProgressTemplateCssClass(), this.GetUpdateProgressSpinnerImageCssClass(), this.Page.ResolveUrl(this.GetUpdateProgressSpinnerImagePath()));
            p.Controls.Add(this.updateProgress);
        }

        private string GetUpdateProgressTemplateCssClass()
        {
            var cssClass = this.UpdateProgressTemplateCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter( "UpdateProgressTemplateCssClass");
            }

            return cssClass;
        }

        private string GetUpdateProgressSpinnerImageCssClass()
        {
            var cssClass = this.UpdateProgressSpinnerImageCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter( "UpdateProgressSpinnerImageCssClass");
            }

            return cssClass;
        }

        private string GetUpdateProgressSpinnerImagePath()
        {
            var cssClass = this.UpdateProgressSpinnerImagePath;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("UpdateProgressSpinnerImagePath");
            }

            return cssClass;
        }
    }
}
