using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.StockAdjustmentFactory.Resources;
using System.Text;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void AddInlineScript()
        {
            StringBuilder inline = new StringBuilder();
            inline.Append("var referencingSidesNotEqualErrorLocalized = '" + Errors.ReferencingSidesNotEqual + "';");
            inline.Append("var storeServiceUrl = '" + this.StoreServiceUrl + "';");
            inline.Append("var itemServiceUrl = '" + this.ItemServiceUrl + "';");
            inline.Append("var unitServiceUrl ='" + this.UnitServiceUrl + "';");
            inline.Append("var itemPopupUrl ='" + this.ItemPopupUrl + "';");
            inline.Append("var itemIdQuerySericeUrl ='" + this.ItemIdQuerySericeUrl + "';");
            inline.Append("var validateSides= " + this.ValidateSides.ToString().ToLower() + ";");

            Net.Common.PageUtility.RegisterJavascript("StockAdjustmentFormViewInlineScript", inline.ToString(), this.Page, true);
        }

        private void AddJavaScript()
        {
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.StockAdjustmentFactory.FormView.js", "party_control", typeof(FormView));
        }
    }
}
