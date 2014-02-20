/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MixERP.Net.BusinessLayer;
using MixERP.Net.Common;
using System.Collections.ObjectModel;

namespace MixERP.Net.FrontEnd.Dashboard
{
    public partial class Index : MixERPWebpage
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
                    SizeX = 4,
                    SizeY = 2,
                    WidgetSource = "~/UserControls/Widgets/SalesByOfficeWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 1,
                    ColumnNumber = 2,
                    SizeX = 4,
                    SizeY = 2,
                    WidgetSource = "~/UserControls/Widgets/CurrentOfficeSalesByMonthWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 4,
                    SizeX = 2,
                    SizeY = 2,
                    WidgetSource = "~/UserControls/Widgets/WorkflowWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 1,
                    SizeX = 2,
                    SizeY = 2,
                    WidgetSource = "~/UserControls/Widgets/OfficeInformationWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 3,
                    SizeX = 2,
                    SizeY = 2,
                    WidgetSource = "~/UserControls/Widgets/AlertsWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 2,
                    ColumnNumber = 2,
                    SizeX = 2,
                    SizeY = 2,
                    WidgetSource = "~/UserControls/Widgets/LinksWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 3,
                    ColumnNumber = 1,
                    SizeX = 4,
                    SizeY = 2,
                    WidgetSource = "~/UserControls/Widgets/TopSellingProductOfAllTimeWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 3,
                    ColumnNumber = 2,
                    SizeX = 4,
                    SizeY = 2,
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

            using (HtmlGenericControl div = new HtmlGenericControl())
            {
                div.TagName = "div";
                div.Attributes.Add("class", "gridster ready");

                using (HtmlGenericControl ul = new HtmlGenericControl())
                {
                    ul.TagName = "ul";
                    ul.Attributes.Add("style", "position: relative;");

                    foreach (WidgetModel widgetModel in widgetModels.OrderBy(x=>x.RowNumber).ThenBy(x=>x.ColumnNumber))
                    {
                        using (HtmlGenericControl li = new HtmlGenericControl())
                        {
                            li.TagName = "li";
                            li.Attributes.Add("data-row", Conversion.TryCastString(widgetModel.RowNumber));
                            li.Attributes.Add("data-col", Conversion.TryCastString(widgetModel.ColumnNumber));
                            li.Attributes.Add("class", "gs_w");
                            li.Attributes.Add("data-sizex", Conversion.TryCastString(widgetModel.SizeX));
                            li.Attributes.Add("data-sizey", Conversion.TryCastString(widgetModel.SizeY));

                            using (MixERPWidget widget = this.LoadControl(widgetModel.WidgetSource) as MixERPWidget)
                            {
                                if (widget != null)
                                {
                                    li.Controls.Add(widget);                                    
                                }
                            }

                            ul.Controls.Add(li);
                        }
                    }

                    div.Controls.Add(ul);
                }

                placeholder.Controls.Add(div);
            }
        }

    }
}