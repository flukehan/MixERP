using System;
using System.Text;

namespace MixERP.Net.Framework
{
    public static class TagBuilder
    {
        public static string GetDivider()
        {
            return "<div class='ui divider'></div>";
        }

        public static string GetDiv(string cssClass, string innerText)
        {
            StringBuilder div = new StringBuilder();

            Begin(div, "div");
            AddClass(div, cssClass);
            Close(div);

            div.Append(innerText);

            EndTag(div, "div");

            return div.ToString();
        }

        public static string GetDiv(string id, string cssClass, string innerText)
        {
            StringBuilder div = new StringBuilder();

            Begin(div, "div");

            AddClass(div, cssClass);
            AddId(div, id);

            Close(div);

            div.Append(innerText);

            EndTag(div, "div");

            return div.ToString();
        }

        public static string GetHtmlButton(string title, string onclick, string value, string cssClass,
            string iconCssClass)
        {
            StringBuilder button = new StringBuilder();

            Begin(button, "button");
            AddType(button, "button");
            AddClass(button, cssClass);
            AddTitle(button, title);
            AddAttribute(button, "onclick", onclick);
            Close(button);

            if (!String.IsNullOrWhiteSpace(iconCssClass))
            {
                AddIcon(button, iconCssClass);
            }

            button.Append(value);
            EndTag(button, "button");

            return button.ToString();
        }

        public static void AddAttribute(StringBuilder builder, string key, object value)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return;
            }

            builder.Append(" ");
            builder.Append(key);

            if (value == null)
            {
                return;
            }
            
            builder.Append("='");
            builder.Append(System.Net.WebUtility.HtmlEncode(value.ToString()));
            builder.Append("'");
        }

        public static void AddIcon(StringBuilder builder, string cssClass)
        {
            Begin(builder, "i");
            AddClass(builder, cssClass);
            Close(builder);
            EndTag(builder, "i");
        }



        public static void AddId(StringBuilder builder, string value)
        {
            AddAttribute(builder, "id", value);
        }


        public static void AddDisabled(StringBuilder builder)
        {
            AddAttribute(builder, "disabled", "disabled");
        }

        public static void AddType(StringBuilder builder, string value)
        {
            AddAttribute(builder, "type", value);
        }

        public static void AddValue(StringBuilder builder, string value)
        {
            AddAttribute(builder, "value", value);
        }

        public static void AddTitle(StringBuilder builder, string value)
        {
            AddAttribute(builder, "title", value);
        }

        public static void AddClass(StringBuilder builder, string value)
        {
            AddAttribute(builder, "class", value);
        }

        public static void AddRequired(StringBuilder builder)
        {
            AddAttribute(builder, "required", null);
        }

        public static void AddSelected(StringBuilder builder)
        {
            AddAttribute(builder, "selected", null);
        }


        public static void AddRadioField(StringBuilder builder, string id, string value, string text, bool selected = false, bool disabled = false)
        {
            Begin(builder, "div");
            AddClass(builder, "field");
            Close(builder);

            Begin(builder, "div");
            AddClass(builder, "ui radio checkbox");
            Close(builder);


            Begin(builder, "input");
            AddType(builder, "radio");
            AddAttribute(builder, "name", id);

            AddAttribute(builder, "value", value);

            if (selected)
            {
                AddAttribute(builder, "checked", "checked");
            }

            if (disabled)
            {
                AddAttribute(builder, "disabled", "disabled");
            }

            Close(builder, true);


            Begin(builder, "label");
            Close(builder);
            builder.Append(text);
            EndTag(builder, "label");



            EndTag(builder, "div");
            EndTag(builder, "div");
        }

        public static void AddStyle(StringBuilder builder, string value)
        {
            AddAttribute(builder, "style", value);
        }

        public static void Begin(StringBuilder builder, string tagName, bool close = false)
        {
            builder.Append("<" + tagName);
            if (close)
            {
                builder.Append(">");
            }
        }

        public static void Close(StringBuilder builder, bool isSelfClosing = false)
        {
            if (isSelfClosing)
            {
                builder.Append(" />");
                return;
            }

            builder.Append(">");
        }

        public static void EndTag(StringBuilder builder, string tagName)
        {
            builder.Append("</" + tagName + ">");
        }
    }
}