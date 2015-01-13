DROP FUNCTION IF EXISTS transactions.get_retained_earnings
(
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer
);

CREATE FUNCTION transactions.get_retained_earnings
(
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer
)
RETURNS decimal(24, 4)
AS
$$
    DECLARE     _date_from              date;
    DECLARE     _net_profit             decimal(24, 4);
    DECLARE     _paid_dividends         decimal(24, 4);
BEGIN
    IF(COALESCE(_factor, 0) = 0) THEN
        _factor := 1;
    END IF;
    _date_from              := core.get_fiscal_year_start_date(_office_id);    
    _net_profit             := transactions.get_net_profit(_date_from, _date_to, _office_id, _factor, true);

    SELECT 
        COALESCE(SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END) / _factor, 0)
    INTO 
        _paid_dividends
    FROM transactions.verified_transaction_mat_view
    WHERE value_date <=_date_to
    AND account_master_id BETWEEN 15300 AND 15400
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id));
    
    RETURN _net_profit - _paid_dividends;
END
$$
LANGUAGE plpgsql;