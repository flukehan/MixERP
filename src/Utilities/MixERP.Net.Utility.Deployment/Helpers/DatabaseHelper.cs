using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MixERP.Net.Utility.Installer.Helpers
{
   public class DatabaseHelper
    {
       public string DatabaseName { get; set; }
       public string Password { get;set; }
       public string UserName { get; set; }

       public DatabaseHelper(string db, string password)
       {
           this.UserName = "postgres";
           this.DatabaseName = db;
           this.Password = password;
       }

       public DatabaseHelper(string db, string userName, string password)
       {
           this.DatabaseName = db;
           this.UserName = userName;
           this.Password = password;
       }

       private string GetConnectionString()
       {
           NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder
           {
               Host = "localhost",
               Database = this.DatabaseName,
               UserName = this.UserName,
               Password = this.Password,
               ApplicationName = "MixERP Installer"
           };

           return builder.ConnectionString;
       }

       public void ExecuteNonQuery(NpgsqlCommand command)
       {
           using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString()))
           {
               command.Connection = connection;
               connection.Open();
               command.CommandType = CommandType.Text;
               command.ExecuteNonQuery();
           }
       }

       public object GetScalarValue(NpgsqlCommand command)
       {
           using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString()))
           {
               command.Connection = connection;
               connection.Open();
               command.CommandType = CommandType.Text;
               return command.ExecuteScalar();
           }
       }
    }
}
