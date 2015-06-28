using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n.Resources;
using Npgsql;
using PetaPoco;

namespace MixERP.Net.Entities
{
    public sealed class Factory
    {
        private const string ProviderName = "Npgsql";
        public static string MetaDatabase = ConfigurationHelper.GetDbServerParameter("MetaDatabase");


        public static IEnumerable<T> Get<T>(string catalog, string sql, params object[] args)
        {
            try
            {
                using (Database db = new Database(GetConnectionString(catalog), ProviderName))
                {
                    return db.Query<T>(sql, args);
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.Code.StartsWith("P"))
                {
                    string errorMessage = GetDBErrorResource(ex);
                    throw new MixERPException(errorMessage, ex);
                }

                throw;
            }
        }

        public static object Insert(string catalog, object poco)
        {
            try
            {
                using (Database db = new Database(GetConnectionString(catalog), ProviderName))
                {
                    return db.Insert(poco);
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.Code.StartsWith("P"))
                {
                    string errorMessage = GetDBErrorResource(ex);
                    throw new MixERPException(errorMessage, ex);
                }

                throw;
            }
        }

        public static T Scalar<T>(string catalog, string sql, params object[] args)
        {
            try
            {
                using (Database db = new Database(GetConnectionString(catalog), ProviderName))
                {
                    return db.ExecuteScalar<T>(sql, args);
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.Code.StartsWith("P"))
                {
                    string errorMessage = GetDBErrorResource(ex);
                    throw new MixERPException(errorMessage, ex);
                }

                throw;
            }
        }

        public static void NonQuery(string catalog, string sql, params object[] args)
        {
            try
            {
                using (Database db = new Database(GetConnectionString(catalog), ProviderName))
                {
                    db.Execute(sql, args);
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.Code.StartsWith("P"))
                {
                    string errorMessage = GetDBErrorResource(ex);
                    throw new MixERPException(errorMessage, ex);
                }

                throw;
            }
        }

        private static string GetDBErrorResource(NpgsqlException ex)
        {
            string message = DbErrors.Get(ex.Code);

            if (message == ex.Code)
            {
                return ex.Message;
            }

            return message;
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
                Port = Conversion.TryCastInteger(ConfigurationHelper.GetDbServerParameter("Port")),
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