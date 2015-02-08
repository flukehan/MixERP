DROP VIEW IF EXISTS transactions.stock_transaction_view CASCADE;

CREATE VIEW transactions.stock_transaction_view
AS
SELECT
        transactions.transaction_master.transaction_master_id,
        transactions.stock_master.stock_master_id,
        transactions.stock_details.stock_detail_id,
        transactions.transaction_master.book,
        transactions.transaction_master.transaction_counter,
        transactions.transaction_master.transaction_code,
        transactions.transaction_master.value_date,
        transactions.transaction_master.transaction_ts,
        transactions.transaction_master.login_id,
        transactions.transaction_master.user_id,
        transactions.transaction_master.sys_user_id,
        transactions.transaction_master.office_id,
        transactions.transaction_master.cost_center_id,
        transactions.transaction_master.reference_number,
        transactions.transaction_master.statement_reference,
        transactions.transaction_master.last_verified_on,
        transactions.transaction_master.verified_by_user_id,
        transactions.transaction_master.verification_status_id,
        transactions.transaction_master.verification_reason,
        transactions.stock_master.party_id,
        core.parties.country_id,
        core.parties.state_id,
        transactions.stock_master.salesperson_id,
        transactions.stock_master.price_type_id,
        transactions.stock_master.is_credit,
        transactions.stock_master.shipper_id,
        transactions.stock_master.shipping_address_id,
        transactions.stock_master.shipping_charge,
        transactions.stock_master.store_id AS stock_master_store_id,
        transactions.stock_master.cash_repository_id,
        transactions.stock_details.tran_type,
        transactions.stock_details.store_id,
        transactions.stock_details.item_id,
        transactions.stock_details.quantity,
        transactions.stock_details.unit_id,
        transactions.stock_details.base_quantity,
        transactions.stock_details.base_unit_id,
        transactions.stock_details.price,
        transactions.stock_details.discount,
        transactions.stock_details.sales_tax_id,
        transactions.stock_details.tax
FROM transactions.stock_details
INNER JOIN transactions.stock_master
ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
INNER JOIN transactions.transaction_master
ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
INNER JOIN core.parties
ON transactions.stock_master.party_id = core.parties.party_id;