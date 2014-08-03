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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.WebControls.ScrudFactory.Controls;
using MixERP.Net.WebControls.ScrudFactory.Data;
using MixERP.Net.WebControls.ScrudFactory.Helpers;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void LoadForm(Panel container, DataTable values)
        {
            using (var htmlTable = new HtmlTable())
            {
                htmlTable.Attributes["class"] = "valignmiddle";

                using (var table = TableHelper.GetTable(this.TableSchema, this.Table, this.Exclude))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            var columnName = Conversion.TryCastString(row["column_name"]);
                            var defaultValue = Conversion.TryCastString(row["column_default"]); //nextval('%_seq'::regclass)
                            var isSerial = defaultValue.StartsWith("nextval", StringComparison.OrdinalIgnoreCase);
                            var isNullable = Conversion.TryCastBoolean(row["is_nullable"]);
                            var dataType = Conversion.TryCastString(row["data_type"]);
                            var domain = Conversion.TryCastString(row["domain_name"]);
                            var maxLength = Conversion.TryCastInteger(row["character_maximum_length"]);

                            var parentTableSchema = Conversion.TryCastString(row["references_schema"]);
                            var parentTable = Conversion.TryCastString(row["references_table"]);
                            var parentTableColumn = Conversion.TryCastString(row["references_field"]);

                            if (values.Rows.Count.Equals(1))
                            {
                                defaultValue = Conversion.TryCastString(values.Rows[0][columnName]);
                            }

                            ScrudFactoryHelper.AddField(htmlTable, this.GetResourceClassName(),this.GetItemSelectorPath(), columnName, defaultValue, isSerial, isNullable, dataType, domain, maxLength, parentTableSchema, parentTable, parentTableColumn, this.DisplayFields, this.DisplayViews, this.UseDisplayViewsAsParents, this.SelectedValues);
                        }
                    }
                }

                container.Controls.Add(htmlTable);
            }
        }
        
        /// <summary>
        /// This function iterates through all the dynamically added controls, 
        /// checks their values, and returns a list of column and values 
        /// mapped as KeyValuePair of column_name (key) and value.
        /// </summary>
        /// <param name="skipSerial">
        /// Skip the PostgreSQL serial column. 
        /// There is no need to explicitly set the value for the serial column. 
        /// This value should be <strong>true</strong> if you are obtaining the form to insert the record. 
        /// Set this paramter to <b>false</b> if you want to update the form, based on the serial's columns value.
        /// </param>
        /// <returns>Returns a list of column and values mapped as 
        /// KeyValuePair of column_name (key) and value.</returns>
        private Collection<KeyValuePair<string, string>> GetFormCollection(bool skipSerial)
        {
            var list = new Collection<KeyValuePair<string, string>>();

            using (var table = TableHelper.GetTable(this.TableSchema, this.Table, this.Exclude))
            {
                if(table.Rows.Count > 0)
                {
                    foreach(DataRow row in table.Rows)
                    {
                        var columnName = Conversion.TryCastString(row["column_name"]);
                        var defaultValue = Conversion.TryCastString(row["column_default"]);
                        var isSerial = defaultValue.StartsWith("nextval", StringComparison.OrdinalIgnoreCase);
                        var parentTableColumn = Conversion.TryCastString(row["references_field"]);
                        var dataType = Conversion.TryCastString(row["data_type"]);

                        if(skipSerial)
                        {
                            if(isSerial)
                            {
                                continue;
                            }
                        }

                        if(string.IsNullOrWhiteSpace(parentTableColumn))
                        {
                            switch(dataType)
                            {
                                case "national character varying":
                                case "character varying":
                                case "national character":
                                case "character":
                                case "char":
                                case "varchar":
                                case "nvarchar":
                                case "text":
                                case "date":
                                case "smallint":
                                case "integer":
                                case "bigint":
                                case "numeric":
                                case "money":
                                case "double":
                                case "double precision":
                                case "float":
                                case "real":
                                case "currency":
                                    //TextBox
                                    var t = (TextBox)this.formContainer.FindControl(columnName + "_textbox");
                                    if(t != null)
                                    {
                                        list.Add(new KeyValuePair<string, string>(columnName, t.Text));
                                    }
                                    break;
                                case "boolean":
                                    var r = (RadioButtonList)this.formContainer.FindControl(columnName + "_radiobuttonlist");
                                    list.Add(new KeyValuePair<string, string>(columnName, r.Text));
                                    break;
                                case "bytea":
                                    var f = (FileUpload)this.formContainer.FindControl(columnName + "_fileupload");
                                    var file = ScrudFileUpload.UploadFile(f);
                                    list.Add(new KeyValuePair<string, string>(columnName, file));
                                    this.imageColumn = columnName;
                                    break;
                            }

                        }
                        else
                        {
                            //DropDownList
                            var d = (DropDownList)this.formContainer.FindControl(columnName + "_dropdownlist");
                            list.Add(new KeyValuePair<string, string>(columnName, d.Text));
                        }
                    }
                }
            }

            return list;
        }
    }
}