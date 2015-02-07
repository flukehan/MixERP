using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Utility.Installer.Helpers
{
    public static class StringUtility
    {
        public static bool AreNullOrWhitespace(params string[] strings)
        {
            return strings.Any(string.IsNullOrWhiteSpace);
        }

        public static bool Equal(this string str, params string[] values)
        {
            return values.Any(s => s.Equals(str));
        }
    }
}
