using System.Text;
using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    internal sealed class Radios : ScrudLayout
    {
        public Radios(Config config, FieldConfig fieldConfig)
            : base(config)
        {
            this.FieldConfig = fieldConfig;
        }

        private FieldConfig FieldConfig { get; set; }

        public override string Get()
        {
            StringBuilder list = new StringBuilder();

            TagBuilder.Begin(list, "div");
            TagBuilder.AddId(list, this.FieldConfig.ColumnName);
            TagBuilder.AddClass(list, "grouped inline fields");
            TagBuilder.AddAttribute(list, "data-scrud", "radio");
            TagBuilder.Close(list);

            TagBuilder.AddRadioField(list, this.FieldConfig.ColumnName, "yes", Resources.Titles.Yes,
                this.FieldConfig.DefaultValue.ToUpperInvariant().Equals("TRUE"), this.FieldConfig.IsDisabled);
            TagBuilder.AddRadioField(list, this.FieldConfig.ColumnName, "no", Resources.Titles.No,
                this.FieldConfig.DefaultValue.ToUpperInvariant().Equals("FALSE"), this.FieldConfig.IsDisabled);


            TagBuilder.EndTag(list, "div");

            return list.ToString();
        }
    }
}