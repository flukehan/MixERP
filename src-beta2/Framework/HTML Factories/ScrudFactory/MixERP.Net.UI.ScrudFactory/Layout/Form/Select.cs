using System.Data;
using System.Linq;
using System.Text;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Data;

namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    internal sealed class Select : ScrudLayout
    {
        public Select(Config config, FieldConfig fieldConfig)
            : base(config)
        {
            this.FieldConfig = fieldConfig;
        }

        private FieldConfig FieldConfig { get; set; }

        public override string Get()
        {
            StringBuilder table = new StringBuilder();
            TagBuilder.Begin(table, "table");
            TagBuilder.AddAttribute(table, "role", "item-selector-table");
            TagBuilder.AddStyle(table, "border-collapse:collapse;width:100%;");
            TagBuilder.Close(table);

            TagBuilder.Begin(table, "tr", true);

            //Select Begin
            TagBuilder.Begin(table, "td", true);
            table.Append(GetSelect());
            TagBuilder.EndTag(table, "td");
            //Select End


            //Item Selector Begin
            TagBuilder.Begin(table, "td");
            TagBuilder.AddStyle(table, "width:24px;");
            TagBuilder.Close(table);

            ItemSelector selector = new ItemSelector(this.Config, this.FieldConfig);
            table.Append(selector.Get());
            TagBuilder.EndTag(table, "td");
            //Item Selector End


            TagBuilder.EndTag(table, "tr");
            TagBuilder.EndTag(table, "table");
            return table.ToString();
        }

        private string GetSelect()
        {
            StringBuilder select = new StringBuilder();

            TagBuilder.Begin(select, "select");
            TagBuilder.AddId(select, this.FieldConfig.ColumnName);
            TagBuilder.AddAttribute(select, "data-scrud", "select");

            if (!this.FieldConfig.IsNullable)
            {
                TagBuilder.AddRequired(select);
            }

            if (this.FieldConfig.IsDisabled)
            {
                TagBuilder.AddDisabled(select);
            }


            TagBuilder.Close(select);

            using (
                DataTable table = GetTable(this.FieldConfig.ParentTableSchema, this.FieldConfig.ParentTable,
                    this.FieldConfig.ParentTableColumn, this.Config.DisplayViews, this.Config.UseDisplayViewsAsParents))
            {
                //Get the expression value of display field from comma separated list of expressions.
                //The expression can be either the column name or a column expression.
                string columnOrExpression = Helpers.Expression.GetExpressionValue(this.Config.DisplayFields,
                    this.FieldConfig.ParentTableSchema, this.FieldConfig.ParentTable, this.FieldConfig.ParentTableColumn);

                //Let's check whether the display field expression really exists.
                //If it does not exist, it is probably an expression column.
                if (!table.Columns.Contains(columnOrExpression) && !string.IsNullOrWhiteSpace(columnOrExpression))
                {
                    //Add the expression as a new column in the datatable.
                    table.Columns.Add("DataTextField", typeof (string), columnOrExpression);
                    columnOrExpression = "DataTextField";
                }

                if (table.Rows.Count > 0)
                {
                    if (this.FieldConfig.IsNullable)
                    {
                        TagBuilder.Begin(select, "option");
                        TagBuilder.AddAttribute(select, "value", string.Empty);
                        TagBuilder.Close(select);
                        select.Append(string.Empty);
                        TagBuilder.EndTag(select, "option");
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        string value = row[this.FieldConfig.ParentTableColumn].ToString();

                        TagBuilder.Begin(select, "option");
                        TagBuilder.AddAttribute(select, "value", value);


                        if (value == this.FieldConfig.DefaultValue)
                        {
                            TagBuilder.AddSelected(select);
                        }

                        TagBuilder.Close(select);

                        @select.Append(!string.IsNullOrWhiteSpace(columnOrExpression) ? row[columnOrExpression] : row[1]);

                        TagBuilder.EndTag(select, "option");
                    }
                }
            }

            TagBuilder.EndTag(select, "select");

            return select.ToString();
        }

        private static DataTable GetTable(string tableSchema, string tableName, string tableColumn, string displayViews,
            bool useDisplayViewsAsParent)
        {
            if (useDisplayViewsAsParent)
            {
                //Get the expression value of display view from comma separated list of expressions.
                //The expression must be a valid fully qualified table or view name.
                string viewRelation = Helpers.Expression.GetExpressionValue(displayViews, tableSchema, tableName,
                    tableColumn);

                string schema = viewRelation.Split('.').First();
                string view = viewRelation.Split('.').Last();

                //Sanitize the schema and the view
                schema = Sanitizer.SanitizeIdentifierName(schema);
                view = Sanitizer.SanitizeIdentifierName(view);

                if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(view))
                {
                    return FormHelper.GetTable(tableSchema, tableName, "1");
                }

                return FormHelper.GetTable(schema, view, "1");
            }

            return FormHelper.GetTable(tableSchema, tableName, "1");
        }
    }
}