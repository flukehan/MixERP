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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Sales.Data.Reports;
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Common;

namespace MixERP.Net.Core.Modules.Sales.Widgets
{
    public partial class SalesByGeographyWidget : MixERPWidget
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateGridView(this.Placeholder1);
            this.CreateWidget(this.Placeholder1);
            this.RegisterJavascriptVariables();
        }

        private void CreateGridView(Control container)
        {
            using (MixERPGridView grid = new MixERPGridView())
            {
                grid.ID = "SalesByGeographyGridView";
                grid.CssClass = "initially hidden";
                grid.DataSource = SalesByGeography.GetSalesByCountry();
                grid.DataBind();

                container.Controls.Add(grid);
            }
        }

        private void CreateWidget(Control container)
        {
            using (HtmlGenericControl widget = new HtmlGenericControl("div"))
            {
                widget.ID = "SalesByGeographyWidget";
                widget.Attributes.Add("class", "sixteen wide column widget");

                using (HtmlGenericControl segment = HtmlControlHelper.GetSegment())
                {
                    using (HtmlGenericControl header = new HtmlGenericControl("h2"))
                    {
                        header.Attributes.Add("class", "ui purple header");
                        header.InnerText = Titles.WorldSalesStatistics;
                        segment.Controls.Add(header);
                    }

                    using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
                    {
                        segment.Controls.Add(divider);
                    }

                    using (HtmlGenericControl mapContainer = new HtmlGenericControl("div"))
                    {
                        mapContainer.ID = "map-container";
                        mapContainer.Attributes.Add("style", "height: 400px; width: 980px;");
                        segment.Controls.Add(mapContainer);
                    }

                    widget.Controls.Add(segment);
                }

                container.Controls.Add(widget);
            }
        }

        private void RegisterJavascriptVariables()
        {
            string javascript = JSUtility.GetVar("totalSalesLocalized", Titles.TotalSales);
            javascript += JSUtility.GetVar("baseCurrencyCode", CurrentSession.GetBaseCurrency());

            PageUtility.RegisterJavascript("SalesByGeographyWidget_Localized", javascript, this.Page, true);
        }
    }
}