using MixERP.Net.BusinessLayer;

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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Dashboard
{
    public partial class Index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Todo:Store this in database.
            Collection<WidgetModel> models = new Collection<WidgetModel>
            {
                new WidgetModel
                {
                    RowNumber = 1,
                    ColumnNumber = 1,
                    ColSpan = 2,
                    WidgetSource = "~/UserControls/Widgets/SalesByOfficeWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 1,
                    ColumnNumber = 2,
                    ColSpan = 2,
                    WidgetSource = "~/UserControls/Widgets/CurrentOfficeSalesByMonthWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 4,
                    WidgetSource = "~/UserControls/Widgets/WorkflowWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 1,
                    WidgetSource = "~/UserControls/Widgets/OfficeInformationWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 3,
                    WidgetSource = "~/UserControls/Widgets/AlertsWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 2,
                    WidgetSource = "~/UserControls/Widgets/LinksWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 3,
                    ColumnNumber = 1,
                    ColSpan = 2,
                    WidgetSource = "~/UserControls/Widgets/TopSellingProductOfAllTimeWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 3,
                    ColumnNumber = 2,
                    ColSpan = 2,
                    WidgetSource = "~/UserControls/Widgets/TopSellingProductOfAllTimeCurrentWidget.ascx"
                }
            };

            this.LoadWidgets(models, this.WidgetPlaceholder);
        }

        private void LoadWidgets(IEnumerable<WidgetModel> widgetModels, PlaceHolder placeholder)
        {
            if (placeholder == null)
            {
                return;
            }

            //
            var groups = widgetModels.OrderBy(x => x.RowNumber).ThenBy(x => x.ColumnNumber).GroupBy(x => new { x.RowNumber });

            foreach (var group in groups)
            {
                foreach (var item in group)
                {
                    using (HtmlGenericControl div = new HtmlGenericControl())
                    {
                        div.TagName = "div";
                        div.Attributes.Add("class", "sortable-item col-md-" + 12 / group.Count());

                        if (item.ColSpan > 1)
                        {
                            div.Attributes.Add("data-ss-colspan", item.ColSpan.ToString(CultureInfo.CurrentUICulture));
                        }

                        using (MixERPWidget widget = this.LoadControl(item.WidgetSource) as MixERPWidget)
                        {
                            if (widget != null)
                            {
                                div.Controls.Add(widget);
                            }
                        }

                        placeholder.Controls.Add(div);
                    }
                }
            }
        }
    }
}