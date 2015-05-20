using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Utility.Installer.Helpers
{
    public static class Conversion
    {
        public static int TryCastInteger(object value)
        {
            int result;
            int.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            return result;
        }
    }
}
