/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

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

        public static HtmlAnchor GetAnchor(string cssClass, string text, string tabName, string icon)
        {
            using (HtmlAnchor a = new HtmlAnchor())
            {
                if (!string.IsNullOrWhiteSpace(icon))
                {
                    a.InnerHtml = "<i class='" + icon + "'></i>";
                }

                if (!string.IsNullOrWhiteSpace(text))
                {
                    a.InnerHtml += text;
                }

                if (!string.IsNullOrWhiteSpace(tabName))
                {
                    a.Attributes.Add("data-tab", tabName);
                }

                a.Attributes.Add("class", cssClass);
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
    }
}