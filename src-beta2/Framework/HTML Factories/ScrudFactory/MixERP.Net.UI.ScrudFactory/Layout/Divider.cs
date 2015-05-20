using MixERP.Net.Framework;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class Divider : ScrudLayout
    {
        internal Divider(Config config)
            : base(config)
        {
        }

        public override string Get()
        {
            return TagBuilder.GetDivider();
        }
    }
}