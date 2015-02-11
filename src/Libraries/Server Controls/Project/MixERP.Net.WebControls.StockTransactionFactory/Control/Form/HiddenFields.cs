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
            this.CreateItemCodeHidden(container);
            this.CreateItemIdHidden(container);
            this.CreateModeHidden(container);
            this.CreatePartyCodeHidden(container);
            this.CreatePartyIdHidden(container);
            this.CreateProductGridViewDataHidden(container);
            this.CreatePriceTypeIdHidden(container);
            this.CreateTranIdCollectionHidden(container);
            this.CreateUnitIdHidden(container);
            this.CreateUnitNameHidden(container);
            this.CreateStoreIdHidden(container);
            this.CreateShipperIdHidden(container);
            this.CreateShippingAddressCodeHidden(container);
            this.CreateSalesPersonIdHidden(container);
        }

        private void CreateItemCodeHidden(Control container)
        {
            this.itemCodeHidden = new HiddenField();
            this.itemCodeHidden.ID = "ItemCodeHidden";

            container.Controls.Add(this.itemCodeHidden);
        }

        private void CreateItemIdHidden(Control container)
        {
            this.itemIdHidden = new HiddenField();
            this.itemIdHidden.ID = "ItemIdHidden";

            container.Controls.Add(this.itemIdHidden);
        }

        private void CreateModeHidden(Control container)
        {
            this.modeHidden = new HiddenField();
            this.modeHidden.ID = "ModeHiddenField";

            container.Controls.Add(this.modeHidden);
        }

        private void CreatePartyCodeHidden(Control container)
        {
            this.partyCodeHidden = new HiddenField();
            this.partyCodeHidden.ID = "PartyCodeHidden";

            container.Controls.Add(this.partyCodeHidden);
        }

        private void CreatePartyIdHidden(Control container)
        {
            this.partyIdHidden = new HiddenField();
            this.partyIdHidden.ID = "PartyIdHidden";

            container.Controls.Add(this.partyIdHidden);
        }

        private void CreateProductGridViewDataHidden(Control container)
        {
            this.productGridViewDataHidden = new HiddenField();
            this.productGridViewDataHidden.ID = "ProductGridViewDataHidden";

            container.Controls.Add(productGridViewDataHidden);
        }

        private void CreatePriceTypeIdHidden(Control container)
        {
            this.priceTypeIdHidden = new HiddenField();
            this.priceTypeIdHidden.ID = "PriceTypeIdHidden";

            container.Controls.Add(this.priceTypeIdHidden);
        }

        private void CreateTranIdCollectionHidden(Control container)
        {
            this.tranIdCollectionHidden = new HiddenField();
            this.tranIdCollectionHidden.ID = "TranIdCollectionHiddenField";

            container.Controls.Add(this.tranIdCollectionHidden);
        }

        private void CreateUnitIdHidden(Control container)
        {
            this.unitIdHidden = new HiddenField();
            this.unitIdHidden.ID = "UnitIdHidden";

            container.Controls.Add(this.unitIdHidden);
        }

        private void CreateUnitNameHidden(Control container)
        {
            this.unitNameHidden = new HiddenField();
            this.unitNameHidden.ID = "UnitNameHidden";

            container.Controls.Add(this.unitNameHidden);
        }

        private void CreateStoreIdHidden(Control container)
        {
            this.storeIdHidden = new HiddenField();
            this.storeIdHidden.ID = "StoreIdHidden";

            container.Controls.Add(this.storeIdHidden);
        }

        private void CreateShipperIdHidden(Control container)
        {
            this.shipperIdHidden = new HiddenField();
            this.shipperIdHidden.ID = "ShipperIdHidden";

            container.Controls.Add(this.shipperIdHidden);
        }

        private void CreateShippingAddressCodeHidden(Control container)
        {
            this.shippingAddressCodeHidden = new HiddenField();
            this.shippingAddressCodeHidden.ID = "ShippingAddressCodeHidden";

            container.Controls.Add(this.shippingAddressCodeHidden);
        }

        private void CreateSalesPersonIdHidden(Control container)
        {
            this.salesPersonIdHidden = new HiddenField();
            this.salesPersonIdHidden.ID = "SalesPersonIdHidden";

            container.Controls.Add(this.salesPersonIdHidden);
        }

    }
}