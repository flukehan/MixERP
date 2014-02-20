/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web;
using System.Web.SessionState;

namespace MixERP.Net.Common.Helpers
{
    public static class SessionHelper
    {
        public static string GetSessionValueByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            HttpSessionState session = HttpContext.Current.Session;
            {
                if (session != null)
                {
                    if (session[key] != null)
                    {
                        return Conversion.TryCastString(session[key]);
                    }
                }
            }

            return string.Empty;
        }
    }
}
