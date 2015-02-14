DROP FUNCTION IF EXISTS policy.is_transaction_restricted
(
    _office_id      integer
);

CREATE FUNCTION policy.is_transaction_restricted
(
    _office_id      integer
)
RETURNS boolean
STABLE
AS
$$
BEGIN
    RETURN NOT allow_transaction_posting
    FROM office.offices
    WHERE office_id=$1;
END
$$
LANGUAGE plpgsql;

ALTER TABLE transactions.transaction_master
ADD CONSTRAINT transaction_master_office_id_chk
CHECK(NOT policy.is_transaction_restricted(office_id));