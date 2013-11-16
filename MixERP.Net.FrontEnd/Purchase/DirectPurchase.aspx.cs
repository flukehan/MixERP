/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Purchase
{
    public partial class DirectPurchase : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Purchase_SaveButtonClick(object sender, EventArgs e)
        {
            bool isCredit = DirectPurchaseControl.GetForm.TransactionType.Equals(Resources.Titles.Credit); ;

            long transactionMasterId = MixERP.Net.BusinessLayer.Transactions.DirectPurchase.Add(DirectPurchaseControl.GetForm.Date, DirectPurchaseControl.GetForm.StoreId, isCredit, DirectPurchaseControl.GetForm.PartyCode, DirectPurchaseControl.GetForm.Details, DirectPurchaseControl.GetForm.CashRepositoryId, DirectPurchaseControl.GetForm.CostCenterId, DirectPurchaseControl.GetForm.ReferenceNumber, DirectPurchaseControl.GetForm.StatementReference);
            if(transactionMasterId > 0)
            {
                Response.Redirect("~/Purchase/Confirmation/DirectPurchase.aspx?TranId=" + transactionMasterId, true);
            }
        }

    }
}