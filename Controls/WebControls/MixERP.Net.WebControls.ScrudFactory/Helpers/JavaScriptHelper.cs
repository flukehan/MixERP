/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.IO;
using System.Reflection;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class JavaScriptHelper
    {
        public static string GetEmbeddedScript(string embeddedScriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
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
