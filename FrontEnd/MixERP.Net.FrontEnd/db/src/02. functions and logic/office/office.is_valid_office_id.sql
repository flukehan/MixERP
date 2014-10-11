DROP FUNCTION IF EXISTS office.is_valid_office_id(integer);

CREATE FUNCTION office.is_valid_office_id(integer)
RETURNS boolean
AS
$$
BEGIN
        IF EXISTS(SELECT 1 FROM office.offices WHERE office_id=$1) THEN
                RETURN true;
        END IF;

        RETURN false;
END
$$
LANGUAGE plpgsql;
