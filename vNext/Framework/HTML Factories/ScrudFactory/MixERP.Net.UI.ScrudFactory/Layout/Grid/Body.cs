using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Layout.Grid
{
    internal sealed class Body : ScrudLayout, IDisposable
    {
        public Body(Config config, DataTable table, List<Column> columns)
            : base(config)
        {
            this.Table = table;
            this.Columns = columns;
        }

        private DataTable Table { get; set; }
        private List<Column> Columns { get; set; }

        public void Dispose()
        {
            if (this.Table != null)
            {
                this.Table.Dispose();
                this.Table = null;
            }
        }

        public override string Get()
        {
            StringBuilder rows = new StringBuilder();
            TagBuilder.Begin(rows, "tbody", true);

            for (int i = 0; i < this.Table.Rows.Count; i++)
            {
                DataRow row = this.Table.Rows[i];
                string keyValue = row[this.Config.KeyColumn].ToString();

                TagBuilder.Begin(rows, "tr", true);

                TagBuilder.Begin(rows, "td", true);

                TagBuilder.Begin(rows, "input");
                TagBuilder.AddType(rows, "radio");
                TagBuilder.AddId(rows, "SelectRadio" + keyValue);
                TagBuilder.AddValue(rows, keyValue);

                TagBuilder.Close(rows, true);

                TagBuilder.EndTag(rows, "td");


                foreach (Column column in this.Columns)
                {
                    TagBuilder.Begin(rows, "td");

                    switch (column.Type.FullName)
                    {
                        case "System.Decimal":
                        case "System.Double":
                        case "System.Single":
                            TagBuilder.AddClass(rows, "text right");
                            TagBuilder.Close(rows);

                            decimal value = Conversion.TryCastDecimal(row[column.ColumnName]);

                            if (!value.Equals(0))
                            {
                                rows.Append(value.ToString("N", CultureInfo.CurrentCulture));
                            }

                            break;
                        case "System.DateTime":
                            TagBuilder.AddClass(rows, "text right");
                            TagBuilder.Close(rows);

                            DateTime date = Conversion.TryCastDate(row[column.ColumnName]);

                            rows.Append(date.Date == date
                                ? Conversion.TryCastDate(date).ToString("D", CultureInfo.CurrentCulture)
                                : Conversion.TryCastDate(date).ToString("F", CultureInfo.CurrentCulture));

                            break;
                        default:
                            TagBuilder.Close(rows);
                            rows.Append(HttpUtility.HtmlEncode(row[column.ColumnName]));
                            break;
                    }

                    TagBuilder.EndTag(rows, "td");
                }

                TagBuilder.EndTag(rows, "tr");
            }

            TagBuilder.EndTag(rows, "tbody");

            return rows.ToString();
        }
    }
}