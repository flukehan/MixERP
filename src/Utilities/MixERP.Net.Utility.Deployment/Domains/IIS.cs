using Microsoft.Win32;

namespace MixERP.Net.Utility.Installer.Domains
{
    public class IIS : IProgram
    {
        private const string Key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\InetStp";

        public IIS()
        {
            object major = Registry.GetValue(Key, "MajorVersion", null);
            object minor = Registry.GetValue(Key, "MinorVersion", null);
            object wwwRoot = Registry.GetValue(Key, "PathWWWRoot", null);

            if (major != null)
            {
                this.MajorVersion = (int)major;
                this.IsInstalled = true;
            }

            if (minor != null)
            {
                this.MinorVersion = (int)minor;
            }

            if (wwwRoot != null)
            {
                this.WwwRoot = (string)wwwRoot;
            }
        }

        public int MajorVersion { get; private set; }
        public int MinorVersion { get; private set; }
        public bool IsInstalled { get; private set; }
        public string WwwRoot { get; private set; }
    }
}