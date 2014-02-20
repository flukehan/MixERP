/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
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