using System;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.UI.ScrudFactory.Helpers
{
    internal class Expression
    {
        public static string GetExpressionValue(string expressions, string schema, string table, string column)
        {
            if (new[] {expressions, schema, table, column}.AnyNullOrWhitespace())
            {
                return string.Empty;
            }

            //Fully qualified relation name (PostgreSQL Terminology).
            string relation = schema + "." + table + "." + column;

            char itemSeparator = char.Parse(ConfigurationHelper.GetScrudParameter("ItemSeparator"));
            string expressionSeparator = ConfigurationHelper.GetScrudParameter("ExpressionSeparator");

            foreach (string item in expressions.Split(itemSeparator))
            {
                //First, trim the field to remove whitespace.
                string expression = item.Trim();

                //Check whether this expression matches with the fully qualified column name.
                if (expression.StartsWith(relation, StringComparison.OrdinalIgnoreCase))
                {
                    //Remove the column name and expression separator and return the actual expression value.
                    return expression.Replace(relation + expressionSeparator, string.Empty);
                }
                //Probably that was not the field we were looking for.
            }

            return string.Empty;
        }
    }
}