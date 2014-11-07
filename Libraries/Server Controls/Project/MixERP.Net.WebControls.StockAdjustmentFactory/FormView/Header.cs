using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void CreateHeader()
        {
            using (HtmlGenericControl header = new HtmlGenericControl())
            {
                header.TagName = "h2";
                header.InnerText = this.Text;
                this.container.Controls.Add(header);
            }
        }

        private void AddRuler()
        {
            using (HtmlGenericControl divider = new HtmlGenericControl("div"))
            {
                divider.Attributes.Add("class", "ui divider");
                this.container.Controls.Add(divider);
            }
        }
    }
}
