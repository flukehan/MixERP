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