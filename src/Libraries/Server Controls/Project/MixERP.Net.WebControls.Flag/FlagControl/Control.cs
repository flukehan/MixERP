using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Flag.Data;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Flag
{
    [ToolboxData("<{0}:Flag runat=server />")]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed partial class FlagControl : CompositeControl
    {
        private Panel container;
        private Button updateButton;

        protected override void CreateChildControls()
        {
            this.container = new Panel();
            this.Initialize();
            this.AddScript();
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.container.RenderControl(w);
        }

        private void BindFlagTypeDropDownList()
        {
            this.flagDropDownlist.DataSource = FlagType.GetFlagTypes(this.Catalog);
            this.flagDropDownlist.DataTextField = "FlagTypeName";
            this.flagDropDownlist.DataValueField = "FlagTypeId";
            this.flagDropDownlist.DataBind();
        }

        private void Initialize()
        {
            using (HtmlGenericControl div = new HtmlGenericControl("div"))
            {
                div.Attributes.Add("id", this.ID);
                div.Attributes.Add("style", "width:300px;z-index:2;");
                div.Attributes.Add("class", this.CssClass);

                using (HtmlGenericControl h3 = new HtmlGenericControl("h3"))
                {
                    h3.Attributes.Add("class", "panel-title");
                    h3.InnerText = Titles.FlagThisTransaction;
                    div.Controls.Add(h3);
                }

                using (HtmlGenericControl contents = new HtmlGenericControl("div"))
                {
                    contents.InnerText = Labels.FlagLabel;
                    div.Controls.Add(contents);
                }

                using (HtmlGenericControl selectParagraph = new HtmlGenericControl("p"))
                {
                    selectParagraph.InnerText = Labels.SelectAFlag;
                    div.Controls.Add(selectParagraph);
                }

                this.flagDropDownlist = new DropDownList();
                this.flagDropDownlist.ID = this.ID + "DropDownList";
                this.BindFlagTypeDropDownList();
                div.Controls.Add(this.flagDropDownlist);

                using (HtmlGenericControl buttonContainer = new HtmlGenericControl("p"))
                {
                    buttonContainer.Attributes.Add("class", "ui buttons");
                    this.updateButton = new Button();

                    this.updateButton.Text = Titles.Update;
                    this.updateButton.CssClass = "green small ui button";
                    this.updateButton.OnClientClick = this.OnClientClick;
                    this.updateButton.Click += this.ButtonOnClick;

                    buttonContainer.Controls.Add(this.updateButton);

                    using (HtmlInputButton anchor = new HtmlInputButton())
                    {
                        anchor.Attributes.Add("onclick", string.Format("$('#{0}').toggle(500);", this.ID));
                        anchor.Attributes.Add("class", "red small ui button");
                        anchor.Value = Titles.Close;

                        buttonContainer.Controls.Add(anchor);
                    }

                    div.Controls.Add(buttonContainer);
                }

                this.container.Controls.Add(div);
                this.Controls.Add(this.container);
            }
        }
    }
}