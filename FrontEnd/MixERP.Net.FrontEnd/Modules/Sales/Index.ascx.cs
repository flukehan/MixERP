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
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Sales
{
    public partial class Index : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.LoadWidget();
            base.OnControlLoad(sender, e);
        }

        public void LoadWidget()
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