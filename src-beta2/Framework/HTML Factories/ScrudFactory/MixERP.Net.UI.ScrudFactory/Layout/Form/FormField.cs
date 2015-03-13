using System.Text;

namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    internal sealed class FormField : ScrudLayout
    {
        public FormField(Config config, FieldConfig fieldConfig, string control) : base(config)
        {
            this.FieldConfig = fieldConfig;
            this.Control = control;
        }

        private FieldConfig FieldConfig { get; set; }
        private string Control { get; set; }

        public override string Get()
        {
            StringBuilder field = new StringBuilder();

            string label = this.FieldConfig.ColumnNameLocalized;

            if (!this.FieldConfig.IsNullable)
            {
                label += Resources.Titles.RequiredFieldIndicator;
            }

            Helpers.ScrudFormHelper.AddScrudFormRow(field, this.FieldConfig.ColumnName, label, this.Control);

            return field.ToString();
        }
    }
}