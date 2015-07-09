
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;

namespace MixERP.Net.Entities.Config
{

    [TableName("config.attachment_factory")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class AttachmentFactory : PetaPocoDB.Record<AttachmentFactory> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("config.currency_layer")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class CurrencyLayer : PetaPocoDB.Record<CurrencyLayer> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("config.db_paramters")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class DbParamter : PetaPocoDB.Record<DbParamter> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("config.messaging")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class Messaging : PetaPocoDB.Record<Messaging> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("config.mixerp")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class Mixerp : PetaPocoDB.Record<Mixerp> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("config.open_exchange_rates")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class OpenExchangeRate : PetaPocoDB.Record<OpenExchangeRate> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("config.scrud_factory")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class ScrudFactory : PetaPocoDB.Record<ScrudFactory> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("config.switches")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class Switch : PetaPocoDB.Record<Switch> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public bool? Value { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }
}

