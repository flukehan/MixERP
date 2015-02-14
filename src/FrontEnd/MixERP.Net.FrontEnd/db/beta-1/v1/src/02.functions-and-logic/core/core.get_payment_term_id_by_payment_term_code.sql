DROP FUNCTION IF EXISTS core.get_payment_term_id_by_payment_term_code(text);

CREATE FUNCTION core.get_payment_term_id_by_payment_term_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN payment_term_id
    FROM core.payment_terms
    WHERE payment_term_code=$1;
END
$$
LANGUAGE plpgsql;