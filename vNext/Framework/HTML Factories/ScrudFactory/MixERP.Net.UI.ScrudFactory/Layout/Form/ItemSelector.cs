using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Data;

namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    internal sealed class ItemSelector : ScrudLayout
    {
        public ItemSelector(Config config, FieldConfig fieldConfig)
            : base(config)
        {
            this.FieldConfig = fieldConfig;
            this.InitializeRelation();
            this.Initialize();
        }

        private string CssClass { get; set; }
        private string HtmlRole { get; set; }
        private string Schema { get; set; }
        private string View { get; set; }
        private string HRef { get; set; }
        private int TabIndex { get; set; }
        private FieldConfig FieldConfig { get; set; }

        private void InitializeRelation()
        {
            string relation = Helpers.Expression.GetExpressionValue(this.Config.DisplayViews,
                this.FieldConfig.ParentTableSchema, this.FieldConfig.ParentTable, this.FieldConfig.ParentTableColumn);

            this.Schema = relation.Split('.').First();
            this.View = relation.Split('.').Last();

            //Sanitize the schema and the view
            this.Schema = Sanitizer.SanitizeIdentifierName(this.Schema);
            this.View = Sanitizer.SanitizeIdentifierName(this.View);
        }

        private void Initialize()
        {
            this.CssClass = ConfigBuilder.GetItemSelectorAnchorCssClass(this.Config);
            this.HtmlRole = "item-selector";

            string path = ConfigBuilder.GetItemSelectorPath(this.Config);

            path += "?Schema={0}&View={1}&AssociatedControlId={2}&Assembly={3}&ResourceClassName={4}";

            path = string.Format(CultureInfo.InvariantCulture, path, this.Schema, this.View, this.FieldConfig.ColumnName,
                this.Config.ResourceAssembly, ConfigBuilder.GetResourceClassName(this.Config));

            this.HRef = UrlHelper.GenerateContentUrl(path, new HttpContextWrapper(HttpContext.Current));
            this.TabIndex = 2;
        }

        public override string Get()
        {
            StringBuilder selector = new StringBuilder();

            TagBuilder.Begin(selector, "a");

            TagBuilder.AddClass(selector, this.CssClass);

            TagBuilder.AddAttribute(selector, "role", this.HtmlRole);
            TagBuilder.AddAttribute(selector, "tabindex", this.TabIndex);
            TagBuilder.AddAttribute(selector, "data-title", this.FieldConfig.ColumnNameLocalized);
            TagBuilder.AddAttribute(selector, "href", this.HRef);

            if (this.FieldConfig.IsDisabled)
            {
                TagBuilder.AddStyle(selector, "pointer-events:none;");
            }

            TagBuilder.Close(selector);
            TagBuilder.EndTag(selector, "a");

            return selector.ToString();
        }
    }
}