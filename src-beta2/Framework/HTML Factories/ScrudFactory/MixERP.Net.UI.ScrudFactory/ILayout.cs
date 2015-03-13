namespace MixERP.Net.UI.ScrudFactory
{
    public interface ILayout
    {
        Config Config { get; set; }
        string Get();
    }
}