using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;
using Microsoft.Web.Administration;
using MixERP.Net.Utility.Installer.Helpers;
using Application = Microsoft.Web.Administration.Application;
using Binding = Microsoft.Web.Administration.Binding;
using System.Configuration;
using System.Diagnostics;

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
            this.CreateLogDirectory();
            this.RegisterDotNetInIIS();
        }

        private void RegisterDotNetInIIS()
        {
            var windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string path = windowsPath + @"\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe";

            if (Environment.Is64BitOperatingSystem)
            {
                path = windowsPath + @"\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe";
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = path,
                Arguments = " -i"
            };

            Process process = Process.Start(startInfo);

            if (process != null)
            {
                process.WaitForExit();
            }

        }


        private void CreateLogDirectory()
        {
            string path = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest, "ApplicationLogDirectory");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string userId = "IIS AppPool\\" + this.AppPoolName;

            FileHelper.SetPermission(path, FileSystemRights.Modify, userId);
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
            string userId = "IIS AppPool\\" + this.AppPoolName;

            FileHelper.SetPermission(this.InstallDirectory, FileSystemRights.ReadAndExecute, userId);

            string writableDirectories = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest,
                "WritableDirectories");

            foreach (string path in writableDirectories.Split(','))
            {
                string directory = Path.Combine(this.InstallDirectory, path.Trim());
                FileHelper.SetPermission(directory, FileSystemRights.Modify, userId);
            }
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
            string mixerpConfig = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest,
                "MixERPConfigFileLocation");

            string logDirectory = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest,
                "ApplicationLogDirectory");

            string minLogLevel = ConfigurationHelper.GetConfigurationValues(this.InstallerManifest,
                "MinimumLogLevel");


            this.UpdateConfig(dbConfig, "Database", this.DatabaseName);
            this.UpdateConfig(dbConfig, "Catalogs", this.DatabaseName);
            this.UpdateConfig(dbConfig, "UserId", "mix_erp");
            this.UpdateConfig(dbConfig, "Password", this.MixERPPassword);
            this.UpdateConfig(dbConfig, "PostgreSQLBinDirectory", this.PostgreSQLBinDirectory);

            this.UpdateConfig(reportConfig, "DbLoginName", "report_user");
            this.UpdateConfig(reportConfig, "DbPassword", this.ReportUserPassword);

            this.UpdateConfig(mixerpConfig, "ApplicationLogDirectory", logDirectory);
            this.UpdateConfig(mixerpConfig, "MinimumLogLevel", minLogLevel);
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


            string archive = ConfigurationManager.AppSettings["ArchiveName"];
            archive = Path.Combine(this.DownloadDirectory, archive);

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