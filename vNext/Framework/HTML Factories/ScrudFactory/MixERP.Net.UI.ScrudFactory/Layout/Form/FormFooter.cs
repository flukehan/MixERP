using System.Text;
using System.Web;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Resources;

namespace MixERP.Net.UI.ScrudFactory.Layout.Form
{
    public class FormFooter : ScrudLayout
    {
        public FormFooter(Config config)
            : base(config)
        {
            this.Initialize();
        }

        private string ButtonCssClass { get; set; }

        private void Initialize()
        {
            this.ButtonCssClass = ConfigBuilder.GetButtonCssClass(this.Config);
        }

        public override string Get()
        {
            StringBuilder footer = new StringBuilder();
            StringBuilder buttons = new StringBuilder();

            this.BuildUseButton(buttons);
            this.BuildSaveButton(buttons);
            this.BuildCancelButton(buttons);
            this.BuildRestButton(buttons);


            Helpers.ScrudFormHelper.AddScrudFormRow(footer, string.Empty, string.Empty, buttons.ToString());

            return footer.ToString();
        }

        private void BuildRestButton(StringBuilder builder)
        {
            this.BuildButton(builder, "reset", "ResetButton", "", Titles.Reset);
        }

        private void BuildUseButton(StringBuilder builder)
        {
            if (IsModal())
            {
                this.BuildButton(builder, "button", "UseButton", "scrudDispalyLoading();", Titles.Use);
            }
        }

        private void BuildButton(StringBuilder builder, string type, string id, string click, string value)
        {
            TagBuilder.Begin(builder, "input");
            TagBuilder.AddType(builder, type);
            TagBuilder.AddId(builder, id);

            if (!string.IsNullOrWhiteSpace(click))
            {
                TagBuilder.AddAttribute(builder, "onclick", click);
            }

            if (!string.IsNullOrWhiteSpace(this.ButtonCssClass))
            {
                TagBuilder.AddClass(builder, this.ButtonCssClass);
            }

            TagBuilder.AddValue(builder, value);
            TagBuilder.Close(builder, true);
        }

        private void BuildSaveButton(StringBuilder builder)
        {
            this.BuildButton(builder, "button", "SaveButton", "save();", Titles.Save);
        }

        private void BuildCancelButton(StringBuilder builder)
        {
            this.BuildButton(builder, "button", "CancelButton", "cancel();", Titles.Cancel);
        }

        private static bool IsModal()
        {
            string modal = HttpContext.Current.Request.QueryString["modal"];
            return modal != null && modal.Equals("1");
        }
    }
}