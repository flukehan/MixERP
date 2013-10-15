using MixERP.Net.WebControls.ScrudFactory.Helpers;
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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    public partial class ScrudDropDownList
    {
        //Todo: Simplify this class.
        public static void AddDropDownList(HtmlTable t, string columnName, bool isNullable, string tableSchema, string tableName, string tableColumn, string defaultValue, string displayFields, string displayViews, string selectedValues)
        {
            string selectedItemValue = string.Empty;
            string dataTextField = string.Empty;
            string relation = string.Empty;
            string viewRelation = string.Empty;
            string selected = string.Empty;
            string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("FormResource", columnName);
            string schema = string.Empty;
            string view = string.Empty;
            HtmlAnchor itemSelectorAnchor = null;

            DropDownList dropDownList = GetDropDownList(columnName + "_dropdownlist");


            using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable(tableSchema, tableName))
            {
                if(table.Rows.Count > 0)
                {
                    //See DisplayFields Property for more information.
                    //Loop through all the DisplayFields to match this control.
                    foreach(string displayField in displayFields.Split(','))
                    {
                        //First, trim the field to remove whitespaces.
                        viewRelation = displayField.Trim();

                        //The fully qualified name of this column.
                        relation = tableSchema + "." + tableName + "." + tableColumn;

                        //Check whether this field matches exactly with this column.
                        if(viewRelation.StartsWith(relation, StringComparison.OrdinalIgnoreCase))
                        {
                            //This field in this loop contained the column name we were looking for.
                            //Now, get the mapped column (display field) to show on the drop down list.
                            //This should be done by :
                            //1. Removing the column name from the field.
                            //2. Removign the the "-->" symbol.
                            //What we get is the name of the field that is displayed on this drop down list.
                            dataTextField = viewRelation.Replace(relation + "-->", "");

                            //Since we have found the field we needed, let's break this loop.
                            break;
                        }
                        //Probably this was not the field we were looking for.
                    }

                    //The display field can be an existing column or a representation of different columns (formula).
                    //Let's check whether the display field really exists.
                    if(!table.Columns.Contains(dataTextField))
                    {
                        //This display field was a formula based various columns.
                        //Now, we are adding a new column "DataTextField" in the data table using the requested formula.
                        table.Columns.Add("DataTextField", typeof(string), dataTextField);
                        dataTextField = "DataTextField";
                    }

                    dropDownList.DataSource = table;
                    dropDownList.DataValueField = tableColumn;
                    dropDownList.DataTextField = dataTextField;
                    dropDownList.DataBind();

                    if(!string.IsNullOrWhiteSpace(displayViews))
                    {
                        //See DisplayViews Property for more information.
                        //Loop through all the DisplayViews to match this control.
                        foreach(string displayView in displayViews.Split(','))
                        {
                            //First, trim the field to remove whitespaces.
                            viewRelation = displayView.Trim();

                            //The fully qualified name of this column.
                            relation = tableSchema + "." + tableName + "." + tableColumn;

                            //Check whether this view matches exactly with this column.
                            if(viewRelation.StartsWith(relation, StringComparison.OrdinalIgnoreCase))
                            {
                                //This view in this loop starts with the column name we were looking for.
                                //Now, get the mapped view to show on the modal page.
                                //This should be done by :
                                //1. Removing the column name from the field.
                                //2. Removign the the "-->" symbol.
                                //What we get is the name of the view that is shown on the modal page.
                                viewRelation = viewRelation.Replace(relation + "-->", "");

                                //Since we have found the field we needed, let's break this loop.
                                break;
                            }
                            //Probably this was not the field we were looking for.
                        }

                        schema = viewRelation.Split('.').First();
                        view = viewRelation.Split('.').Last();

                        //Sanitize the schema and the view
                        schema = MixERP.Net.BusinessLayer.DBFactory.Sanitizer.SanitizeIdentifierName(schema);
                        view = MixERP.Net.BusinessLayer.DBFactory.Sanitizer.SanitizeIdentifierName(view);

                        if(string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(view))
                        {
                            schema = string.Empty;
                            view = string.Empty;
                        }
                        else
                        {
                            itemSelectorAnchor = new HtmlAnchor();
                            itemSelectorAnchor.Attributes["class"] = "item-selector";
                            itemSelectorAnchor.HRef = "/General/ItemSelector.aspx?Schema=" + schema + "&View=" + view + "&AssociatedControlId=" + dropDownList.ID;
                        }
                    }

                }
            }


            //Determining the value which will be pre-selected when this drop down list is displayed.

            //If the "defaultValue" parameter has a value, it means that the form is being edited.
            //Check if "defaultValue" is empty.
            if(!string.IsNullOrWhiteSpace(defaultValue))
            {
                selectedItemValue = defaultValue;
            }
            else
            {
                //In this case, this is probably a fresh form for entry.
                //Checking if the "SelectedValues" has a value.
                if(!string.IsNullOrWhiteSpace(selectedValues))
                {
                    foreach(string selectedValue in selectedValues.Split(','))
                    {
                        //Trim whitespaces
                        selected = selectedValue.Trim();

                        //The plain old fully qualified name of this column.
                        relation = tableSchema + "." + tableName + "." + tableColumn;

                        //Checking again whether this field matches exactly with this column.
                        if(selected.StartsWith(relation, StringComparison.OrdinalIgnoreCase))
                        {
                            //This field in this loop contained the column name we were looking for.
                            //Now, get the mapped column (display field) to show on the drop down list.
                            //This should be done by :
                            //1. Removing the column name from the field.
                            //2. Removign the the "-->" symbol.
                            string value = selected.Replace(relation + "-->", "");

                            //Check the type of the value.
                            //If the value starts with single inverted comma, the value is a text.
                            if(value.StartsWith("'", StringComparison.OrdinalIgnoreCase))
                            {
                                //The selected item value from the drop down list text fields.
                                ListItem item = dropDownList.Items.FindByText(value.Replace("'", ""));

                                if(item != null)
                                {
                                    selectedItemValue = item.Value;
                                }
                            }
                            else
                            {
                                selectedItemValue = value;
                            }
                            break;
                        }
                    }
                }
            }


            if(!string.IsNullOrWhiteSpace(selectedItemValue))
            {
                dropDownList.SelectedValue = selectedItemValue;
            }


            if(isNullable)
            {
                dropDownList.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ScrudFactoryHelper.AddRow(t, label, dropDownList, itemSelectorAnchor);
            }
            else
            {
                RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(dropDownList);
                ScrudFactoryHelper.AddRow(t, label + Resources.ScrudResource.RequiredFieldIndicator, dropDownList, required, itemSelectorAnchor);
            }

        }

        private static DropDownList GetDropDownList(string id, string keys, string values, string selectedValues)
        {
            DropDownList list = new DropDownList();
            list.ID = id;
            list.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            Helper.AddListItems(list, keys, values, selectedValues);
            return list;
        }

        private static DropDownList GetDropDownList(string id)
        {
            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = id;

            dropDownList.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            return dropDownList;
        }
    }
}
