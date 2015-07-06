using System.Collections.Generic;
using Npgsql;
using PetaPoco;

namespace MixERP.Net.i18n
{
    public sealed class Factory
    {
        private const string ProviderName = "Npgsql";
        public static string MetaDatabase = ConfigurationHelper.GetDbServerParameter("MetaDatabase");

        public static IEnumerable<T> Get<T>(string catalog, string sql, params object[] args)
        {
            using (Database db = new Database(GetConnectionString(catalog), ProviderName))
            {
                return db.Query<T>(sql, args);
            }
        }

        public static T Scalar<T>(string catalog, string sql, params object[] args)
        {
            using (Database db = new Database(GetConnectionString(catalog), ProviderName))
            {
                return db.ExecuteScalar<T>(sql, args);
            }
        }

        public static void NonQuery(string catalog, string sql, params object[] args)
        {
            using (Database db = new Database(GetConnectionString(catalog), ProviderName))
            {
                db.Execute(sql, args);
            }
        }

        public static string GetConnectionString(string catalog = "")
        {
            string database = ConfigurationHelper.GetDbServerParameter("Database");

            if (!string.IsNullOrWhiteSpace(catalog))
            {
                database = catalog;
            }

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = ConfigurationHelper.GetDbServerParameter("Server"),
                Database = database,
                UserName = ConfigurationHelper.GetDbServerParameter("UserId"),
                Password = ConfigurationHelper.GetDbServerParameter("Password"),
                Port = int.Parse(ConfigurationHelper.GetDbServerParameter("Port")),
                SyncNotification = true,
                Pooling = true,
                SSL = true,
                SslMode = SslMode.Prefer,
                MinPoolSize = 10,
                MaxPoolSize = 100,
                ApplicationName = "MixERP"
            };

            return connectionStringBuilder.ConnectionString;
        }
    }
}