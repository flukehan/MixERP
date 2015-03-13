using System.Text;
using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class GridPanel : ScrudLayout
    {
        internal GridPanel(Config config)
            : base(config)
        {
        }

        public override string Get()
        {
            GridView grid = new GridView(this.Config);

            StringBuilder gridPanel = new StringBuilder();

            TagBuilder.Begin(gridPanel, "div");
            TagBuilder.AddId(gridPanel, "GridPanel");
            TagBuilder.AddClass(gridPanel, ConfigBuilder.GetGridPanelCssClass(this.Config));
            TagBuilder.AddStyle(gridPanel, ConfigBuilder.GetGridPanelStyle(this.Config));
            TagBuilder.Close(gridPanel);

            gridPanel.Append(grid.Get());

            Pager pager = new Pager(this.Config);
            gridPanel.Append(pager.Get());

            TagBuilder.EndTag(gridPanel, "div");

            return gridPanel.ToString();
        }
    }
}