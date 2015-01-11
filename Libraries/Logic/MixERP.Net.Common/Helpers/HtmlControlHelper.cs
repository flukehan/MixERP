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

using System.Globalization;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.Common.Helpers
{
    public static class HtmlControlHelper
    {
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

        public static HtmlGenericControl GetFormSegment()
        {
            using (HtmlGenericControl field = new HtmlGenericControl("div"))
            {
                field.Attributes.Add("class", "ui form segment");

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

        public static string GetLabelHtml(string text)
        {
            return string.Format(CultureInfo.InvariantCulture, "<label>{0}</label>", text);
        }

        public static string GetLabelHtml(string text, string targetControlId)
        {
            return string.Format(CultureInfo.InvariantCulture, "<label for='{1}'>{0}</label>", text, targetControlId);
        }
    }
}