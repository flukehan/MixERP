/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        private void LoadForm(Panel container, System.Data.DataTable values)
        {
            using (HtmlTable htmlTable = new HtmlTable())
            {
                htmlTable.Attributes["class"] = "valignmiddle";

                using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.TableHelper.GetTable(this.TableSchema, this.Table, this.Exclude))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow row in table.Rows)
                        {
                            string columnName = MixERP.Net.Common.Conversion.TryCastString(row["column_name"]);
                            string defaultValue = MixERP.Net.Common.Conversion.TryCastString(row["column_default"]); //nextval('%_seq'::regclass)
                            bool isSerial = defaultValue.StartsWith("nextval", StringComparison.OrdinalIgnoreCase);
                            bool isNullable = MixERP.Net.Common.Conversion.TryCastBoolean(row["is_nullable"]);
                            string dataType = MixERP.Net.Common.Conversion.TryCastString(row["data_type"]);
                            string domain = MixERP.Net.Common.Conversion.TryCastString(row["domain_name"]);
                            int maxLength = MixERP.Net.Common.Conversion.TryCastInteger(row["character_maximum_length"]);

                            string parentTableSchema = MixERP.Net.Common.Conversion.TryCastString(row["references_schema"]);
                            string parentTable = MixERP.Net.Common.Conversion.TryCastString(row["references_table"]);
                            string parentTableColumn = MixERP.Net.Common.Conversion.TryCastString(row["references_field"]);

                            if (values.Rows.Count.Equals(1))
                            {
                                defaultValue = MixERP.Net.Common.Conversion.TryCastString(values.Rows[0][columnName]);
                            }

                            MixERP.Net.WebControls.ScrudFactory.Helpers.ScrudFactoryHelper.AddField(htmlTable, columnName, defaultValue, isSerial, isNullable, dataType, domain, maxLength, parentTableSchema, parentTable, parentTableColumn, this.DisplayFields, this.DisplayViews, this.SelectedValues);
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
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();

            using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.TableHelper.GetTable(this.TableSchema, this.Table, this.Exclude))
            {
                if(table.Rows.Count > 0)
                {
                    foreach(System.Data.DataRow row in table.Rows)
                    {
                        string columnName = MixERP.Net.Common.Conversion.TryCastString(row["column_name"]);
                        string defaultValue = MixERP.Net.Common.Conversion.TryCastString(row["column_default"]);
                        bool isSerial = defaultValue.StartsWith("nextval", StringComparison.OrdinalIgnoreCase);
                        string parentTableColumn = MixERP.Net.Common.Conversion.TryCastString(row["references_field"]);
                        string dataType = MixERP.Net.Common.Conversion.TryCastString(row["data_type"]);

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
                                    TextBox t = (TextBox)formContainer.FindControl(columnName + "_textbox");
                                    if(t != null)
                                    {
                                        list.Add(new KeyValuePair<string, string>(columnName, t.Text));
                                    }
                                    break;
                                case "boolean":
                                    RadioButtonList r = (RadioButtonList)formContainer.FindControl(columnName + "_radiobuttonlist");
                                    list.Add(new KeyValuePair<string, string>(columnName, r.Text));
                                    break;
                                case "bytea":
                                    FileUpload f = (FileUpload)formContainer.FindControl(columnName + "_fileupload");
                                    string file = MixERP.Net.WebControls.ScrudFactory.Controls.ScrudFileUpload.UploadFile(f);
                                    list.Add(new KeyValuePair<string, string>(columnName, file));
                                    imageColumn = columnName;
                                    break;
                            }

                        }
                        else
                        {
                            //DropDownList
                            DropDownList d = (DropDownList)formContainer.FindControl(columnName + "_dropdownlist");
                            list.Add(new KeyValuePair<string, string>(columnName, d.Text));
                        }
                    }
                }
            }

            return list;
        }


    }
}