using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class Description : ScrudLayout
    {
        internal Description(Config config)
            : base(config)
        {
        }

        public override string Get()
        {
            if (string.IsNullOrWhiteSpace(this.Config.Description))
            {
                return string.Empty;
            }

            string cssClass = ConfigBuilder.GetDescriptionCssClass(this.Config);
            return TagBuilder.GetDiv(cssClass, this.Config.Description);
        }
    }
}