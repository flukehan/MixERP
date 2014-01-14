/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
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
