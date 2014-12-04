using MixERP.Net.Common;

namespace MixERP.Net.WebControls.Flag
{
    public partial class FlagControl
    {
        private void AddScript()
        {
            string script = "$(document).ready(function() {" +
                            string.Format("var flagPopunder = $('#{0}');var flagButton = $('#{1}');", this.ID, this.AssociatedControlId) +
                            "flagButton.click(function () {" +
                            "popUnder(flagPopunder, flagButton);" +
                            "});});";

            PageUtility.RegisterJavascript("flag", script, this.Page, true);
        }
    }
}