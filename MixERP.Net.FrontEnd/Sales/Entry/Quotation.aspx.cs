/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Sales.Entry
{
    public partial class Quotation : MixERP.Net.BusinessLayer.MixERPWebPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "~/Sales/Quotation.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SalesQuotation_SaveButtonClick(object sender, EventArgs e)
        {
            //DateTime valueDate = MixERP.Net.Common.Conversion.TryCastDate(SalesQuotation.GetForm.DateTextBox.Text);
            //string partyCode = SalesQuotation.GetForm.PartyDropDownList.SelectedItem.Value;
            //int priceTypeId = MixERP.Net.Common.Conversion.TryCastInteger(SalesQuotation.GetForm.PriceTypeDropDownList.SelectedItem.Value);
            //GridView grid = SalesQuotation.GetForm.Grid;
            //string referenceNumber = SalesQuotation.GetForm.ReferenceNumberTextBox.Text;
            //string statementReference = SalesQuotation.GetForm.StatementReferenceTextBox.Text;
            //Collection<int> tranIdCollection = SalesQuotation.GetTranIdCollection();

            //long nonGlStockMasterId = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.Add("Sales.Quotation", valueDate, partyCode, priceTypeId, grid, referenceNumber, statementReference, tranIdCollection);
            //if(nonGlStockMasterId > 0)
            //{
            //    Response.Redirect("~/Sales/Quotation.aspx?TranId=" + nonGlStockMasterId, true);
            //}

            Collection<int> tranIdCollection = SalesQuotation.GetTranIdCollection();

            long nonGlStockMasterId = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.Add("Sales.Quotation", SalesQuotation.GetForm.Date, SalesQuotation.GetForm.PartyCode, SalesQuotation.GetForm.PriceTypeId, SalesQuotation.GetForm.Details, SalesQuotation.GetForm.ReferenceNumber, SalesQuotation.GetForm.StatementReference, tranIdCollection);
            if (nonGlStockMasterId > 0)
            {
                Response.Redirect("~/Sales/Quotation.aspx?TranId=" + nonGlStockMasterId, true);
            }

        }
    }
}