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
using MixERP.Net.BusinessLayer;
using Resources;

namespace MixERP.Net.FrontEnd.Purchase
{
    public partial class DirectPurchase : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Purchase_SaveButtonClick(object sender, EventArgs e)
        {
            bool isCredit = this.DirectPurchaseControl.GetForm.TransactionType.Equals(Titles.Credit);

            long transactionMasterId = BusinessLayer.Transactions.DirectPurchase.Add(this.DirectPurchaseControl.GetForm.Date, this.DirectPurchaseControl.GetForm.StoreId, isCredit, this.DirectPurchaseControl.GetForm.PartyCode, this.DirectPurchaseControl.GetForm.Details, this.DirectPurchaseControl.GetForm.CashRepositoryId, this.DirectPurchaseControl.GetForm.CostCenterId, this.DirectPurchaseControl.GetForm.ReferenceNumber, this.DirectPurchaseControl.GetForm.StatementReference);
            if(transactionMasterId > 0)
            {
                this.Response.Redirect("~/Purchase/Confirmation/DirectPurchase.aspx?TranId=" + transactionMasterId, true);
            }
        }

    }
}