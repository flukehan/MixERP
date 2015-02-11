using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: WebResource("MixERP.Net.WebControls.TransactionViewFactory.TransactionViewFactory.js", "application/x-javascript")]

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    [ToolboxData("<{0}:TransactionView runat=server></{0}:TransactionView>")]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed partial class TransactionView : CompositeControl
    {
        private HtmlGenericControl panel;

        protected override void CreateChildControls()
        {
            this.panel = new HtmlGenericControl();

            this.AddHeading(this.panel);
            this.AddButtons(this.panel);
            this.AddTopPanel(this.panel);
            this.AddGridPanel(this.panel);
            this.AddHiddenFields(this.panel);
            this.AddFlagControl(this.panel);

            this.SetDefaultValues();
            this.BindGrid();
            this.AddJavascript();

            this.Controls.Add(this.panel);
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.panel.RenderControl(w);
        }
    }
}