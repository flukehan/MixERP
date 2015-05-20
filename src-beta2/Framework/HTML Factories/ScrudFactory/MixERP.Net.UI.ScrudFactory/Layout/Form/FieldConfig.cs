namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    internal sealed class FieldConfig
    {
        internal string ResourceClassName { get; set; }
        internal string ItemSelectorPath { get; set; }
        internal string ColumnName { get; set; }
        internal string ColumnNameLocalized { get; set; }
        internal string DefaultValue { get; set; }
        internal bool IsSerial { get; set; }
        internal bool IsNullable { get; set; }
        internal string DataType { get; set; }
        internal string Domain { get; set; }
        internal int MaxLength { get; set; }
        internal string ParentTableSchema { get; set; }
        internal string ParentTable { get; set; }
        internal string ParentTableColumn { get; set; }
        internal string SelectedValues { get; set; }
        internal string ErrorCssClass { get; set; }
        internal bool IsDisabled { get; set; }
    }
}