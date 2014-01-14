/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using Npgsql;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;

namespace MixERP.Net.DatabaseLayer.Helpers
{
    public static class FormHelper
    {
        public static DataTable GetTable(string tableSchema, string tableName)
        {
            return MixERP.Net.DBFactory.FormHelper.GetTable(tableSchema, tableName);
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValues)
        {
            return MixERP.Net.DBFactory.FormHelper.GetTable(tableSchema, tableName, columnNames, columnValues);
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValuesLike, int limit)
        {
            return MixERP.Net.DBFactory.FormHelper.GetTable(tableSchema, tableName, columnNames, columnValuesLike, limit);
        }
    }
}
