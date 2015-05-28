using MixERP.Net.UI.ScrudFactory.Helpers;

namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    internal sealed class Field : ScrudLayout
    {
        internal Field(Config config, FieldConfig fieldConfig)
            : base(config)
        {
            this.FieldConfig = fieldConfig;
        }

        private FieldConfig FieldConfig { get; set; }

        public override string Get()
        {
            if (this.FieldConfig.IsSerial)
            {
                this.FieldConfig.IsDisabled = true;
            }


            if (string.IsNullOrWhiteSpace(this.FieldConfig.ColumnName))
            {
                return string.Empty;
            }

            ILayout layout = string.IsNullOrWhiteSpace(this.FieldConfig.ParentTableColumn)
                ? ScrudTypes.GetLayout(this.FieldConfig.DataType, this.Config, this.FieldConfig)
                : new Select(this.Config, this.FieldConfig);

            FormField field = new FormField(this.Config, this.FieldConfig, layout.Get());
            return field.Get();
        }
    }
}