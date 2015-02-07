using System.Linq;
using Microsoft.Win32;

namespace MixERP.Net.Utility.Installer.Domains
{
    public class PostgreSQL : IProgram
    {
        private const string Key = @"SOFTWARE\PostgreSQL\Installations\";

        public PostgreSQL()
        {
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey key = hklm.OpenSubKey(Key))
                {
                    if (key == null)
                    {
                        return;
                    }

                    string subKeyName = key.GetSubKeyNames().LastOrDefault();

                    using (RegistryKey postgresKey = hklm.OpenSubKey(Key + subKeyName))
                    {
                        if (postgresKey == null) return;

                        object version = postgresKey.GetValue("Version");
                        object baseDirectory = postgresKey.GetValue("Base Directory");

                        if (version == null || baseDirectory == null) return;

                        this.BaseDirectory = baseDirectory.ToString();
                        this.VersionNumber = version.ToString();

                        int major = 0;
                        int.TryParse(this.VersionNumber.Split('.').FirstOrDefault(), out major);

                        int minor = 0;
                        int.TryParse(this.VersionNumber.Split('.').Skip(1).Take(1).FirstOrDefault(), out minor);

                        this.MajorVersion = major;
                        this.MinorVersion = minor;

                        this.IsInstalled = true;
                    }
                }
            }
        }

        public string BaseDirectory { get; private set; }
        public int MajorVersion { get; private set; }
        public int MinorVersion { get; private set; }
        public string VersionNumber { get; private set; }
        public bool IsInstalled { get; private set; }
    }
}