DROP FUNCTION IF EXISTS transactions.get_accrued_interest(office_id integer, party_id bigint);

CREATE FUNCTION transactions.get_accrued_interest(office_id integer, party_id bigint)
RETURNS money_strict2
AS
$$
BEGIN
    RETURN NULL;
END
$$
LANGUAGE plpgsql;
