
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;

namespace MixERP.Net.Entities.Policy
{

    [TableName("policy.lock_outs")]
    [PrimaryKey("lock_out_id")]
    [ExplicitColumns]
    public class LockOut : PetaPocoDB.Record<LockOut> , IPoco
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

    [TableName("policy.menu_access")]
    [PrimaryKey("access_id")]
    [ExplicitColumns]
    public class MenuAccess : PetaPocoDB.Record<MenuAccess> , IPoco
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

    [TableName("policy.store_policies")]
    [PrimaryKey("store_policy_id")]
    [ExplicitColumns]
    public class StorePolicy : PetaPocoDB.Record<StorePolicy> , IPoco
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

    [TableName("policy.store_policy_details")]
    [PrimaryKey("store_policy_detail_id")]
    [ExplicitColumns]
    public class StorePolicyDetail : PetaPocoDB.Record<StorePolicyDetail> , IPoco
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

    [TableName("policy.menu_policy")]
    [PrimaryKey("policy_id")]
    [ExplicitColumns]
    public class MenuPolicy : PetaPocoDB.Record<MenuPolicy> , IPoco
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

    [TableName("policy.auto_verification_policy_scrud_view")]
    [ExplicitColumns]
    public class AutoVerificationPolicyScrudView : PetaPocoDB.Record<AutoVerificationPolicyScrudView> , IPoco
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

    [TableName("policy.http_actions")]
    [PrimaryKey("http_action_code", autoIncrement=false)]
    [ExplicitColumns]
    public class HttpAction : PetaPocoDB.Record<HttpAction> , IPoco
    {
        [Column("http_action_code")] 
        public string HttpActionCode { get; set; }

    }

    [TableName("policy.api_access_policy")]
    [PrimaryKey("api_access_policy_id")]
    [ExplicitColumns]
    public class ApiAccessPolicy : PetaPocoDB.Record<ApiAccessPolicy> , IPoco
    {
        [Column("api_access_policy_id")] 
        public long ApiAccessPolicyId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("poco_type_name")] 
        public string PocoTypeName { get; set; }

        [Column("http_action_code")] 
        public string HttpActionCode { get; set; }

        [Column("valid_till")] 
        public DateTime ValidTill { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("policy.voucher_verification_policy")]
    [PrimaryKey("policy_id")]
    [ExplicitColumns]
    public class VoucherVerificationPolicy : PetaPocoDB.Record<VoucherVerificationPolicy> , IPoco
    {
        [Column("policy_id")] 
        public int PolicyId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

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

        [Column("office_id")] 
        public int OfficeId { get; set; }

    }

    [TableName("policy.auto_verification_policy")]
    [PrimaryKey("office_id", autoIncrement=false)]
    [ExplicitColumns]
    public class AutoVerificationPolicy : PetaPocoDB.Record<AutoVerificationPolicy> , IPoco
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

        [Column("office_id")] 
        public int OfficeId { get; set; }

    }

    [TableName("policy.voucher_verification_policy_scrud_view")]
    [ExplicitColumns]
    public class VoucherVerificationPolicyScrudView : PetaPocoDB.Record<VoucherVerificationPolicyScrudView> , IPoco
    {
        [Column("policy_id")] 
        public int? PolicyId { get; set; }

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

    [FunctionName("get_menu")]
    [ExplicitColumns]
    public class DbGetMenuResult : PetaPocoDB.Record<DbGetMenuResult> , IPoco
    {
        [Column("menu_id")] 
        public int MenuId { get; set; }

        [Column("menu_text")] 
        public string MenuText { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("menu_code")] 
        public string MenuCode { get; set; }

        [Column("level")] 
        public short Level { get; set; }

        [Column("parent_menu_id")] 
        public int ParentMenuId { get; set; }

    }

    [FunctionName("get_menu_policy")]
    [ExplicitColumns]
    public class DbGetMenuPolicyResult : PetaPocoDB.Record<DbGetMenuPolicyResult> , IPoco
    {
        [Column("row_number")] 
        public long RowNumber { get; set; }

        [Column("access")] 
        public bool Access { get; set; }

        [Column("menu_id")] 
        public int MenuId { get; set; }

        [Column("menu_code")] 
        public string MenuCode { get; set; }

        [Column("menu_text")] 
        public string MenuText { get; set; }

        [Column("url")] 
        public string Url { get; set; }

    }
}

