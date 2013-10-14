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
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Helpers;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        UpdateProgress updateProgress;

        private void AddUpdateProgress(Panel p)
        {
            updateProgress = new UpdateProgress();
            updateProgress.ID = "ScrudUpdateProgress";

            updateProgress.ProgressTemplate = new AjaxProgressTemplate(this.UpdateProgressTemplateCssClass, this.UpdateProgressSpinnerImageCssClass, this.UpdateProgressSpinnerImagePath);
            p.Controls.Add(updateProgress);
        }
    }
}
