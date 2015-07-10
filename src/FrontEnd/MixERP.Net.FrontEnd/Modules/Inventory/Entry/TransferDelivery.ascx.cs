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
using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Core.Modules.Inventory.Data.Transactions;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.StockAdjustmentFactory;

namespace MixERP.Net.Core.Modules.Inventory.Entry
{
    public partial class TransferDelivery : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            long tranId = Conversion.TryCastLong(this.Page.Request.QueryString["RequestId"]);

            if (!this.IsValidStockTransferRequestId(tranId))
            {
                using (HtmlGenericControl header = new HtmlGenericControl("h1"))
                {
                    header.InnerHtml = Warnings.AccessIsDenied;
                    this.Placeholder1.Controls.Add(header);

                    return;
                }
            }

            using (FormView form = new FormView())
            {
                form.Text = Titles.StockTransferDelivery;
                form.StoreServiceUrl = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
                form.ItemServiceUrl = "/Modules/Inventory/Services/ItemData.asmx/GetItems";
                form.UnitServiceUrl = "/Modules/Inventory/Services/ItemData.asmx/GetUnits";
                form.ItemPopupUrl = "/Modules/Inventory/Setup/ItemsPopup.mix?modal=1&CallBackFunctionName=loadItems&AssociatedControlId=ItemIdHidden";
                form.ItemIdQuerySericeUrl = "/Modules/Inventory/Services/ItemData.asmx/GetItemCodeByItemId";
                form.ShippingCompanyServiceUrl = "/Modules/Inventory/Services/ItemData.asmx/GetShippers";
                form.DisplayShipper = true;
                form.DisplaySourceStore = true;
                form.ValidateSides = false;
                form.HideSides = true;
                form.Catalog = AppUsers.GetCurrentUserDB();
                form.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

                this.Placeholder1.Controls.Add(form);
            }
        }

        private bool IsValidStockTransferRequestId(long requestId)
        {
            if (requestId <= 0)
            {
                return false;
            }

            return StockTransferRequest.IsValidStockTransferRequestId(AppUsers.GetCurrentUserDB(), requestId);
        }
    }
}