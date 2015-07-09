
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;

namespace MixERP.Net.Entities.Audit
{

    [TableName("audit.failed_logins")]
    [PrimaryKey("failed_login_id")]
    [ExplicitColumns]
    public class FailedLogin : PetaPocoDB.Record<FailedLogin> , IPoco
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
    public class LoggedAction : PetaPocoDB.Record<LoggedAction> , IPoco
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

    [TableName("audit.logins")]
    [PrimaryKey("login_id")]
    [ExplicitColumns]
    public class Login : PetaPocoDB.Record<Login> , IPoco
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

    [FunctionName("get_office_information_model")]
    [ExplicitColumns]
    public class DbGetOfficeInformationModelResult : PetaPocoDB.Record<DbGetOfficeInformationModelResult> , IPoco
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

