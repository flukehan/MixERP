using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using MixERP.Net.Common;
using MixERP.Net.UI.ScrudFactory.Data;
using MixERP.Net.UI.ScrudFactory.Layout.Form;

namespace MixERP.Net.UI.ScrudFactory.Helpers
{
    internal static class FieldConfigHelper
    {
        public static IEnumerable<FieldConfig> GetFields(DataTable defaultValues, Config config,
            bool editing)
        {
            using (
                DataTable table = TableHelper.GetTable(config.TableSchema, config.Table, config.Exclude))
            {

                Collection<FieldConfig> fields = new Collection<FieldConfig>();

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        // ReSharper disable once UseObjectOrCollectionInitializer
                        FieldConfig fieldConfig = new FieldConfig();

                        fieldConfig.ColumnName = Conversion.TryCastString(row["column_name"]);
                        fieldConfig.ColumnNameLocalized =
                            ScrudLocalizationHelper.GetResourceString(config.ResourceAssembly,
                                ConfigBuilder.GetResourceClassName(config), fieldConfig.ColumnName);
                        fieldConfig.DefaultValue = Conversion.TryCastString(row["column_default"]);
                        fieldConfig.IsSerial = fieldConfig.DefaultValue.StartsWith("nextval",
                            StringComparison.OrdinalIgnoreCase);
                        fieldConfig.IsNullable = Conversion.TryCastBoolean(row["is_nullable"]);
                        fieldConfig.DataType = Conversion.TryCastString(row["data_type"]);
                        fieldConfig.Domain = Conversion.TryCastString(row["domain_name"]);
                        fieldConfig.MaxLength = Conversion.TryCastInteger(row["character_maximum_length"]);

                        fieldConfig.ParentTableSchema = Conversion.TryCastString(row["references_schema"]);
                        fieldConfig.ParentTable = Conversion.TryCastString(row["references_table"]);
                        fieldConfig.ParentTableColumn = Conversion.TryCastString(row["references_field"]);

                        if (defaultValues.Rows.Count.Equals(1))
                        {
                            fieldConfig.DefaultValue =
                                Conversion.TryCastString(defaultValues.Rows[0][fieldConfig.ColumnName]);
                        }

                        if (editing)
                        {
                            if (!string.IsNullOrWhiteSpace(config.ExcludeEdit))
                            {
                                if (
                                    config.ExcludeEdit.Split(',')
                                        .Any(
                                            column =>
                                                column.Trim()
                                                    .ToUpperInvariant()
                                                    .Equals(fieldConfig.ColumnName.ToUpperInvariant())))
                                {
                                    fieldConfig.IsDisabled = true;
                                }
                            }
                        }


                        fields.Add(fieldConfig);
                    }
                }

                return fields;
            }
        }
    }
}