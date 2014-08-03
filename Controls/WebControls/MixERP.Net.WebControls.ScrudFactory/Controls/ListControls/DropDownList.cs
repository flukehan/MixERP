/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.WebControls.ScrudFactory.Data;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using FormHelper = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    public static class ScrudDropDownList
    {
        public static void AddDropDownList(HtmlTable htmlTable, string resourceClassName, string itemSelectorPath, string columnName, bool isNullable, string tableSchema, string tableName, string tableColumn, string defaultValue, string displayFields, string displayViews, bool useDisplayViewsAsParent, string selectedValues)
        {
            var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);

            var dropDownList = GetDropDownList(columnName + "_dropdownlist");

            HtmlAnchor itemSelectorAnchor;

            using (var table = GetTable(tableSchema, tableName, tableColumn, displayViews, useDisplayViewsAsParent))
            {
                SetDisplayFields(dropDownList, table, tableSchema, tableName, tableColumn, displayFields);
                itemSelectorAnchor = GetItemSelector(dropDownList.ClientID, table, itemSelectorPath, tableSchema, tableName, tableColumn, displayViews);
            }

            SetSelectedValue(dropDownList, tableSchema, tableName, tableColumn, defaultValue, selectedValues);

            if (isNullable)
            {
                dropDownList.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ScrudFactoryHelper.AddRow(htmlTable, label, dropDownList, itemSelectorAnchor);
            }
            else
            {
                var required = ScrudFactoryHelper.GetRequiredFieldValidator(dropDownList);
                ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, dropDownList, required, itemSelectorAnchor);
            }
        }

        private static DataTable GetTable(string tableSchema, string tableName, string tableColumn, string displayViews, bool useDisplayViewsAsParent)
        {
            if (useDisplayViewsAsParent)
            {
                //Get the expression value of display view from comma seprated list of expressions.
                //The expression must be a valid fully qualified table or view name.
                string viewRelation = GetExpressionValue(displayViews, tableSchema, tableName, tableColumn);

                string schema = viewRelation.Split('.').First();
                string view = viewRelation.Split('.').Last();

                //Sanitize the schema and the view
                schema = Sanitizer.SanitizeIdentifierName(schema);
                view = Sanitizer.SanitizeIdentifierName(view);

                if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(view))
                {
                    return FormHelper.GetTable(tableSchema, tableName);
                }

                return FormHelper.GetTable(schema, view);
            }

            return FormHelper.GetTable(tableSchema, tableName);
        }


        private static string GetExpressionValue(string expressions, string schema, string table, string column)
        {
            if (string.IsNullOrWhiteSpace(expressions) || string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(column))
            {
                return string.Empty;
            }

            //Fully qualified relation name (PostgreSQL Terminology).
            var relation = schema + "." + table + "." + column;

            var itemSeparator = char.Parse(ConfigurationHelper.GetScrudParameter("ItemSeparator"));
            var expressionSeparator = ConfigurationHelper.GetScrudParameter("ExpressionSeparator");

            foreach (var item in expressions.Split(itemSeparator))
            {
                //First, trim the field to remove whitespaces.
                var expression = item.Trim();

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

        private static void SetDisplayFields(DropDownList dropDownList, DataTable table, string tableSchema, string tableName, string tableColumn, string displayFields)
        {
            //See DisplayFields Property for more information.

            if (table.Rows.Count > 0)
            {
                //Get the expression value of display field from comma seprated list of expressions.
                //The expression can be either the column name or a column expression.
                string columnOrExpression = GetExpressionValue(displayFields, tableSchema, tableName, tableColumn);

                //Let's check whether the display field expression really exists.
                //If it does not exist, it is probably an expression column.
                if (!table.Columns.Contains(columnOrExpression))
                {
                    //Add the expression as a new column in the datatable.
                    table.Columns.Add("DataTextField", typeof(string), columnOrExpression);
                    columnOrExpression = "DataTextField";
                }

                dropDownList.DataSource = table;
                dropDownList.DataValueField = tableColumn;
                dropDownList.DataTextField = columnOrExpression;
                dropDownList.DataBind();
            }
        }

        /// <summary>
        /// Creates item selector html anchor which bascially is an extender of the control.
        /// The extender is an html image button which, when clicked, will open 
        /// a popup window which allows selection, filtering, search, etc. on the target table.
        /// </summary>
        /// <param name="associatedControlId">ClientID of the DropDownList control to wich this control is associated to.</param>
        /// <param name="table"></param>
        /// <param name="itemSelectorPath"></param>
        /// <param name="tableSchema"></param>
        /// <param name="tableName"></param>
        /// <param name="tableColumn"></param>
        /// <param name="displayViews"></param>
        /// <returns></returns>
        private static HtmlAnchor GetItemSelector(string associatedControlId, DataTable table, string itemSelectorPath, string tableSchema, string tableName, string tableColumn, string displayViews)
        {
            if (table.Rows.Count.Equals(0) || string.IsNullOrWhiteSpace(displayViews))
            {
                return null;
            }

            using (var itemSelectorAnchor = new HtmlAnchor())
            {
                //string relation = string.Empty;

                //Get the expression value of display view from comma seprated list of expressions.
                //The expression must be a valid fully qualified table or view name.
                string viewRelation = GetExpressionValue(displayViews, tableSchema, tableName, tableColumn);

                string schema = viewRelation.Split('.').First();
                string view = viewRelation.Split('.').Last();

                //Sanitize the schema and the view
                schema = Sanitizer.SanitizeIdentifierName(schema);
                view = Sanitizer.SanitizeIdentifierName(view);

                if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(view))
                {
                    return null;
                }

                itemSelectorAnchor.Attributes["class"] = ConfigurationHelper.GetScrudParameter("ItemSelectorAnchorCssClass");
                itemSelectorAnchor.HRef = itemSelectorPath + "?Schema=" + schema + "&View=" + view + "&AssociatedControlId=" + associatedControlId;

                return itemSelectorAnchor;
            }
        }

        private static void SetSelectedValue(DropDownList dropDownList, string schema, string table, string column, string postbackValue, string selectedValueExpressions)
        {
            var selectedItemValue = string.Empty;

            if (dropDownList == null || string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(column))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(postbackValue) && string.IsNullOrWhiteSpace(selectedValueExpressions))
            {
                return;
            }

            //If the post back contains a value, skip finding value from expressions.
            if (!string.IsNullOrWhiteSpace(postbackValue))
            {
                selectedItemValue = postbackValue;
            }
            else
            {
                //Find value from expressions.
                if (!string.IsNullOrWhiteSpace(selectedValueExpressions))
                {
                    var value = GetExpressionValue(selectedValueExpressions, schema, table, column);

                    if (value.StartsWith("'", StringComparison.OrdinalIgnoreCase))
                    {
                        //If the value starts with a quote, find the value by the text.
                        var item = dropDownList.Items.FindByText(value.Replace("'", ""));

                        if (item != null)
                        {
                            selectedItemValue = item.Value;
                        }
                    }
                    else
                    {
                        selectedItemValue = value;
                    }
                }
            }


            if (!string.IsNullOrWhiteSpace(selectedItemValue))
            {
                dropDownList.SelectedValue = selectedItemValue;
            }
        }

        //private static DropDownList GetDropDownList(string id, string keys, string values, string selectedValues)
        //{
        //    DropDownList dropDownList = GetDropDownList(id);
        //    Helper.AddListItems(dropDownList, keys, values, selectedValues);
        //    return dropDownList;
        //}

        private static DropDownList GetDropDownList(string id)
        {
            using (var dropDownList = new DropDownList())
            {
                dropDownList.ID = id;
                dropDownList.ClientIDMode = ClientIDMode.Static;

                return dropDownList;
            }
        }
    }
}
