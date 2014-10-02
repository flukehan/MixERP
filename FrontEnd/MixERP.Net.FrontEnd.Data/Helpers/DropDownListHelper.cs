/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Data.Helpers
{
    public static class DropDownListHelper
    {
        public static void BindDropDownList(ListControl list, string schemaName, string tableName, string valueField, string displayField, string orderBy)
        {
            if (list == null)
            {
                return;
            }

            using (DataTable table = FormHelper.GetTable(schemaName, tableName, orderBy))
            {
                table.Columns.Add("text_field", typeof(string), displayField);

                list.DataSource = table;
                list.DataValueField = valueField;
                list.DataTextField = "text_field";
                list.DataBind();
            }
        }

        public static void BindDropDownList(ListControl list, DataTable table, string valueField, string displayField)
        {
            if (list == null)
            {
                return;
            }

            if (table == null)
            {
                return;
            }

            table.Columns.Add("text_field", typeof(string), displayField);

            list.DataSource = table;
            list.DataValueField = valueField;
            list.DataTextField = "text_field";
            list.DataBind();
        }
    }
}