using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Data;
using MixERP.Net.UI.ScrudFactory.Helpers;
using MixERP.Net.UI.ScrudFactory.Layout.Form;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal class ScrudForm : ScrudLayout, IDisposable
    {
        internal ScrudForm(Config config)
            : base(config)
        {
            string id = HttpContext.Current.Request.QueryString["edit"];
            this.Values = new DataTable();

            if (!string.IsNullOrWhiteSpace(id))
            {
                this.Editing = true;

                using (
                    DataTable table = FormHelper.GetTable(this.Config.TableSchema, this.Config.Table,
                        this.Config.KeyColumn, id, this.Config.KeyColumn))
                {
                    if (table.Rows.Count.Equals(1))
                    {
                        this.Values = table;
                    }
                }
            }

            this.ScrudAssembly = config.ResourceAssembly;
        }

        private DataTable Values { get; set; }
        public Assembly ScrudAssembly { get; set; }
        private bool Editing { get; set; }

        public void Dispose()
        {
            if (this.Values != null)
            {
                this.Values.Dispose();
                this.Values = null;
            }
        }

        public override string Get()
        {
            StringBuilder form = new StringBuilder();

            TagBuilder.Begin(form, "form");
            TagBuilder.AddId(form, "FormPanel");
            TagBuilder.AddStyle(form, "display:none;");
            TagBuilder.AddClass(form, ConfigBuilder.GetFormPanelCssClass(this.Config));
            TagBuilder.Close(form);

            TagBuilder.Begin(form, "div");
            TagBuilder.AddClass(form, ConfigBuilder.GetFormCssClass(this.Config));
            TagBuilder.Close(form);

            TagBuilder.Begin(form, "div");
            TagBuilder.AddClass(form, ConfigBuilder.GetFormDescriptionCssClass(this.Config));
            TagBuilder.Close(form);

            form.Append(Resources.Titles.RequiredFieldDetails);

            TagBuilder.EndTag(form, "div");


            TagBuilder.Begin(form, "table");
            TagBuilder.AddAttribute(form, "role", "scrud");
            TagBuilder.Close(form);

            IEnumerable<FieldConfig> fields = FieldConfigHelper.GetFields(this.Values, this.Config, this.Editing);

            foreach (Field field in fields.Select(fieldConfig => new Field(this.Config, fieldConfig)))
            {
                form.Append(field.Get());
            }


            FormFooter footer = new FormFooter(this.Config);
            form.Append(footer.Get());

            TagBuilder.EndTag(form, "table");
            TagBuilder.EndTag(form, "div");
            TagBuilder.EndTag(form, "form");

            return form.ToString();
        }
    }
}