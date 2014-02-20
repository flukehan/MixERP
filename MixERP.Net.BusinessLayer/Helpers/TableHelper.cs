/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class TableHelper
    {
        public static DataTable GetTable(string schema, string tableName, string exclusion)
        {
            return DatabaseLayer.Helpers.TableHelper.GetTable(schema, tableName, exclusion);
        }
    }
}
