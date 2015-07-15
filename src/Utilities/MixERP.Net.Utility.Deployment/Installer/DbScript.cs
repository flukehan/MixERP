using System.Configuration;
using System.IO;
using System.Text;
using MixERP.Net.Utility.Installer.Domains;
using MixERP.Net.Utility.Installer.Helpers;
using Npgsql;

namespace MixERP.Net.Utility.Installer.Installer
{
    public sealed class DbScript : IInstaller
    {
        public bool InstallSample { get; set; }
        public string InstallerManifest { get; set; }
        public string ExtractDirectory { get; set; }
        public string DatabaseName { get; set; }
        public string Password { get; set; }
        public string MixERPRolePassword { get; set; }
        public Office Office { get; set; }

        public void Install()
        {
            if (string.IsNullOrWhiteSpace(this.InstallerManifest))
            {
                throw new ConfigurationErrorsException("Installer manifest location not found.");
            }

            this.InstallerManifest = Path.Combine(this.ExtractDirectory, this.InstallerManifest);

            string dbScript = this.GetDbScript();

            this.RunDbScript(dbScript);
            this.AddOfficeScript();
            this.CreateOffice();
            this.DropOfficeScript();
        }

        private string GetDbScript()
        {
            if (this.InstallSample)
            {
                return ConfigurationHelper.GetConfigurationValues(this.InstallerManifest, "SampleDatabaseInstallScript");
            }

            return ConfigurationHelper.GetConfigurationValues(this.InstallerManifest, "DatabaseInstallScript");
        }

        private void DropOfficeScript()
        {
            string sql = FileHelper.ReadSqlResource("drop-office.sql");
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, "mix_erp", this.MixERPRolePassword);
                helper.ExecuteNonQuery(command);
            }
        }

        private void AddOfficeScript()
        {
            string sql = FileHelper.ReadSqlResource("office.sql");
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, "mix_erp", this.MixERPRolePassword);
                helper.ExecuteNonQuery(command);
            }
        }

        private void CreateOffice()
        {
            const string sql =
                "SELECT * FROM add_office(@OfficeCode, @OfficeName, @NickName, @RegistrationDate, @CurrencyCode, @CurrencySymbol, @CurrencyName, @HundredthName, @AdminName, @UserName, @Password);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeCode", this.Office.OfficeCode);
                command.Parameters.AddWithValue("@OfficeName", this.Office.OfficeName);
                command.Parameters.AddWithValue("@NickName", this.Office.NickName);
                command.Parameters.AddWithValue("@RegistrationDate", this.Office.RegistrationDate);
                command.Parameters.AddWithValue("@CurrencyCode", this.Office.CurrencyCode);
                command.Parameters.AddWithValue("@CurrencySymbol", this.Office.CurrencySymbol);
                command.Parameters.AddWithValue("@CurrencyName", this.Office.CurrencyName);
                command.Parameters.AddWithValue("@HundredthName", this.Office.HundredthName);
                command.Parameters.AddWithValue("@AdminName", this.Office.AdminName);
                command.Parameters.AddWithValue("@UserName", this.Office.UserName);
                command.Parameters.AddWithValue("@Password", this.Office.Password);

                DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, "mix_erp", this.MixERPRolePassword);

                helper.ExecuteNonQuery(command);
            }
        }

        private void RunDbScript(string scriptPath)
        {
            string path = FileHelper.CombineWithBaseDirectory(this.ExtractDirectory);
            path = Path.Combine(path, scriptPath);

            string sql = File.ReadAllText(path, Encoding.UTF8);

            DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, "mix_erp", this.MixERPRolePassword);
            helper.ExecuteNonQuery(new NpgsqlCommand(sql));
        }

        public bool IsInstalled { get; set; }

        public string Name
        {
            get { return "MixERP Database Script"; }
        }
    }
}