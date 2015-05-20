using System.Text;
using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Helpers
{
    internal static class ScrudFormHelper
    {
        internal static void AddScrudFormRow(StringBuilder builder, string targetControlId, string label, string control)
        {
            TagBuilder.Begin(builder, "tr", true);
            TagBuilder.Begin(builder, "td");
            TagBuilder.AddClass(builder, "label-cell");
            TagBuilder.Close(builder);

            TagBuilder.Begin(builder, "label");
            TagBuilder.AddAttribute(builder, "for", targetControlId);
            TagBuilder.Close(builder);

            builder.Append(label);

            TagBuilder.EndTag(builder, "label");

            TagBuilder.EndTag(builder, "td");

            TagBuilder.Begin(builder, "td");
            TagBuilder.AddClass(builder, "control-cell");
            TagBuilder.Close(builder);

            builder.Append(control);

            TagBuilder.EndTag(builder, "td");
            TagBuilder.EndTag(builder, "tr");
        }
    }
}