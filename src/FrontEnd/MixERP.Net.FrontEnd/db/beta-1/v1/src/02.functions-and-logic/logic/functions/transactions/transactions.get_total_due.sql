CREATE FUNCTION transactions.get_total_due(office_id integer, party_id bigint)
RETURNS DECIMAL(24, 4)
AS
$$
    DECLARE _accrued_interest DECIMAL(24, 4)= transactions.get_accrued_interest($1, $2);
    DECLARE _account_id bigint= core.get_account_id_by_party_id($2);
    DECLARE _debit DECIMAL(24, 4) = 0;
    DECLARE _credit DECIMAL(24, 4) = 0;
    DECLARE _local_currency_code national character varying(12) = core.get_currency_code_by_office_id($1); 
    DECLARE _base_currency_code  national character varying(12) = core.get_currency_code_by_party_id($2);
    DECLARE _amount_in_local_currency DECIMAL(24, 4)= 0;
    DECLARE _amount_in_base_currency DECIMAL(24, 4)= 0;
    DECLARE _er decimal_strict2 = 0;
BEGIN

    SELECT SUM(amount_in_local_currency)
    INTO _debit
    FROM transactions.verified_transaction_view
    WHERE transactions.verified_transaction_view.account_id IN (SELECT * FROM core.get_account_ids(_account_id))
    AND transactions.verified_transaction_view.office_id IN (SELECT * FROM office.get_office_ids($1))
    AND tran_type='Dr';

    SELECT SUM(amount_in_local_currency)
    INTO _credit
    FROM transactions.verified_transaction_view
    WHERE transactions.verified_transaction_view.account_id IN (SELECT * FROM core.get_account_ids(_account_id))
    AND transactions.verified_transaction_view.office_id IN (SELECT * FROM office.get_office_ids($1))
    AND tran_type='Cr';

    _er := COALESCE(transactions.get_exchange_rate($1, _local_currency_code, _base_currency_code), 0);

    IF(_er = 0) THEN
        RAISE EXCEPTION 'Exchange rate between % and % was not found.', _local_currency_code, _base_currency_code
        USING ERRCODE='P4010';
    END IF;


    _amount_in_local_currency = COALESCE(_credit, 0) - COALESCE(_debit, 0) - COALESCE(_accrued_interest, 0);


    _amount_in_base_currency = _amount_in_local_currency * _er; 

    RETURN _amount_in_base_currency;
END
$$
LANGUAGE plpgsql;


