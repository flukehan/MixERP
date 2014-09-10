DROP FUNCTION IF EXISTS transactions.get_average_party_transaction(party_id bigint);


CREATE FUNCTION transactions.get_average_party_transaction(party_id bigint)
RETURNS money_strict2
AS
$$
	DECLARE _account_id bigint= core.get_account_id_by_party_id($1);
	DECLARE _debit money_strict2 = 0;
	DECLARE _credit money_strict2 = 0;
BEGIN

	SELECT SUM(amount_in_local_currency)
	INTO _debit
	FROM transactions.verified_transactions_view
	WHERE transactions.verified_transactions_view.account_id=_account_id
	AND tran_type='Dr';

	SELECT SUM(amount_in_local_currency)
	INTO _credit
	FROM transactions.verified_transactions_view
	WHERE transactions.verified_transactions_view.account_id=_account_id
	AND tran_type='Cr';

	RETURN FLOOR( (COALESCE(_credit, '0') + COALESCE(_debit, '0')) /2 );
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS transactions.get_average_party_transaction(party_id bigint, office_id integer);


CREATE FUNCTION transactions.get_average_party_transaction(party_id bigint, office_id integer)
RETURNS money_strict2
AS
$$
	DECLARE _account_id bigint = core.get_account_id_by_party_id($1);
	DECLARE _debit money_strict2 = 0;
	DECLARE _credit money_strict2 = 0;
BEGIN

	SELECT SUM(amount_in_local_currency)
	INTO _debit
	FROM transactions.verified_transactions_view
	WHERE transactions.verified_transactions_view.account_id=_account_id
	AND transactions.verified_transactions_view.office_id=$2
	AND tran_type='Dr';

	SELECT SUM(amount_in_local_currency)
	INTO _credit
	FROM transactions.verified_transactions_view
	WHERE transactions.verified_transactions_view.account_id=_account_id
	AND transactions.verified_transactions_view.office_id=$2
	AND tran_type='Cr';

	RETURN FLOOR( (COALESCE(_credit, '0') + COALESCE(_debit, '0')) /2 );
END
$$
LANGUAGE plpgsql;
