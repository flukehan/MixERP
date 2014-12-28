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

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Common.Helpers
{
    public static class GridViewHelper
    {
        public static void AddDataBoundControl(GridView grid, string dataField, string headerText, string dataFormatString = "")
        {
            BoundField field = new BoundField();
            field.DataField = dataField;
            field.HeaderText = headerText;
            field.DataFormatString = dataFormatString;

            grid.Columns.Add(field);
        }

        public static void SetHeaderRow(GridView grid)
        {
            if (grid == null)
            {
                return;
            }

            if (grid.Rows.Count.Equals(0))
            {
                return;
            }

            if (grid.HeaderRow == null)
            {
                return;
            }

            grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        public class GridViewSelectTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                using (HtmlGenericControl toggleCheckBox = new HtmlGenericControl("div"))
                {
                    toggleCheckBox.Attributes.Add("class", "ui toggle checkbox");

                    using (HtmlInputCheckBox checkBox = new HtmlInputCheckBox())
                    {
                        toggleCheckBox.Controls.Add(checkBox);
                    }

                    using (HtmlGenericControl label = new HtmlGenericControl("label"))
                    {
                        toggleCheckBox.Controls.Add(label);
                    }

                    container.Controls.Add(toggleCheckBox);
                }
            }
        }
    }
}