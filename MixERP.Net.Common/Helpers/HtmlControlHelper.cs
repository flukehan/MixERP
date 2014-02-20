/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Globalization;

namespace MixERP.Net.Common.Helpers
{
    public static class HtmlControlHelper
    {
        public static string GetLabel(string text)
        {
            return string.Format(CultureInfo.InvariantCulture, "<label>{0}</label>", text);
        }

        public static string GetLabel(string controlId, string text)
        {
            return string.Format(CultureInfo.InvariantCulture, "<label for='{0}'>{1}</label>", controlId, text);
        }
    }
}
