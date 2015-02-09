
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace MixERP.Net.Entities.Core
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
    


    [TableName("core.flag_types")]
    [PrimaryKey("flag_type_id")]
    [ExplicitColumns]
    public class FlagType : PetaPocoDB.Record<FlagType> 
    {
        [Column("flag_type_id")] 
        public int FlagTypeId { get; set; }

        [Column("flag_type_name")] 
        public string FlagTypeName { get; set; }

        [Column("background_color")] 
        public string BackgroundColor { get; set; }

        [Column("foreground_color")] 
        public string ForegroundColor { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.flags")]
    [PrimaryKey("flag_id")]
    [ExplicitColumns]
    public class Flag : PetaPocoDB.Record<Flag> 
    {
        [Column("flag_id")] 
        public long FlagId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("flag_type_id")] 
        public int FlagTypeId { get; set; }

        [Column("resource")] 
        public string Resource { get; set; }

        [Column("resource_key")] 
        public string ResourceKey { get; set; }

        [Column("resource_id")] 
        public string ResourceId { get; set; }

        [Column("flagged_on")] 
        public DateTime? FlaggedOn { get; set; }

    }

    [TableName("core.zip_code_types")]
    [PrimaryKey("zip_code_type_id")]
    [ExplicitColumns]
    public class ZipCodeType : PetaPocoDB.Record<ZipCodeType> 
    {
        [Column("zip_code_type_id")] 
        public int ZipCodeTypeId { get; set; }

        [Column("type")] 
        public string Type { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.zip_codes")]
    [PrimaryKey("zip_code_id")]
    [ExplicitColumns]
    public class ZipCode : PetaPocoDB.Record<ZipCode> 
    {
        [Column("zip_code_id")] 
        public long ZipCodeId { get; set; }

        [Column("state_id")] 
        public int StateId { get; set; }

        [Column("code")] 
        public string Code { get; set; }

        [Column("zip_code_type_id")] 
        public int ZipCodeTypeId { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("lat")] 
        public decimal? Lat { get; set; }

        [Column("lon")] 
        public decimal? Lon { get; set; }

        [Column("x_axis")] 
        public decimal? XAxis { get; set; }

        [Column("y_axis")] 
        public decimal? YAxis { get; set; }

        [Column("z_axis")] 
        public decimal? ZAxis { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.attachment_lookup")]
    [PrimaryKey("attachment_lookup_id")]
    [ExplicitColumns]
    public class AttachmentLookup : PetaPocoDB.Record<AttachmentLookup> 
    {
        [Column("attachment_lookup_id")] 
        public int AttachmentLookupId { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("resource")] 
        public string Resource { get; set; }

        [Column("resource_key")] 
        public string ResourceKey { get; set; }

    }

    [TableName("core.attachments")]
    [PrimaryKey("attachment_id")]
    [ExplicitColumns]
    public class Attachment : PetaPocoDB.Record<Attachment> 
    {
        [Column("attachment_id")] 
        public long AttachmentId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("resource")] 
        public string Resource { get; set; }

        [Column("resource_key")] 
        public string ResourceKey { get; set; }

        [Column("resource_id")] 
        public long ResourceId { get; set; }

        [Column("original_file_name")] 
        public string OriginalFileName { get; set; }

        [Column("file_extension")] 
        public string FileExtension { get; set; }

        [Column("file_path")] 
        public string FilePath { get; set; }

        [Column("comment")] 
        public string Comment { get; set; }

        [Column("added_on")] 
        public DateTime AddedOn { get; set; }

    }

    [TableName("core.exchange_rates")]
    [PrimaryKey("exchange_rate_id")]
    [ExplicitColumns]
    public class ExchangeRate : PetaPocoDB.Record<ExchangeRate> 
    {
        [Column("exchange_rate_id")] 
        public long ExchangeRateId { get; set; }

        [Column("updated_on")] 
        public DateTime UpdatedOn { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("status")] 
        public bool Status { get; set; }

    }

    [TableName("core.exchange_rate_details")]
    [PrimaryKey("exchange_rate_detail_id")]
    [ExplicitColumns]
    public class ExchangeRateDetail : PetaPocoDB.Record<ExchangeRateDetail> 
    {
        [Column("exchange_rate_detail_id")] 
        public long ExchangeRateDetailId { get; set; }

        [Column("exchange_rate_id")] 
        public long ExchangeRateId { get; set; }

        [Column("local_currency_code")] 
        public string LocalCurrencyCode { get; set; }

        [Column("foreign_currency_code")] 
        public string ForeignCurrencyCode { get; set; }

        [Column("unit")] 
        public int Unit { get; set; }

        [Column("exchange_rate")] 
        public decimal ExchangeRate { get; set; }

    }

    [TableName("core.menu_locale")]
    [PrimaryKey("menu_locale_id")]
    [ExplicitColumns]
    public class MenuLocale : PetaPocoDB.Record<MenuLocale> 
    {
        [Column("menu_locale_id")] 
        public int MenuLocaleId { get; set; }

        [Column("menu_id")] 
        public int MenuId { get; set; }

        [Column("culture")] 
        public string Culture { get; set; }

        [Column("menu_text")] 
        public string MenuText { get; set; }

    }

    [TableName("core.menus")]
    [PrimaryKey("menu_id")]
    [ExplicitColumns]
    public class Menu : PetaPocoDB.Record<Menu> 
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
        public int? ParentMenuId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.fiscal_year")]
    [PrimaryKey("fiscal_year_code", autoIncrement=false)]
    [ExplicitColumns]
    public class FiscalYear : PetaPocoDB.Record<FiscalYear> 
    {
        [Column("fiscal_year_code")] 
        public string FiscalYearCode { get; set; }

        [Column("fiscal_year_name")] 
        public string FiscalYearName { get; set; }

        [Column("starts_from")] 
        public DateTime StartsFrom { get; set; }

        [Column("ends_on")] 
        public DateTime EndsOn { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.frequency_setups")]
    [PrimaryKey("frequency_setup_id")]
    [ExplicitColumns]
    public class FrequencySetup : PetaPocoDB.Record<FrequencySetup> 
    {
        [Column("frequency_setup_id")] 
        public int FrequencySetupId { get; set; }

        [Column("fiscal_year_code")] 
        public string FiscalYearCode { get; set; }

        [Column("frequency_setup_code")] 
        public string FrequencySetupCode { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("frequency_id")] 
        public int FrequencyId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.compound_units")]
    [PrimaryKey("compound_unit_id")]
    [ExplicitColumns]
    public class CompoundUnit : PetaPocoDB.Record<CompoundUnit> 
    {
        [Column("compound_unit_id")] 
        public int CompoundUnitId { get; set; }

        [Column("base_unit_id")] 
        public int BaseUnitId { get; set; }

        [Column("value")] 
        public short Value { get; set; }

        [Column("compare_unit_id")] 
        public int CompareUnitId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.transaction_types")]
    [PrimaryKey("transaction_type_id", autoIncrement=false)]
    [ExplicitColumns]
    public class TransactionType : PetaPocoDB.Record<TransactionType> 
    {
        [Column("transaction_type_id")] 
        public short TransactionTypeId { get; set; }

        [Column("transaction_type_code")] 
        public string TransactionTypeCode { get; set; }

        [Column("transaction_type_name")] 
        public string TransactionTypeName { get; set; }

    }

    [TableName("core.cash_flow_headings")]
    [PrimaryKey("cash_flow_heading_id", autoIncrement=false)]
    [ExplicitColumns]
    public class CashFlowHeading : PetaPocoDB.Record<CashFlowHeading> 
    {
        [Column("cash_flow_heading_id")] 
        public int CashFlowHeadingId { get; set; }

        [Column("cash_flow_heading_code")] 
        public string CashFlowHeadingCode { get; set; }

        [Column("cash_flow_heading_name")] 
        public string CashFlowHeadingName { get; set; }

        [Column("cash_flow_heading_type")] 
        public string CashFlowHeadingType { get; set; }

        [Column("is_debit")] 
        public bool IsDebit { get; set; }

        [Column("is_sales")] 
        public bool IsSales { get; set; }

        [Column("is_purchase")] 
        public bool IsPurchase { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.account_masters")]
    [PrimaryKey("account_master_id", autoIncrement=false)]
    [ExplicitColumns]
    public class AccountMaster : PetaPocoDB.Record<AccountMaster> 
    {
        [Column("account_master_id")] 
        public short AccountMasterId { get; set; }

        [Column("account_master_code")] 
        public string AccountMasterCode { get; set; }

        [Column("account_master_name")] 
        public string AccountMasterName { get; set; }

        [Column("normally_debit")] 
        public bool NormallyDebit { get; set; }

        [Column("parent_account_master_id")] 
        public short? ParentAccountMasterId { get; set; }

    }

    [TableName("core.cash_flow_setup")]
    [PrimaryKey("cash_flow_setup_id")]
    [ExplicitColumns]
    public class CashFlowSetup : PetaPocoDB.Record<CashFlowSetup> 
    {
        [Column("cash_flow_setup_id")] 
        public int CashFlowSetupId { get; set; }

        [Column("cash_flow_heading_id")] 
        public int CashFlowHeadingId { get; set; }

        [Column("account_master_id")] 
        public short AccountMasterId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.sales_teams")]
    [PrimaryKey("sales_team_id")]
    [ExplicitColumns]
    public class SalesTeam : PetaPocoDB.Record<SalesTeam> 
    {
        [Column("sales_team_id")] 
        public int SalesTeamId { get; set; }

        [Column("sales_team_code")] 
        public string SalesTeamCode { get; set; }

        [Column("sales_team_name")] 
        public string SalesTeamName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.bonus_slab_details")]
    [PrimaryKey("bonus_slab_detail_id")]
    [ExplicitColumns]
    public class BonusSlabDetail : PetaPocoDB.Record<BonusSlabDetail> 
    {
        [Column("bonus_slab_detail_id")] 
        public int BonusSlabDetailId { get; set; }

        [Column("bonus_slab_id")] 
        public int BonusSlabId { get; set; }

        [Column("amount_from")] 
        public decimal AmountFrom { get; set; }

        [Column("amount_to")] 
        public decimal AmountTo { get; set; }

        [Column("bonus_rate")] 
        public decimal BonusRate { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.bonus_slabs")]
    [PrimaryKey("bonus_slab_id")]
    [ExplicitColumns]
    public class BonusSlab : PetaPocoDB.Record<BonusSlab> 
    {
        [Column("bonus_slab_id")] 
        public int BonusSlabId { get; set; }

        [Column("bonus_slab_code")] 
        public string BonusSlabCode { get; set; }

        [Column("bonus_slab_name")] 
        public string BonusSlabName { get; set; }

        [Column("effective_from")] 
        public DateTime EffectiveFrom { get; set; }

        [Column("ends_on")] 
        public DateTime EndsOn { get; set; }

        [Column("checking_frequency_id")] 
        public int CheckingFrequencyId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.salesperson_bonus_setups")]
    [PrimaryKey("salesperson_bonus_setup_id")]
    [ExplicitColumns]
    public class SalespersonBonusSetup : PetaPocoDB.Record<SalespersonBonusSetup> 
    {
        [Column("salesperson_bonus_setup_id")] 
        public int SalespersonBonusSetupId { get; set; }

        [Column("salesperson_id")] 
        public int SalespersonId { get; set; }

        [Column("bonus_slab_id")] 
        public int BonusSlabId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.ageing_slabs")]
    [PrimaryKey("ageing_slab_id")]
    [ExplicitColumns]
    public class AgeingSlab : PetaPocoDB.Record<AgeingSlab> 
    {
        [Column("ageing_slab_id")] 
        public int AgeingSlabId { get; set; }

        [Column("ageing_slab_name")] 
        public string AgeingSlabName { get; set; }

        [Column("from_days")] 
        public int FromDays { get; set; }

        [Column("to_days")] 
        public int ToDays { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.countries")]
    [PrimaryKey("country_id")]
    [ExplicitColumns]
    public class Country : PetaPocoDB.Record<Country> 
    {
        [Column("country_id")] 
        public int CountryId { get; set; }

        [Column("country_code")] 
        public string CountryCode { get; set; }

        [Column("country_name")] 
        public string CountryName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.income_tax_setup")]
    [PrimaryKey("income_tax_setup_id")]
    [ExplicitColumns]
    public class IncomeTaxSetup : PetaPocoDB.Record<IncomeTaxSetup> 
    {
        [Column("income_tax_setup_id")] 
        public int IncomeTaxSetupId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("effective_from")] 
        public DateTime EffectiveFrom { get; set; }

        [Column("tax_rate")] 
        public decimal TaxRate { get; set; }

        [Column("tax_authority_id")] 
        public int TaxAuthorityId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.states")]
    [PrimaryKey("state_id")]
    [ExplicitColumns]
    public class State : PetaPocoDB.Record<State> 
    {
        [Column("state_id")] 
        public int StateId { get; set; }

        [Column("country_id")] 
        public int CountryId { get; set; }

        [Column("state_code")] 
        public string StateCode { get; set; }

        [Column("state_name")] 
        public string StateName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.counties")]
    [PrimaryKey("county_id")]
    [ExplicitColumns]
    public class County : PetaPocoDB.Record<County> 
    {
        [Column("county_id")] 
        public int CountyId { get; set; }

        [Column("county_code")] 
        public string CountyCode { get; set; }

        [Column("county_name")] 
        public string CountyName { get; set; }

        [Column("state_id")] 
        public int StateId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.tax_base_amount_types")]
    [PrimaryKey("tax_base_amount_type_code", autoIncrement=false)]
    [ExplicitColumns]
    public class TaxBaseAmountType : PetaPocoDB.Record<TaxBaseAmountType> 
    {
        [Column("tax_base_amount_type_code")] 
        public string TaxBaseAmountTypeCode { get; set; }

        [Column("tax_base_amount_type_name")] 
        public string TaxBaseAmountTypeName { get; set; }

    }

    [TableName("core.sales_tax_types")]
    [PrimaryKey("sales_tax_type_id")]
    [ExplicitColumns]
    public class SalesTaxType : PetaPocoDB.Record<SalesTaxType> 
    {
        [Column("sales_tax_type_id")] 
        public int SalesTaxTypeId { get; set; }

        [Column("sales_tax_type_code")] 
        public string SalesTaxTypeCode { get; set; }

        [Column("sales_tax_type_name")] 
        public string SalesTaxTypeName { get; set; }

        [Column("is_vat")] 
        public bool IsVat { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.tax_rate_types")]
    [PrimaryKey("tax_rate_type_code", autoIncrement=false)]
    [ExplicitColumns]
    public class TaxRateType : PetaPocoDB.Record<TaxRateType> 
    {
        [Column("tax_rate_type_code")] 
        public string TaxRateTypeCode { get; set; }

        [Column("tax_rate_type_name")] 
        public string TaxRateTypeName { get; set; }

    }

    [TableName("core.tax_authorities")]
    [PrimaryKey("tax_authority_id")]
    [ExplicitColumns]
    public class TaxAuthority : PetaPocoDB.Record<TaxAuthority> 
    {
        [Column("tax_authority_id")] 
        public int TaxAuthorityId { get; set; }

        [Column("tax_master_id")] 
        public int TaxMasterId { get; set; }

        [Column("tax_authority_code")] 
        public string TaxAuthorityCode { get; set; }

        [Column("tax_authority_name")] 
        public string TaxAuthorityName { get; set; }

        [Column("country_id")] 
        public int CountryId { get; set; }

        [Column("state_id")] 
        public int? StateId { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.rounding_methods")]
    [PrimaryKey("rounding_method_code", autoIncrement=false)]
    [ExplicitColumns]
    public class RoundingMethod : PetaPocoDB.Record<RoundingMethod> 
    {
        [Column("rounding_method_code")] 
        public string RoundingMethodCode { get; set; }

        [Column("rounding_method_name")] 
        public string RoundingMethodName { get; set; }

    }

    [TableName("core.tax_master")]
    [PrimaryKey("tax_master_id")]
    [ExplicitColumns]
    public class TaxMaster : PetaPocoDB.Record<TaxMaster> 
    {
        [Column("tax_master_id")] 
        public int TaxMasterId { get; set; }

        [Column("tax_master_code")] 
        public string TaxMasterCode { get; set; }

        [Column("tax_master_name")] 
        public string TaxMasterName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.tax_exempt_types")]
    [PrimaryKey("tax_exempt_type_id")]
    [ExplicitColumns]
    public class TaxExemptType : PetaPocoDB.Record<TaxExemptType> 
    {
        [Column("tax_exempt_type_id")] 
        public int TaxExemptTypeId { get; set; }

        [Column("tax_exempt_type_code")] 
        public string TaxExemptTypeCode { get; set; }

        [Column("tax_exempt_type_name")] 
        public string TaxExemptTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.entities")]
    [PrimaryKey("entity_id")]
    [ExplicitColumns]
    public class Entity : PetaPocoDB.Record<Entity> 
    {
        [Column("entity_id")] 
        public int EntityId { get; set; }

        [Column("entity_name")] 
        public string EntityName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.industries")]
    [PrimaryKey("industry_id")]
    [ExplicitColumns]
    public class Industry : PetaPocoDB.Record<Industry> 
    {
        [Column("industry_id")] 
        public int IndustryId { get; set; }

        [Column("industry_name")] 
        public string IndustryName { get; set; }

        [Column("parent_industry_id")] 
        public int? ParentIndustryId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.item_groups")]
    [PrimaryKey("item_group_id")]
    [ExplicitColumns]
    public class ItemGroup : PetaPocoDB.Record<ItemGroup> 
    {
        [Column("item_group_id")] 
        public int ItemGroupId { get; set; }

        [Column("item_group_code")] 
        public string ItemGroupCode { get; set; }

        [Column("item_group_name")] 
        public string ItemGroupName { get; set; }

        [Column("exclude_from_purchase")] 
        public bool ExcludeFromPurchase { get; set; }

        [Column("exclude_from_sales")] 
        public bool ExcludeFromSales { get; set; }

        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("sales_account_id")] 
        public long SalesAccountId { get; set; }

        [Column("sales_discount_account_id")] 
        public long SalesDiscountAccountId { get; set; }

        [Column("sales_return_account_id")] 
        public long SalesReturnAccountId { get; set; }

        [Column("purchase_account_id")] 
        public long PurchaseAccountId { get; set; }

        [Column("purchase_discount_account_id")] 
        public long PurchaseDiscountAccountId { get; set; }

        [Column("inventory_account_id")] 
        public long InventoryAccountId { get; set; }

        [Column("cost_of_goods_sold_account_id")] 
        public long CostOfGoodsSoldAccountId { get; set; }

        [Column("parent_item_group_id")] 
        public int? ParentItemGroupId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.item_types")]
    [PrimaryKey("item_type_id")]
    [ExplicitColumns]
    public class ItemType : PetaPocoDB.Record<ItemType> 
    {
        [Column("item_type_id")] 
        public int ItemTypeId { get; set; }

        [Column("item_type_code")] 
        public string ItemTypeCode { get; set; }

        [Column("item_type_name")] 
        public string ItemTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.brands")]
    [PrimaryKey("brand_id")]
    [ExplicitColumns]
    public class Brand : PetaPocoDB.Record<Brand> 
    {
        [Column("brand_id")] 
        public int BrandId { get; set; }

        [Column("brand_code")] 
        public string BrandCode { get; set; }

        [Column("brand_name")] 
        public string BrandName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.shipping_mail_types")]
    [PrimaryKey("shipping_mail_type_id")]
    [ExplicitColumns]
    public class ShippingMailType : PetaPocoDB.Record<ShippingMailType> 
    {
        [Column("shipping_mail_type_id")] 
        public int ShippingMailTypeId { get; set; }

        [Column("shipping_mail_type_code")] 
        public string ShippingMailTypeCode { get; set; }

        [Column("shipping_mail_type_name")] 
        public string ShippingMailTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.shipping_package_shapes")]
    [PrimaryKey("shipping_package_shape_id")]
    [ExplicitColumns]
    public class ShippingPackageShape : PetaPocoDB.Record<ShippingPackageShape> 
    {
        [Column("shipping_package_shape_id")] 
        public int ShippingPackageShapeId { get; set; }

        [Column("shipping_package_shape_code")] 
        public string ShippingPackageShapeCode { get; set; }

        [Column("shipping_package_shape_name")] 
        public string ShippingPackageShapeName { get; set; }

        [Column("is_rectangular")] 
        public bool IsRectangular { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.sales_tax_exempt_details")]
    [PrimaryKey("sales_tax_exempt_detail_id")]
    [ExplicitColumns]
    public class SalesTaxExemptDetail : PetaPocoDB.Record<SalesTaxExemptDetail> 
    {
        [Column("sales_tax_exempt_detail_id")] 
        public int SalesTaxExemptDetailId { get; set; }

        [Column("sales_tax_exempt_id")] 
        public int? SalesTaxExemptId { get; set; }

        [Column("entity_id")] 
        public int? EntityId { get; set; }

        [Column("industry_id")] 
        public int? IndustryId { get; set; }

        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("item_group_id")] 
        public int? ItemGroupId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.late_fee")]
    [PrimaryKey("late_fee_id")]
    [ExplicitColumns]
    public class LateFee : PetaPocoDB.Record<LateFee> 
    {
        [Column("late_fee_id")] 
        public int LateFeeId { get; set; }

        [Column("late_fee_code")] 
        public string LateFeeCode { get; set; }

        [Column("late_fee_name")] 
        public string LateFeeName { get; set; }

        [Column("is_flat_amount")] 
        public bool IsFlatAmount { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.compound_items")]
    [PrimaryKey("compound_item_id")]
    [ExplicitColumns]
    public class CompoundItem : PetaPocoDB.Record<CompoundItem> 
    {
        [Column("compound_item_id")] 
        public int CompoundItemId { get; set; }

        [Column("compound_item_code")] 
        public string CompoundItemCode { get; set; }

        [Column("compound_item_name")] 
        public string CompoundItemName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.frequencies")]
    [PrimaryKey("frequency_id")]
    [ExplicitColumns]
    public class Frequency : PetaPocoDB.Record<Frequency> 
    {
        [Column("frequency_id")] 
        public int FrequencyId { get; set; }

        [Column("frequency_code")] 
        public string FrequencyCode { get; set; }

        [Column("frequency_name")] 
        public string FrequencyName { get; set; }

    }

    [TableName("core.recurring_invoices")]
    [PrimaryKey("recurring_invoice_id")]
    [ExplicitColumns]
    public class RecurringInvoice : PetaPocoDB.Record<RecurringInvoice> 
    {
        [Column("recurring_invoice_id")] 
        public int RecurringInvoiceId { get; set; }

        [Column("recurring_invoice_code")] 
        public string RecurringInvoiceCode { get; set; }

        [Column("recurring_invoice_name")] 
        public string RecurringInvoiceName { get; set; }

        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("compound_item_id")] 
        public int? CompoundItemId { get; set; }

        [Column("recurring_frequency_id")] 
        public int RecurringFrequencyId { get; set; }

        [Column("recurring_amount")] 
        public decimal RecurringAmount { get; set; }

        [Column("auto_trigger_on_sales")] 
        public bool AutoTriggerOnSales { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.recurring_invoice_setup")]
    [PrimaryKey("recurring_invoice_setup_id")]
    [ExplicitColumns]
    public class RecurringInvoiceSetup : PetaPocoDB.Record<RecurringInvoiceSetup> 
    {
        [Column("recurring_invoice_setup_id")] 
        public int RecurringInvoiceSetupId { get; set; }

        [Column("recurring_invoice_id")] 
        public int RecurringInvoiceId { get; set; }

        [Column("party_id")] 
        public long PartyId { get; set; }

        [Column("starts_from")] 
        public DateTime StartsFrom { get; set; }

        [Column("ends_on")] 
        public DateTime EndsOn { get; set; }

        [Column("recurring_amount")] 
        public decimal RecurringAmount { get; set; }

        [Column("payment_term_id")] 
        public int PaymentTermId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.party_types")]
    [PrimaryKey("party_type_id")]
    [ExplicitColumns]
    public class PartyType : PetaPocoDB.Record<PartyType> 
    {
        [Column("party_type_id")] 
        public int PartyTypeId { get; set; }

        [Column("party_type_code")] 
        public string PartyTypeCode { get; set; }

        [Column("party_type_name")] 
        public string PartyTypeName { get; set; }

        [Column("is_supplier")] 
        public bool IsSupplier { get; set; }

        [Column("account_id")] 
        public long AccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.sales_tax_exempts")]
    [PrimaryKey("sales_tax_exempt_id")]
    [ExplicitColumns]
    public class SalesTaxExempt : PetaPocoDB.Record<SalesTaxExempt> 
    {
        [Column("sales_tax_exempt_id")] 
        public int SalesTaxExemptId { get; set; }

        [Column("tax_master_id")] 
        public int TaxMasterId { get; set; }

        [Column("sales_tax_exempt_code")] 
        public string SalesTaxExemptCode { get; set; }

        [Column("sales_tax_exempt_name")] 
        public string SalesTaxExemptName { get; set; }

        [Column("tax_exempt_type_id")] 
        public int TaxExemptTypeId { get; set; }

        [Column("store_id")] 
        public int StoreId { get; set; }

        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("valid_from")] 
        public DateTime ValidFrom { get; set; }

        [Column("valid_till")] 
        public DateTime ValidTill { get; set; }

        [Column("price_from")] 
        public decimal PriceFrom { get; set; }

        [Column("price_to")] 
        public decimal PriceTo { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.verification_statuses")]
    [PrimaryKey("verification_status_id", autoIncrement=false)]
    [ExplicitColumns]
    public class VerificationStatus : PetaPocoDB.Record<VerificationStatus> 
    {
        [Column("verification_status_id")] 
        public short VerificationStatusId { get; set; }

        [Column("verification_status_name")] 
        public string VerificationStatusName { get; set; }

    }

    [TableName("core.accounts")]
    [PrimaryKey("account_id")]
    [ExplicitColumns]
    public class Account : PetaPocoDB.Record<Account> 
    {
        [Column("account_id")] 
        public long AccountId { get; set; }

        [Column("account_master_id")] 
        public short AccountMasterId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("external_code")] 
        public string ExternalCode { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("confidential")] 
        public bool Confidential { get; set; }

        [Column("is_transaction_node")] 
        public bool IsTransactionNode { get; set; }

        [Column("sys_type")] 
        public bool SysType { get; set; }

        [Column("parent_account_id")] 
        public long? ParentAccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.currencies")]
    [PrimaryKey("currency_code", autoIncrement=false)]
    [ExplicitColumns]
    public class Currency : PetaPocoDB.Record<Currency> 
    {
        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("currency_symbol")] 
        public string CurrencySymbol { get; set; }

        [Column("currency_name")] 
        public string CurrencyName { get; set; }

        [Column("hundredth_name")] 
        public string HundredthName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.payment_terms")]
    [PrimaryKey("payment_term_id")]
    [ExplicitColumns]
    public class PaymentTerm : PetaPocoDB.Record<PaymentTerm> 
    {
        [Column("payment_term_id")] 
        public int PaymentTermId { get; set; }

        [Column("payment_term_code")] 
        public string PaymentTermCode { get; set; }

        [Column("payment_term_name")] 
        public string PaymentTermName { get; set; }

        [Column("due_on_date")] 
        public bool DueOnDate { get; set; }

        [Column("due_days")] 
        public int DueDays { get; set; }

        [Column("due_frequency_id")] 
        public int? DueFrequencyId { get; set; }

        [Column("grace_peiod")] 
        public int GracePeiod { get; set; }

        [Column("late_fee_id")] 
        public int? LateFeeId { get; set; }

        [Column("late_fee_posting_frequency_id")] 
        public int? LateFeePostingFrequencyId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.price_types")]
    [PrimaryKey("price_type_id")]
    [ExplicitColumns]
    public class PriceType : PetaPocoDB.Record<PriceType> 
    {
        [Column("price_type_id")] 
        public int PriceTypeId { get; set; }

        [Column("price_type_code")] 
        public string PriceTypeCode { get; set; }

        [Column("price_type_name")] 
        public string PriceTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.salespersons")]
    [PrimaryKey("salesperson_id")]
    [ExplicitColumns]
    public class Salesperson : PetaPocoDB.Record<Salesperson> 
    {
        [Column("salesperson_id")] 
        public int SalespersonId { get; set; }

        [Column("sales_team_id")] 
        public int SalesTeamId { get; set; }

        [Column("salesperson_code")] 
        public string SalespersonCode { get; set; }

        [Column("salesperson_name")] 
        public string SalespersonName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

        [Column("contact_number")] 
        public string ContactNumber { get; set; }

        [Column("commission_rate")] 
        public decimal CommissionRate { get; set; }

        [Column("account_id")] 
        public long AccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.units")]
    [PrimaryKey("unit_id")]
    [ExplicitColumns]
    public class Unit : PetaPocoDB.Record<Unit> 
    {
        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("unit_code")] 
        public string UnitCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.sales_taxes")]
    [PrimaryKey("sales_tax_id")]
    [ExplicitColumns]
    public class SalesTax : PetaPocoDB.Record<SalesTax> 
    {
        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("tax_master_id")] 
        public int TaxMasterId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("sales_tax_code")] 
        public string SalesTaxCode { get; set; }

        [Column("sales_tax_name")] 
        public string SalesTaxName { get; set; }

        [Column("is_exemption")] 
        public bool IsExemption { get; set; }

        [Column("tax_base_amount_type_code")] 
        public string TaxBaseAmountTypeCode { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.sales_tax_details")]
    [PrimaryKey("sales_tax_detail_id")]
    [ExplicitColumns]
    public class SalesTaxDetail : PetaPocoDB.Record<SalesTaxDetail> 
    {
        [Column("sales_tax_detail_id")] 
        public int SalesTaxDetailId { get; set; }

        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("sales_tax_type_id")] 
        public int SalesTaxTypeId { get; set; }

        [Column("priority")] 
        public short Priority { get; set; }

        [Column("sales_tax_detail_code")] 
        public string SalesTaxDetailCode { get; set; }

        [Column("sales_tax_detail_name")] 
        public string SalesTaxDetailName { get; set; }

        [Column("based_on_shipping_address")] 
        public bool BasedOnShippingAddress { get; set; }

        [Column("check_nexus")] 
        public bool CheckNexus { get; set; }

        [Column("applied_on_shipping_charge")] 
        public bool AppliedOnShippingCharge { get; set; }

        [Column("state_sales_tax_id")] 
        public int? StateSalesTaxId { get; set; }

        [Column("county_sales_tax_id")] 
        public int? CountySalesTaxId { get; set; }

        [Column("tax_rate_type_code")] 
        public string TaxRateTypeCode { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("reporting_tax_authority_id")] 
        public int ReportingTaxAuthorityId { get; set; }

        [Column("collecting_tax_authority_id")] 
        public int CollectingTaxAuthorityId { get; set; }

        [Column("collecting_account_id")] 
        public long CollectingAccountId { get; set; }

        [Column("use_tax_collecting_account_id")] 
        public long? UseTaxCollectingAccountId { get; set; }

        [Column("rounding_method_code")] 
        public string RoundingMethodCode { get; set; }

        [Column("rounding_decimal_places")] 
        public int RoundingDecimalPlaces { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.state_sales_taxes")]
    [PrimaryKey("state_sales_tax_id")]
    [ExplicitColumns]
    public class StateSalesTax : PetaPocoDB.Record<StateSalesTax> 
    {
        [Column("state_sales_tax_id")] 
        public int StateSalesTaxId { get; set; }

        [Column("state_sales_tax_code")] 
        public string StateSalesTaxCode { get; set; }

        [Column("state_sales_tax_name")] 
        public string StateSalesTaxName { get; set; }

        [Column("state_id")] 
        public int StateId { get; set; }

        [Column("entity_id")] 
        public int? EntityId { get; set; }

        [Column("industry_id")] 
        public int? IndustryId { get; set; }

        [Column("item_group_id")] 
        public int? ItemGroupId { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.county_sales_taxes")]
    [PrimaryKey("county_sales_tax_id")]
    [ExplicitColumns]
    public class CountySalesTax : PetaPocoDB.Record<CountySalesTax> 
    {
        [Column("county_sales_tax_id")] 
        public int CountySalesTaxId { get; set; }

        [Column("county_sales_tax_code")] 
        public string CountySalesTaxCode { get; set; }

        [Column("county_sales_tax_name")] 
        public string CountySalesTaxName { get; set; }

        [Column("county_id")] 
        public int? CountyId { get; set; }

        [Column("entity_id")] 
        public int? EntityId { get; set; }

        [Column("industry_id")] 
        public int? IndustryId { get; set; }

        [Column("item_group_id")] 
        public int? ItemGroupId { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.config")]
    [PrimaryKey("config_id", autoIncrement=false)]
    [ExplicitColumns]
    public class Config : PetaPocoDB.Record<Config> 
    {
        [Column("config_id")] 
        public int ConfigId { get; set; }

        [Column("config_name")] 
        public string ConfigName { get; set; }

    }

    [TableName("core.bank_accounts")]
    [PrimaryKey("account_id", autoIncrement=false)]
    [ExplicitColumns]
    public class BankAccount : PetaPocoDB.Record<BankAccount> 
    {
        [Column("account_id")] 
        public long AccountId { get; set; }

        [Column("maintained_by_user_id")] 
        public int MaintainedByUserId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("bank_name")] 
        public string BankName { get; set; }

        [Column("bank_branch")] 
        public string BankBranch { get; set; }

        [Column("bank_contact_number")] 
        public string BankContactNumber { get; set; }

        [Column("bank_address")] 
        public string BankAddress { get; set; }

        [Column("bank_account_number")] 
        public string BankAccountNumber { get; set; }

        [Column("bank_account_type")] 
        public string BankAccountType { get; set; }

        [Column("relationship_officer_name")] 
        public string RelationshipOfficerName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.account_scrud_view")]
    [ExplicitColumns]
    public class AccountScrudView : PetaPocoDB.Record<AccountScrudView> 
    {
        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_master_code")] 
        public string AccountMasterCode { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("external_code")] 
        public string ExternalCode { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("confidential")] 
        public bool? Confidential { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("sys_type")] 
        public bool? SysType { get; set; }

        [Column("parent")] 
        public string Parent { get; set; }

        [Column("has_child")] 
        public bool? HasChild { get; set; }

    }

    [TableName("core.bonus_slab_detail_scrud_view")]
    [ExplicitColumns]
    public class BonusSlabDetailScrudView : PetaPocoDB.Record<BonusSlabDetailScrudView> 
    {
        [Column("bonus_slab_detail_id")] 
        public int? BonusSlabDetailId { get; set; }

        [Column("bonus_slab_id")] 
        public int? BonusSlabId { get; set; }

        [Column("slab_name")] 
        public string SlabName { get; set; }

        [Column("amount_from")] 
        public decimal? AmountFrom { get; set; }

        [Column("amount_to")] 
        public decimal? AmountTo { get; set; }

        [Column("bonus_rate")] 
        public decimal? BonusRate { get; set; }

    }

    [TableName("core.bonus_slab_scrud_view")]
    [ExplicitColumns]
    public class BonusSlabScrudView : PetaPocoDB.Record<BonusSlabScrudView> 
    {
        [Column("bonus_slab_id")] 
        public int? BonusSlabId { get; set; }

        [Column("bonus_slab_code")] 
        public string BonusSlabCode { get; set; }

        [Column("bonus_slab_name")] 
        public string BonusSlabName { get; set; }

        [Column("effective_from")] 
        public DateTime? EffectiveFrom { get; set; }

        [Column("ends_on")] 
        public DateTime? EndsOn { get; set; }

        [Column("checking_frequency")] 
        public string CheckingFrequency { get; set; }

    }

    [TableName("core.brand_scrud_view")]
    [ExplicitColumns]
    public class BrandScrudView : PetaPocoDB.Record<BrandScrudView> 
    {
        [Column("brand_id")] 
        public int? BrandId { get; set; }

        [Column("brand_code")] 
        public string BrandCode { get; set; }

        [Column("brand_name")] 
        public string BrandName { get; set; }

    }

    [TableName("core.cash_flow_heading_scrud_view")]
    [ExplicitColumns]
    public class CashFlowHeadingScrudView : PetaPocoDB.Record<CashFlowHeadingScrudView> 
    {
        [Column("cash_flow_heading_id")] 
        public int? CashFlowHeadingId { get; set; }

        [Column("cash_flow_heading_code")] 
        public string CashFlowHeadingCode { get; set; }

        [Column("cash_flow_heading_name")] 
        public string CashFlowHeadingName { get; set; }

        [Column("cash_flow_heading_type")] 
        public string CashFlowHeadingType { get; set; }

        [Column("is_debit")] 
        public bool? IsDebit { get; set; }

        [Column("is_sales")] 
        public bool? IsSales { get; set; }

        [Column("is_purchase")] 
        public bool? IsPurchase { get; set; }

    }

    [TableName("core.cash_flow_setup_scrud_view")]
    [ExplicitColumns]
    public class CashFlowSetupScrudView : PetaPocoDB.Record<CashFlowSetupScrudView> 
    {
        [Column("cash_flow_setup_id")] 
        public int? CashFlowSetupId { get; set; }

        [Column("cash_flow_heading")] 
        public string CashFlowHeading { get; set; }

        [Column("account_master")] 
        public string AccountMaster { get; set; }

    }

    [TableName("core.compound_item_detail_scrud_view")]
    [ExplicitColumns]
    public class CompoundItemDetailScrudView : PetaPocoDB.Record<CompoundItemDetailScrudView> 
    {
        [Column("compound_item_detail_id")] 
        public int? CompoundItemDetailId { get; set; }

        [Column("compound_item_id")] 
        public int? CompoundItemId { get; set; }

        [Column("compound_item_code")] 
        public string CompoundItemCode { get; set; }

        [Column("compound_item_name")] 
        public string CompoundItemName { get; set; }

        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("item")] 
        public string Item { get; set; }

        [Column("unit")] 
        public string Unit { get; set; }

        [Column("quantity")] 
        public int? Quantity { get; set; }

    }

    [TableName("core.compound_item_scrud_view")]
    [ExplicitColumns]
    public class CompoundItemScrudView : PetaPocoDB.Record<CompoundItemScrudView> 
    {
        [Column("compound_item_id")] 
        public int? CompoundItemId { get; set; }

        [Column("compound_item_code")] 
        public string CompoundItemCode { get; set; }

        [Column("compound_item_name")] 
        public string CompoundItemName { get; set; }

    }

    [TableName("core.compound_unit_scrud_view")]
    [ExplicitColumns]
    public class CompoundUnitScrudView : PetaPocoDB.Record<CompoundUnitScrudView> 
    {
        [Column("compound_unit_id")] 
        public int? CompoundUnitId { get; set; }

        [Column("base_unit_name")] 
        public string BaseUnitName { get; set; }

        [Column("value")] 
        public short? Value { get; set; }

        [Column("compare_unit_name")] 
        public string CompareUnitName { get; set; }

    }

    [TableName("core.country_scrud_view")]
    [ExplicitColumns]
    public class CountryScrudView : PetaPocoDB.Record<CountryScrudView> 
    {
        [Column("country_id")] 
        public int? CountryId { get; set; }

        [Column("country_code")] 
        public string CountryCode { get; set; }

        [Column("country_name")] 
        public string CountryName { get; set; }

    }

    [TableName("core.county_sales_tax_scrud_view")]
    [ExplicitColumns]
    public class CountySalesTaxScrudView : PetaPocoDB.Record<CountySalesTaxScrudView> 
    {
        [Column("county_sales_tax_id")] 
        public int? CountySalesTaxId { get; set; }

        [Column("county_sales_tax_code")] 
        public string CountySalesTaxCode { get; set; }

        [Column("county_sales_tax_name")] 
        public string CountySalesTaxName { get; set; }

        [Column("county")] 
        public string County { get; set; }

        [Column("entity_name")] 
        public string EntityName { get; set; }

        [Column("industry_name")] 
        public string IndustryName { get; set; }

        [Column("item_group")] 
        public string ItemGroup { get; set; }

        [Column("rate")] 
        public decimal? Rate { get; set; }

    }

    [TableName("core.county_scrud_view")]
    [ExplicitColumns]
    public class CountyScrudView : PetaPocoDB.Record<CountyScrudView> 
    {
        [Column("county_id")] 
        public int? CountyId { get; set; }

        [Column("county_code")] 
        public string CountyCode { get; set; }

        [Column("county_name")] 
        public string CountyName { get; set; }

        [Column("state")] 
        public string State { get; set; }

    }

    [TableName("core.currency_scrud_view")]
    [ExplicitColumns]
    public class CurrencyScrudView : PetaPocoDB.Record<CurrencyScrudView> 
    {
        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("currency_symbol")] 
        public string CurrencySymbol { get; set; }

        [Column("currency_name")] 
        public string CurrencyName { get; set; }

        [Column("hundredth_name")] 
        public string HundredthName { get; set; }

    }

    [TableName("core.entity_scrud_view")]
    [ExplicitColumns]
    public class EntityScrudView : PetaPocoDB.Record<EntityScrudView> 
    {
        [Column("entity_id")] 
        public int? EntityId { get; set; }

        [Column("entity_name")] 
        public string EntityName { get; set; }

    }

    [TableName("core.fiscal_year_scrud_view")]
    [ExplicitColumns]
    public class FiscalYearScrudView : PetaPocoDB.Record<FiscalYearScrudView> 
    {
        [Column("fiscal_year_code")] 
        public string FiscalYearCode { get; set; }

        [Column("fiscal_year_name")] 
        public string FiscalYearName { get; set; }

        [Column("starts_from")] 
        public DateTime? StartsFrom { get; set; }

        [Column("ends_on")] 
        public DateTime? EndsOn { get; set; }

    }

    [TableName("core.frequency_setup_scrud_view")]
    [ExplicitColumns]
    public class FrequencySetupScrudView : PetaPocoDB.Record<FrequencySetupScrudView> 
    {
        [Column("frequency_setup_id")] 
        public int? FrequencySetupId { get; set; }

        [Column("fiscal_year_code")] 
        public string FiscalYearCode { get; set; }

        [Column("value_date")] 
        public DateTime? ValueDate { get; set; }

        [Column("frequency_code")] 
        public string FrequencyCode { get; set; }

    }

    [TableName("core.industry_scrud_view")]
    [ExplicitColumns]
    public class IndustryScrudView : PetaPocoDB.Record<IndustryScrudView> 
    {
        [Column("industry_id")] 
        public int? IndustryId { get; set; }

        [Column("industry_name")] 
        public string IndustryName { get; set; }

        [Column("parent_industry_name")] 
        public string ParentIndustryName { get; set; }

    }

    [TableName("core.item_cost_price_scrud_view")]
    [ExplicitColumns]
    public class ItemCostPriceScrudView : PetaPocoDB.Record<ItemCostPriceScrudView> 
    {
        [Column("item_cost_price_id")] 
        public long? ItemCostPriceId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("party_code")] 
        public string PartyCode { get; set; }

        [Column("party_name")] 
        public string PartyName { get; set; }

        [Column("unit")] 
        public string Unit { get; set; }

        [Column("price")] 
        public decimal? Price { get; set; }

    }

    [TableName("core.item_group_scrud_view")]
    [ExplicitColumns]
    public class ItemGroupScrudView : PetaPocoDB.Record<ItemGroupScrudView> 
    {
        [Column("item_group_id")] 
        public int? ItemGroupId { get; set; }

        [Column("item_group_code")] 
        public string ItemGroupCode { get; set; }

        [Column("item_group_name")] 
        public string ItemGroupName { get; set; }

        [Column("exclude_from_purchase")] 
        public bool? ExcludeFromPurchase { get; set; }

        [Column("exclude_from_sales")] 
        public bool? ExcludeFromSales { get; set; }

        [Column("sales_tax")] 
        public string SalesTax { get; set; }

        [Column("parent")] 
        public string Parent { get; set; }

    }

    [TableName("core.item_scrud_view")]
    [ExplicitColumns]
    public class ItemScrudView : PetaPocoDB.Record<ItemScrudView> 
    {
        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("item_group")] 
        public string ItemGroup { get; set; }

        [Column("maintain_stock")] 
        public bool? MaintainStock { get; set; }

        [Column("brand")] 
        public string Brand { get; set; }

        [Column("preferred_supplier")] 
        public string PreferredSupplier { get; set; }

        [Column("lead_time_in_days")] 
        public int? LeadTimeInDays { get; set; }

        [Column("weight_in_grams")] 
        public double? WeightInGrams { get; set; }

        [Column("width_in_centimeters")] 
        public double? WidthInCentimeters { get; set; }

        [Column("height_in_centimeters")] 
        public double? HeightInCentimeters { get; set; }

        [Column("length_in_centimeters")] 
        public double? LengthInCentimeters { get; set; }

        [Column("machinable")] 
        public bool? Machinable { get; set; }

        [Column("preferred_shipping_mail_type")] 
        public string PreferredShippingMailType { get; set; }

        [Column("preferred_shipping_package_shape")] 
        public string PreferredShippingPackageShape { get; set; }

        [Column("unit")] 
        public string Unit { get; set; }

        [Column("hot_item")] 
        public bool? HotItem { get; set; }

        [Column("cost_price")] 
        public decimal? CostPrice { get; set; }

        [Column("cost_price_includes_tax")] 
        public bool? CostPriceIncludesTax { get; set; }

        [Column("selling_price")] 
        public decimal? SellingPrice { get; set; }

        [Column("selling_price_includes_tax")] 
        public bool? SellingPriceIncludesTax { get; set; }

        [Column("sales_tax")] 
        public string SalesTax { get; set; }

        [Column("reorder_unit")] 
        public string ReorderUnit { get; set; }

        [Column("reorder_level")] 
        public int? ReorderLevel { get; set; }

        [Column("reorder_quantity")] 
        public int? ReorderQuantity { get; set; }

    }

    [TableName("core.item_selling_price_scrud_view")]
    [ExplicitColumns]
    public class ItemSellingPriceScrudView : PetaPocoDB.Record<ItemSellingPriceScrudView> 
    {
        [Column("item_selling_price_id")] 
        public long? ItemSellingPriceId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("party_type_code")] 
        public string PartyTypeCode { get; set; }

        [Column("party_type_name")] 
        public string PartyTypeName { get; set; }

        [Column("unit")] 
        public string Unit { get; set; }

        [Column("price")] 
        public decimal? Price { get; set; }

    }

    [TableName("core.item_type_scrud_view")]
    [ExplicitColumns]
    public class ItemTypeScrudView : PetaPocoDB.Record<ItemTypeScrudView> 
    {
        [Column("item_type_id")] 
        public int? ItemTypeId { get; set; }

        [Column("item_type_code")] 
        public string ItemTypeCode { get; set; }

        [Column("item_type_name")] 
        public string ItemTypeName { get; set; }

    }

    [TableName("core.late_fee_scrud_view")]
    [ExplicitColumns]
    public class LateFeeScrudView : PetaPocoDB.Record<LateFeeScrudView> 
    {
        [Column("late_fee_id")] 
        public int? LateFeeId { get; set; }

        [Column("late_fee_code")] 
        public string LateFeeCode { get; set; }

        [Column("late_fee_name")] 
        public string LateFeeName { get; set; }

        [Column("is_flat_amount")] 
        public bool? IsFlatAmount { get; set; }

        [Column("rate")] 
        public decimal? Rate { get; set; }

    }

    [TableName("core.party_scrud_view")]
    [ExplicitColumns]
    public class PartyScrudView : PetaPocoDB.Record<PartyScrudView> 
    {
        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("is_supplier")] 
        public bool? IsSupplier { get; set; }

        [Column("party_type")] 
        public string PartyType { get; set; }

        [Column("party_code")] 
        public string PartyCode { get; set; }

        [Column("first_name")] 
        public string FirstName { get; set; }

        [Column("middle_name")] 
        public string MiddleName { get; set; }

        [Column("last_name")] 
        public string LastName { get; set; }

        [Column("party_name")] 
        public string PartyName { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

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

        [Column("country")] 
        public string Country { get; set; }

        [Column("allow_credit")] 
        public bool? AllowCredit { get; set; }

        [Column("maximum_credit_period")] 
        public short? MaximumCreditPeriod { get; set; }

        [Column("maximum_credit_amount")] 
        public decimal? MaximumCreditAmount { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("gl_head")] 
        public string GlHead { get; set; }

    }

    [TableName("core.party_type_scrud_view")]
    [ExplicitColumns]
    public class PartyTypeScrudView : PetaPocoDB.Record<PartyTypeScrudView> 
    {
        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("party_type_code")] 
        public string PartyTypeCode { get; set; }

        [Column("party_type_name")] 
        public string PartyTypeName { get; set; }

        [Column("is_supplier")] 
        public bool? IsSupplier { get; set; }

    }

    [TableName("core.payment_term_scrud_view")]
    [ExplicitColumns]
    public class PaymentTermScrudView : PetaPocoDB.Record<PaymentTermScrudView> 
    {
        [Column("payment_term_id")] 
        public int? PaymentTermId { get; set; }

        [Column("payment_term_code")] 
        public string PaymentTermCode { get; set; }

        [Column("payment_term_name")] 
        public string PaymentTermName { get; set; }

        [Column("due_on_date")] 
        public bool? DueOnDate { get; set; }

        [Column("due_days")] 
        public int? DueDays { get; set; }

        [Column("due_frequency")] 
        public string DueFrequency { get; set; }

        [Column("grace_peiod")] 
        public int? GracePeiod { get; set; }

        [Column("late_fee")] 
        public string LateFee { get; set; }

        [Column("late_fee_posting_frequency")] 
        public string LateFeePostingFrequency { get; set; }

    }

    [TableName("core.recurring_invoice_scrud_view")]
    [ExplicitColumns]
    public class RecurringInvoiceScrudView : PetaPocoDB.Record<RecurringInvoiceScrudView> 
    {
        [Column("recurring_invoice_id")] 
        public int? RecurringInvoiceId { get; set; }

        [Column("recurring_invoice_code")] 
        public string RecurringInvoiceCode { get; set; }

        [Column("recurring_invoice_name")] 
        public string RecurringInvoiceName { get; set; }

        [Column("item")] 
        public string Item { get; set; }

        [Column("compound_item")] 
        public string CompoundItem { get; set; }

        [Column("recurring_frequency")] 
        public string RecurringFrequency { get; set; }

        [Column("recurring_amount")] 
        public decimal? RecurringAmount { get; set; }

        [Column("auto_trigger_on_sales")] 
        public bool? AutoTriggerOnSales { get; set; }

    }

    [TableName("core.recurring_invoice_setup_scrud_view")]
    [ExplicitColumns]
    public class RecurringInvoiceSetupScrudView : PetaPocoDB.Record<RecurringInvoiceSetupScrudView> 
    {
        [Column("recurring_invoice_setup_id")] 
        public int? RecurringInvoiceSetupId { get; set; }

        [Column("recurring_invoice")] 
        public string RecurringInvoice { get; set; }

        [Column("party")] 
        public string Party { get; set; }

        [Column("starts_from")] 
        public DateTime? StartsFrom { get; set; }

        [Column("ends_on")] 
        public DateTime? EndsOn { get; set; }

        [Column("recurring_amount")] 
        public decimal? RecurringAmount { get; set; }

        [Column("payment_term")] 
        public string PaymentTerm { get; set; }

    }

    [TableName("core.sales_tax_detail_scrud_view")]
    [ExplicitColumns]
    public class SalesTaxDetailScrudView : PetaPocoDB.Record<SalesTaxDetailScrudView> 
    {
        [Column("sales_tax_detail_id")] 
        public int? SalesTaxDetailId { get; set; }

        [Column("sales_tax")] 
        public string SalesTax { get; set; }

        [Column("sales_tax_type")] 
        public string SalesTaxType { get; set; }

        [Column("priority")] 
        public short? Priority { get; set; }

        [Column("sales_tax_detail_code")] 
        public string SalesTaxDetailCode { get; set; }

        [Column("sales_tax_detail_name")] 
        public string SalesTaxDetailName { get; set; }

        [Column("based_on_shipping_address")] 
        public bool? BasedOnShippingAddress { get; set; }

        [Column("check_nexus")] 
        public bool? CheckNexus { get; set; }

        [Column("applied_on_shipping_charge")] 
        public bool? AppliedOnShippingCharge { get; set; }

        [Column("state_sales_tax")] 
        public string StateSalesTax { get; set; }

        [Column("county_sales_tax")] 
        public string CountySalesTax { get; set; }

        [Column("tax_rate_type")] 
        public string TaxRateType { get; set; }

        [Column("rate")] 
        public decimal? Rate { get; set; }

        [Column("reporting_tax_authority")] 
        public string ReportingTaxAuthority { get; set; }

        [Column("collecting_tax_authority")] 
        public string CollectingTaxAuthority { get; set; }

        [Column("collecting_account")] 
        public string CollectingAccount { get; set; }

        [Column("use_tax_collecting_account")] 
        public string UseTaxCollectingAccount { get; set; }

        [Column("rounding_method")] 
        public string RoundingMethod { get; set; }

        [Column("rounding_decimal_places")] 
        public int? RoundingDecimalPlaces { get; set; }

    }

    [TableName("core.sales_tax_exempt_detail_scrud_view")]
    [ExplicitColumns]
    public class SalesTaxExemptDetailScrudView : PetaPocoDB.Record<SalesTaxExemptDetailScrudView> 
    {
        [Column("sales_tax_exempt_detail_id")] 
        public int? SalesTaxExemptDetailId { get; set; }

        [Column("sales_tax_exempt")] 
        public string SalesTaxExempt { get; set; }

        [Column("entity_name")] 
        public string EntityName { get; set; }

        [Column("industry_name")] 
        public string IndustryName { get; set; }

        [Column("party")] 
        public string Party { get; set; }

        [Column("party_type")] 
        public string PartyType { get; set; }

        [Column("item")] 
        public string Item { get; set; }

        [Column("item_group")] 
        public string ItemGroup { get; set; }

    }

    [TableName("core.sales_tax_exempt_scrud_view")]
    [ExplicitColumns]
    public class SalesTaxExemptScrudView : PetaPocoDB.Record<SalesTaxExemptScrudView> 
    {
        [Column("sales_tax_exempt_id")] 
        public int? SalesTaxExemptId { get; set; }

        [Column("tax_master")] 
        public string TaxMaster { get; set; }

        [Column("sales_tax_exempt_code")] 
        public string SalesTaxExemptCode { get; set; }

        [Column("sales_tax_exempt_name")] 
        public string SalesTaxExemptName { get; set; }

        [Column("tax_exempt_type")] 
        public string TaxExemptType { get; set; }

        [Column("store")] 
        public string Store { get; set; }

        [Column("sales_tax")] 
        public string SalesTax { get; set; }

        [Column("valid_from")] 
        public DateTime? ValidFrom { get; set; }

        [Column("valid_till")] 
        public DateTime? ValidTill { get; set; }

        [Column("price_from")] 
        public decimal? PriceFrom { get; set; }

        [Column("price_to")] 
        public decimal? PriceTo { get; set; }

    }

    [TableName("core.sales_tax_scrud_view")]
    [ExplicitColumns]
    public class SalesTaxScrudView : PetaPocoDB.Record<SalesTaxScrudView> 
    {
        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("tax_master")] 
        public string TaxMaster { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("sales_tax_code")] 
        public string SalesTaxCode { get; set; }

        [Column("sales_tax_name")] 
        public string SalesTaxName { get; set; }

        [Column("is_exemption")] 
        public bool? IsExemption { get; set; }

        [Column("tax_base_amount")] 
        public string TaxBaseAmount { get; set; }

        [Column("rate")] 
        public decimal? Rate { get; set; }

    }

    [TableName("core.sales_tax_type_scrud_view")]
    [ExplicitColumns]
    public class SalesTaxTypeScrudView : PetaPocoDB.Record<SalesTaxTypeScrudView> 
    {
        [Column("sales_tax_type_id")] 
        public int? SalesTaxTypeId { get; set; }

        [Column("sales_tax_type_code")] 
        public string SalesTaxTypeCode { get; set; }

        [Column("sales_tax_type_name")] 
        public string SalesTaxTypeName { get; set; }

        [Column("is_vat")] 
        public bool? IsVat { get; set; }

    }

    [TableName("core.sales_team_scrud_view")]
    [ExplicitColumns]
    public class SalesTeamScrudView : PetaPocoDB.Record<SalesTeamScrudView> 
    {
        [Column("sales_team_id")] 
        public int? SalesTeamId { get; set; }

        [Column("sales_team_code")] 
        public string SalesTeamCode { get; set; }

        [Column("sales_team_name")] 
        public string SalesTeamName { get; set; }

    }

    [TableName("core.salesperson_bonus_setup_scrud_view")]
    [ExplicitColumns]
    public class SalespersonBonusSetupScrudView : PetaPocoDB.Record<SalespersonBonusSetupScrudView> 
    {
        [Column("salesperson_bonus_setup_id")] 
        public int? SalespersonBonusSetupId { get; set; }

        [Column("salesperson_name")] 
        public string SalespersonName { get; set; }

        [Column("bonus_slab_name")] 
        public string BonusSlabName { get; set; }

    }

    [TableName("core.salesperson_scrud_view")]
    [ExplicitColumns]
    public class SalespersonScrudView : PetaPocoDB.Record<SalespersonScrudView> 
    {
        [Column("salesperson_id")] 
        public int? SalespersonId { get; set; }

        [Column("salesperson_code")] 
        public string SalespersonCode { get; set; }

        [Column("salesperson_name")] 
        public string SalespersonName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

        [Column("contact_number")] 
        public string ContactNumber { get; set; }

        [Column("commission_rate")] 
        public decimal? CommissionRate { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

    }

    [TableName("core.shipper_scrud_view")]
    [ExplicitColumns]
    public class ShipperScrudView : PetaPocoDB.Record<ShipperScrudView> 
    {
        [Column("shipper_id")] 
        public int? ShipperId { get; set; }

        [Column("shipper_code")] 
        public string ShipperCode { get; set; }

        [Column("company_name")] 
        public string CompanyName { get; set; }

        [Column("shipper_name")] 
        public string ShipperName { get; set; }

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

        [Column("country")] 
        public string Country { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("contact_person")] 
        public string ContactPerson { get; set; }

        [Column("contact_po_box")] 
        public string ContactPoBox { get; set; }

        [Column("contact_address_line_1")] 
        public string ContactAddressLine1 { get; set; }

        [Column("contact_address_line_2")] 
        public string ContactAddressLine2 { get; set; }

        [Column("contact_street")] 
        public string ContactStreet { get; set; }

        [Column("contact_city")] 
        public string ContactCity { get; set; }

        [Column("contact_state")] 
        public string ContactState { get; set; }

        [Column("contact_country")] 
        public string ContactCountry { get; set; }

        [Column("contact_email")] 
        public string ContactEmail { get; set; }

        [Column("contact_phone")] 
        public string ContactPhone { get; set; }

        [Column("contact_cell")] 
        public string ContactCell { get; set; }

        [Column("factory_address")] 
        public string FactoryAddress { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("account")] 
        public string Account { get; set; }

    }

    [TableName("core.shipping_address_scrud_view")]
    [ExplicitColumns]
    public class ShippingAddressScrudView : PetaPocoDB.Record<ShippingAddressScrudView> 
    {
        [Column("shipping_address_id")] 
        public long? ShippingAddressId { get; set; }

        [Column("shipping_address_code")] 
        public string ShippingAddressCode { get; set; }

        [Column("party")] 
        public string Party { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

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

        [Column("country")] 
        public string Country { get; set; }

    }

    [TableName("core.state_scrud_view")]
    [ExplicitColumns]
    public class StateScrudView : PetaPocoDB.Record<StateScrudView> 
    {
        [Column("state_id")] 
        public int? StateId { get; set; }

        [Column("country_name")] 
        public string CountryName { get; set; }

        [Column("state_code")] 
        public string StateCode { get; set; }

        [Column("state_name")] 
        public string StateName { get; set; }

    }

    [TableName("core.tax_authority_scrud_view")]
    [ExplicitColumns]
    public class TaxAuthorityScrudView : PetaPocoDB.Record<TaxAuthorityScrudView> 
    {
        [Column("tax_authority_id")] 
        public int? TaxAuthorityId { get; set; }

        [Column("tax_master")] 
        public string TaxMaster { get; set; }

        [Column("tax_authority_code")] 
        public string TaxAuthorityCode { get; set; }

        [Column("tax_authority_name")] 
        public string TaxAuthorityName { get; set; }

        [Column("country")] 
        public string Country { get; set; }

        [Column("county")] 
        public string County { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

    }

    [TableName("core.tax_exempt_type_scrud_view")]
    [ExplicitColumns]
    public class TaxExemptTypeScrudView : PetaPocoDB.Record<TaxExemptTypeScrudView> 
    {
        [Column("tax_exempt_type_id")] 
        public int? TaxExemptTypeId { get; set; }

        [Column("tax_exempt_type_code")] 
        public string TaxExemptTypeCode { get; set; }

        [Column("tax_exempt_type_name")] 
        public string TaxExemptTypeName { get; set; }

    }

    [TableName("core.tax_master_scrud_view")]
    [ExplicitColumns]
    public class TaxMasterScrudView : PetaPocoDB.Record<TaxMasterScrudView> 
    {
        [Column("tax_master_id")] 
        public int? TaxMasterId { get; set; }

        [Column("tax_master_code")] 
        public string TaxMasterCode { get; set; }

        [Column("tax_master_name")] 
        public string TaxMasterName { get; set; }

    }

    [TableName("core.unit_scrud_view")]
    [ExplicitColumns]
    public class UnitScrudView : PetaPocoDB.Record<UnitScrudView> 
    {
        [Column("unit_id")] 
        public int? UnitId { get; set; }

        [Column("unit_code")] 
        public string UnitCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

    }

    [TableName("core.account_master_selector_view")]
    [ExplicitColumns]
    public class AccountMasterSelectorView : PetaPocoDB.Record<AccountMasterSelectorView> 
    {
        [Column("account_master_id")] 
        public short? AccountMasterId { get; set; }

        [Column("account_master_code")] 
        public string AccountMasterCode { get; set; }

        [Column("account_master_name")] 
        public string AccountMasterName { get; set; }

        [Column("normally_debit")] 
        public bool? NormallyDebit { get; set; }

        [Column("parent_account_master_id")] 
        public short? ParentAccountMasterId { get; set; }

    }

    [TableName("core.account_selector_view")]
    [ExplicitColumns]
    public class AccountSelectorView : PetaPocoDB.Record<AccountSelectorView> 
    {
        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("sys_type")] 
        public bool? SysType { get; set; }

        [Column("parent_account_id")] 
        public long? ParentAccountId { get; set; }

        [Column("parent_account_number")] 
        public string ParentAccountNumber { get; set; }

        [Column("parent_account_name")] 
        public string ParentAccountName { get; set; }

        [Column("account_master_id")] 
        public short? AccountMasterId { get; set; }

        [Column("account_master_code")] 
        public string AccountMasterCode { get; set; }

        [Column("account_master_name")] 
        public string AccountMasterName { get; set; }

    }

    [TableName("core.bonus_slab_selector_view")]
    [ExplicitColumns]
    public class BonusSlabSelectorView : PetaPocoDB.Record<BonusSlabSelectorView> 
    {
        [Column("bonus_slab_id")] 
        public int? BonusSlabId { get; set; }

        [Column("bonus_slab_code")] 
        public string BonusSlabCode { get; set; }

        [Column("bonus_slab_name")] 
        public string BonusSlabName { get; set; }

        [Column("effective_from")] 
        public DateTime? EffectiveFrom { get; set; }

        [Column("ends_on")] 
        public DateTime? EndsOn { get; set; }

        [Column("checking_frequency_id")] 
        public int? CheckingFrequencyId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.brand_selector_view")]
    [ExplicitColumns]
    public class BrandSelectorView : PetaPocoDB.Record<BrandSelectorView> 
    {
        [Column("brand_id")] 
        public int? BrandId { get; set; }

        [Column("brand_code")] 
        public string BrandCode { get; set; }

        [Column("brand_name")] 
        public string BrandName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.compound_item_selector_view")]
    [ExplicitColumns]
    public class CompoundItemSelectorView : PetaPocoDB.Record<CompoundItemSelectorView> 
    {
        [Column("compound_item_id")] 
        public int? CompoundItemId { get; set; }

        [Column("compound_item_code")] 
        public string CompoundItemCode { get; set; }

        [Column("compound_item_name")] 
        public string CompoundItemName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.currency_selector_view")]
    [ExplicitColumns]
    public class CurrencySelectorView : PetaPocoDB.Record<CurrencySelectorView> 
    {
        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("currency_symbol")] 
        public string CurrencySymbol { get; set; }

        [Column("currency_name")] 
        public string CurrencyName { get; set; }

        [Column("hundredth_name")] 
        public string HundredthName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.fiscal_year_selector_view")]
    [ExplicitColumns]
    public class FiscalYearSelectorView : PetaPocoDB.Record<FiscalYearSelectorView> 
    {
        [Column("fiscal_year_code")] 
        public string FiscalYearCode { get; set; }

        [Column("fiscal_year_name")] 
        public string FiscalYearName { get; set; }

        [Column("starts_from")] 
        public DateTime? StartsFrom { get; set; }

        [Column("ends_on")] 
        public DateTime? EndsOn { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.frequency_selector_view")]
    [ExplicitColumns]
    public class FrequencySelectorView : PetaPocoDB.Record<FrequencySelectorView> 
    {
        [Column("frequency_id")] 
        public int? FrequencyId { get; set; }

        [Column("frequency_code")] 
        public string FrequencyCode { get; set; }

        [Column("frequency_name")] 
        public string FrequencyName { get; set; }

    }

    [TableName("core.item_group_selector_view")]
    [ExplicitColumns]
    public class ItemGroupSelectorView : PetaPocoDB.Record<ItemGroupSelectorView> 
    {
        [Column("item_group_id")] 
        public int? ItemGroupId { get; set; }

        [Column("item_group_code")] 
        public string ItemGroupCode { get; set; }

        [Column("item_group_name")] 
        public string ItemGroupName { get; set; }

        [Column("exclude_from_purchase")] 
        public bool? ExcludeFromPurchase { get; set; }

        [Column("exclude_from_sales")] 
        public bool? ExcludeFromSales { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("sales_account_id")] 
        public long? SalesAccountId { get; set; }

        [Column("sales_discount_account_id")] 
        public long? SalesDiscountAccountId { get; set; }

        [Column("sales_return_account_id")] 
        public long? SalesReturnAccountId { get; set; }

        [Column("purchase_account_id")] 
        public long? PurchaseAccountId { get; set; }

        [Column("purchase_discount_account_id")] 
        public long? PurchaseDiscountAccountId { get; set; }

        [Column("inventory_account_id")] 
        public long? InventoryAccountId { get; set; }

        [Column("cost_of_goods_sold_account_id")] 
        public long? CostOfGoodsSoldAccountId { get; set; }

        [Column("parent_item_group_id")] 
        public int? ParentItemGroupId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.item_selector_view")]
    [ExplicitColumns]
    public class ItemSelectorView : PetaPocoDB.Record<ItemSelectorView> 
    {
        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("item_group_id")] 
        public int? ItemGroupId { get; set; }

        [Column("item_type_id")] 
        public int? ItemTypeId { get; set; }

        [Column("brand_id")] 
        public int? BrandId { get; set; }

        [Column("preferred_supplier_id")] 
        public long? PreferredSupplierId { get; set; }

        [Column("lead_time_in_days")] 
        public int? LeadTimeInDays { get; set; }

        [Column("weight_in_grams")] 
        public double? WeightInGrams { get; set; }

        [Column("width_in_centimeters")] 
        public double? WidthInCentimeters { get; set; }

        [Column("height_in_centimeters")] 
        public double? HeightInCentimeters { get; set; }

        [Column("length_in_centimeters")] 
        public double? LengthInCentimeters { get; set; }

        [Column("machinable")] 
        public bool? Machinable { get; set; }

        [Column("preferred_shipping_mail_type_id")] 
        public int? PreferredShippingMailTypeId { get; set; }

        [Column("shipping_package_shape_id")] 
        public int? ShippingPackageShapeId { get; set; }

        [Column("unit_id")] 
        public int? UnitId { get; set; }

        [Column("hot_item")] 
        public bool? HotItem { get; set; }

        [Column("cost_price")] 
        public decimal? CostPrice { get; set; }

        [Column("cost_price_includes_tax")] 
        public bool? CostPriceIncludesTax { get; set; }

        [Column("selling_price")] 
        public decimal? SellingPrice { get; set; }

        [Column("selling_price_includes_tax")] 
        public bool? SellingPriceIncludesTax { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("reorder_unit_id")] 
        public int? ReorderUnitId { get; set; }

        [Column("reorder_level")] 
        public int? ReorderLevel { get; set; }

        [Column("reorder_quantity")] 
        public int? ReorderQuantity { get; set; }

        [Column("maintain_stock")] 
        public bool? MaintainStock { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.party_selector_view")]
    [ExplicitColumns]
    public class PartySelectorView : PetaPocoDB.Record<PartySelectorView> 
    {
        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("is_supplier")] 
        public bool? IsSupplier { get; set; }

        [Column("party_type")] 
        public string PartyType { get; set; }

        [Column("party_code")] 
        public string PartyCode { get; set; }

        [Column("first_name")] 
        public string FirstName { get; set; }

        [Column("middle_name")] 
        public string MiddleName { get; set; }

        [Column("last_name")] 
        public string LastName { get; set; }

        [Column("party_name")] 
        public string PartyName { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

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

        [Column("country")] 
        public string Country { get; set; }

        [Column("allow_credit")] 
        public bool? AllowCredit { get; set; }

        [Column("maximum_credit_period")] 
        public short? MaximumCreditPeriod { get; set; }

        [Column("maximum_credit_amount")] 
        public decimal? MaximumCreditAmount { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("gl_head")] 
        public string GlHead { get; set; }

    }

    [TableName("core.party_type_selector_view")]
    [ExplicitColumns]
    public class PartyTypeSelectorView : PetaPocoDB.Record<PartyTypeSelectorView> 
    {
        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("party_type_code")] 
        public string PartyTypeCode { get; set; }

        [Column("party_type_name")] 
        public string PartyTypeName { get; set; }

        [Column("is_supplier")] 
        public bool? IsSupplier { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.price_type_selector_view")]
    [ExplicitColumns]
    public class PriceTypeSelectorView : PetaPocoDB.Record<PriceTypeSelectorView> 
    {
        [Column("price_type_id")] 
        public int? PriceTypeId { get; set; }

        [Column("price_type_code")] 
        public string PriceTypeCode { get; set; }

        [Column("price_type_name")] 
        public string PriceTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.sales_tax_selector_view")]
    [ExplicitColumns]
    public class SalesTaxSelectorView : PetaPocoDB.Record<SalesTaxSelectorView> 
    {
        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("tax_master_id")] 
        public int? TaxMasterId { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("sales_tax_code")] 
        public string SalesTaxCode { get; set; }

        [Column("sales_tax_name")] 
        public string SalesTaxName { get; set; }

        [Column("is_exemption")] 
        public bool? IsExemption { get; set; }

        [Column("tax_base_amount_type_code")] 
        public string TaxBaseAmountTypeCode { get; set; }

        [Column("rate")] 
        public decimal? Rate { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.sales_team_selector_view")]
    [ExplicitColumns]
    public class SalesTeamSelectorView : PetaPocoDB.Record<SalesTeamSelectorView> 
    {
        [Column("sales_team_id")] 
        public int? SalesTeamId { get; set; }

        [Column("sales_team_code")] 
        public string SalesTeamCode { get; set; }

        [Column("sales_team_name")] 
        public string SalesTeamName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.salesperson_selector_view")]
    [ExplicitColumns]
    public class SalespersonSelectorView : PetaPocoDB.Record<SalespersonSelectorView> 
    {
        [Column("salesperson_id")] 
        public int? SalespersonId { get; set; }

        [Column("salesperson_code")] 
        public string SalespersonCode { get; set; }

        [Column("salesperson_name")] 
        public string SalespersonName { get; set; }

        [Column("address")] 
        public string Address { get; set; }

        [Column("contact_number")] 
        public string ContactNumber { get; set; }

        [Column("commission_rate")] 
        public decimal? CommissionRate { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

    }

    [TableName("core.shipping_mail_type_selector_view")]
    [ExplicitColumns]
    public class ShippingMailTypeSelectorView : PetaPocoDB.Record<ShippingMailTypeSelectorView> 
    {
        [Column("shipping_mail_type_id")] 
        public int? ShippingMailTypeId { get; set; }

        [Column("shipping_mail_type_code")] 
        public string ShippingMailTypeCode { get; set; }

        [Column("shipping_mail_type_name")] 
        public string ShippingMailTypeName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.shipping_package_shape_selector_view")]
    [ExplicitColumns]
    public class ShippingPackageShapeSelectorView : PetaPocoDB.Record<ShippingPackageShapeSelectorView> 
    {
        [Column("shipping_package_shape_id")] 
        public int? ShippingPackageShapeId { get; set; }

        [Column("shipping_package_shape_code")] 
        public string ShippingPackageShapeCode { get; set; }

        [Column("shipping_package_shape_name")] 
        public string ShippingPackageShapeName { get; set; }

        [Column("is_rectangular")] 
        public bool? IsRectangular { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.supplier_selector_view")]
    [ExplicitColumns]
    public class SupplierSelectorView : PetaPocoDB.Record<SupplierSelectorView> 
    {
        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("party_code")] 
        public string PartyCode { get; set; }

        [Column("first_name")] 
        public string FirstName { get; set; }

        [Column("middle_name")] 
        public string MiddleName { get; set; }

        [Column("last_name")] 
        public string LastName { get; set; }

        [Column("party_name")] 
        public string PartyName { get; set; }

        [Column("date_of_birth")] 
        public DateTime? DateOfBirth { get; set; }

        [Column("entity_id")] 
        public int? EntityId { get; set; }

        [Column("industry_id")] 
        public int? IndustryId { get; set; }

        [Column("country_id")] 
        public int? CountryId { get; set; }

        [Column("state_id")] 
        public int? StateId { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("allow_credit")] 
        public bool? AllowCredit { get; set; }

        [Column("maximum_credit_period")] 
        public short? MaximumCreditPeriod { get; set; }

        [Column("maximum_credit_amount")] 
        public decimal? MaximumCreditAmount { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.unit_selector_view")]
    [ExplicitColumns]
    public class UnitSelectorView : PetaPocoDB.Record<UnitSelectorView> 
    {
        [Column("unit_id")] 
        public int? UnitId { get; set; }

        [Column("unit_code")] 
        public string UnitCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.account_view")]
    [ExplicitColumns]
    public class AccountView : PetaPocoDB.Record<AccountView> 
    {
        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Column("external_code")] 
        public string ExternalCode { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("confidential")] 
        public bool? Confidential { get; set; }

        [Column("normally_debit")] 
        public bool? NormallyDebit { get; set; }

        [Column("is_transaction_node")] 
        public bool? IsTransactionNode { get; set; }

        [Column("sys_type")] 
        public bool? SysType { get; set; }

        [Column("parent_account_id")] 
        public long? ParentAccountId { get; set; }

        [Column("parent_account_number")] 
        public string ParentAccountNumber { get; set; }

        [Column("parent_account_name")] 
        public string ParentAccountName { get; set; }

        [Column("parent_account")] 
        public string ParentAccount { get; set; }

        [Column("account_master_id")] 
        public short? AccountMasterId { get; set; }

        [Column("account_master_code")] 
        public string AccountMasterCode { get; set; }

        [Column("account_master_name")] 
        public string AccountMasterName { get; set; }

        [Column("has_child")] 
        public bool? HasChild { get; set; }

    }

    [TableName("core.bank_account_view")]
    [ExplicitColumns]
    public class BankAccountView : PetaPocoDB.Record<BankAccountView> 
    {
        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("maintained_by")] 
        public string MaintainedBy { get; set; }

        [Column("bank_name")] 
        public string BankName { get; set; }

        [Column("bank_branch")] 
        public string BankBranch { get; set; }

        [Column("bank_contact_number")] 
        public string BankContactNumber { get; set; }

        [Column("bank_address")] 
        public string BankAddress { get; set; }

        [Column("bank_account_number")] 
        public string BankAccountNumber { get; set; }

        [Column("bank_account_type")] 
        public string BankAccountType { get; set; }

        [Column("relation_officer")] 
        public string RelationOfficer { get; set; }

    }

    [TableName("core.item_view")]
    [ExplicitColumns]
    public class ItemView : PetaPocoDB.Record<ItemView> 
    {
        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("item_group")] 
        public string ItemGroup { get; set; }

        [Column("item_type")] 
        public string ItemType { get; set; }

        [Column("maintain_stock")] 
        public bool? MaintainStock { get; set; }

        [Column("brand")] 
        public string Brand { get; set; }

        [Column("preferred_supplier")] 
        public string PreferredSupplier { get; set; }

        [Column("lead_time_in_days")] 
        public int? LeadTimeInDays { get; set; }

        [Column("weight_in_grams")] 
        public double? WeightInGrams { get; set; }

        [Column("width_in_centimeters")] 
        public double? WidthInCentimeters { get; set; }

        [Column("height_in_centimeters")] 
        public double? HeightInCentimeters { get; set; }

        [Column("length_in_centimeters")] 
        public double? LengthInCentimeters { get; set; }

        [Column("machinable")] 
        public bool? Machinable { get; set; }

        [Column("preferred_shipping_mail_type")] 
        public string PreferredShippingMailType { get; set; }

        [Column("preferred_shipping_package_shape")] 
        public string PreferredShippingPackageShape { get; set; }

        [Column("unit")] 
        public string Unit { get; set; }

        [Column("base_unit")] 
        public string BaseUnit { get; set; }

        [Column("hot_item")] 
        public bool? HotItem { get; set; }

        [Column("cost_price")] 
        public decimal? CostPrice { get; set; }

        [Column("cost_price_includes_tax")] 
        public bool? CostPriceIncludesTax { get; set; }

        [Column("selling_price")] 
        public decimal? SellingPrice { get; set; }

        [Column("selling_price_includes_tax")] 
        public bool? SellingPriceIncludesTax { get; set; }

        [Column("sales_tax")] 
        public string SalesTax { get; set; }

        [Column("reorder_unit")] 
        public string ReorderUnit { get; set; }

        [Column("reorder_level")] 
        public int? ReorderLevel { get; set; }

        [Column("reorder_quantity")] 
        public int? ReorderQuantity { get; set; }

    }

    [TableName("core.party_user_control_view")]
    [ExplicitColumns]
    public class PartyUserControlView : PetaPocoDB.Record<PartyUserControlView> 
    {
        [Column("party_type_code")] 
        public string PartyTypeCode { get; set; }

        [Column("party_type_name")] 
        public string PartyTypeName { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("allow_credit")] 
        public bool? AllowCredit { get; set; }

        [Column("maximum_credit_period")] 
        public short? MaximumCreditPeriod { get; set; }

        [Column("maximum_credit_amount")] 
        public decimal? MaximumCreditAmount { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("state")] 
        public string State { get; set; }

        [Column("country")] 
        public string Country { get; set; }

    }

    [TableName("core.party_view")]
    [ExplicitColumns]
    public class PartyView : PetaPocoDB.Record<PartyView> 
    {
        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("is_supplier")] 
        public bool? IsSupplier { get; set; }

        [Column("party_type")] 
        public string PartyType { get; set; }

        [Column("party_code")] 
        public string PartyCode { get; set; }

        [Column("first_name")] 
        public string FirstName { get; set; }

        [Column("middle_name")] 
        public string MiddleName { get; set; }

        [Column("last_name")] 
        public string LastName { get; set; }

        [Column("party_name")] 
        public string PartyName { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

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

        [Column("country")] 
        public string Country { get; set; }

        [Column("allow_credit")] 
        public bool? AllowCredit { get; set; }

        [Column("maximum_credit_period")] 
        public short? MaximumCreditPeriod { get; set; }

        [Column("maximum_credit_amount")] 
        public decimal? MaximumCreditAmount { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("gl_head")] 
        public string GlHead { get; set; }

    }

    [TableName("core.shipping_address_view")]
    [ExplicitColumns]
    public class ShippingAddressView : PetaPocoDB.Record<ShippingAddressView> 
    {
        [Column("shipping_address_id")] 
        public long? ShippingAddressId { get; set; }

        [Column("shipping_address_code")] 
        public string ShippingAddressCode { get; set; }

        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("party")] 
        public string Party { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

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

        [Column("country")] 
        public string Country { get; set; }

    }

    [TableName("core.supplier_view")]
    [ExplicitColumns]
    public class SupplierView : PetaPocoDB.Record<SupplierView> 
    {
        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("party_code")] 
        public string PartyCode { get; set; }

        [Column("first_name")] 
        public string FirstName { get; set; }

        [Column("middle_name")] 
        public string MiddleName { get; set; }

        [Column("last_name")] 
        public string LastName { get; set; }

        [Column("party_name")] 
        public string PartyName { get; set; }

        [Column("date_of_birth")] 
        public DateTime? DateOfBirth { get; set; }

        [Column("entity_id")] 
        public int? EntityId { get; set; }

        [Column("industry_id")] 
        public int? IndustryId { get; set; }

        [Column("country_id")] 
        public int? CountryId { get; set; }

        [Column("state_id")] 
        public int? StateId { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("allow_credit")] 
        public bool? AllowCredit { get; set; }

        [Column("maximum_credit_period")] 
        public short? MaximumCreditPeriod { get; set; }

        [Column("maximum_credit_amount")] 
        public decimal? MaximumCreditAmount { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.unit_view")]
    [ExplicitColumns]
    public class UnitView : PetaPocoDB.Record<UnitView> 
    {
        [Column("unit_id")] 
        public int? UnitId { get; set; }

        [Column("unit_code")] 
        public string UnitCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.shippers")]
    [PrimaryKey("shipper_id")]
    [ExplicitColumns]
    public class Shipper : PetaPocoDB.Record<Shipper> 
    {
        [Column("shipper_id")] 
        public int ShipperId { get; set; }

        [Column("shipper_code")] 
        public string ShipperCode { get; set; }

        [Column("company_name")] 
        public string CompanyName { get; set; }

        [Column("shipper_name")] 
        public string ShipperName { get; set; }

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

        [Column("country")] 
        public string Country { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("contact_person")] 
        public string ContactPerson { get; set; }

        [Column("contact_po_box")] 
        public string ContactPoBox { get; set; }

        [Column("contact_address_line_1")] 
        public string ContactAddressLine1 { get; set; }

        [Column("contact_address_line_2")] 
        public string ContactAddressLine2 { get; set; }

        [Column("contact_street")] 
        public string ContactStreet { get; set; }

        [Column("contact_city")] 
        public string ContactCity { get; set; }

        [Column("contact_state")] 
        public string ContactState { get; set; }

        [Column("contact_country")] 
        public string ContactCountry { get; set; }

        [Column("contact_email")] 
        public string ContactEmail { get; set; }

        [Column("contact_phone")] 
        public string ContactPhone { get; set; }

        [Column("contact_cell")] 
        public string ContactCell { get; set; }

        [Column("factory_address")] 
        public string FactoryAddress { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("account_id")] 
        public long AccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.shipping_addresses")]
    [PrimaryKey("shipping_address_id")]
    [ExplicitColumns]
    public class ShippingAddress : PetaPocoDB.Record<ShippingAddress> 
    {
        [Column("shipping_address_id")] 
        public long ShippingAddressId { get; set; }

        [Column("shipping_address_code")] 
        public string ShippingAddressCode { get; set; }

        [Column("party_id")] 
        public long PartyId { get; set; }

        [Column("country_id")] 
        public int CountryId { get; set; }

        [Column("state_id")] 
        public int StateId { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.compound_item_details")]
    [PrimaryKey("compound_item_detail_id")]
    [ExplicitColumns]
    public class CompoundItemDetail : PetaPocoDB.Record<CompoundItemDetail> 
    {
        [Column("compound_item_detail_id")] 
        public int CompoundItemDetailId { get; set; }

        [Column("compound_item_id")] 
        public int CompoundItemId { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.item_cost_prices")]
    [PrimaryKey("item_cost_price_id")]
    [ExplicitColumns]
    public class ItemCostPrice : PetaPocoDB.Record<ItemCostPrice> 
    {
        [Column("item_cost_price_id")] 
        public long ItemCostPriceId { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("entry_ts")] 
        public DateTime EntryTs { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("lead_time_in_days")] 
        public int LeadTimeInDays { get; set; }

        [Column("includes_tax")] 
        public bool IncludesTax { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.item_selling_prices")]
    [PrimaryKey("item_selling_price_id")]
    [ExplicitColumns]
    public class ItemSellingPrice : PetaPocoDB.Record<ItemSellingPrice> 
    {
        [Column("item_selling_price_id")] 
        public long ItemSellingPriceId { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("party_type_id")] 
        public int? PartyTypeId { get; set; }

        [Column("price_type_id")] 
        public int? PriceTypeId { get; set; }

        [Column("includes_tax")] 
        public bool IncludesTax { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.item_opening_inventory")]
    [PrimaryKey("item_opening_inventory_id")]
    [ExplicitColumns]
    public class ItemOpeningInventory : PetaPocoDB.Record<ItemOpeningInventory> 
    {
        [Column("item_opening_inventory_id")] 
        public long ItemOpeningInventoryId { get; set; }

        [Column("entry_ts")] 
        public DateTime EntryTs { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("store_id")] 
        public int StoreId { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("amount")] 
        public decimal Amount { get; set; }

        [Column("base_unit_id")] 
        public int BaseUnitId { get; set; }

        [Column("base_quantity")] 
        public decimal BaseQuantity { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.items")]
    [PrimaryKey("item_id")]
    [ExplicitColumns]
    public class Item : PetaPocoDB.Record<Item> 
    {
        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("item_group_id")] 
        public int ItemGroupId { get; set; }

        [Column("item_type_id")] 
        public int ItemTypeId { get; set; }

        [Column("brand_id")] 
        public int BrandId { get; set; }

        [Column("preferred_supplier_id")] 
        public long PreferredSupplierId { get; set; }

        [Column("lead_time_in_days")] 
        public int LeadTimeInDays { get; set; }

        [Column("weight_in_grams")] 
        public double WeightInGrams { get; set; }

        [Column("width_in_centimeters")] 
        public double WidthInCentimeters { get; set; }

        [Column("height_in_centimeters")] 
        public double HeightInCentimeters { get; set; }

        [Column("length_in_centimeters")] 
        public double LengthInCentimeters { get; set; }

        [Column("machinable")] 
        public bool Machinable { get; set; }

        [Column("preferred_shipping_mail_type_id")] 
        public int? PreferredShippingMailTypeId { get; set; }

        [Column("shipping_package_shape_id")] 
        public int? ShippingPackageShapeId { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("hot_item")] 
        public bool HotItem { get; set; }

        [Column("cost_price")] 
        public decimal CostPrice { get; set; }

        [Column("cost_price_includes_tax")] 
        public bool CostPriceIncludesTax { get; set; }

        [Column("selling_price")] 
        public decimal SellingPrice { get; set; }

        [Column("selling_price_includes_tax")] 
        public bool SellingPriceIncludesTax { get; set; }

        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("reorder_unit_id")] 
        public int ReorderUnitId { get; set; }

        [Column("reorder_level")] 
        public int ReorderLevel { get; set; }

        [Column("reorder_quantity")] 
        public int ReorderQuantity { get; set; }

        [Column("maintain_stock")] 
        public bool MaintainStock { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("core.parties")]
    [PrimaryKey("party_id")]
    [ExplicitColumns]
    public class Party : PetaPocoDB.Record<Party> 
    {
        [Column("party_id")] 
        public long PartyId { get; set; }

        [Column("party_type_id")] 
        public int PartyTypeId { get; set; }

        [Column("party_code")] 
        public string PartyCode { get; set; }

        [Column("first_name")] 
        public string FirstName { get; set; }

        [Column("middle_name")] 
        public string MiddleName { get; set; }

        [Column("last_name")] 
        public string LastName { get; set; }

        [Column("party_name")] 
        public string PartyName { get; set; }

        [Column("date_of_birth")] 
        public DateTime? DateOfBirth { get; set; }

        [Column("entity_id")] 
        public int? EntityId { get; set; }

        [Column("industry_id")] 
        public int? IndustryId { get; set; }

        [Column("country_id")] 
        public int CountryId { get; set; }

        [Column("state_id")] 
        public int StateId { get; set; }

        [Column("zip_code")] 
        public string ZipCode { get; set; }

        [Column("address_line_1")] 
        public string AddressLine1 { get; set; }

        [Column("address_line_2")] 
        public string AddressLine2 { get; set; }

        [Column("street")] 
        public string Street { get; set; }

        [Column("city")] 
        public string City { get; set; }

        [Column("phone")] 
        public string Phone { get; set; }

        [Column("fax")] 
        public string Fax { get; set; }

        [Column("cell")] 
        public string Cell { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("url")] 
        public string Url { get; set; }

        [Column("pan_number")] 
        public string PanNumber { get; set; }

        [Column("sst_number")] 
        public string SstNumber { get; set; }

        [Column("cst_number")] 
        public string CstNumber { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("allow_credit")] 
        public bool? AllowCredit { get; set; }

        [Column("maximum_credit_period")] 
        public short? MaximumCreditPeriod { get; set; }

        [Column("maximum_credit_amount")] 
        public decimal? MaximumCreditAmount { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [FunctionName("get_account_view_by_account_master_id")]
    [ExplicitColumns]
    public class DbGetAccountViewByAccountMasterIdResult : PetaPocoDB.Record<DbGetAccountViewByAccountMasterIdResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("account_id")] 
        public string AccountId { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

    }

    [FunctionName("get_associated_units_from_item_id")]
    [ExplicitColumns]
    public class DbGetAssociatedUnitsFromItemIdResult : PetaPocoDB.Record<DbGetAssociatedUnitsFromItemIdResult> 
    {
        [Column("unit_id")] 
        public string UnitId { get; set; }

        [Column("unit_code")] 
        public string UnitCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

    }

    [FunctionName("get_associated_units")]
    [ExplicitColumns]
    public class DbGetAssociatedUnitsResult : PetaPocoDB.Record<DbGetAssociatedUnitsResult> 
    {
        [Column("unit_id")] 
        public string UnitId { get; set; }

        [Column("unit_code")] 
        public string UnitCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

    }

    [FunctionName("get_associated_units_from_item_code")]
    [ExplicitColumns]
    public class DbGetAssociatedUnitsFromItemCodeResult : PetaPocoDB.Record<DbGetAssociatedUnitsFromItemCodeResult> 
    {
        [Column("unit_id")] 
        public string UnitId { get; set; }

        [Column("unit_code")] 
        public string UnitCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

    }
}


