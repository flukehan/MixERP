using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Data;
using MixERP.Net.UI.ScrudFactory.Layout.Grid;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class GridView : ScrudLayout
    {
        internal GridView(Config config)
            : base(config)
        {
        }

        public override string Get()
        {
            StringBuilder grid = new StringBuilder();

            using (DataTable table = this.GetTable())
            {
                if (table.Rows.Count.Equals(0))
                {
                    return "<div class='ui message'>No record found</div>";
                }

                TagBuilder.Begin(grid, "table");
                TagBuilder.AddId(grid, "FormGridView");
                TagBuilder.AddClass(grid, ConfigBuilder.GetGridViewCssClass(this.Config));
                TagBuilder.AddStyle(grid, ConfigBuilder.GetGridViewWidth(this.Config) + ";white-space: nowrap;");
                TagBuilder.Close(grid);

                List<Column> columns = GetColumns(table).ToList();

                HeaderRow header = new HeaderRow(this.Config, columns);
                grid.Append(header.Get());

                using (Body body = new Body(this.Config, table, columns))
                {
                    grid.Append(body.Get());
                }


                TagBuilder.EndTag(grid, "table");
            }


            return grid.ToString();
        }

        private static IEnumerable<Column> GetColumns(DataTable table)
        {
            Collection<Column> columns = new Collection<Column>();

            foreach (DataColumn column in table.Columns)
            {
                columns.Add(new Column
                {
                    ColumnName = column.ColumnName,
                    Type = column.DataType
                });
            }

            return columns;
        }

        private DataTable GetTable()
        {
            var limit = 10;
            var offset = 0;

            if (ConfigBuilder.GetPageSize(this.Config) != 0)
            {
                limit = ConfigBuilder.GetPageSize(this.Config);
            }


            if (HttpContext.Current.Request["page"] != null)
            {
                offset = (Conversion.TryCastInteger(HttpContext.Current.Request["page"]) - 1)*limit;
            }

            using (
                DataTable table = FormHelper.GetView(this.Config.ViewSchema, this.Config.View,
                    this.Config.KeyColumn, limit, offset))
            {
                return table;
            }
        }
    }
}