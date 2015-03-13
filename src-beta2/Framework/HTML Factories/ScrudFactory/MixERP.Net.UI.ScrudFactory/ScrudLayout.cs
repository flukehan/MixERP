namespace MixERP.Net.UI.ScrudFactory
{
    public abstract class ScrudLayout : ILayout
    {
        protected internal ScrudLayout(Config config)
        {
            this.Config = config;
        }

        public Config Config { get; set; }
        public abstract string Get();
    }
}