using System.Collections.ObjectModel;
using MixERP.Net.Utility.Installer.Domains;
using MixERP.Net.Utility.Installer.Helpers;

namespace MixERP.Net.Utility.Installer.Installer
{
    public sealed class InstallationFactory
    {
        private readonly string[] _sysdbs = {"postgres", "template0", "template1"};
        public string DatabaseName { get; set; }
        public string Password { get; set; }
        public string MixERPPassword { get; set; }
        public string ReportUserPassword { get; set; }
        public string SiteDirectory { get; set; }
        public string SiteName { get; set; }
        public string SiteAppPoolName { get; set; }
        public int SitePortNumber { get; set; }
        public string SiteHostName { get; set; }
        public bool IsValid { get; private set; }
        public Office Office { get; set; }
        public bool InstallSample { get; set; }
        public Collection<IInstaller> Installers { get; private set; }

        private void Validate()
        {
            this.IsValid = true;

            if (StringUtility.AreNullOrWhitespace(this.DatabaseName, this.Password, this.MixERPPassword,
                this.ReportUserPassword))
            {
                this.IsValid = false;
            }
        }

        public void Initialize()
        {
            this.Validate();

            if (!this.IsValid)
            {
                Program.warn("Cannot install. Please make sure all parameters are correct.");
                return;
            }

            Collection<IInstaller> installer = new Collection<IInstaller>();

            if (DatabaseName.Equal(this._sysdbs))
            {
                Program.warn("Invalid database name " + this.DatabaseName);
                return;
            }

            Domains.PostgreSQL postgresDomain = new Domains.PostgreSQL();
            installer.Add(new PostgreSQL
            {
                Password = this.Password,
                MixERPPassword = this.MixERPPassword,
                ReportUserPassword = this.ReportUserPassword,
                IsInstalled = postgresDomain.IsInstalled,
                PostgreSQL32Installer = ConfigurationHelper.GetPostgreSQL32(),
                PostgreSQL64Installer = ConfigurationHelper.GetPostgreSQL64(),
                InstallationDirectory = ConfigurationHelper.GetPostgreSQLInstallationDirectory()
            });


            installer.Add(new MixERP
            {
                InstallDirectory = this.SiteDirectory,
                HostName = this.SiteHostName,
                AppPoolName = this.SiteAppPoolName,
                SiteName = this.SiteName,
                PortNumber = this.SitePortNumber,
                DatabaseName = this.DatabaseName,
                MixERPPassword = this.MixERPPassword,
                ReportUserPassword = this.ReportUserPassword,
                DownloadDirectory = ConfigurationHelper.GetDownloadDirectory(),
                ExtractDirectory = ConfigurationHelper.GetExtractDirectory(),
                InstallerManifest = ConfigurationHelper.GetInstallerManifest(),
                PostgreSQLBinDirectory = ConfigurationHelper.GetPostgreSQLBinDirectory()
            });


            installer.Add(new Database
            {
                DatabaseName = this.DatabaseName,
                Password = this.Password,
                MixERPPassword = this.MixERPPassword,
                ReportUserPassword = this.ReportUserPassword
            });

            installer.Add(new DbScript
            {
                InstallerManifest = ConfigurationHelper.GetInstallerManifest(),
                InstallSample = this.InstallSample,
                ExtractDirectory = ConfigurationHelper.GetExtractDirectory(),
                DatabaseName = this.DatabaseName,
                Office = this.Office,
                Password = this.Password,
                MixERPRolePassword = this.MixERPPassword
            });

            this.Installers = installer;
        }
    }
}