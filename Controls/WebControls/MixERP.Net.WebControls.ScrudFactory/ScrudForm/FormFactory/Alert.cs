/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void DisplaySuccess()
        {
            this.messageLabel.CssClass = "success";
            this.messageLabel.Text = ScrudResource.TaskCompletedSuccessfully;

            this.gridPanel.Attributes["style"] = "display:block;";
            this.formPanel.Attributes["style"] = "display:none;";
            PageUtility.ExecuteJavaScript("resetForm", "$('#form1').each(function(){this.reset();});", this.Page);
        }
    }
}
