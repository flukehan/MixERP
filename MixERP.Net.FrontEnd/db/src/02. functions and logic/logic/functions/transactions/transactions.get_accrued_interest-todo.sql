DROP FUNCTION IF EXISTS transactions.get_accrued_interest(office_id integer, party_id integer);

CREATE FUNCTION transactions.get_accrued_interest(office_id integer, party_id integer)
RETURNS money_strict2
AS
$$
BEGIN
	RETURN NULL;
END
$$
LANGUAGE plpgsql;

