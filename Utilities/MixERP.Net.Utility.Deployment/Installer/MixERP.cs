using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Web.Administration;
using MixERP.Net.Utility.Installer.Helpers;

namespace MixERP.Net.Utility.Installer.Installer
{
    public class MixERP : IInstaller
    {
        public const string RuntimeVersion = "v4.0"; //Todo:parameterize the following.
        public const string BindingProtocol = "http";
        public string DatabaseName { get; set; }
        public string MixERPPassword { get; set; }
        public string ReportUserPassword { get; set; }
        public string DownloadDirectory { get; set; }
        public string ExtractDirectory { get; set; }
        public string InstallDirectory { get; set; }
        public string InstallerManifest { get; set; }
        public string SiteName { get; set; }
        public string AppPoolName { get; set; }
        public int PortNumber { get; set; }
        public string HostName { get; set; }
        public bool IsValid { get; private set; }
        public string PostgreSQLBinDirectory { get; set; }
        public bool IsInstalled { get; set; }

        public string Name
        {
            get { return "MixERP IIS Application"; }
        }

        public void Install()
        {
            this.Validate();

            if (!this.IsValid)
            {
                Program.warn("Cannot install MixERP Application. Please make sure that all your settings are correct.");
                return;
            }

            this.InstallerManifest = Path.Combine(this.ExtractDirectory, this.InstallerManifest);

            this.ExtractArchive();
            this.WriteConfiguration();
            this.CopySite();
            this.CreateApplicationPool();
            this.CreateSite();
            this.CreateBinding();
            this.CreateApplication();
            this.WritePermission();
        }

        private void CreateApplication()
        {
            using (ServerManager manager = new ServerManager())
            {
                ApplicationPool pool = manager.ApplicationPools[this.AppPoolName];
                if (pool == null)
                {
                    throw new Exception(
                        "Could not create MixERP Application because IIS Application Pool does not exist.");
                }

                Site site = manager.Sites[this.SiteName];
                if (site == null)
                {
                    throw new Exception("Could not create MixERP Application because IIS Site does not exist.");
                }

                Application application = site.Applications.CreateElement();
                application.Path = "/";
                application.ApplicationPoolName = pool.Name;

                VirtualDirectory vdir = application.VirtualDirectories.CreateElement();
                vdir.Path = "/";
                vdir.PhysicalPath = this.InstallDirectory;

                application.VirtualDirectories.Add(vdir);
                site.Applications.Add(application);

                manager.CommitChanges();
            }
        }

        private void WritePermission()
        {
            this.SetPermission(this.InstallDirectory, FileSystemRights.ReadAndExecute);

            string writableDirectories = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest,
                "WritableDirectories");

            foreach (string path in writableDirectories.Split(','))
            {
                string directory = Path.Combine(this.InstallDirectory, path.Trim());
                this.SetPermission(directory, FileSystemRights.Modify);
            }
        }

        private void SetPermission(string directory, FileSystemRights permission)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }

            DirectoryInfo info = new DirectoryInfo(directory);
            DirectorySecurity security = info.GetAccessControl();
            string userId = "IIS AppPool\\" + this.AppPoolName;

            FileSystemAccessRule rule = new FileSystemAccessRule(userId, permission, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None, AccessControlType.Allow);

            security.AddAccessRule(rule);
            info.SetAccessControl(security);
        }

        private void CreateApplicationPool()
        {
            using (ServerManager manager = new ServerManager())
            {
                ApplicationPool pool = manager.ApplicationPools[this.AppPoolName] ??
                                       manager.ApplicationPools.Add(this.AppPoolName);


                pool.ProcessModel.IdentityType = ProcessModelIdentityType.ApplicationPoolIdentity;
                pool.ManagedRuntimeVersion = RuntimeVersion;
                pool.AutoStart = true;
                pool.Enable32BitAppOnWin64 = true;
                pool.ManagedPipelineMode = ManagedPipelineMode.Integrated;

                manager.CommitChanges();
            }
        }

        private void CreateSite()
        {
            using (ServerManager manager = new ServerManager())
            {
                Site site = manager.Sites.CreateElement();
                site.Name = this.SiteName;
                site.ServerAutoStart = true;
                site.Id = GenerateSiteId(this.SiteName);

                manager.Sites.Add(site);

                manager.CommitChanges();
            }
        }

        public static uint GenerateSiteId(string siteName)
        {
            char[] arr = siteName.ToCharArray(); //Convert the sitename to a Char Array
            uint id = arr.Select(intc => intc & '\x00df')
                .Aggregate<int, uint>(0, (current, upper) => (uint) (current*101) + (uint) upper);
            return (id%Int32.MaxValue) + 1; //do a MOD and add 1
        }

        private void CreateBinding()
        {
            using (ServerManager manager = new ServerManager())
            {
                Site site = manager.Sites[this.SiteName];

                if (site == null)
                {
                    throw new Exception("Could not add binding information because the site was not found.");
                }

                string bindingInfo = string.Format(CultureInfo.InvariantCulture, "*:{0}:{1}", this.PortNumber,
                    this.HostName);

                if (
                    site.Bindings.Any(
                        binding =>
                            binding.Protocol.Equals(BindingProtocol) && binding.BindingInformation.Equals(bindingInfo)))
                {
                    return;
                }

                Binding newBinding = site.Bindings.CreateElement();
                newBinding.Protocol = BindingProtocol;
                newBinding.BindingInformation = bindingInfo;
                site.Bindings.Add(newBinding);
                manager.CommitChanges();
            }
        }

        private void CopySite()
        {
            string source = FileHelper.CombineWithBaseDirectory(this.ExtractDirectory);
            string destination = this.InstallDirectory;

            FileHelper.CopyFilesRecursively(source, destination, true);
        }

        private void WriteConfiguration()
        {
            string dbConfig = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest,
                "DbServerConfigFileLocation");
            string reportConfig = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest,
                "ReportConfigFileLocation");

            this.UpdateConfig(dbConfig, "Database", this.DatabaseName);
            this.UpdateConfig(dbConfig, "UserId", "mix_erp");
            this.UpdateConfig(dbConfig, "Password", this.MixERPPassword);
            this.UpdateConfig(dbConfig, "PostgreSQLBinDirectory", this.PostgreSQLBinDirectory);

            this.UpdateConfig(reportConfig, "DbLoginName", "report_user");
            this.UpdateConfig(reportConfig, "DbPassword", this.ReportUserPassword);
        }

        private void UpdateConfig(string configFileRelativePath, string key, string value)
        {
            string configFile = Path.Combine(this.ExtractDirectory, configFileRelativePath);
            ConfigurationHelper.SetConfigurationValues(configFile, key, value);
        }

        private void ExtractArchive()
        {
            DirectoryInfo extractTarget = new DirectoryInfo(this.ExtractDirectory);

            if (extractTarget.Exists)
            {
                extractTarget.Empty();
            }
            else
            {
                extractTarget.Create();
            }

            string archive = Directory.GetFiles(this.DownloadDirectory, "*.zip").FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(archive))
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(archive, this.ExtractDirectory);
            }
        }

        public void Validate()
        {
            if (StringUtility.AreNullOrWhitespace(this.DownloadDirectory, this.ExtractDirectory, this.InstallerManifest))
            {
                this.IsValid = false;
            }

            this.IsValid = true;
        }
    }
}