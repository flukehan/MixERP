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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.Common;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public sealed partial class StockTransactionForm
    {
        private DateTextBox dateTextBox;

        private bool disposed;
        private HiddenField itemCodeHidden;
        private HiddenField itemIdHidden;
        private HiddenField modeHiddenField;
        private HiddenField partyCodeHidden;
        private HtmlInputText partyCodeInputText;
        private HiddenField partyIdHidden;
        private HiddenField priceTypeIdHidden;
        private HiddenField productGridViewDataHidden;
        private HtmlInputText referenceNumberInputText;
        private HiddenField salesPersonIdHidden;
        private HiddenField shipperIdHidden;
        private HiddenField shippingAddressCodeHidden;
        private HiddenField storeIdHidden;
        private HtmlGenericControl title;
        private HiddenField tranIdCollectionHiddenField;
        private HiddenField unitIdHidden;
        private HiddenField unitNameHidden;
        private PlaceHolder placeHolder;

        
        public sealed override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }


        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.dateTextBox != null)
            {
                this.dateTextBox.Dispose();
                this.dateTextBox = null;
            }

            if (this.itemCodeHidden != null)
            {
                this.itemCodeHidden.Dispose();
                this.itemCodeHidden = null;
            }

            if (this.itemIdHidden != null)
            {
                this.itemIdHidden.Dispose();
                this.itemIdHidden = null;
            }

            if (this.modeHiddenField != null)
            {
                this.modeHiddenField.Dispose();
                this.modeHiddenField = null;
            }

            if (this.partyCodeHidden != null)
            {
                this.partyCodeHidden.Dispose();
                this.partyCodeHidden = null;
            }

            if (this.partyCodeInputText != null)
            {
                this.partyCodeInputText.Dispose();
                this.partyCodeInputText = null;
            }

            if (this.partyIdHidden != null)
            {
                this.partyIdHidden.Dispose();
                this.partyIdHidden = null;
            }

            if (this.priceTypeIdHidden != null)
            {
                this.priceTypeIdHidden.Dispose();
                this.priceTypeIdHidden = null;
            }

            if (this.productGridViewDataHidden != null)
            {
                this.productGridViewDataHidden.Dispose();
                this.productGridViewDataHidden = null;
            }

            if (this.referenceNumberInputText != null)
            {
                this.referenceNumberInputText.Dispose();
                this.referenceNumberInputText = null;
            }

            if (this.salesPersonIdHidden != null)
            {
                this.salesPersonIdHidden.Dispose();
                this.salesPersonIdHidden = null;
            }

            if (this.shipperIdHidden != null)
            {
                this.shipperIdHidden.Dispose();
                this.shipperIdHidden = null;
            }

            if (this.shippingAddressCodeHidden != null)
            {
                this.shippingAddressCodeHidden.Dispose();
                this.shippingAddressCodeHidden = null;
            }

            if (this.storeIdHidden != null)
            {
                this.storeIdHidden.Dispose();
                this.storeIdHidden = null;
            }

            if (this.title != null)
            {
                this.title.Dispose();
                this.title = null;
            }

            if (this.tranIdCollectionHiddenField != null)
            {
                this.tranIdCollectionHiddenField.Dispose();
                this.tranIdCollectionHiddenField = null;
            }

            if (this.unitIdHidden != null)
            {
                this.unitIdHidden.Dispose();
                this.unitIdHidden = null;
            }

            if (this.unitNameHidden != null)
            {
                this.unitNameHidden.Dispose();
                this.unitNameHidden = null;
            }

            if (this.placeHolder != null)
            {
                this.placeHolder.Dispose();
                this.placeHolder = null;
            }

            this.disposed = true;
        }

    }
}