using System.IO;
using System.Reflection;

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
    }
}