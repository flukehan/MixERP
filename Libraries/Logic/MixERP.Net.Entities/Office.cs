
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace MixERP.Net.Entities.Office
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
    


    [TableName("office.cash_repository_scrud_view")]
    [ExplicitColumns]
    public class CashRepositoryScrudView : PetaPocoDB.Record<CashRepositoryScrudView> 
    {
        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("cash_repository_code")] 
        public string CashRepositoryCode { get; set; }

        [Column("cash_repository_name")] 
        public string CashRepositoryName { get; set; }

        [Column("parent_cr_code")] 
        public string ParentCrCode { get; set; }

        [Column("parent_cr_name")] 
        public string ParentCrName { get; set; }

        [Column("description")] 
        public string Description { get; set; }

    }

    [TableName("office.cost_center_scrud_view")]
    [ExplicitColumns]
    public class CostCenterScrudView : PetaPocoDB.Record<CostCenterScrudView> 
    {
        [Column("cost_center_id")] 
        public int? CostCenterId { get; set; }

        [Column("cost_center_code")] 
        public string CostCenterCode { get; set; }

        [Column("cost_center_name")] 
        public string CostCenterName { get; set; }

    }

    [TableName("office.counter_scrud_view")]
    [ExplicitColumns]
    public class CounterScrudView : PetaPocoDB.Record<CounterScrudView> 
    {
        [Column("counter_id")] 
        public int? CounterId { get; set; }

        [Column("store")] 
        public string Store { get; set; }

        [Column("cash_repository")] 
        public string CashRepository { get; set; }

        [Column("counter_code")] 
        public string CounterCode { get; set; }

        [Column("counter_name")] 
        public string CounterName { get; set; }

    }

    [TableName("office.department_scrud_view")]
    [ExplicitColumns]
    public class DepartmentScrudView : PetaPocoDB.Record<DepartmentScrudView> 
    {
        [Column("department_id")] 
        public int? DepartmentId { get; set; }

        [Column("department_code")] 
        public string DepartmentCode { get; set; }

        [Column("department_name")] 
        public string DepartmentName { get; set; }

    }

    [TableName("office.office_scrud_view")]
    [ExplicitColumns]
    public class OfficeScrudView : PetaPocoDB.Record<OfficeScrudView> 
    {
        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("nick_name")] 
        public string NickName { get; set; }

        [Column("registration_date")] 
        public DateTime? RegistrationDate { get; set; }

        [Column("currency")] 
        public string Currency { get; set; }

        [Column("po_box")] 
        public string PoBox { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("state")] 
        public string State { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("country")] 
        public string Country { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("registration_number")] 
        public string RegistrationNumber { get; set; }

        [Column("parent_office")] 
        public string ParentOffice { get; set; }

    }

    [TableName("office.role_scrud_view")]
    [ExplicitColumns]
    public class RoleScrudView : PetaPocoDB.Record<RoleScrudView> 
    {
        [Column("role_id")] 
        public int? RoleId { get; set; }

        [Column("role_code")] 
        public string RoleCode { get; set; }

        [Column("role_name")] 
        public string RoleName { get; set; }

        [Column("is_admin")] 
        public bool? IsAdmin { get; set; }

        [Column("is_system")] 
        public bool? IsSystem { get; set; }

    }

    [TableName("office.store_scrud_view")]
    [ExplicitColumns]
    public class StoreScrudView : PetaPocoDB.Record<StoreScrudView> 
    {
        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("store_code")] 
        public string StoreCode { get; set; }

        [Column("store_name")] 
        public string StoreName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

        [Column("store_type")] 
        public string StoreType { get; set; }

        [Column("allow_sales")] 
        public bool? AllowSales { get; set; }

        [Column("sales_tax")] 
        public string SalesTax { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("cash_repository")] 
        public string CashRepository { get; set; }

    }

    [TableName("office.store_type_scrud_view")]
    [ExplicitColumns]
    public class StoreTypeScrudView : PetaPocoDB.Record<StoreTypeScrudView> 
    {
        [Column("store_type_id")] 
        public int? StoreTypeId { get; set; }

        [Column("store_type_code")] 
        public string StoreTypeCode { get; set; }

        [Column("store_type_name")] 
        public string StoreTypeName { get; set; }

    }

    [TableName("office.cash_repository_selector_view")]
    [ExplicitColumns]
    public class CashRepositorySelectorView : PetaPocoDB.Record<CashRepositorySelectorView> 
    {
        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("cash_repository_code")] 
        public string CashRepositoryCode { get; set; }

        [Column("cash_repository_name")] 
        public string CashRepositoryName { get; set; }

        [Column("parent_cr_code")] 
        public string ParentCrCode { get; set; }

        [Column("parent_cr_name")] 
        public string ParentCrName { get; set; }

        [Column("description")] 
        public string Description { get; set; }

    }

    [TableName("office.office_selector_view")]
    [ExplicitColumns]
    public class OfficeSelectorView : PetaPocoDB.Record<OfficeSelectorView> 
    {
        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("nick_name")] 
        public string NickName { get; set; }

        [Column("registration_date")] 
        public DateTime? RegistrationDate { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("po_box")] 
        public string PoBox { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("state")] 
        public string State { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("country")] 
        public string Country { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("registration_number")] 
        public string RegistrationNumber { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

        [Column("parent_office_id")] 
        public int? ParentOfficeId { get; set; }

    }

    [TableName("office.store_selector_view")]
    [ExplicitColumns]
    public class StoreSelectorView : PetaPocoDB.Record<StoreSelectorView> 
    {
        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("store_code")] 
        public string StoreCode { get; set; }

        [Column("store_name")] 
        public string StoreName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

        [Column("store_type_id")] 
        public int? StoreTypeId { get; set; }

        [Column("allow_sales")] 
        public bool? AllowSales { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("default_cash_account_id")] 
        public long? DefaultCashAccountId { get; set; }

        [Column("default_cash_repository_id")] 
        public int? DefaultCashRepositoryId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.store_type_selector_view")]
    [ExplicitColumns]
    public class StoreTypeSelectorView : PetaPocoDB.Record<StoreTypeSelectorView> 
    {
        [Column("store_type_id")] 
        public int? StoreTypeId { get; set; }

        [Column("store_type_code")] 
        public string StoreTypeCode { get; set; }

        [Column("store_type_name")] 
        public string StoreTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.user_selector_view")]
    [ExplicitColumns]
    public class UserSelectorView : PetaPocoDB.Record<UserSelectorView> 
    {
        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("full_name")] 
        public string FullName { get; set; }

        [Column("role_name")] 
        public string RoleName { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

    }

    [TableName("office.office_view")]
    [ExplicitColumns]
    public class OfficeView : PetaPocoDB.Record<OfficeView> 
    {
        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("nick_name")] 
        public string NickName { get; set; }

        [Column("registration_date")] 
        public DateTime? RegistrationDate { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("po_box")] 
        public string PoBox { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("state")] 
        public string State { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("country")] 
        public string Country { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("registration_number")] 
        public string RegistrationNumber { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

        [Column("parent_office_id")] 
        public int? ParentOfficeId { get; set; }

    }

    [TableName("office.role_view")]
    [ExplicitColumns]
    public class RoleView : PetaPocoDB.Record<RoleView> 
    {
        [Column("role_id")] 
        public int? RoleId { get; set; }

        [Column("role_code")] 
        public string RoleCode { get; set; }

        [Column("role_name")] 
        public string RoleName { get; set; }

    }

    [TableName("office.sign_in_view")]
    [ExplicitColumns]
    public class SignInView : PetaPocoDB.Record<SignInView> 
    {
        [Column("login_id")] 
        public long? LoginId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("role_id")] 
        public int? RoleId { get; set; }

        [Column("role")] 
        public string Role { get; set; }

        [Column("role_code")] 
        public string RoleCode { get; set; }

        [Column("role_name")] 
        public string RoleName { get; set; }

        [Column("is_admin")] 
        public bool? IsAdmin { get; set; }

        [Column("is_system")] 
        public bool? IsSystem { get; set; }

        [Column("browser")] 
        public string Browser { get; set; }

        [Column("ip_address")] 
        public string IpAddress { get; set; }

        [Column("login_date_time")] 
        public DateTime? LoginDateTime { get; set; }

        [Column("remote_user")] 
        public string RemoteUser { get; set; }

        [Column("culture")] 
        public string Culture { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("full_name")] 
        public string FullName { get; set; }

        [Column("elevated")] 
        public bool? Elevated { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("nick_name")] 
        public string NickName { get; set; }

        [Column("registration_date")] 
        public DateTime? RegistrationDate { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("po_box")] 
        public string PoBox { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("state")] 
        public string State { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("country")] 
        public string Country { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("registration_number")] 
        public string RegistrationNumber { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

    }

    [TableName("office.store_view")]
    [ExplicitColumns]
    public class StoreView : PetaPocoDB.Record<StoreView> 
    {
        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("store_code")] 
        public string StoreCode { get; set; }

        [Column("store_name")] 
        public string StoreName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

        [Column("store_type_id")] 
        public int? StoreTypeId { get; set; }

        [Column("allow_sales")] 
        public bool? AllowSales { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("default_cash_account_id")] 
        public long? DefaultCashAccountId { get; set; }

        [Column("default_cash_repository_id")] 
        public int? DefaultCashRepositoryId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.user_view")]
    [ExplicitColumns]
    public class UserView : PetaPocoDB.Record<UserView> 
    {
        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("full_name")] 
        public string FullName { get; set; }

        [Column("role_name")] 
        public string RoleName { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

    }

    [TableName("office.work_center_view")]
    [ExplicitColumns]
    public class WorkCenterView : PetaPocoDB.Record<WorkCenterView> 
    {
        [Column("work_center_id")] 
        public int? WorkCenterId { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("work_center_code")] 
        public string WorkCenterCode { get; set; }

        [Column("work_center_name")] 
        public string WorkCenterName { get; set; }

    }

    [TableName("office.configuration")]
    [PrimaryKey("configuration_id")]
    [ExplicitColumns]
    public class Configuration : PetaPocoDB.Record<Configuration> 
    {
        [Column("configuration_id")] 
        public int ConfigurationId { get; set; }

        [Column("config_id")] 
        public int? ConfigId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("configuration_details")] 
        public string ConfigurationDetails { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.roles")]
    [PrimaryKey("role_id")]
    [ExplicitColumns]
    public class Role : PetaPocoDB.Record<Role> 
    {
        [Column("role_id")] 
        public int RoleId { get; set; }

        [Column("role_code")] 
        public string RoleCode { get; set; }

        [Column("role_name")] 
        public string RoleName { get; set; }

        [Column("is_admin")] 
        public bool IsAdmin { get; set; }

        [Column("is_system")] 
        public bool IsSystem { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.store_types")]
    [PrimaryKey("store_type_id")]
    [ExplicitColumns]
    public class StoreType : PetaPocoDB.Record<StoreType> 
    {
        [Column("store_type_id")] 
        public int StoreTypeId { get; set; }

        [Column("store_type_code")] 
        public string StoreTypeCode { get; set; }

        [Column("store_type_name")] 
        public string StoreTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.counters")]
    [PrimaryKey("counter_id")]
    [ExplicitColumns]
    public class Counter : PetaPocoDB.Record<Counter> 
    {
        [Column("counter_id")] 
        public int CounterId { get; set; }

        [Column("store_id")] 
        public int StoreId { get; set; }

        [Column("cash_repository_id")] 
        public int CashRepositoryId { get; set; }

        [Column("counter_code")] 
        public string CounterCode { get; set; }

        [Column("counter_name")] 
        public string CounterName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.cashiers")]
    [PrimaryKey("cashier_id")]
    [ExplicitColumns]
    public class Cashier : PetaPocoDB.Record<Cashier> 
    {
        [Column("cashier_id")] 
        public long CashierId { get; set; }

        [Column("counter_id")] 
        public int CounterId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("assigned_by_user_id")] 
        public int AssignedByUserId { get; set; }

        [Column("transaction_date")] 
        public DateTime TransactionDate { get; set; }

        [Column("closed")] 
        public bool Closed { get; set; }

    }

    [TableName("office.cost_centers")]
    [PrimaryKey("cost_center_id")]
    [ExplicitColumns]
    public class CostCenter : PetaPocoDB.Record<CostCenter> 
    {
        [Column("cost_center_id")] 
        public int CostCenterId { get; set; }

        [Column("cost_center_code")] 
        public string CostCenterCode { get; set; }

        [Column("cost_center_name")] 
        public string CostCenterName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.cash_repositories")]
    [PrimaryKey("cash_repository_id")]
    [ExplicitColumns]
    public class CashRepository : PetaPocoDB.Record<CashRepository> 
    {
        [Column("cash_repository_id")] 
        public int CashRepositoryId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("cash_repository_code")] 
        public string CashRepositoryCode { get; set; }

        [Column("cash_repository_name")] 
        public string CashRepositoryName { get; set; }

        [Column("parent_cash_repository_id")] 
        public int? ParentCashRepositoryId { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.work_centers")]
    [PrimaryKey("work_center_id")]
    [ExplicitColumns]
    public class WorkCenter : PetaPocoDB.Record<WorkCenter> 
    {
        [Column("work_center_id")] 
        public int WorkCenterId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("work_center_code")] 
        public string WorkCenterCode { get; set; }

        [Column("work_center_name")] 
        public string WorkCenterName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.departments")]
    [PrimaryKey("department_id")]
    [ExplicitColumns]
    public class Department : PetaPocoDB.Record<Department> 
    {
        [Column("department_id")] 
        public int DepartmentId { get; set; }

        [Column("department_code")] 
        public string DepartmentCode { get; set; }

        [Column("department_name")] 
        public string DepartmentName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.stores")]
    [PrimaryKey("store_id")]
    [ExplicitColumns]
    public class Store : PetaPocoDB.Record<Store> 
    {
        [Column("store_id")] 
        public int StoreId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("store_code")] 
        public string StoreCode { get; set; }

        [Column("store_name")] 
        public string StoreName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

        [Column("store_type_id")] 
        public int StoreTypeId { get; set; }

        [Column("allow_sales")] 
        public bool AllowSales { get; set; }

        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("default_cash_account_id")] 
        public long DefaultCashAccountId { get; set; }

        [Column("default_cash_repository_id")] 
        public int DefaultCashRepositoryId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("office.offices")]
    [PrimaryKey("office_id")]
    [ExplicitColumns]
    public class Office : PetaPocoDB.Record<Office> 
    {
        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("nick_name")] 
        public string NickName { get; set; }

        [Column("registration_date")] 
        public DateTime RegistrationDate { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("po_box")] 
        public string PoBox { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("state")] 
        public string State { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("country")] 
        public string Country { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("registration_number")] 
        public string RegistrationNumber { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

        [Column("parent_office_id")] 
        public int? ParentOfficeId { get; set; }

    }

    [TableName("office.users")]
    [PrimaryKey("user_id")]
    [ExplicitColumns]
    public class User : PetaPocoDB.Record<User> 
    {
        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("role_id")] 
        public int RoleId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("full_name")] 
        public string FullName { get; set; }

        [Column("can_change_password")] 
        public bool CanChangePassword { get; set; }

        [Column("password")] 
        public string Password { get; set; }

        [Column("elevated")] 
        public bool Elevated { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [FunctionName("get_offices")]
    [ExplicitColumns]
    public class DbGetOfficesResult : PetaPocoDB.Record<DbGetOfficesResult> 
    {
        [Column("office_id")] 
        public string OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

    }

    [FunctionName("sign_in")]
    [ExplicitColumns]
    public class DbSignInResult : PetaPocoDB.Record<DbSignInResult> 
    {
        [Column("login_id")] 
        public string LoginId { get; set; }

        [Column("message")] 
        public string Message { get; set; }

    }

    [FunctionName("can_login")]
    [ExplicitColumns]
    public class DbCanLoginResult : PetaPocoDB.Record<DbCanLoginResult> 
    {
        [Column("result")] 
        public string Result { get; set; }

        [Column("message")] 
        public string Message { get; set; }

    }
}


