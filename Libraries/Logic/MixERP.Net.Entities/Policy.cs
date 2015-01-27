
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace MixERP.Net.Entities.Policy
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
	

    
    [TableName("auto_verification_policy_scrud_view")]
    [ExplicitColumns]
    public class AutoVerificationPolicyScrudView : PetaPocoDB.Record<AutoVerificationPolicyScrudView> 
    {
        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("verify_sales_transactions")] 
        public bool? VerifySalesTransactions { get; set; }

        [Column("sales_verification_limit")] 
        public decimal? SalesVerificationLimit { get; set; }

        [Column("verify_purchase_transactions")] 
        public bool? VerifyPurchaseTransactions { get; set; }

        [Column("purchase_verification_limit")] 
        public decimal? PurchaseVerificationLimit { get; set; }

        [Column("verify_gl_transactions")] 
        public bool? VerifyGlTransactions { get; set; }

        [Column("gl_verification_limit")] 
        public decimal? GlVerificationLimit { get; set; }

        [Column("effective_from")] 
        public DateTime? EffectiveFrom { get; set; }

        [Column("ends_on")] 
        public DateTime? EndsOn { get; set; }

        [Column("is_active")] 
        public bool? IsActive { get; set; }

    }
    
    [TableName("voucher_verification_policy_scrud_view")]
    [ExplicitColumns]
    public class VoucherVerificationPolicyScrudView : PetaPocoDB.Record<VoucherVerificationPolicyScrudView> 
    {
        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("can_verify_sales_transactions")] 
        public bool? CanVerifySalesTransactions { get; set; }

        [Column("sales_verification_limit")] 
        public decimal? SalesVerificationLimit { get; set; }

        [Column("can_verify_purchase_transactions")] 
        public bool? CanVerifyPurchaseTransactions { get; set; }

        [Column("purchase_verification_limit")] 
        public decimal? PurchaseVerificationLimit { get; set; }

        [Column("can_verify_gl_transactions")] 
        public bool? CanVerifyGlTransactions { get; set; }

        [Column("gl_verification_limit")] 
        public decimal? GlVerificationLimit { get; set; }

        [Column("can_self_verify")] 
        public bool? CanSelfVerify { get; set; }

        [Column("self_verification_limit")] 
        public decimal? SelfVerificationLimit { get; set; }

        [Column("effective_from")] 
        public DateTime? EffectiveFrom { get; set; }

        [Column("ends_on")] 
        public DateTime? EndsOn { get; set; }

        [Column("is_active")] 
        public bool? IsActive { get; set; }

    }
    
    [TableName("store_policy_details")]
    [PrimaryKey("store_policy_detail_id")]
    [ExplicitColumns]
    public class StorePolicyDetail : PetaPocoDB.Record<StorePolicyDetail> 
    {
        [Column("store_policy_detail_id")] 
        public long StorePolicyDetailId { get; set; }

        [Column("store_policy_id")] 
        public long StorePolicyId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("store_id")] 
        public int StoreId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }
    
    [TableName("store_policies")]
    [PrimaryKey("store_policy_id")]
    [ExplicitColumns]
    public class StorePolicy : PetaPocoDB.Record<StorePolicy> 
    {
        [Column("store_policy_id")] 
        public long StorePolicyId { get; set; }

        [Column("written_by_user_id")] 
        public int WrittenByUserId { get; set; }

        [Column("status")] 
        public bool Status { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }
    
    [TableName("menu_policy")]
    [PrimaryKey("policy_id")]
    [ExplicitColumns]
    public class MenuPolicy : PetaPocoDB.Record<MenuPolicy> 
    {
        [Column("policy_id")] 
        public int PolicyId { get; set; }

        [Column("menu_id")] 
        public int MenuId { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("inherit_in_child_offices")] 
        public bool InheritInChildOffices { get; set; }

        [Column("role_id")] 
        public int? RoleId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("scope")] 
        public string Scope { get; set; }

    }
    
    [TableName("menu_access")]
    [PrimaryKey("access_id")]
    [ExplicitColumns]
    public class MenuAccess : PetaPocoDB.Record<MenuAccess> 
    {
        [Column("access_id")] 
        public long AccessId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("menu_id")] 
        public int MenuId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

    }
    
    [TableName("voucher_verification_policy")]
    [PrimaryKey("user_id", autoIncrement=false)]
    [ExplicitColumns]
    public class VoucherVerificationPolicy : PetaPocoDB.Record<VoucherVerificationPolicy> 
    {
        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("can_verify_sales_transactions")] 
        public bool CanVerifySalesTransactions { get; set; }

        [Column("sales_verification_limit")] 
        public decimal SalesVerificationLimit { get; set; }

        [Column("can_verify_purchase_transactions")] 
        public bool CanVerifyPurchaseTransactions { get; set; }

        [Column("purchase_verification_limit")] 
        public decimal PurchaseVerificationLimit { get; set; }

        [Column("can_verify_gl_transactions")] 
        public bool CanVerifyGlTransactions { get; set; }

        [Column("gl_verification_limit")] 
        public decimal GlVerificationLimit { get; set; }

        [Column("can_self_verify")] 
        public bool CanSelfVerify { get; set; }

        [Column("self_verification_limit")] 
        public decimal SelfVerificationLimit { get; set; }

        [Column("effective_from")] 
        public DateTime EffectiveFrom { get; set; }

        [Column("ends_on")] 
        public DateTime EndsOn { get; set; }

        [Column("is_active")] 
        public bool IsActive { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }
    
    [TableName("lock_outs")]
    [PrimaryKey("lock_out_id")]
    [ExplicitColumns]
    public class LockOut : PetaPocoDB.Record<LockOut> 
    {
        [Column("lock_out_id")] 
        public long LockOutId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("lock_out_time")] 
        public DateTime LockOutTime { get; set; }

        [Column("lock_out_till")] 
        public DateTime LockOutTill { get; set; }

    }
    
    [TableName("auto_verification_policy")]
    [PrimaryKey("user_id", autoIncrement=false)]
    [ExplicitColumns]
    public class AutoVerificationPolicy : PetaPocoDB.Record<AutoVerificationPolicy> 
    {
        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("verify_sales_transactions")] 
        public bool VerifySalesTransactions { get; set; }

        [Column("sales_verification_limit")] 
        public decimal SalesVerificationLimit { get; set; }

        [Column("verify_purchase_transactions")] 
        public bool VerifyPurchaseTransactions { get; set; }

        [Column("purchase_verification_limit")] 
        public decimal PurchaseVerificationLimit { get; set; }

        [Column("verify_gl_transactions")] 
        public bool VerifyGlTransactions { get; set; }

        [Column("gl_verification_limit")] 
        public decimal GlVerificationLimit { get; set; }

        [Column("effective_from")] 
        public DateTime EffectiveFrom { get; set; }

        [Column("ends_on")] 
        public DateTime EndsOn { get; set; }

        [Column("is_active")] 
        public bool IsActive { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }
}


