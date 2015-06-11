using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class Title : ScrudLayout
    {
        internal Title(Config config)
            : base(config)
        {
        }

        public override string Get()
        {
            string cssClass = ConfigBuilder.GetTitleLabelCssClass(this.Config);
            return TagBuilder.GetDiv("TitleLabel", cssClass, this.Config.Text);
        }
    }
}