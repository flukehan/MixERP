using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.PartyControl
{
    public static class ControlHelper
    {
        public static HtmlTableRow GetNewRow(string text, string id, string tagName)
        {
            using (HtmlTableRow row = new HtmlTableRow())
            {
                using (HtmlTableCell cell = new HtmlTableCell())
                {
                    cell.Style.Add("width", "300px");
                    cell.InnerHtml = @"<strong>" + text + @"</strong>";
                    row.Cells.Add(cell);
                }

                using (HtmlTableCell cell = new HtmlTableCell())
                {
                    using (HtmlGenericControl element = new HtmlGenericControl())
                    {
                        element.ID = id;
                        element.TagName = tagName;
                        cell.Controls.Add(element);
                    }

                    row.Cells.Add(cell);
                }

                return row;
            }
        }

        public static string GetLabelText(string targetControlId, string text)
        {
            if (string.IsNullOrWhiteSpace(targetControlId))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            return string.Format(@"<label for='{0}'>{1}</label>", targetControlId, text);
        }

        public static HtmlAnchor GetAnchor(string href, string role, string dataToggle, string text)
        {
            using (HtmlAnchor a = new HtmlAnchor())
            {
                if (!string.IsNullOrWhiteSpace(href))
                {
                    a.HRef = href;
                }

                if (!string.IsNullOrWhiteSpace(role))
                {
                    a.Attributes.Add("role", role);
                }

                if (!string.IsNullOrWhiteSpace(dataToggle))
                {
                    a.Attributes.Add("data-toggle", dataToggle);
                }

                if (!string.IsNullOrWhiteSpace(text))
                {
                    a.InnerText = text;
                }

                return a;
            }
        }

        public static HtmlButton GetButton(string id, string cssClass, string text)
        {
            using (HtmlButton button = new HtmlButton())
            {
                button.Attributes.Add("type", "button");

                if (!string.IsNullOrWhiteSpace(id))
                {
                    button.ID = id;
                }

                if (!string.IsNullOrWhiteSpace(cssClass))
                {
                    button.Attributes.Add("class", cssClass);
                }

                button.InnerText = text;

                return button;
            }
        }

        public static HtmlInputText GetInputText(string id, string cssClass)
        {
            using (HtmlInputText input = new HtmlInputText())
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    input.ID = id;
                }

                if (!string.IsNullOrWhiteSpace(cssClass))
                {
                    input.Attributes.Add("class", cssClass);
                }

                return input;
            }
        }

        public static HtmlSelect GetSelect(string id, string cssClass)
        {
            using (HtmlSelect select = new HtmlSelect())
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    select.ID = id;
                }

                if (!string.IsNullOrWhiteSpace(cssClass))
                {
                    select.Attributes.Add("class", cssClass);
                }

                return select;
            }
        }

        public static HtmlGenericControl GetGenericControl(string tagName, string cssClass)
        {
            using (HtmlGenericControl genericControl = new HtmlGenericControl())
            {
                if (!string.IsNullOrWhiteSpace(tagName))
                {
                    genericControl.TagName = tagName;
                }

                if (!string.IsNullOrWhiteSpace(cssClass))
                {
                    genericControl.Attributes.Add("class", cssClass);
                }

                return genericControl;
            }
        }

        public static HtmlGenericControl GetGenericControl(string tagName, string cssClass, string role)
        {
            using (HtmlGenericControl genericControl = new HtmlGenericControl())
            {
                if (!string.IsNullOrWhiteSpace(tagName))
                {
                    genericControl.TagName = tagName;
                }

                if (!string.IsNullOrWhiteSpace(role))
                {
                    genericControl.Attributes.Add("role", role);
                }

                if (!string.IsNullOrWhiteSpace(cssClass))
                {
                    genericControl.Attributes.Add("class", cssClass);
                }

                return genericControl;
            }
        }
    }
}