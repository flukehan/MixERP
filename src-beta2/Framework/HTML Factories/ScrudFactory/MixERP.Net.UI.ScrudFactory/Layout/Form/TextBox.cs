using System;
using System.Text;
using MixERP.Net.Common;
using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    internal sealed class TextBox : ScrudLayout
    {
        public TextBox(Config config, FieldConfig fieldConfig, string validationType = "")
            : base(config)
        {
            this.FieldConfig = fieldConfig;
            this.ValidationType = validationType;
        }

        private FieldConfig FieldConfig { get; set; }
        private string ValidationType { get; set; }

        public override string Get()
        {
            if (string.IsNullOrWhiteSpace(this.FieldConfig.ColumnName))
            {
                return string.Empty;
            }

            StringBuilder textBox = new StringBuilder();
            string type = "text";


            bool isPasswordField = this.FieldConfig.ColumnName.ToUpperInvariant().Equals("PASSWORD");

            TagBuilder.Begin(textBox, "input");
            TagBuilder.AddId(textBox, this.FieldConfig.ColumnName);
            TagBuilder.AddAttribute(textBox, "data-scrud", "text");

            if (isPasswordField && this.FieldConfig.IsDisabled)
            {
                type = "password";
                this.FieldConfig.DefaultValue = "fake-password";
            }

            if (this.FieldConfig.IsDisabled)
            {
                textBox.Append(" readonly='readonly'");
            }

            if (!string.IsNullOrWhiteSpace(this.ValidationType))
            {
                TagBuilder.AddAttribute(textBox, "data-vtype", this.ValidationType);
            }


            TagBuilder.AddType(textBox, type);

            if (this.FieldConfig.DataType.ToUpperInvariant().Equals("COLOR"))
            {
                TagBuilder.AddClass(textBox, "color");
            }

            if (!this.FieldConfig.DefaultValue.StartsWith("nextVal", StringComparison.OrdinalIgnoreCase))
            {
                if (this.ValidationType == "date")
                {
                    DateTime date = Conversion.TryCastDate(this.FieldConfig.DefaultValue);

                    if (date != DateTime.MinValue)
                    {
                        TagBuilder.AddValue(textBox, date.Date == date ? date.ToString("d") : date.ToString("f"));
                    }

                    TagBuilder.AddClass(textBox, "date");
                }
                else
                {
                    TagBuilder.AddValue(textBox, this.FieldConfig.DefaultValue);
                }
            }


            if (!this.FieldConfig.IsNullable)
            {
                TagBuilder.AddRequired(textBox);
            }

            if (this.FieldConfig.MaxLength > 0)
            {
                TagBuilder.AddAttribute(textBox, "maxlength", this.FieldConfig.MaxLength);
            }

            TagBuilder.Close(textBox, true);

            return textBox.ToString();
        }
    }
}