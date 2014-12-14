CREATE FUNCTION office.has_child_offices(integer)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS(SELECT 0 FROM office.offices WHERE parent_office_id=$1 LIMIT 1) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;
