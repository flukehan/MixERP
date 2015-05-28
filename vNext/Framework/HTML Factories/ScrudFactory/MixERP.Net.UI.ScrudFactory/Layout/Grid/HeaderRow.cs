using System.Collections.Generic;
using System.Linq;
using System.Text;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Helpers;

namespace MixERP.Net.UI.ScrudFactory.Layout.Grid
{
    internal sealed class HeaderRow : ScrudLayout
    {
        public HeaderRow(Config config, IEnumerable<Column> columns)
            : base(config)
        {
            this.Columns = columns;
        }

        private IEnumerable<Column> Columns { get; set; }

        public override string Get()
        {
            StringBuilder header = new StringBuilder();
            TagBuilder.Begin(header, "thead", true);
            TagBuilder.Begin(header, "tr", true);
            TagBuilder.Begin(header, "th", true);
            TagBuilder.EndTag(header, "th");

            foreach (Column column in this.Columns)
            {
                string cssClass = string.Empty;

                if ((new[] {"System.Single", "System.Double", "System.Decimal"}).Contains(column.Type.FullName))
                {
                    cssClass = " class='text right'";
                }

                TagBuilder.Begin(header, "th");
                header.Append(cssClass);
                TagBuilder.Close(header);


                header.Append(ScrudLocalizationHelper.GetResourceString(this.Config.ResourceAssembly,
                    ConfigBuilder.GetResourceClassName(this.Config), column.ColumnName));
                TagBuilder.EndTag(header, "th");
            }


            TagBuilder.EndTag(header, "tr");
            TagBuilder.EndTag(header, "thead");

            return header.ToString();
        }
    }
}