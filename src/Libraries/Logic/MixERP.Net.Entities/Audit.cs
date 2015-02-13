
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace MixERP.Net.Entities.Audit
{
    public partial class PetaPocoDB : Database
    {
        public PetaPocoDB(): base("")
        {
            CommonConstruct();
        }

        public PetaPocoDB(string connectionStringName): base(connectionStringName)
        {
            CommonConstruct();
        }
        
        partial void CommonConstruct();
        
        public interface IFactory
        {
            PetaPocoDB GetInstance();
        }
        
        public static IFactory Factory { get; set; }
        public static PetaPocoDB GetInstance()
        {
            if (_instance!=null)
                return _instance;
                
            if (Factory!=null)
                return Factory.GetInstance();
            else
                return new PetaPocoDB();
        }

        [ThreadStatic] static PetaPocoDB _instance;
        
        public override void OnBeginTransaction()
        {
            if (_instance==null)
                _instance=this;
        }
        
        public override void OnEndTransaction()
        {
            if (_instance==this)
                _instance=null;
        }
        
        public class Record<T> where T:new()
        {
            public static PetaPocoDB repo { get { return PetaPocoDB.GetInstance(); } }
            public bool IsNew() { return repo.IsNew(this); }
            public object Insert() { return repo.Insert(this); }
            public void Save() { repo.Save(this); }
            public int Update() { return repo.Update(this); }
            public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
            public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
            public static int Update(Sql sql) { return repo.Update<T>(sql); }
            public int Delete() { return repo.Delete(this); }
            public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
            public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
            public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
            public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
            public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
            public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
            public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
            public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
            public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
            public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
            public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
            public static T Single(Sql sql) { return repo.Single<T>(sql); }
            public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
            public static T First(Sql sql) { return repo.First<T>(sql); }
            public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
            public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
            public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
            public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
            public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
            public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
            public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
            public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
            public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
            public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
        }
    }
    


    [TableName("audit.logins")]
    [PrimaryKey("login_id")]
    [ExplicitColumns]
    public class Login : PetaPocoDB.Record<Login> 
    {
        [Column("login_id")] 
        public long LoginId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("browser")] 
        public string Browser { get; set; }

        [Column("ip_address")] 
        public string IpAddress { get; set; }

        [Column("login_date_time")] 
        public DateTime LoginDateTime { get; set; }

        [Column("remote_user")] 
        public string RemoteUser { get; set; }

        [Column("culture")] 
        public string Culture { get; set; }

    }

    [TableName("audit.failed_logins")]
    [PrimaryKey("failed_login_id")]
    [ExplicitColumns]
    public class FailedLogin : PetaPocoDB.Record<FailedLogin> 
    {
        [Column("failed_login_id")] 
        public long FailedLoginId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("browser")] 
        public string Browser { get; set; }

        [Column("ip_address")] 
        public string IpAddress { get; set; }

        [Column("failed_date_time")] 
        public DateTime FailedDateTime { get; set; }

        [Column("remote_user")] 
        public string RemoteUser { get; set; }

        [Column("details")] 
        public string Details { get; set; }

    }

    [TableName("audit.logged_actions")]
    [PrimaryKey("event_id")]
    [ExplicitColumns]
    public class LoggedAction : PetaPocoDB.Record<LoggedAction> 
    {
        [Column("event_id")] 
        public long EventId { get; set; }

        [Column("schema_name")] 
        public string SchemaName { get; set; }

        [Column("table_name")] 
        public string TableName { get; set; }

        [Column("relid")] 
        public string Relid { get; set; }

        [Column("session_user_name")] 
        public string SessionUserName { get; set; }

        [Column("application_user_name")] 
        public string ApplicationUserName { get; set; }

        [Column("action_tstamp_tx")] 
        public DateTime ActionTstampTx { get; set; }

        [Column("action_tstamp_stm")] 
        public DateTime ActionTstampStm { get; set; }

        [Column("action_tstamp_clk")] 
        public DateTime ActionTstampClk { get; set; }

        [Column("transaction_id")] 
        public long? TransactionId { get; set; }

        [Column("application_name")] 
        public string ApplicationName { get; set; }

        [Column("client_addr")] 
        public string ClientAddr { get; set; }

        [Column("client_port")] 
        public int? ClientPort { get; set; }

        [Column("client_query")] 
        public string ClientQuery { get; set; }

        [Column("action")] 
        public string Action { get; set; }

        [Column("row_data")] 
        public string RowData { get; set; }

        [Column("changed_fields")] 
        public string ChangedFields { get; set; }

        [Column("statement_only")] 
        public bool StatementOnly { get; set; }

    }

    [FunctionName("get_office_information_model")]
    [ExplicitColumns]
    public class DbGetOfficeInformationModelResult : PetaPocoDB.Record<DbGetOfficeInformationModelResult> 
    {
        [Column("office")] 
        public string Office { get; set; }

        [Column("logged_in_to")] 
        public string LoggedInTo { get; set; }

        [Column("last_login_ip")] 
        public string LastLoginIp { get; set; }

        [Column("last_login_on")] 
        public DateTime LastLoginOn { get; set; }

        [Column("current_ip")] 
        public string CurrentIp { get; set; }

        [Column("current_login_on")] 
        public DateTime CurrentLoginOn { get; set; }

        [Column("role")] 
        public string Role { get; set; }

        [Column("department")] 
        public string Department { get; set; }

    }
}


