using System.Globalization;
using MixERP.Net.Utility.Installer.Helpers;
using Npgsql;

namespace MixERP.Net.Utility.Installer.Installer
{
    public sealed class Database : IInstaller
    {
        public string DatabaseName { get; set; }
        public string Password { get; set; }
        public string MixERPPassword { get; set; }
        public string ReportUserPassword { get; set; }
        public bool IsInstalled { get; set; }

        public string Name
        {
            get { return "MixERP Database"; }
        }

        public void Install()
        {
            this.CreateDatabase();
            this.CreateRoles();
            this.AllowLoginToRoles();
        }

        private void AllowLoginToRoles()
        {
            this.AllowLoginToRole("mix_erp");
            this.AllowLoginToRole("report_user");
        }

        private void AllowLoginToRole(string roleName)
        {
            string sql = "ALTER ROLE {0} WITH LOGIN;";
            sql = string.Format(CultureInfo.InvariantCulture, sql, roleName);

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, this.Password);
                helper.ExecuteNonQuery(command);
            }
        }

        public static bool DbExists(string database, string password)
        {
            DatabaseHelper helper = new DatabaseHelper(string.Empty, password);

            const string sql = "SELECT COUNT(*) FROM pg_database WHERE datname=@Database;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@Database", database);
                object result = helper.GetScalarValue(command);

                return result.ToString().Equals("1");
            }
        }

        private void CreateRoles()
        {
            this.CreateRoleProcedure();
            this.CreateRole("mix_erp", this.MixERPPassword);
            this.CreateRole("report_user", this.ReportUserPassword);
            this.TakeOwnership();
            this.DropRoleProcedure();
        }

        private void DropRoleProcedure()
        {
            const string sql = "DROP FUNCTION IF EXISTS add_role(text, text);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, this.Password);
                helper.ExecuteNonQuery(command);
            }
        }

        private void CreateRoleProcedure()
        {
            string sql = FileHelper.ReadSqlResource("role.sql");
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, this.Password);
                helper.ExecuteNonQuery(command);
            }
        }

        private void CreateRole(string roleName, string password)
        {
            const string sql = "SELECT * FROM add_role(@RoleName, @Password);";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@RoleName", roleName);
                command.Parameters.AddWithValue("@Password", password);

                DatabaseHelper helper = new DatabaseHelper(this.DatabaseName, this.Password);
                helper.ExecuteNonQuery(command);
            }
        }

        private void CreateDatabase()
        {
            string sql = "CREATE DATABASE " + this.DatabaseName + " ENCODING='UTF8' LC_COLLATE='C' LC_CTYPE='C' TEMPLATE=template0;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                DatabaseHelper helper = new DatabaseHelper(string.Empty, this.Password);
                helper.ExecuteNonQuery(command);
            }
        }

        private void TakeOwnership()
        {
            string sql = string.Format(CultureInfo.InvariantCulture, "ALTER DATABASE {0} OWNER TO mix_erp;",
                this.DatabaseName);
            DatabaseHelper helper = new DatabaseHelper(string.Empty, this.Password);
            helper.ExecuteNonQuery(new NpgsqlCommand(sql));
        }
    }
}