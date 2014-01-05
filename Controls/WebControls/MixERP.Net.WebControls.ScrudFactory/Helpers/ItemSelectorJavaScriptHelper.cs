using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class ItemSelectorJavaScriptHelper
    {
        public static string GetScript()
        {
            string resource = Helpers.JavaScriptHelper.GetEmbeddedScript("MixERP.Net.WebControls.ScrudFactory.ScrudItemSelector.js");
            StringBuilder script = new StringBuilder();

            script.Append(resource);
            return script.ToString();
        }


    }
}
