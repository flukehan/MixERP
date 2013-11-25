using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
