
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixERP.Net.Entities.Config
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
    


    [TableName("config.attachment_factory")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class AttachmentFactory : PetaPocoDB.Record<AttachmentFactory> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

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

    }

    [TableName("config.parameters")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class Parameter : PetaPocoDB.Record<Parameter> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("config.report_api")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class ReportApi : PetaPocoDB.Record<ReportApi> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("config.report")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class Report : PetaPocoDB.Record<Report> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

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

    }

    [TableName("config.stock_transaction_factory")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class StockTransactionFactory : PetaPocoDB.Record<StockTransactionFactory> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

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

    }

    [TableName("config.transaction_checklist")]
    [PrimaryKey("key", autoIncrement=false)]
    [ExplicitColumns]
    public class TransactionChecklist : PetaPocoDB.Record<TransactionChecklist> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }
}

