DROP FUNCTION IF EXISTS transactions.get_cash_repository_balance(_cash_repository_id integer, _currency_code national character varying(12));
CREATE FUNCTION transactions.get_cash_repository_balance(_cash_repository_id integer, _currency_code national character varying(12))
RETURNS money_strict2
AS
$$
    DECLARE _debit money_strict2;
    DECLARE _credit money_strict2;
BEGIN
    SELECT COALESCE(SUM(amount_in_currency), 0::money_strict2) INTO _debit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Dr';

    SELECT COALESCE(SUM(amount_in_currency), 0::money_strict2) INTO _credit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Cr';

    RETURN _debit - _credit;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS transactions.get_cash_repository_balance(_cash_repository_id integer);
CREATE FUNCTION transactions.get_cash_repository_balance(_cash_repository_id integer)
RETURNS money_strict2
AS
$$
    DECLARE _local_currency_code national character varying(12) = transactions.get_default_currency_code($1);
    DECLARE _debit money_strict2;
    DECLARE _credit money_strict2;
BEGIN
    SELECT COALESCE(SUM(amount_in_currency), 0::money_strict2) INTO _debit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=_local_currency_code
    AND tran_type='Dr';

    SELECT COALESCE(SUM(amount_in_currency), 0::money_strict2) INTO _credit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=_local_currency_code
    AND tran_type='Cr';

    RETURN _debit - _credit;
END
$$
LANGUAGE plpgsql;


