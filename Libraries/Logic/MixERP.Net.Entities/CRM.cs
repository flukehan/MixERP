
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace MixERP.Net.Entities.CRM
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
    


    [TableName("lead_sources")]
    [PrimaryKey("lead_source_id")]
    [ExplicitColumns]
    public class LeadSource : PetaPocoDB.Record<LeadSource> 
    {
        [Column("lead_source_id")] 
        public int LeadSourceId { get; set; }

        [Column("lead_source_code")] 
        public string LeadSourceCode { get; set; }

        [Column("lead_source_name")] 
        public string LeadSourceName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("opportunity_stages")]
    [PrimaryKey("opportunity_stage_id")]
    [ExplicitColumns]
    public class OpportunityStage : PetaPocoDB.Record<OpportunityStage> 
    {
        [Column("opportunity_stage_id")] 
        public int OpportunityStageId { get; set; }

        [Column("opportunity_stage_code")] 
        public string OpportunityStageCode { get; set; }

        [Column("opportunity_stage_name")] 
        public string OpportunityStageName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("lead_statuses")]
    [PrimaryKey("lead_status_id")]
    [ExplicitColumns]
    public class LeadStatus : PetaPocoDB.Record<LeadStatus> 
    {
        [Column("lead_status_id")] 
        public int LeadStatusId { get; set; }

        [Column("lead_status_code")] 
        public string LeadStatusCode { get; set; }

        [Column("lead_status_name")] 
        public string LeadStatusName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }
}


