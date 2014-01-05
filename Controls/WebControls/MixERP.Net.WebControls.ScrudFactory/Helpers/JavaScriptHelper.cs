using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class JavaScriptHelper
    {
        public static string GetEmbeddedScript(string embeddedScriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            Stream stream = assembly.GetManifestResourceStream(embeddedScriptName);

            try
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream = null;
                }
            }

        }

    }
}
