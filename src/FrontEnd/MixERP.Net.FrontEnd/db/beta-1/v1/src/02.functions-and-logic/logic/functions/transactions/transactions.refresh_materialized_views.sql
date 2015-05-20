DROP FUNCTION IF EXISTS transactions.refresh_materialized_views(_office_id integer);

CREATE FUNCTION transactions.refresh_materialized_views(_office_id integer)
RETURNS void
AS
$$
BEGIN
        REFRESH MATERIALIZED VIEW transactions.trial_balance_view;
        REFRESH MATERIALIZED VIEW transactions.verified_stock_transaction_view;
        REFRESH MATERIALIZED VIEW transactions.verified_transaction_mat_view;
        REFRESH MATERIALIZED VIEW transactions.verified_cash_transaction_mat_view;
END
$$
LANGUAGE plpgsql;


SELECT transactions.create_routine('REF-MV', 'transactions.refresh_materialized_views', 1000);
