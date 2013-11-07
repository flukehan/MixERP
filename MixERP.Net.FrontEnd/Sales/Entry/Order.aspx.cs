using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Sales.Entry
{
    public partial class Order : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.OverridePath = "/Sales/Order.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void SalesOrder_SaveButtonClick(object sender, EventArgs e)
        {
            DateTime valueDate = MixERP.Net.Common.Conversion.TryCastDate(SalesOrder.GetForm.DateTextBox.Text);
            string partyCode = SalesOrder.GetForm.PartyDropDownList.SelectedItem.Value;
            int priceTypeId = MixERP.Net.Common.Conversion.TryCastInteger(SalesOrder.GetForm.PriceTypeDropDownList.SelectedItem.Value);
            GridView grid = SalesOrder.GetForm.Grid;
            string referenceNumber = SalesOrder.GetForm.ReferenceNumberTextBox.Text;
            string statementReference = SalesOrder.GetForm.StatementReferenceTextBox.Text;
            Collection<int> tranIdCollection = SalesOrder.GetTranIdCollection();

            long nonGlStockMasterId = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.Add("Sales.Order", valueDate, partyCode, priceTypeId, grid, referenceNumber, statementReference, tranIdCollection);
            if (nonGlStockMasterId > 0)
            {
                Response.Redirect("~/Dashboard/Index.aspx?TranId=" + nonGlStockMasterId, true);
            }
        }
    }
}