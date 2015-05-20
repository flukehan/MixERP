DROP FUNCTION IF EXISTS transactions.get_average_party_transaction(party_id bigint);


CREATE FUNCTION transactions.get_average_party_transaction(party_id bigint)
RETURNS money_strict2
STABLE
AS
$$
    DECLARE _account_id bigint= 0;
    DECLARE _debit money_strict2 = 0;
    DECLARE _credit money_strict2 = 0;
BEGIN

    IF(COALESCE($1, 0) <= 0) THEN
        RAISE EXCEPTION 'Invalid party.'
        USING ERRCODE='P3050';
    END IF;

    _account_id := core.get_account_id_by_party_id($1);

    IF(COALESCE(_account_id, 0) <= 0) THEN
        RAISE EXCEPTION 'Invalid party.'
        USING ERRCODE='P3050';
    END IF;

    
    SELECT SUM(amount_in_local_currency)
    INTO _debit
    FROM transactions.verified_transaction_view
    WHERE transactions.verified_transaction_view.account_id=_account_id
    AND tran_type='Dr';

    SELECT SUM(amount_in_local_currency)
    INTO _credit
    FROM transactions.verified_transaction_view
    WHERE transactions.verified_transaction_view.account_id=_account_id
    AND tran_type='Cr';

    RETURN FLOOR( (COALESCE(_credit, '0') + COALESCE(_debit, '0')) /2 );
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS transactions.get_average_party_transaction(party_id bigint, office_id integer);


CREATE FUNCTION transactions.get_average_party_transaction(party_id bigint, office_id integer)
RETURNS money_strict2
STABLE
AS
$$
    DECLARE _account_id bigint = 0;
    DECLARE _debit money_strict2 = 0;
    DECLARE _credit money_strict2 = 0;
BEGIN
    IF(COALESCE($1, 0) <= 0) THEN
        RAISE EXCEPTION 'Invalid party.'
        USING ERRCODE='P3050';
    END IF;

    IF(COALESCE($2, 0) <= 0) THEN
        RAISE EXCEPTION 'Invalid office.'
        USING ERRCODE='P3011';
    END IF;

    _account_id := core.get_account_id_by_party_id($1);

    IF(COALESCE(_account_id, 0) <= 0) THEN
        RAISE EXCEPTION 'Invalid party.'
        USING ERRCODE='P3050';
    END IF;

    SELECT SUM(amount_in_local_currency)
    INTO _debit
    FROM transactions.verified_transaction_view
    WHERE transactions.verified_transaction_view.account_id=_account_id
    AND transactions.verified_transaction_view.office_id=$2
    AND tran_type='Dr';

    SELECT SUM(amount_in_local_currency)
    INTO _credit
    FROM transactions.verified_transaction_view
    WHERE transactions.verified_transaction_view.account_id=_account_id
    AND transactions.verified_transaction_view.office_id=$2
    AND tran_type='Cr';

    RETURN FLOOR( (COALESCE(_credit, '0') + COALESCE(_debit, '0')) /2 );
END
$$
LANGUAGE plpgsql;


