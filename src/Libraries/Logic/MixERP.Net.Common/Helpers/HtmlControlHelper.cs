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

using System;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.Common.Helpers
{
    public static class HtmlControlHelper
    {
        public static HtmlGenericControl GetDivider()
        {
            using (HtmlGenericControl divider = new HtmlGenericControl("div"))
            {
                divider.Attributes.Add("class", "ui divider");

                return divider;
            }
        }

        public static HtmlGenericControl GetField()
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "field");

                return field;
            }
        }

        public static HtmlGenericControl GetField(string cssClass)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", cssClass);

                return field;
            }
        }

        public static HtmlGenericControl GetFields()
        {
            using (HtmlGenericControl fields = new HtmlGenericControl("div"))
            {
                fields.Attributes.Add("class", "fields");

                return fields;
            }
        }

        public static HtmlGenericControl GetFields(string cssClass)
        {
            using (HtmlGenericControl fields = new HtmlGenericControl("div"))
            {
                fields.Attributes.Add("class", cssClass);

                return fields;
            }
        }

        public static HtmlGenericControl GetForm()
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "ui form");

                return field;
            }
        }

        public static HtmlGenericControl GetFormSegment()
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "ui form segment");

                return field;
            }
        }

        public static HtmlGenericControl GetFormSegment(string cssClass)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", cssClass);

                return field;
            }
        }

        public static HtmlGenericControl GetIcon(string cssClass)
        {
            using (HtmlGenericControl field = new HtmlGenericControl("i"))
            {
                field.Attributes.Add("class", cssClass);

                return field;
            }
        }

        public static HtmlGenericControl GetInlineFields()
        {
            using (HtmlGenericControl fields = new HtmlGenericControl("div"))
            {
                fields.Attributes.Add("class", "inline fields");

                return fields;
            }
        }

        public static HtmlGenericControl GetLabel(string text)
        {
            using (HtmlGenericControl label = new HtmlGenericControl("label"))
            {
                label.InnerText = text;
                return label;
            }
        }

        public static HtmlGenericControl GetLabel(string text, string targetControlId)
        {
            using (HtmlGenericControl label = new HtmlGenericControl("label"))
            {
                label.Attributes.Add("for", targetControlId);
                label.InnerText = text;
                return label;
            }
        }

        public static HtmlGenericControl GetPageHeader(string text)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("div"))
            {
                using (HtmlGenericControl h1 = new HtmlGenericControl("h1"))
                {
                    h1.Attributes.Add("class", "ui purple header");
                    h1.InnerText = text;

                    header.Controls.Add(h1);
                }

                using (HtmlGenericControl divider = GetDivider())
                {
                    header.Controls.Add(divider);
                }

                return header;
            }
        }

        public static string GetLabelHtml(string text)
        {
            return String.Format(CultureInfo.InvariantCulture, "<label>{0}</label>", text);
        }

        public static string GetLabelHtml(string text, string targetControlId)
        {
            return String.Format(CultureInfo.InvariantCulture, "<label for='{1}'>{0}</label>", text, targetControlId);
        }

        public static HtmlGenericControl GetLeftIconInput()
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "ui left icon input");

                return field;
            }
        }

        public static HtmlGenericControl GetModal()
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "ui modal");

                return field;
            }
        }

        public static HtmlGenericControl GetSegment()
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "ui segment");

                return field;
            }
        }

        public static void AddListItem(HtmlGenericControl container, string key, object value)
        {
            using (HtmlGenericControl li = new HtmlGenericControl("li"))
            {
                li.InnerText = key + " : " + value;
                container.Controls.Add(li);
            }

        }


        public static HtmlGenericControl GetToggleCheckBox()
        {
            using (HtmlGenericControl toggleCheckBox = new HtmlGenericControl("div"))
            {
                toggleCheckBox.Attributes.Add("class", "ui toggle checkbox");

                return toggleCheckBox;
            }
        }
    }
}