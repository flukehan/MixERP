/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MixERP.Net.BusinessLayer.Helpers
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
