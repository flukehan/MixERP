using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MixERP.Net.Utility.Installer.Domains;
using MixERP.Net.Utility.Installer.Helpers;
using MixERP.Net.Utility.Installer.Installer;
using Npgsql;
using PostgreSQL = MixERP.Net.Utility.Installer.Domains.PostgreSQL;

namespace MixERP.Net.Utility.Installer.UI
{
    public partial class InstallationForm : Form
    {
        private bool _invalidDb;
        private bool _invalidSite;
        private string _iisRoot;
        private Office _office;

        public InstallationForm()
        {
            InitializeComponent();
        }

        #region Validation
        private void ValidateDatabase()
        {
            PostgreSQL postgres = new PostgreSQL();

            if (!postgres.IsInstalled) return;

            this.PostgreSQLStatusLabel.Text = @"PostgreSQL Server is installed.";
            this.WillBeCreatedLabel.Visible = true;
            this.ValidateDatabaseButton.Visible = true;
            this._invalidDb = true;
            this.InstallButton.Enabled = false;
        }

        private bool ValidateSite()
        {
            string siteName = this.SiteNameTextBox.Text;

            Site site = new Site(siteName);

            if (!site.IsInstalled)
            {
                return true;
            }

            string message = string.Format(CultureInfo.InvariantCulture, @"{0} is already installed on '{1}'.",
                siteName, site.Path);
            Program.warn(message);
            this._invalidSite = true;
            this.ValidateSiteButton.Visible = true;
            this.InstallButton.Enabled = false;

            return false;
        }

        #endregion

        private void VerifyIisInstallation()
        {
            IIS iis = new IIS();

            if (!iis.IsInstalled)
            {
                MessageBox.Show(@"IIS is not installed.", @"Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                this.Close();
            }

            this.IISStatusLabel.Text = @"IIS is installed.";
            this._iisRoot = iis.WwwRoot;
            this.UpdateInstallDirectory();
        }

        private void UpdateInstallDirectory()
        {
            this.InstallationDirectoryTextBox.Text = Path.Combine(this._iisRoot, this.SiteNameTextBox.Text);
        }

        #region Events

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            this.FolderBrowser.SelectedPath = this.InstallationDirectoryTextBox.Text;
            this.FolderBrowser.ShowDialog();

            this.InstallationDirectoryTextBox.Text = this.FolderBrowser.SelectedPath;
        }

        private void InstallationForm_Load(object sender, EventArgs e)
        {
            this.VerifyIisInstallation();
            this.ValidateDatabase();
            this.ValidateSite();
            this.WriteConfigurationInfos();
        }

        private void WriteConfigurationInfos()
        {
            this.PostgresPassword.Text = ConfigurationHelper.ReadConfiguration("PostgresPassword");
            this.MixERPPassword.Text = ConfigurationHelper.ReadConfiguration("MixERPPassword");
            this.ReportUserPassword.Text = ConfigurationHelper.ReadConfiguration("ReportUserPassword");

            this.OfficeCodeTextBox.Text = ConfigurationHelper.ReadConfiguration("OfficeCode");
            this.OfficeNameTextBox.Text = ConfigurationHelper.ReadConfiguration("OfficeName");
            this.NickNameTextBox.Text = ConfigurationHelper.ReadConfiguration("NickName");
            this.CurrencyCodeTextBox.Text = ConfigurationHelper.ReadConfiguration("CurrencyCode");
            this.CurrencySymbolTextBox.Text = ConfigurationHelper.ReadConfiguration("CurrencySymbol");
            this.CurrencyNameTextBox.Text = ConfigurationHelper.ReadConfiguration("CurrencyName");
            this.HundredthNameTextBox.Text = ConfigurationHelper.ReadConfiguration("HundredthName");
            this.AdminNameTextBox.Text = ConfigurationHelper.ReadConfiguration("AdminName");
            this.AdminUserNameTextBox.Text = ConfigurationHelper.ReadConfiguration("UserName");
            this.RegistrationDatePicker.Text = ConfigurationHelper.ReadConfiguration("RegistrationDate");
            this.InstallSampleCheckBox.Checked =
                ConfigurationHelper.ReadConfiguration("InstallSample").ToUpperInvariant().Equals("TRUE");
        }

        private bool IsEmpty(string directory)
        {
            if (Directory.Exists(directory))
            {
                return !Directory.EnumerateFiles(directory).Any();
            }

            return true;
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            this.CreateOffice();
            this._office.Validate();

            if (!this._office.IsValid)
            {
                Program.warn("Cannot install. Office information was not provided.");
                return;
            }

            string destination = this.InstallationDirectoryTextBox.Text;

            bool isEmpty = this.IsEmpty(destination);

            if (!isEmpty)
            {
                string message = string.Format(CultureInfo.InvariantCulture,
                    "The destination directory {0} is not empty. Would you like to empty it now?", destination);
                DialogResult result = MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    Directory.Delete(destination, true);
                }
                else
                {
                    Program.error("Cannot install MixERP.");
                    return;
                }
            }


            this.InstallButton.Enabled = false;
            this.CloseButton.Enabled = false;
            this.GroupBox.Enabled = false;
            this.OfficeInfoGroupBox.Enabled = false;

            InstallationFactory factory = new InstallationFactory
            {
                DatabaseName = this.DatabaseNameTextBox.Text,
                InstallSample = this.InstallSampleCheckBox.Checked,
                Office = this._office,
                Password = this.PostgresPassword.Text,
                MixERPPassword = this.MixERPPassword.Text,
                ReportUserPassword = this.ReportUserPassword.Text,
                SiteDirectory = this.InstallationDirectoryTextBox.Text,
                SiteAppPoolName = this.ApplicationPoolNameTextBox.Text,
                SiteHostName = this.HostNameTextBox.Text,
                SiteName = this.SiteNameTextBox.Text,
                SitePortNumber = Conversion.TryCastInteger(this.PortNumberTextBox.Text)
            };

            factory.Initialize();
            IEnumerable<IInstaller> installers = factory.Installers;

            ActivityProgressBar.Style = ProgressBarStyle.Marquee;

            this.StatusLabel.Visible = true;
            this.StatusProgressLabel.Visible = true;

            try
            {
                this.StatusProgressLabel.Text = string.Format("0/{0} tasks completed.", installers.Count());

                Task.Run(() =>
                {
                    int count = installers.Count();
                    int counter = 0;
                    foreach (IInstaller installer in installers)
                    {
                        counter++;
                        IInstaller installer1 = installer;
                        int counter1 = counter;

                        this.Invoke(
                            (MethodInvoker)
                                delegate { this.StatusLabel.Text = string.Format("Installing {0}.", installer1.Name); });

                        installer.Install();

                        this.Invoke((MethodInvoker)delegate
                        {
                            this.ActivityProgressBar.Style = ProgressBarStyle.Marquee;
                            this.StatusProgressBar.Style = ProgressBarStyle.Blocks;

                            this.StatusProgressBar.Value = (int)100.00 * counter1 / count;
                            this.StatusProgressLabel.Text = string.Format("{0}/{1} task completed.", counter1, count);

                            if (counter1.Equals(count))
                            {
                                this.ActivityProgressBar.Style = ProgressBarStyle.Blocks;
                                this.ActivityProgressBar.Value = 100;
                                this.CloseButton.Enabled = true;
                                Program.success("MixERP Installation was successful.");

                                this.BrowseMixERP();
                            }
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }
        }

        private void BrowseMixERP()
        {
            string site = string.Format(CultureInfo.InvariantCulture, "http://{0}:{1}", this.HostNameTextBox.Text, this.PortNumberTextBox.Text);
            System.Diagnostics.Process.Start(site);
        }

        private void CreateOffice()
        {
            this._office = new Office
            {
                AdminName =this.AdminNameTextBox.Text,                
                CurrencyCode = this.CurrencyCodeTextBox.Text,
                CurrencyName = this.CurrencyNameTextBox.Text,
                CurrencySymbol = this.CurrencySymbolTextBox.Text,
                HundredthName = this.HundredthNameTextBox.Text,
                NickName = this.NickNameTextBox.Text,
                OfficeCode = this.OfficeCodeTextBox.Text,
                OfficeName = this.OfficeNameTextBox.Text,
                Password = this.AdminPasswordTextBox.Text,
                RegistrationDate = this.RegistrationDatePicker.Value,
                UserName = this.AdminUserNameTextBox.Text
            };
        }

        private void ValidateDatabaseButton_Click(object sender, EventArgs e)
        {
            this.CreateOffice();
            this._office.Validate();

            if (!this._office.IsValid)
            {
                Program.warn("Invalid office information form.");
                return;
            }

            string password = this.PostgresPassword.Text;
            string database = this.DatabaseNameTextBox.Text;

            try
            {
                bool exists = Database.DbExists(database, password);

                if (exists)
                {
                    Program.warn("Database already exists. Please type a different name.");
                    return;
                }

                this._invalidDb = false;

                Program.success("Connection was successful.");
                this.DatabaseNameTextBox.Enabled = false;
                this.PostgresPassword.Enabled = false;

                if (this._invalidSite)
                {
                    Program.warn("Please validate MixERP Application Website as well.");
                    return;
                }

                this.InstallButton.Enabled = true;
            }
            catch (NpgsqlException ex)
            {
                Program.warn(ex.Message);
            }
        }

        private void ValidateSiteButton_Click(object sender, EventArgs e)
        {
            this._invalidSite = !this.ValidateSite();

            if (this._invalidSite)
            {
                return;
            }

            string message = string.Format("'{0}' is available.", this.SiteNameTextBox.Text);
            Program.success(message);

            if (this._invalidDb)
            {
                message = "Please validate MixERP Database as well.";
                Program.warn(message);
                return;
            }

            this.InstallButton.Enabled = true;
        }

        #endregion

        private void SiteNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ApplicationPoolNameTextBox.Text = this.SiteNameTextBox.Text;
            this.UpdateInstallDirectory();
        }

    }
}