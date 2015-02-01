using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using PetaPoco;

namespace MixERP.Net.Entities
{
    public sealed class Factory
    {
        private const string ProviderName = "Npgsql";

        public static IEnumerable<T> Get<T>(string sql, params object[] args)
        {
            using (Database db = new Database(GetConnectionString()))
            {
                return db.Query<T>(sql, args);
            }
        }

        public static T Scalar<T>(string sql, params object[] args)
        {
            using (Database db = new Database(GetConnectionString()))
            {
                return db.ExecuteScalar<T>(sql, args);
            }
        }

        public static void NonQuery(string sql, params object[] args)
        {
            using (Database db = new Database(GetConnectionString()))
            {
                db.Execute(sql, args);
            }
        }

        private static string GetConnectionString()
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder();
            connectionStringBuilder.Host = ConfigurationManager.AppSettings["Server"];
            connectionStringBuilder.Database = ConfigurationManager.AppSettings["Database"];
            connectionStringBuilder.UserName = ConfigurationManager.AppSettings["UserId"];
            connectionStringBuilder.Password = ConfigurationManager.AppSettings["Password"];
            connectionStringBuilder.SyncNotification = true;
            connectionStringBuilder.Pooling = true;
            connectionStringBuilder.SSL = true;
            connectionStringBuilder.SslMode = SslMode.Prefer;

            //connectionStringBuilder.ApplicationName = "MixERP";
            return connectionStringBuilder.ConnectionString;
        }
    }
}