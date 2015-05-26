using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MixERP.Net.Utility.Installer.Helpers;

namespace MixERP.Net.Utility.Installer.Installer
{
    public sealed class PostgreSQL : IInstaller
    {
        private string _installer;

        public PostgreSQL(string password, string mixerpPassword, string reportUserPassword)
        {
            this.Password = password;
            this.MixERPPassword = mixerpPassword;
            this.ReportUserPassword = reportUserPassword;
        }

        public PostgreSQL()
        {
        }

        public string Password { get; set; }
        public string MixERPPassword { get; set; }
        public string ReportUserPassword { get; set; }
        public string InstallationDirectory { get; set; }
        public string PostgreSQL32Installer { get; set; }
        public string PostgreSQL64Installer { get; set; }

        public void Install()
        {
            if (this.IsInstalled)
            {
                return;
            }

            this._installer = this.GetPostgreSQLInstaller();

            if (!File.Exists(this._installer))
            {
                Program.warn("Cannot install PostgreSQL Server because an installer was not found in the assets directory.");
                return;
            }

            this.InstallPostgreSQLServer();
        }

        public bool IsInstalled { get; set; }

        public string Name
        {
            get { return "PostgreSQL Server"; }
        }

        private string GetPostgreSQLInstaller()
        {
            string platform = this.GetOsPlatform();
            string path = platform.Equals("x64") ? this.PostgreSQL64Installer : this.PostgreSQL32Installer;

            return FileHelper.CombineWithBaseDirectory(path);
        }

        private string GetOsPlatform()
        {
            return Environment.Is64BitOperatingSystem ? "x64" : "x86";
        }

        private void InstallPostgreSQLServer()
        {
            string parameters = " --unattendedmodeui minimal --mode unattended --superpassword \"{0}\" --prefix \"{1}";
            parameters = string.Format(parameters, this.Password, this.InstallationDirectory);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = this._installer,
                Arguments = parameters,
            };

            Process process = Process.Start(startInfo);

            if (process != null)
            {
                process.WaitForExit();
            }
        }
    }
}