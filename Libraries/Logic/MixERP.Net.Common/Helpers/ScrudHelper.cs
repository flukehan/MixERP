using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Common.Helpers
{
    public static class ScrudHelper
    {
        public static void AddDisplayField(List<string> collection, string columnName, string expression)
        {
            if (collection == null)
            {
                return;
            }

            collection.Add(columnName + "-->" + expression);
        }

        public static void AddDisplayView(List<string> collection, string columnName, string parentTable)
        {
            if (collection == null)
            {
                return;
            }

            collection.Add(columnName + "-->" + parentTable);
        }

        public static void AddSelectedValue(List<string> collection, string columnName, string selectedValue)
        {
            if (collection == null)
            {
                return;
            }

            collection.Add(columnName + "-->" + selectedValue);
        }
    }
}