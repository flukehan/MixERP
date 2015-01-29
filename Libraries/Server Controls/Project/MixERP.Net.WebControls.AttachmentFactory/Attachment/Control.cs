using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.AttachmentFactory
{
    [ToolboxData("<{0}:Attachment runat=server></{0}:DateTextBox>")]
    public sealed partial class Attachment : CompositeControl
    {
        protected override void CreateChildControls()
        {
            this.placeHolder = new PlaceHolder();
            this.CreateUploadPanel(this.placeHolder);
            this.CreateButtons(this.placeHolder);
            this.CreateWarningLabel(this.placeHolder);
            this.CreateHiddenFields(this.placeHolder);
            this.RegisterJavascript();
            this.AddJavascript();
            this.CheckPermission();
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.placeHolder.RenderControl(w);
        }
    }
}