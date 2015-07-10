
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixERP.Net.Entities.Transactions
{

    [TableName("transactions.transaction_details")]
    [PrimaryKey("transaction_detail_id")]
    [ExplicitColumns]
    public class TransactionDetail : PetaPocoDB.Record<TransactionDetail> , IPoco
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

    [TableName("transactions.stock_details")]
    [PrimaryKey("stock_detail_id")]
    [ExplicitColumns]
    public class StockDetail : PetaPocoDB.Record<StockDetail> , IPoco
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
    public class NonGlStockDetail : PetaPocoDB.Record<NonGlStockDetail> , IPoco
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

    [TableName("transactions.inventory_transfer_requests")]
    [PrimaryKey("inventory_transfer_request_id")]
    [ExplicitColumns]
    public class InventoryTransferRequest : PetaPocoDB.Record<InventoryTransferRequest> , IPoco
    {
        [Column("inventory_transfer_request_id")] 
        public long InventoryTransferRequestId { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("user_id")] 
        public int UserId { get; set; }

        [Column("login_id")] 
        public long LoginId { get; set; }

        [Column("store_id")] 
        public int StoreId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("transaction_ts")] 
        public DateTime? TransactionTs { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("authorization_status_id")] 
        public short AuthorizationStatusId { get; set; }

        [Column("authorized_by_user_id")] 
        public int? AuthorizedByUserId { get; set; }

        [Column("authorized_on")] 
        public DateTime? AuthorizedOn { get; set; }

        [Column("authorization_reason")] 
        public string AuthorizationReason { get; set; }

        [Column("received")] 
        public bool Received { get; set; }

        [Column("received_by_user_id")] 
        public int? ReceivedByUserId { get; set; }

        [Column("received_on")] 
        public DateTime? ReceivedOn { get; set; }

        [Column("delivered")] 
        public bool Delivered { get; set; }

        [Column("delivered_by_user_id")] 
        public int? DeliveredByUserId { get; set; }

        [Column("delivered_on")] 
        public DateTime? DeliveredOn { get; set; }

        [Column("withdrawal_reason")] 
        public string WithdrawalReason { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("transactions.stock_master")]
    [PrimaryKey("stock_master_id")]
    [ExplicitColumns]
    public class StockMaster : PetaPocoDB.Record<StockMaster> , IPoco
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

        [Column("credit_settled")] 
        public bool? CreditSettled { get; set; }

    }

    [TableName("transactions.late_fee")]
    [PrimaryKey("transaction_master_id", autoIncrement=false)]
    [ExplicitColumns]
    public class LateFee : PetaPocoDB.Record<LateFee> , IPoco
    {
        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

        [Column("party_id")] 
        public long PartyId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("late_fee_tran_id")] 
        public long LateFeeTranId { get; set; }

        [Column("amount")] 
        public decimal? Amount { get; set; }

    }

    [TableName("transactions.transaction_master")]
    [PrimaryKey("transaction_master_id")]
    [ExplicitColumns]
    public class TransactionMaster : PetaPocoDB.Record<TransactionMaster> , IPoco
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
        public long? LoginId { get; set; }

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

        [Column("cascading_tran_id")] 
        public long? CascadingTranId { get; set; }

        [Column("book_date")] 
        public DateTime BookDate { get; set; }

    }

    [TableName("transactions.transaction_view")]
    [ExplicitColumns]
    public class TransactionView : PetaPocoDB.Record<TransactionView> , IPoco
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
    public class VerifiedTransactionView : PetaPocoDB.Record<VerifiedTransactionView> , IPoco
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
    public class StockTransactionView : PetaPocoDB.Record<StockTransactionView> , IPoco
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

        [Column("country_id")] 
        public int? CountryId { get; set; }

        [Column("state_id")] 
        public int? StateId { get; set; }

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

        [Column("amount")] 
        public decimal? Amount { get; set; }

    }

    [TableName("transactions.sales_by_country_view")]
    [ExplicitColumns]
    public class SalesByCountryView : PetaPocoDB.Record<SalesByCountryView> , IPoco
    {
        [Column("country_code")] 
        public string CountryCode { get; set; }

        [Column("sales")] 
        public decimal? Sales { get; set; }

    }

    [TableName("transactions.verified_stock_details_view")]
    [ExplicitColumns]
    public class VerifiedStockDetailsView : PetaPocoDB.Record<VerifiedStockDetailsView> , IPoco
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

    [TableName("transactions.inventory_transfer_request_details")]
    [PrimaryKey("inventory_transfer_request_detail_id")]
    [ExplicitColumns]
    public class InventoryTransferRequestDetail : PetaPocoDB.Record<InventoryTransferRequestDetail> , IPoco
    {
        [Column("inventory_transfer_request_detail_id")] 
        public long InventoryTransferRequestDetailId { get; set; }

        [Column("inventory_transfer_request_id")] 
        public long InventoryTransferRequestId { get; set; }

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

    }

    [TableName("transactions.customer_receipts")]
    [PrimaryKey("receipt_id")]
    [ExplicitColumns]
    public class CustomerReceipt : PetaPocoDB.Record<CustomerReceipt> , IPoco
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

    [TableName("transactions.stock_tax_details")]
    [ExplicitColumns]
    public class StockTaxDetail : PetaPocoDB.Record<StockTaxDetail> , IPoco
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

    [TableName("transactions.stock_return")]
    [PrimaryKey("sales_return_id")]
    [ExplicitColumns]
    public class StockReturn : PetaPocoDB.Record<StockReturn> , IPoco
    {
        [Column("sales_return_id")] 
        public long SalesReturnId { get; set; }

        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

        [Column("return_transaction_master_id")] 
        public long ReturnTransactionMasterId { get; set; }

    }

    [TableName("transactions.non_gl_stock_tax_details")]
    [ExplicitColumns]
    public class NonGlStockTaxDetail : PetaPocoDB.Record<NonGlStockTaxDetail> , IPoco
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

    [TableName("transactions.non_gl_stock_master_relations")]
    [PrimaryKey("non_gl_stock_master_relation_id")]
    [ExplicitColumns]
    public class NonGlStockMasterRelation : PetaPocoDB.Record<NonGlStockMasterRelation> , IPoco
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
    public class NonGlStockMaster : PetaPocoDB.Record<NonGlStockMaster> , IPoco
    {
        [Column("non_gl_stock_master_id")] 
        public long NonGlStockMasterId { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("book")] 
        public string Book { get; set; }

        [Column("party_id")] 
        public long PartyId { get; set; }

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
    public class StockMasterNonGlRelation : PetaPocoDB.Record<StockMasterNonGlRelation> , IPoco
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
    public class DayOperation : PetaPocoDB.Record<DayOperation> , IPoco
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

    [TableName("transactions.routines")]
    [PrimaryKey("routine_id")]
    [ExplicitColumns]
    public class Routine : PetaPocoDB.Record<Routine> , IPoco
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

    [TableName("transactions.day_operation_routines")]
    [PrimaryKey("day_operation_routine_id")]
    [ExplicitColumns]
    public class DayOperationRoutine : PetaPocoDB.Record<DayOperationRoutine> , IPoco
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

    [FunctionName("get_journal_view")]
    [ExplicitColumns]
    public class DbGetJournalViewResult : PetaPocoDB.Record<DbGetJournalViewResult> , IPoco
    {
        [Column("transaction_master_id")] 
        public long TransactionMasterId { get; set; }

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
        public DateTime VerifiedOn { get; set; }

        [Column("reason")] 
        public string Reason { get; set; }

        [Column("transaction_ts")] 
        public DateTime TransactionTs { get; set; }

        [Column("flag_bg")] 
        public string FlagBg { get; set; }

        [Column("flag_fg")] 
        public string FlagFg { get; set; }

    }

    [FunctionName("get_stock_account_statement")]
    [ExplicitColumns]
    public class DbGetStockAccountStatementResult : PetaPocoDB.Record<DbGetStockAccountStatementResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

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

        [Column("book")] 
        public string Book { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("posted_on")] 
        public DateTime PostedOn { get; set; }

        [Column("posted_by")] 
        public string PostedBy { get; set; }

        [Column("approved_by")] 
        public string ApprovedBy { get; set; }

        [Column("verification_status")] 
        public int VerificationStatus { get; set; }

        [Column("flag_bg")] 
        public string FlagBg { get; set; }

        [Column("flag_fg")] 
        public string FlagFg { get; set; }

    }

    [FunctionName("get_sales_by_offices")]
    [ExplicitColumns]
    public class DbGetSalesByOfficesResult : PetaPocoDB.Record<DbGetSalesByOfficesResult> , IPoco
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

    [FunctionName("get_product_view")]
    [ExplicitColumns]
    public class DbGetProductViewResult : PetaPocoDB.Record<DbGetProductViewResult> , IPoco
    {
        [Column("id")] 
        public long Id { get; set; }

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
        public DateTime TransactionTs { get; set; }

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
        public bool IsCredit { get; set; }

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

    [FunctionName("get_trial_balance")]
    [ExplicitColumns]
    public class DbGetTrialBalanceResult : PetaPocoDB.Record<DbGetTrialBalanceResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("account_id")] 
        public int AccountId { get; set; }

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

    [FunctionName("get_balance_sheet")]
    [ExplicitColumns]
    public class DbGetBalanceSheetResult : PetaPocoDB.Record<DbGetBalanceSheetResult> , IPoco
    {
        [Column("id")] 
        public long Id { get; set; }

        [Column("item")] 
        public string Item { get; set; }

        [Column("previous_period")] 
        public decimal PreviousPeriod { get; set; }

        [Column("current_period")] 
        public decimal CurrentPeriod { get; set; }

        [Column("account_id")] 
        public int AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("is_retained_earning")] 
        public bool IsRetainedEarning { get; set; }

    }

    [FunctionName("get_reorder_view_function")]
    [ExplicitColumns]
    public class DbGetReorderViewFunctionResult : PetaPocoDB.Record<DbGetReorderViewFunctionResult> , IPoco
    {
        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("unit")] 
        public string Unit { get; set; }

        [Column("quantity_on_hand")] 
        public decimal QuantityOnHand { get; set; }

        [Column("reorder_level")] 
        public int ReorderLevel { get; set; }

        [Column("reorder_quantity")] 
        public int ReorderQuantity { get; set; }

        [Column("preferred_supplier_id")] 
        public long PreferredSupplierId { get; set; }

        [Column("preferred_supplier")] 
        public string PreferredSupplier { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

    }

    [FunctionName("get_retained_earnings_statement")]
    [ExplicitColumns]
    public class DbGetRetainedEarningsStatementResult : PetaPocoDB.Record<DbGetRetainedEarningsStatementResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

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
        public int AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("posted_on")] 
        public DateTime PostedOn { get; set; }

        [Column("posted_by")] 
        public string PostedBy { get; set; }

        [Column("approved_by")] 
        public string ApprovedBy { get; set; }

        [Column("verification_status")] 
        public int VerificationStatus { get; set; }

    }

    [FunctionName("list_closing_stock")]
    [ExplicitColumns]
    public class DbListClosingStockResult : PetaPocoDB.Record<DbListClosingStockResult> , IPoco
    {
        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

    }

    [FunctionName("get_top_selling_products_by_office")]
    [ExplicitColumns]
    public class DbGetTopSellingProductsByOfficeResult : PetaPocoDB.Record<DbGetTopSellingProductsByOfficeResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("office_id")] 
        public int OfficeId { get; set; }

        [Column("office_code")] 
        public string OfficeCode { get; set; }

        [Column("office_name")] 
        public string OfficeName { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("total_sales")] 
        public decimal TotalSales { get; set; }

    }

    [FunctionName("get_top_selling_products_of_all_time")]
    [ExplicitColumns]
    public class DbGetTopSellingProductsOfAllTimeResult : PetaPocoDB.Record<DbGetTopSellingProductsOfAllTimeResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("item_name")] 
        public string ItemName { get; set; }

        [Column("total_sales")] 
        public decimal TotalSales { get; set; }

    }

    [FunctionName("get_account_statement")]
    [ExplicitColumns]
    public class DbGetAccountStatementResult : PetaPocoDB.Record<DbGetAccountStatementResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("book_date")] 
        public DateTime BookDate { get; set; }

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
        public int AccountId { get; set; }

        [Column("account_number")] 
        public string AccountNumber { get; set; }

        [Column("account")] 
        public string Account { get; set; }

        [Column("posted_on")] 
        public DateTime PostedOn { get; set; }

        [Column("posted_by")] 
        public string PostedBy { get; set; }

        [Column("approved_by")] 
        public string ApprovedBy { get; set; }

        [Column("verification_status")] 
        public int VerificationStatus { get; set; }

        [Column("flag_bg")] 
        public string FlagBg { get; set; }

        [Column("flag_fg")] 
        public string FlagFg { get; set; }

    }

    [FunctionName("get_salesperson_report")]
    [ExplicitColumns]
    public class DbGetSalespersonReportResult : PetaPocoDB.Record<DbGetSalespersonReportResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("salesperson_id")] 
        public int SalespersonId { get; set; }

        [Column("salesperson_name")] 
        public string SalespersonName { get; set; }

        [Column("total_sales")] 
        public decimal TotalSales { get; set; }

    }

    [FunctionName("get_inventory_transfer_request_view")]
    [ExplicitColumns]
    public class DbGetInventoryTransferRequestViewResult : PetaPocoDB.Record<DbGetInventoryTransferRequestViewResult> , IPoco
    {
        [Column("id")] 
        public long Id { get; set; }

        [Column("value_date")] 
        public DateTime ValueDate { get; set; }

        [Column("office")] 
        public string Office { get; set; }

        [Column("user_name")] 
        public string UserName { get; set; }

        [Column("store")] 
        public string Store { get; set; }

        [Column("reference_number")] 
        public string ReferenceNumber { get; set; }

        [Column("statement_reference")] 
        public string StatementReference { get; set; }

        [Column("authorized")] 
        public string Authorized { get; set; }

        [Column("delivered")] 
        public string Delivered { get; set; }

        [Column("received")] 
        public string Received { get; set; }

        [Column("flag_background_color")] 
        public string FlagBackgroundColor { get; set; }

        [Column("flag_foreground_color")] 
        public string FlagForegroundColor { get; set; }

    }

    [FunctionName("get_income_expenditure_statement")]
    [ExplicitColumns]
    public class DbGetIncomeExpenditureStatementResult : PetaPocoDB.Record<DbGetIncomeExpenditureStatementResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("account_id")] 
        public int AccountId { get; set; }

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

    [FunctionName("get_sales_tax")]
    [ExplicitColumns]
    public class DbGetSalesTaxResult : PetaPocoDB.Record<DbGetSalesTaxResult> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("sales_tax_detail_id")] 
        public int SalesTaxDetailId { get; set; }

        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("sales_tax_detail_code")] 
        public string SalesTaxDetailCode { get; set; }

        [Column("sales_tax_detail_name")] 
        public string SalesTaxDetailName { get; set; }

        [Column("is_use_tax")] 
        public bool IsUseTax { get; set; }

        [Column("account_id")] 
        public int AccountId { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("discount")] 
        public decimal Discount { get; set; }

        [Column("shipping_charge")] 
        public decimal ShippingCharge { get; set; }

        [Column("taxable_amount")] 
        public decimal TaxableAmount { get; set; }

        [Column("state_sales_tax_id")] 
        public int StateSalesTaxId { get; set; }

        [Column("county_sales_tax_id")] 
        public int CountySalesTaxId { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("base_amount_type")] 
        public string BaseAmountType { get; set; }

        [Column("rate_type")] 
        public string RateType { get; set; }

        [Column("rounding_type")] 
        public string RoundingType { get; set; }

        [Column("rounding_places")] 
        public int RoundingPlaces { get; set; }

        [Column("tax")] 
        public decimal Tax { get; set; }

    }

    [FunctionName("get_receipt_view")]
    [ExplicitColumns]
    public class DbGetReceiptViewResult : PetaPocoDB.Record<DbGetReceiptViewResult> , IPoco
    {
        [Column("id")] 
        public long Id { get; set; }

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
        public decimal Amount { get; set; }

        [Column("transaction_ts")] 
        public DateTime TransactionTs { get; set; }

        [Column("flag_background_color")] 
        public string FlagBackgroundColor { get; set; }

        [Column("flag_foreground_color")] 
        public string FlagForegroundColor { get; set; }

    }

    [FunctionName("get_party_transaction_summary")]
    [ExplicitColumns]
    public class DbGetPartyTransactionSummaryResult : PetaPocoDB.Record<DbGetPartyTransactionSummaryResult> , IPoco
    {
        [Column("currency_code")] 
        public string CurrencyCode { get; set; }

        [Column("currency_symbol")] 
        public string CurrencySymbol { get; set; }

        [Column("total_due_amount")] 
        public decimal TotalDueAmount { get; set; }

        [Column("office_due_amount")] 
        public decimal OfficeDueAmount { get; set; }

        [Column("last_receipt_date")] 
        public DateTime LastReceiptDate { get; set; }

        [Column("transaction_value")] 
        public decimal TransactionValue { get; set; }

    }

    [FunctionName("get_non_gl_product_view")]
    [ExplicitColumns]
    public class DbGetNonGlProductViewResult : PetaPocoDB.Record<DbGetNonGlProductViewResult> , IPoco
    {
        [Column("id")] 
        public long Id { get; set; }

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
        public DateTime TransactionTs { get; set; }

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

    [TableName("transactions.stock_detail_type")]
    [ExplicitColumns]
    public class StockDetailType : PetaPocoDB.Record<StockDetailType> , IPoco
    {
        [Column("store_id")] 
        public int StoreId { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("discount")] 
        public decimal Discount { get; set; }

        [Column("shipping_charge")] 
        public decimal ShippingCharge { get; set; }

        [Column("tax_form")] 
        public string TaxForm { get; set; }

        [Column("tax")] 
        public decimal Tax { get; set; }

    }

    [TableName("transactions.purchase_reorder_type")]
    [ExplicitColumns]
    public class PurchaseReorderType : PetaPocoDB.Record<PurchaseReorderType> , IPoco
    {
        [Column("item_id")] 
        public int ItemId { get; set; }

        [Column("supplier_code")] 
        public string SupplierCode { get; set; }

        [Column("unit_id")] 
        public int UnitId { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("tax_code")] 
        public string TaxCode { get; set; }

        [Column("order_quantity")] 
        public int OrderQuantity { get; set; }

    }

    [TableName("transactions.stock_adjustment_type")]
    [ExplicitColumns]
    public class StockAdjustmentType : PetaPocoDB.Record<StockAdjustmentType> , IPoco
    {
        [Column("tran_type")] 
        public string TranType { get; set; }

        [Column("store_name")] 
        public string StoreName { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

    }

    [TableName("transactions.opening_stock_type")]
    [ExplicitColumns]
    public class OpeningStockType : PetaPocoDB.Record<OpeningStockType> , IPoco
    {
        [Column("store_name")] 
        public string StoreName { get; set; }

        [Column("item_code")] 
        public string ItemCode { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("unit_name")] 
        public string UnitName { get; set; }

        [Column("amount")] 
        public decimal Amount { get; set; }

    }

    [TableName("transactions.sales_tax_type")]
    [ExplicitColumns]
    public class SalesTaxType : PetaPocoDB.Record<SalesTaxType> , IPoco
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("sales_tax_detail_id")] 
        public int SalesTaxDetailId { get; set; }

        [Column("sales_tax_id")] 
        public int SalesTaxId { get; set; }

        [Column("sales_tax_detail_code")] 
        public string SalesTaxDetailCode { get; set; }

        [Column("sales_tax_detail_name")] 
        public string SalesTaxDetailName { get; set; }

        [Column("is_use_tax")] 
        public bool IsUseTax { get; set; }

        [Column("account_id")] 
        public int AccountId { get; set; }

        [Column("price")] 
        public decimal Price { get; set; }

        [Column("quantity")] 
        public int Quantity { get; set; }

        [Column("discount")] 
        public decimal Discount { get; set; }

        [Column("shipping_charge")] 
        public decimal ShippingCharge { get; set; }

        [Column("taxable_amount")] 
        public decimal TaxableAmount { get; set; }

        [Column("state_sales_tax_id")] 
        public int StateSalesTaxId { get; set; }

        [Column("county_sales_tax_id")] 
        public int CountySalesTaxId { get; set; }

        [Column("rate")] 
        public decimal Rate { get; set; }

        [Column("base_amount_type")] 
        public string BaseAmountType { get; set; }

        [Column("rate_type")] 
        public string RateType { get; set; }

        [Column("rounding_type")] 
        public string RoundingType { get; set; }

        [Column("rounding_places")] 
        public int RoundingPlaces { get; set; }

        [Column("tax")] 
        public decimal Tax { get; set; }

    }
}

