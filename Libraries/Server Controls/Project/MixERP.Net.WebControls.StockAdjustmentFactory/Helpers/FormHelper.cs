using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockAdjustmentFactory.Helpers
{
    internal class FormHelper
    {
        internal static HtmlGenericControl GetFields()
        {
            using (HtmlGenericControl row = new HtmlGenericControl())
            {
                row.TagName = "div";
                row.Attributes.Add("class", "inline fields");

                return row;
            }
        }

        internal static HtmlGenericControl GetField()
        {
            using (HtmlGenericControl formGroup = new HtmlGenericControl())
            {
                formGroup.TagName = "div";
                formGroup.Attributes.Add("class", "small field");

                return formGroup;
            }
        }
    }
}
