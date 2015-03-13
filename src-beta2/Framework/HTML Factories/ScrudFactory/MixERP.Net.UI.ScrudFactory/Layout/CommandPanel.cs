using System.Text;
using System.Web;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Resources;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class CommandPanel : ScrudLayout
    {
        internal CommandPanel(Config config)
            : base(config)
        {
        }

        public override string Get()
        {
            return this.GetCommandPanel();
        }

        private string GetCommandPanel()
        {
            StringBuilder panel = new StringBuilder();

            TagBuilder.Begin(panel, "div");
            TagBuilder.AddClass(panel, ConfigBuilder.GetCommandPanelCssClass(this.Config));
            TagBuilder.Close(panel);

            this.AddSelectButton(panel);
            this.AddShowCompactButton(panel);
            this.AddShowAllButton(panel);
            this.AddAddButton(panel);
            this.AddEditButton(panel);
            this.AddDeleteButton(panel);
            this.AddPrintButton(panel);

            TagBuilder.EndTag(panel, "div");

            return panel.ToString();
        }

        private void AddSelectButton(StringBuilder panel)
        {
            if (IsModal())
            {
                panel.Append(TagBuilder.GetHtmlButton("RETURN", "scrudSelectAndClose();", Titles.Select,
                    ConfigBuilder.GetButtonCssClass(this.Config), ConfigBuilder.GetSelectButtonIconCssClass(this.Config)));
            }
        }

        private void AddShowCompactButton(StringBuilder panel)
        {
            panel.Append(TagBuilder.GetHtmlButton("CTRL + SHIFT + C", "scrudShowCompact();", Titles.ShowCompact,
                ConfigBuilder.GetButtonCssClass(this.Config), ConfigBuilder.GetCompactButtonIconCssClass(this.Config)));
        }

        private void AddShowAllButton(StringBuilder panel)
        {
            panel.Append(TagBuilder.GetHtmlButton("CTRL + SHIFT + S", "scrudShowAll();", Titles.ShowAll,
                ConfigBuilder.GetButtonCssClass(this.Config), ConfigBuilder.GetAllButtonIconCssClass(this.Config)));
        }

        private void AddAddButton(StringBuilder panel)
        {
            panel.Append(TagBuilder.GetHtmlButton("CTRL + SHIFT + A", "return(scrudAddNew());", Titles.AddNew,
                ConfigBuilder.GetButtonCssClass(this.Config), ConfigBuilder.GetAddButtonIconCssClass(this.Config)));
        }

        private void AddEditButton(StringBuilder panel)
        {
            panel.Append(TagBuilder.GetHtmlButton("CTRL + SHIFT + E", "scrudEdit();", Titles.EditSelected,
                ConfigBuilder.GetButtonCssClass(this.Config), ConfigBuilder.GetEditButtonIconCssClass(this.Config)));
        }

        private void AddDeleteButton(StringBuilder panel)
        {
            panel.Append(TagBuilder.GetHtmlButton("CTRL + SHIFT + D", "scrudDelete();", Titles.DeleteSelected,
                ConfigBuilder.GetButtonCssClass(this.Config), ConfigBuilder.GetDeleteButtonIconCssClass(this.Config)));
        }

        private void AddPrintButton(StringBuilder panel)
        {
            panel.Append(TagBuilder.GetHtmlButton("CTRL + SHIFT + P", "scrudPrintGridView();", Titles.Print,
                ConfigBuilder.GetButtonCssClass(this.Config), ConfigBuilder.GetPrintButtonIconCssClass(this.Config)));
        }

        private static bool IsModal()
        {
            string modal = HttpContext.Current.Request.QueryString["modal"];
            if (modal == null) return false;

            return modal.Equals("1");
        }
    }
}