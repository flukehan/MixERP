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

using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;
using MixERP.Net.FrontEnd.Base;

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
                    ColSpan = 2,
                    WidgetSource = "~/Modules/Sales/Widgets/SalesByOfficeWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 1,
                    ColumnNumber = 2,
                    ColSpan = 2,
                    WidgetSource = "~/Modules/Sales/Widgets/CurrentOfficeSalesByMonthWidget.ascx"
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
                    WidgetSource = "~/Modules/Sales/Widgets/TopSellingProductOfAllTimeWidget.ascx"
                },
                new WidgetModel
                {
                    RowNumber = 3,
                    ColumnNumber = 2,
                    ColSpan = 2,
                    WidgetSource = "~/Modules/Sales/Widgets/TopSellingProductOfAllTimeCurrentWidget.ascx"
                }
            };

            WidgetHelper.LoadWidgets(models, this.WidgetPlaceholder, this.Page);
        }
    }
}