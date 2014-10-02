DROP FUNCTION IF EXISTS transactions.refresh_materialized_views();

CREATE FUNCTION transactions.refresh_materialized_views()
RETURNS void
AS
$$
BEGIN
        REFRESH MATERIALIZED VIEW transactions.verified_stock_transaction_view;
END
$$
LANGUAGE plpgsql;

