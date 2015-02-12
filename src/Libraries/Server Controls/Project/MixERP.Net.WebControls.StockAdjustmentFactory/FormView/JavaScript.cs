using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.StockAdjustmentFactory.Resources;
using System.Globalization;
using System.Text;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void AddInlineScript()
        {
            string script = string.Empty;

            script += JSUtility.GetVar("referencingSidesNotEqualErrorLocalized", Errors.ReferencingSidesNotEqual);
            script += JSUtility.GetVar("storeServiceUrl", this.StoreServiceUrl);
            script += JSUtility.GetVar("itemServiceUrl", this.ItemServiceUrl);
            script += JSUtility.GetVar("unitServiceUrl", this.UnitServiceUrl);
            script += JSUtility.GetVar("itemPopupUrl", this.ItemPopupUrl);
            script += JSUtility.GetVar("itemIdQuerySericeUrl", this.ItemIdQuerySericeUrl);
            script += JSUtility.GetVar("validateSides", this.ValidateSides.ToString().ToLower(CultureInfo.InvariantCulture));

            Net.Common.PageUtility.RegisterJavascript("StockAdjustmentFormViewInlineScript", script, this.Page, true);
        }

        private void AddJavascript()
        {
            JSUtility.AddJSReference(this.Page, "MixERP.Net.WebControls.StockAdjustmentFactory.FormView.js", "party_control", typeof(FormView));
        }
    }
}