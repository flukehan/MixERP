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
            Collection<int> tranIdCollection = SalesOrder.GetTranIdCollection();
            long nonGlStockMasterId = MixERP.Net.BusinessLayer.Transactions.NonGLStockTransaction.Add("Sales.Order", SalesOrder.GetForm.Date, SalesOrder.GetForm.PartyCode, SalesOrder.GetForm.PriceTypeId, SalesOrder.GetForm.Details, SalesOrder.GetForm.ReferenceNumber, SalesOrder.GetForm.StatementReference, tranIdCollection);
            if (nonGlStockMasterId > 0)
            {
                Response.Redirect("~/Dashboard/Index.aspx?TranId=" + nonGlStockMasterId, true);
            }
        }
    }
}