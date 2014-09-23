using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;
using MixERP.Net.FrontEnd.Base;
using System;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;

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