DROP FUNCTION IF EXISTS core.get_verification_status_name_by_verification_status_id(integer);

CREATE FUNCTION core.get_verification_status_name_by_verification_status_id(integer)
RETURNS text
AS
$$
BEGIN
    RETURN
        verification_status_name
    FROM core.verification_statuses
    WHERE verification_status_id = $1;
END
$$
LANGUAGE plpgsql;