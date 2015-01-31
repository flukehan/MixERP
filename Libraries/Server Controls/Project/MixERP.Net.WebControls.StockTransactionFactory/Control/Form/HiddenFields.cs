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
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {

        private void CreateHiddenFields(Control container)
        {
            ControlHelper.AddHiddenField(container, this.itemCodeHidden, "ItemCodeHidden");
            ControlHelper.AddHiddenField(container, this.itemIdHidden, "ItemIdHidden");
            ControlHelper.AddHiddenField(container, this.modeHiddenField, "ModeHiddenField");
            ControlHelper.AddHiddenField(container, this.partyCodeHidden, "PartyCodeHidden");
            ControlHelper.AddHiddenField(container, this.partyIdHidden, "PartyIdHidden");
            ControlHelper.AddHiddenField(container, this.productGridViewDataHidden, "ProductGridViewDataHidden");
            ControlHelper.AddHiddenField(container, this.priceTypeIdHidden, "PriceTypeIdHidden");
            ControlHelper.AddHiddenField(container, this.tranIdCollectionHiddenField, "TranIdCollectionHiddenField");
            ControlHelper.AddHiddenField(container, this.unitIdHidden, "UnitIdHidden");
            ControlHelper.AddHiddenField(container, this.unitNameHidden, "UnitNameHidden");
            ControlHelper.AddHiddenField(container, this.storeIdHidden, "StoreIdHidden");
            ControlHelper.AddHiddenField(container, this.shipperIdHidden, "ShipperIdHidden");
            ControlHelper.AddHiddenField(container, this.shippingAddressCodeHidden, "ShippingAddressCodeHidden");
            ControlHelper.AddHiddenField(container, this.salesPersonIdHidden, "SalesPersonIdHidden");

        }
    }
}