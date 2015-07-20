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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Controls;
using MixERP.Net.i18n.Resources;
using System;

namespace MixERP.Net.Core.Modules.Sales.Widgets
{
    public partial class CurrentOfficeSalesByMonthWidget : MixERPWidget
    {
        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.PolicyBased; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            string catalog = AppUsers.GetCurrentUserDB();

            this.TitleLiteral.Text = Titles.SalesByMonthInThousands;

            this.SalesByMonthGridView.Attributes.Add("style", "display:none;");
            this.SalesByMonthGridView.DataSource = Data.Reports.SalesByMonth.GetSalesByOffice(catalog, officeId);
            this.SalesByMonthGridView.DataBind();
        }
    }
}