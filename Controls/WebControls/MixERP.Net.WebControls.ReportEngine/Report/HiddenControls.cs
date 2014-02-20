/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
        HiddenField reportHidden;
        HiddenField reportTitleHidden;

        private void AddHiddenControls(Panel p)
        {
            this.reportHidden = new HiddenField();
            this.reportHidden.ID = "ReportHidden";
            p.Controls.Add(this.reportHidden);

            this.reportTitleHidden = new HiddenField();
            this.reportTitleHidden.ID = "ReportTitleHidden";
            p.Controls.Add(this.reportTitleHidden);
        }
    }
}
