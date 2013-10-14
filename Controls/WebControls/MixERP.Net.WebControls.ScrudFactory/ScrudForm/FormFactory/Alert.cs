using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        private void DisplaySuccess()
        {
            messageLabel.CssClass = "success";
            messageLabel.Text = Resources.ScrudResource.TaskCompletedSuccessfully;

            gridPanel.Attributes["style"] = "display:block;";
            formPanel.Attributes["style"] = "display:none;";
            MixERP.Net.Common.PageUtility.ExecuteJavaScript("resetForm", "$('#form1').each(function(){this.reset();});", this.Page);
        }
    }
}
