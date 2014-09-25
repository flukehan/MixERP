using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace MixERP.Net.Common.Helpers
{
    public static class JavaScriptHelper
    {
        public static string GetEmbeddedScript(string embeddedScriptName, Assembly assembly)
        {
            var stream = assembly.GetManifestResourceStream(embeddedScriptName);

            if (stream == null)
            {
                return string.Empty;
            }

            try
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
        }

        public static void AddJSReference(Page page, string resourceName, string key, Type type)
        {
            if (page != null)
            {
                string script = "<script type='text/javascript' src='" + page.ClientScript.GetWebResourceUrl(type, resourceName) + "'></script>";
                PageUtility.RegisterJavascript(key, script, page, false);

                //page.ClientScript.RegisterClientScriptInclude(type, key, page.ClientScript.GetWebResourceUrl(type, resourceName));
            }
        }
    }
}