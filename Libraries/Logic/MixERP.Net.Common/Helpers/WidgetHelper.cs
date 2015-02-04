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

using MixERP.Net.Common.Base;
using MixERP.Net.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace MixERP.Net.Common.Helpers
{
    public static class WidgetHelper
    {
        public static void LoadWidgets(IEnumerable<WidgetModel> widgetModels, Control placeholder, TemplateControl page)
        {
            if (placeholder == null)
            {
                return;
            }

            if (page == null)
            {
                return;
            }

            if (widgetModels == null)
            {
                return;
            }

            var groups = widgetModels.OrderBy(x => x.RowNumber).ThenBy(x => x.ColumnNumber).GroupBy(x => new { x.RowNumber });

            foreach (var group in groups)
            {
                foreach (WidgetModel item in group)
                {
                    using (MixERPWidgetBase widget = page.LoadControl(item.WidgetSource) as MixERPWidgetBase)
                    {
                        if (widget != null)
                        {
                            placeholder.Controls.Add(widget);
                            widget.OnControlLoad(widget, new EventArgs());
                        }
                    }
                }
            }
        }
    }
}