using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        private void AddJavaScript()
        {
            string script = MixERP.Net.WebControls.ScrudFactory.Helpers.ItemSelectorJavaScriptHelper.GetScript();
            MixERP.Net.Common.PageUtility.ExecuteJavaScript("scrudItemSelectorScript", script, this.Page);
        }
    }
}
