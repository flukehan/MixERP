namespace MixERP.Net.Utility.Installer
{
    public interface IInstaller
    {
        bool IsInstalled { get; set; }
        string Name { get; }
        void Install();
    }
}