
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace MixERP.Net.Entities.Transactions
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
    


    [TableName("transactions.stock_master")]
    [PrimaryKey("stock_master_id")]
    [ExplicitColumns]
    public class StockMaster : PetaPocoDB.Record<StockMaster> 
    {
        [Column("stock_master_id")] 
        public long StockMasterId { get; set; }

        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("salesperson_id")] 
        public int? SalespersonId { get; set; }

        [Column("price_type_id")] 
        public int? PriceTypeId { get; set; }

        [Column("is_credit")] 
        public bool IsCredit { get; set; }

        [Column("payment_term_id")] 
        public int? PaymentTermId { get; set; }

        [Column("shipper_id")] 
        public int? ShipperId { get; set; }

        [Column("shipping_address_id")] 
        public long? ShippingAddressId { get; set; }

        [Column("shipping_charge")] 
        public decimal ShippingCharge { get; set; }

        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("non_taxable")] 
        public bool NonTaxable { get; set; }

        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("transactions.customer_receipts")]
    [PrimaryKey("receipt_id")]
    [ExplicitColumns]
    public class CustomerReceipt : PetaPocoDB.Record<CustomerReceipt> 
    {
        [Column("receipt_id")] 
        public long ReceiptId { get; set; }

        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

        [Column("party_id")] 
        public long PartyId { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("amount")] 
        public decimal Amount { get; set; }

        [Column("er_debit")] 
        public decimal ErDebit { get; set; }

        [Column("er_credit")] 
        public decimal ErCredit { get; set; }

        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("posted_date")] 
        public DateTime? PostedDate { get; set; }

        [Column("bank_account_id")] 
        public long? BankAccountId { get; set; }

        [Column("bank_instrument_code")] 
        public string BankInstrumentCode { get; set; }

        [Column("bank_tran_code")] 
        public string BankTranCode { get; set; }

    }

    [TableName("transactions.non_gl_stock_master_relations")]
    [PrimaryKey("non_gl_stock_master_relation_id")]
    [ExplicitColumns]
    public class NonGlStockMasterRelation : PetaPocoDB.Record<NonGlStockMasterRelation> 
    {
        [Column("non_gl_stock_master_relation_id")] 
        public long NonGlStockMasterRelationId { get; set; }

        [Column("order_non_gl_stock_master_id")] 
        public long OrderNonGlStockMasterId { get; set; }

        [Column("quotation_non_gl_stock_master_id")] 
        public long QuotationNonGlStockMasterId { get; set; }

    }

    [TableName("transactions.non_gl_stock_master")]
    [PrimaryKey("non_gl_stock_master_id")]
    [ExplicitColumns]
    public class NonGlStockMaster : PetaPocoDB.Record<NonGlStockMaster> 
    {
        [Column("non_gl_stock_master_id")] 
        public long NonGlStockMasterId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("price_type_id")] 
        public int? PriceTypeId { get; set; }

        [Column("transaction_ts")] 
        public DateTime TransactionTs { get; set; }

        [Column("login_id")] 
        public long LoginId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("non_taxable")] 
        public bool NonTaxable { get; set; }

        [Column("salesperson_id")] 
        public int? SalespersonId { get; set; }

        [Column("shipper_id")] 
        public int? ShipperId { get; set; }

        [Column("shipping_address_id")] 
        public long? ShippingAddressId { get; set; }

        [Column("shipping_charge")] 
        public decimal ShippingCharge { get; set; }

        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("transactions.stock_master_non_gl_relations")]
    [PrimaryKey("stock_master_non_gl_relation_id")]
    [ExplicitColumns]
    public class StockMasterNonGlRelation : PetaPocoDB.Record<StockMasterNonGlRelation> 
    {
        [Column("stock_master_non_gl_relation_id")] 
        public long StockMasterNonGlRelationId { get; set; }

        [Column("stock_master_id")] 
        public long StockMasterId { get; set; }

        [Column("non_gl_stock_master_id")] 
        public long NonGlStockMasterId { get; set; }

    }

    [TableName("transactions.day_operation")]
    [PrimaryKey("day_id")]
    [ExplicitColumns]
    public class DayOperation : PetaPocoDB.Record<DayOperation> 
    {
        [Column("day_id")] 
        public long DayId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("started_on")] 
        public DateTime StartedOn { get; set; }

        [Column("started_by")] 
        public int StartedBy { get; set; }

        [Column("completed_on")] 
        public DateTime? CompletedOn { get; set; }

        [Column("completed_by")] 
        public int? CompletedBy { get; set; }

        [Column("completed")] 
        public bool Completed { get; set; }

    }

    [TableName("transactions.day_operation_routines")]
    [PrimaryKey("day_operation_routine_id")]
    [ExplicitColumns]
    public class DayOperationRoutine : PetaPocoDB.Record<DayOperationRoutine> 
    {
        [Column("day_operation_routine_id")] 
        public long DayOperationRoutineId { get; set; }

        [Column("day_id")] 
        public long DayId { get; set; }

        [Column("routine_id")] 
        public int RoutineId { get; set; }

        [Column("started_on")] 
        public DateTime StartedOn { get; set; }

        [Column("completed_on")] 
        public DateTime? CompletedOn { get; set; }

    }

    [TableName("transactions.transaction_details")]
    [PrimaryKey("transaction_detail_id")]
    [ExplicitColumns]
    public class TransactionDetail : PetaPocoDB.Record<TransactionDetail> 
    {
        [Column("transaction_detail_id")] 
        public long TransactionDetailId { get; set; }

        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("tran_type")] 
        public string TranType { get; set; }

        [Column("account_id")] 
        public long AccountId { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("amount_in_currency")] 
        public decimal AmountInCurrency { get; set; }

        [Column("local_currency_code")] 
        public string LocalCurrencyCode { get; set; }

        [Column("er")] 
        public decimal Er { get; set; }

        [Column("amount_in_local_currency")] 
        public decimal AmountInLocalCurrency { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("transactions.routines")]
    [PrimaryKey("routine_id")]
    [ExplicitColumns]
    public class Routine : PetaPocoDB.Record<Routine> 
    {
        [Column("routine_id")] 
        public int RoutineId { get; set; }

        [Column("order")] 
        public int Order { get; set; }

        [Column("routine_code")] 
        public string RoutineCode { get; set; }

        [Column("routine_name")] 
        public string RoutineName { get; set; }

        [Column("status")] 
        public bool Status { get; set; }

    }

    [TableName("transactions.non_gl_stock_tax_details")]
    [ExplicitColumns]
    public class NonGlStockTaxDetail : PetaPocoDB.Record<NonGlStockTaxDetail> 
    {
        [Column("non_gl_stock_detail_id")] 
        public long NonGlStockDetailId { get; set; }

        [Column("sales_tax_detail_id")] 
        public int SalesTaxDetailId { get; set; }

        [Column("state_sales_tax_id")] 
        public int? StateSalesTaxId { get; set; }

        [Column("county_sales_tax_id")] 
        public int? CountySalesTaxId { get; set; }

        [Column("principal")] 
        public decimal Principal { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("tax")] 
        public decimal Tax { get; set; }

    }

    [TableName("transactions.stock_return")]
    [PrimaryKey("sales_return_id")]
    [ExplicitColumns]
    public class StockReturn : PetaPocoDB.Record<StockReturn> 
    {
        [Column("sales_return_id")] 
        public long SalesReturnId { get; set; }

        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

        [Column("return_transaction_master_id")] 
        public long ReturnTransactionMasterId { get; set; }

    }

    [TableName("transactions.stock_tax_details")]
    [ExplicitColumns]
    public class StockTaxDetail : PetaPocoDB.Record<StockTaxDetail> 
    {
        [Column("stock_detail_id")] 
        public long StockDetailId { get; set; }

        [Column("sales_tax_detail_id")] 
        public int SalesTaxDetailId { get; set; }

        [Column("state_sales_tax_id")] 
        public int? StateSalesTaxId { get; set; }

        [Column("county_sales_tax_id")] 
        public int? CountySalesTaxId { get; set; }

        [Column("principal")] 
        public decimal Principal { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("tax")] 
        public decimal Tax { get; set; }

    }

    [TableName("transactions.stock_details")]
    [PrimaryKey("stock_detail_id")]
    [ExplicitColumns]
    public class StockDetail : PetaPocoDB.Record<StockDetail> 
    {
        [Column("stock_detail_id")] 
        public long StockDetailId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("stock_master_id")] 
        public long StockMasterId { get; set; }

        [Column("tran_type")] 
        public string TranType { get; set; }

        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("base_quantity")] 
        public decimal BaseQuantity { get; set; }

        [Column("base_unit_id")] 
        public int BaseUnitId { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("cost_of_goods_sold")] 
        public decimal CostOfGoodsSold { get; set; }

        [Column("discount")] 
        public decimal Discount { get; set; }

        [Column("shipping_charge")] 
        public decimal ShippingCharge { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("tax")] 
        public decimal Tax { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("transactions.non_gl_stock_details")]
    [PrimaryKey("non_gl_stock_detail_id")]
    [ExplicitColumns]
    public class NonGlStockDetail : PetaPocoDB.Record<NonGlStockDetail> 
    {
        [Column("non_gl_stock_detail_id")] 
        public long NonGlStockDetailId { get; set; }

        [Column("non_gl_stock_master_id")] 
        public long NonGlStockMasterId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("base_quantity")] 
        public decimal BaseQuantity { get; set; }

        [Column("base_unit_id")] 
        public int BaseUnitId { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("discount")] 
        public decimal Discount { get; set; }

        [Column("shipping_charge")] 
        public decimal ShippingCharge { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("tax")] 
        public decimal Tax { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("transactions.transaction_master")]
    [PrimaryKey("transaction_master_id")]
    [ExplicitColumns]
    public class TransactionMaster : PetaPocoDB.Record<TransactionMaster> 
    {
        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

        [Column("transaction_counter")] 
        public int TransactionCounter { get; set; }

        [Column("transaction_code")] 
        public string TransactionCode { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("transaction_ts")] 
        public DateTime TransactionTs { get; set; }

        [Column("login_id")] 
        public long LoginId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("sys_user_id")] 
        public int? SysUserId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("cost_center_id")] 
        public int? CostCenterId { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("last_verified_on")] 
        public DateTime? LastVerifiedOn { get; set; }

        [Column("verified_by_user_id")] 
        public int? VerifiedByUserId { get; set; }

        [Column("verification_status_id")] 
        public short VerificationStatusId { get; set; }

        [Column("verification_reason")] 
        public string VerificationReason { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("transactions.transaction_view")]
    [ExplicitColumns]
    public class TransactionView : PetaPocoDB.Record<TransactionView> 
    {
        [Column("transaction_master_id")] 
        public long? TransactionMasterId { get; set; }

        [Column("transaction_counter")] 
        public int? TransactionCounter { get; set; }

        [Column("transaction_code")] 
        public string TransactionCode { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("value_date")] 
        public DateTime? ValueDate { get; set; }

        [Column("transaction_ts")] 
        public DateTime? TransactionTs { get; set; }

        [Column("login_id")] 
        public long? LoginId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("sys_user_id")] 
        public int? SysUserId { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("cost_center_id")] 
        public int? CostCenterId { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("master_statement_reference")] 
        public string MasterStatementReference { get; set; }

        [Column("last_verified_on")] 
        public DateTime? LastVerifiedOn { get; set; }

        [Column("verified_by_user_id")] 
        public int? VerifiedByUserId { get; set; }

        [Column("verification_status_id")] 
        public short? VerificationStatusId { get; set; }

        [Column("verification_reason")] 
        public string VerificationReason { get; set; }

        [Column("transaction_detail_id")] 
        public long? TransactionDetailId { get; set; }

        [Column("tran_type")] 
        public string TranType { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("normally_debit")] 
        public bool? NormallyDebit { get; set; }

        [Column("account_master_code")] 
        public string AccountMasterCode { get; set; }

        [Column("account_master_name")] 
        public string AccountMasterName { get; set; }

        [Column("account_master_id")] 
        public short? AccountMasterId { get; set; }

        [Column("confidential")] 
        public bool? Confidential { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("amount_in_currency")] 
        public decimal? AmountInCurrency { get; set; }

        [Column("local_currency_code")] 
        public string LocalCurrencyCode { get; set; }

        [Column("amount_in_local_currency")] 
        public decimal? AmountInLocalCurrency { get; set; }

    }

    [TableName("transactions.verified_transaction_view")]
    [ExplicitColumns]
    public class VerifiedTransactionView : PetaPocoDB.Record<VerifiedTransactionView> 
    {
        [Column("transaction_master_id")] 
        public long? TransactionMasterId { get; set; }

        [Column("transaction_counter")] 
        public int? TransactionCounter { get; set; }

        [Column("transaction_code")] 
        public string TransactionCode { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("value_date")] 
        public DateTime? ValueDate { get; set; }

        [Column("transaction_ts")] 
        public DateTime? TransactionTs { get; set; }

        [Column("login_id")] 
        public long? LoginId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("sys_user_id")] 
        public int? SysUserId { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("cost_center_id")] 
        public int? CostCenterId { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("master_statement_reference")] 
        public string MasterStatementReference { get; set; }

        [Column("last_verified_on")] 
        public DateTime? LastVerifiedOn { get; set; }

        [Column("verified_by_user_id")] 
        public int? VerifiedByUserId { get; set; }

        [Column("verification_status_id")] 
        public short? VerificationStatusId { get; set; }

        [Column("verification_reason")] 
        public string VerificationReason { get; set; }

        [Column("transaction_detail_id")] 
        public long? TransactionDetailId { get; set; }

        [Column("tran_type")] 
        public string TranType { get; set; }

        [Column("account_id")] 
        public long? AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account_name")] 
        public string AccountName { get; set; }

        [Column("normally_debit")] 
        public bool? NormallyDebit { get; set; }

        [Column("account_master_code")] 
        public string AccountMasterCode { get; set; }

        [Column("account_master_name")] 
        public string AccountMasterName { get; set; }

        [Column("account_master_id")] 
        public short? AccountMasterId { get; set; }

        [Column("confidential")] 
        public bool? Confidential { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("amount_in_currency")] 
        public decimal? AmountInCurrency { get; set; }

        [Column("local_currency_code")] 
        public string LocalCurrencyCode { get; set; }

        [Column("amount_in_local_currency")] 
        public decimal? AmountInLocalCurrency { get; set; }

    }

    [TableName("transactions.stock_transaction_view")]
    [ExplicitColumns]
    public class StockTransactionView : PetaPocoDB.Record<StockTransactionView> 
    {
        [Column("transaction_master_id")] 
        public long? TransactionMasterId { get; set; }

        [Column("stock_master_id")] 
        public long? StockMasterId { get; set; }

        [Column("stock_detail_id")] 
        public long? StockDetailId { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("transaction_counter")] 
        public int? TransactionCounter { get; set; }

        [Column("transaction_code")] 
        public string TransactionCode { get; set; }

        [Column("value_date")] 
        public DateTime? ValueDate { get; set; }

        [Column("transaction_ts")] 
        public DateTime? TransactionTs { get; set; }

        [Column("login_id")] 
        public long? LoginId { get; set; }

        [Column("user_id")] 
        public int? UserId { get; set; }

        [Column("sys_user_id")] 
        public int? SysUserId { get; set; }

        [Column("office_id")] 
        public int? OfficeId { get; set; }

        [Column("cost_center_id")] 
        public int? CostCenterId { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("last_verified_on")] 
        public DateTime? LastVerifiedOn { get; set; }

        [Column("verified_by_user_id")] 
        public int? VerifiedByUserId { get; set; }

        [Column("verification_status_id")] 
        public short? VerificationStatusId { get; set; }

        [Column("verification_reason")] 
        public string VerificationReason { get; set; }

        [Column("party_id")] 
        public long? PartyId { get; set; }

        [Column("salesperson_id")] 
        public int? SalespersonId { get; set; }

        [Column("price_type_id")] 
        public int? PriceTypeId { get; set; }

        [Column("is_credit")] 
        public bool? IsCredit { get; set; }

        [Column("shipper_id")] 
        public int? ShipperId { get; set; }

        [Column("shipping_address_id")] 
        public long? ShippingAddressId { get; set; }

        [Column("shipping_charge")] 
        public decimal? ShippingCharge { get; set; }

        [Column("stock_master_store_id")] 
        public int? StockMasterStoreId { get; set; }

        [Column("cash_repository_id")] 
        public int? CashRepositoryId { get; set; }

        [Column("tran_type")] 
        public string TranType { get; set; }

        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("quantity")] 
        public int? Quantity { get; set; }

        [Column("unit_id")] 
        public int? UnitId { get; set; }

        [Column("base_quantity")] 
        public decimal? BaseQuantity { get; set; }

        [Column("base_unit_id")] 
        public int? BaseUnitId { get; set; }

        [Column("price")] 
        public decimal? Price { get; set; }

        [Column("discount")] 
        public decimal? Discount { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("tax")] 
        public decimal? Tax { get; set; }

    }

    [TableName("transactions.verified_stock_details_view")]
    [ExplicitColumns]
    public class VerifiedStockDetailsView : PetaPocoDB.Record<VerifiedStockDetailsView> 
    {
        [Column("stock_detail_id")] 
        public long? StockDetailId { get; set; }

        [Column("value_date")] 
        public DateTime? ValueDate { get; set; }

        [Column("stock_master_id")] 
        public long? StockMasterId { get; set; }

        [Column("tran_type")] 
        public string TranType { get; set; }

        [Column("store_id")] 
        public int? StoreId { get; set; }

        [Column("item_id")] 
        public int? ItemId { get; set; }

        [Column("quantity")] 
        public int? Quantity { get; set; }

        [Column("unit_id")] 
        public int? UnitId { get; set; }

        [Column("base_quantity")] 
        public decimal? BaseQuantity { get; set; }

        [Column("base_unit_id")] 
        public int? BaseUnitId { get; set; }

        [Column("price")] 
        public decimal? Price { get; set; }

        [Column("cost_of_goods_sold")] 
        public decimal? CostOfGoodsSold { get; set; }

        [Column("discount")] 
        public decimal? Discount { get; set; }

        [Column("shipping_charge")] 
        public decimal? ShippingCharge { get; set; }

        [Column("sales_tax_id")] 
        public int? SalesTaxId { get; set; }

        [Column("tax")] 
        public decimal? Tax { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [FunctionName("get_sales_by_offices")]
    [ExplicitColumns]
    public class DbGetSalesByOfficesResult : PetaPocoDB.Record<DbGetSalesByOfficesResult> 
    {
        [Column("office")] 
        public string Office { get; set; }

        [Column("jan")] 
        public decimal Jan { get; set; }

        [Column("feb")] 
        public decimal Feb { get; set; }

        [Column("mar")] 
        public decimal Mar { get; set; }

        [Column("apr")] 
        public decimal Apr { get; set; }

        [Column("may")] 
        public decimal May { get; set; }

        [Column("jun")] 
        public decimal Jun { get; set; }

        [Column("jul")] 
        public decimal Jul { get; set; }

        [Column("aug")] 
        public decimal Aug { get; set; }

        [Column("sep")] 
        public decimal Sep { get; set; }

        [Column("oct")] 
        public decimal Oct { get; set; }

        [Column("nov")] 
        public decimal Nov { get; set; }

        [Column("dec")] 
        public decimal Dec { get; set; }

    }

    [FunctionName("get_trial_balance")]
    [ExplicitColumns]
    public class DbGetTrialBalanceResult : PetaPocoDB.Record<DbGetTrialBalanceResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("account_id")] 
        public string AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("previous_debit")] 
        public decimal PreviousDebit { get; set; }

        [Column("previous_credit")] 
        public decimal PreviousCredit { get; set; }

        [Column("debit")] 
        public decimal Debit { get; set; }

        [Column("credit")] 
        public decimal Credit { get; set; }

        [Column("closing_debit")] 
        public decimal ClosingDebit { get; set; }

        [Column("closing_credit")] 
        public decimal ClosingCredit { get; set; }

    }

    [FunctionName("get_reorder_view_function")]
    [ExplicitColumns]
    public class DbGetReorderViewFunctionResult : PetaPocoDB.Record<DbGetReorderViewFunctionResult> 
    {
        [Column("item_id")] 
        public string ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("unit_id")] 
        public string UnitId { get; set; }

        [Column("unit")] 
        public string Unit { get; set; }

        [Column("quantity_on_hand")] 
        public decimal QuantityOnHand { get; set; }

        [Column("reorder_level")] 
        public string ReorderLevel { get; set; }

        [Column("reorder_quantity")] 
        public string ReorderQuantity { get; set; }

        [Column("preferred_supplier_id")] 
        public string PreferredSupplierId { get; set; }

        [Column("preferred_supplier")] 
        public string PreferredSupplier { get; set; }

        [Column("price")] 
        public string Price { get; set; }

        [Column("tax")] 
        public string Tax { get; set; }

    }

    [FunctionName("get_top_selling_products_by_office")]
    [ExplicitColumns]
    public class DbGetTopSellingProductsByOfficeResult : PetaPocoDB.Record<DbGetTopSellingProductsByOfficeResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("office_id")] 
        public string OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("item_id")] 
        public string ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("total_sales")] 
        public decimal TotalSales { get; set; }

    }

    [FunctionName("get_top_selling_products_of_all_time")]
    [ExplicitColumns]
    public class DbGetTopSellingProductsOfAllTimeResult : PetaPocoDB.Record<DbGetTopSellingProductsOfAllTimeResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("item_id")] 
        public string ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("total_sales")] 
        public decimal TotalSales { get; set; }

    }

    [FunctionName("get_sales_tax")]
    [ExplicitColumns]
    public class DbGetSalesTaxResult : PetaPocoDB.Record<DbGetSalesTaxResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("sales_tax_detail_id")] 
        public string SalesTaxDetailId { get; set; }

        [Column("sales_tax_id")] 
        public string SalesTaxId { get; set; }

        [Column("sales_tax_detail_code")] 
        public string SalesTaxDetailCode { get; set; }

        [Column("sales_tax_detail_name")] 
        public string SalesTaxDetailName { get; set; }

        [Column("is_use_tax")] 
        public string IsUseTax { get; set; }

        [Column("account_id")] 
        public string AccountId { get; set; }

        [Column("price")] 
        public string Price { get; set; }

        [Column("quantity")] 
        public string Quantity { get; set; }

        [Column("discount")] 
        public string Discount { get; set; }

        [Column("shipping_charge")] 
        public string ShippingCharge { get; set; }

        [Column("taxable_amount")] 
        public string TaxableAmount { get; set; }

        [Column("state_sales_tax_id")] 
        public string StateSalesTaxId { get; set; }

        [Column("county_sales_tax_id")] 
        public string CountySalesTaxId { get; set; }

        [Column("rate")] 
        public string Rate { get; set; }

        [Column("base_amount_type")] 
        public string BaseAmountType { get; set; }

        [Column("rate_type")] 
        public string RateType { get; set; }

        [Column("rounding_type")] 
        public string RoundingType { get; set; }

        [Column("rounding_places")] 
        public string RoundingPlaces { get; set; }

        [Column("tax")] 
        public string Tax { get; set; }

    }

    [FunctionName("get_receipt_view")]
    [ExplicitColumns]
    public class DbGetReceiptViewResult : PetaPocoDB.Record<DbGetReceiptViewResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("party")] 
        public string Party { get; set; }

        [Column("user")] 
        public string User { get; set; }

        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("amount")] 
        public string Amount { get; set; }

        [Column("transaction_ts")] 
        public string TransactionTs { get; set; }

        [Column("flag_background_color")] 
        public string FlagBackgroundColor { get; set; }

        [Column("flag_foreground_color")] 
        public string FlagForegroundColor { get; set; }

    }

    [FunctionName("get_non_gl_product_view")]
    [ExplicitColumns]
    public class DbGetNonGlProductViewResult : PetaPocoDB.Record<DbGetNonGlProductViewResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("party")] 
        public string Party { get; set; }

        [Column("price_type")] 
        public string PriceType { get; set; }

        [Column("amount")] 
        public decimal Amount { get; set; }

        [Column("transaction_ts")] 
        public string TransactionTs { get; set; }

        [Column("user")] 
        public string User { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("flag_background_color")] 
        public string FlagBackgroundColor { get; set; }

        [Column("flag_foreground_color")] 
        public string FlagForegroundColor { get; set; }

    }

    [FunctionName("get_journal_view")]
    [ExplicitColumns]
    public class DbGetJournalViewResult : PetaPocoDB.Record<DbGetJournalViewResult> 
    {
        [Column("transaction_master_id")] 
        public string TransactionMasterId { get; set; }

        [Column("transaction_code")] 
        public string TransactionCode { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("posted_by")] 
        public string PostedBy { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("status")] 
        public string Status { get; set; }

        [Column("verified_by")] 
        public string VerifiedBy { get; set; }

        [Column("verified_on")] 
        public string VerifiedOn { get; set; }

        [Column("reason")] 
        public string Reason { get; set; }

        [Column("transaction_ts")] 
        public string TransactionTs { get; set; }

        [Column("flag_bg")] 
        public string FlagBg { get; set; }

        [Column("flag_fg")] 
        public string FlagFg { get; set; }

    }

    [FunctionName("get_product_view")]
    [ExplicitColumns]
    public class DbGetProductViewResult : PetaPocoDB.Record<DbGetProductViewResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("party")] 
        public string Party { get; set; }

        [Column("price_type")] 
        public string PriceType { get; set; }

        [Column("amount")] 
        public decimal Amount { get; set; }

        [Column("transaction_ts")] 
        public string TransactionTs { get; set; }

        [Column("user")] 
        public string User { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("salesperson")] 
        public string Salesperson { get; set; }

        [Column("is_credit")] 
        public string IsCredit { get; set; }

        [Column("shipper")] 
        public string Shipper { get; set; }

        [Column("shipping_address_code")] 
        public string ShippingAddressCode { get; set; }

        [Column("store")] 
        public string Store { get; set; }

        [Column("flag_background_color")] 
        public string FlagBackgroundColor { get; set; }

        [Column("flag_foreground_color")] 
        public string FlagForegroundColor { get; set; }

    }

    [FunctionName("get_balance_sheet")]
    [ExplicitColumns]
    public class DbGetBalanceSheetResult : PetaPocoDB.Record<DbGetBalanceSheetResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("item")] 
        public string Item { get; set; }

        [Column("previous_period")] 
        public decimal PreviousPeriod { get; set; }

        [Column("current_period")] 
        public decimal CurrentPeriod { get; set; }

        [Column("account_id")] 
        public string AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("is_retained_earning")] 
        public string IsRetainedEarning { get; set; }

    }

    [FunctionName("get_retained_earnings_statement")]
    [ExplicitColumns]
    public class DbGetRetainedEarningsStatementResult : PetaPocoDB.Record<DbGetRetainedEarningsStatementResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("tran_code")] 
        public string TranCode { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("debit")] 
        public decimal Debit { get; set; }

        [Column("credit")] 
        public decimal Credit { get; set; }

        [Column("balance")] 
        public decimal Balance { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("account_id")] 
        public string AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("posted_on")] 
        public string PostedOn { get; set; }

        [Column("posted_by")] 
        public string PostedBy { get; set; }

        [Column("approved_by")] 
        public string ApprovedBy { get; set; }

        [Column("verification_status")] 
        public string VerificationStatus { get; set; }

    }

    [FunctionName("list_closing_stock")]
    [ExplicitColumns]
    public class DbListClosingStockResult : PetaPocoDB.Record<DbListClosingStockResult> 
    {
        [Column("item_id")] 
        public string ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("unit_id")] 
        public string UnitId { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("quantity")] 
        public string Quantity { get; set; }

    }

    [FunctionName("get_account_statement")]
    [ExplicitColumns]
    public class DbGetAccountStatementResult : PetaPocoDB.Record<DbGetAccountStatementResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("tran_code")] 
        public string TranCode { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("debit")] 
        public decimal Debit { get; set; }

        [Column("credit")] 
        public decimal Credit { get; set; }

        [Column("balance")] 
        public decimal Balance { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("account_id")] 
        public string AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("posted_on")] 
        public string PostedOn { get; set; }

        [Column("posted_by")] 
        public string PostedBy { get; set; }

        [Column("approved_by")] 
        public string ApprovedBy { get; set; }

        [Column("verification_status")] 
        public string VerificationStatus { get; set; }

        [Column("flag_bg")] 
        public string FlagBg { get; set; }

        [Column("flag_fg")] 
        public string FlagFg { get; set; }

    }

    [FunctionName("get_income_expenditure_statement")]
    [ExplicitColumns]
    public class DbGetIncomeExpenditureStatementResult : PetaPocoDB.Record<DbGetIncomeExpenditureStatementResult> 
    {
        [Column("id")] 
        public string Id { get; set; }

        [Column("account_id")] 
        public string AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("previous_debit")] 
        public decimal PreviousDebit { get; set; }

        [Column("previous_credit")] 
        public decimal PreviousCredit { get; set; }

        [Column("previous_balance")] 
        public decimal PreviousBalance { get; set; }

        [Column("debit")] 
        public decimal Debit { get; set; }

        [Column("credit")] 
        public decimal Credit { get; set; }

        [Column("balance")] 
        public decimal Balance { get; set; }

        [Column("closing_debit")] 
        public decimal ClosingDebit { get; set; }

        [Column("closing_credit")] 
        public decimal ClosingCredit { get; set; }

        [Column("closing_balance")] 
        public decimal ClosingBalance { get; set; }

    }

    [FunctionName("get_party_transaction_summary")]
    [ExplicitColumns]
    public class DbGetPartyTransactionSummaryResult : PetaPocoDB.Record<DbGetPartyTransactionSummaryResult> 
    {
        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("currency_symbol")] 
        public string CurrencySymbol { get; set; }

        [Column("total_due_amount")] 
        public decimal TotalDueAmount { get; set; }

        [Column("office_due_amount")] 
        public decimal OfficeDueAmount { get; set; }

        [Column("accrued_interest")] 
        public decimal AccruedInterest { get; set; }

        [Column("last_receipt_date")] 
        public DateTime LastReceiptDate { get; set; }

        [Column("transaction_value")] 
        public decimal TransactionValue { get; set; }

    }
}


