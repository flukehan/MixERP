using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System.Globalization;
using System.Web.UI;

[assembly: WebResource("MixERP.Net.WebControls.StockAdjustmentFactory.FormView.js", "application/x-javascript")]
namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void AddInlineScript()
        {
            string script = string.Empty;

            script += JSUtility.GetVar("storeServiceUrl", this.StoreServiceUrl);
            script += JSUtility.GetVar("shippingCompanyServiceUrl", this.ShippingCompanyServiceUrl);
            script += JSUtility.GetVar("itemServiceUrl", this.ItemServiceUrl);
            script += JSUtility.GetVar("unitServiceUrl", this.UnitServiceUrl);
            script += JSUtility.GetVar("itemPopupUrl", this.ItemPopupUrl);
            script += JSUtility.GetVar("itemIdQuerySericeUrl", this.ItemIdQuerySericeUrl);
            script += JSUtility.GetVar("validateSides", this.ValidateSides.ToString().ToLower(CultureInfo.InvariantCulture));

            PageUtility.RegisterJavascript("StockAdjustmentFormViewInlineScript", script, this.Page, true);
        }

        private void AddJavascript()
        {
            JSUtility.AddJSReference(this.Page, "MixERP.Net.WebControls.StockAdjustmentFactory.FormView.js", "FormView", typeof (FormView));
        }
    }
}