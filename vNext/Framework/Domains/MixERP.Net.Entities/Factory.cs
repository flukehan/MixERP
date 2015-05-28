using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using Npgsql;
using PetaPoco;

namespace MixERP.Net.Entities
{
    public sealed class Factory
    {
        private const string ProviderName = "Npgsql";
        public static IEnumerable<T> Get<T>(string sql, params object[] args)
        {
            try
            {
                using (Database db = new Database(GetConnectionString(), ProviderName))
                {
                    return db.Query<T>(sql, args);
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.Code.StartsWith("P"))
                {
                    string errorMessage = GetDBErrorResource(ex.Code);
                    throw new MixERPException(errorMessage, ex);
                }

                throw;
            }
        }

        public static T Scalar<T>(string sql, params object[] args)
        {
            try
            {
                using (Database db = new Database(GetConnectionString(), ProviderName))
                {
                    return db.ExecuteScalar<T>(sql, args);
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.Code.StartsWith("P"))
                {
                    string errorMessage = GetDBErrorResource(ex.Code);
                    throw new MixERPException(errorMessage, ex);
                }

                throw;
            }
        }

        public static void NonQuery(string sql, params object[] args)
        {
            try
            {
                using (Database db = new Database(GetConnectionString(), ProviderName))
                {
                    db.Execute(sql, args);
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.Code.StartsWith("P"))
                {
                    string errorMessage = GetDBErrorResource(ex.Code);
                    throw new MixERPException(errorMessage, ex);
                }

                throw;
            }
        }

        private static string GetDBErrorResource(string key)
        {
            Assembly ass = GetAssemblyByName("MixERP.Net.DbFactory");
            return LocalizationHelper.GetResourceString(ass, "MixERP.Net.DbFactory.Resources.DbErrors", key);
        }

        private static Assembly GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);
        }

        public static string GetConnectionString()
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = ConfigurationHelper.GetDbServerParameter("Server"),
                Database = ConfigurationHelper.GetDbServerParameter("Database"),
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