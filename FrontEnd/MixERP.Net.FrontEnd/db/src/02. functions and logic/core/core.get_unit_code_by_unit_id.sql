DROP FUNCTION IF EXISTS core.get_unit_code_by_unit_id(integer);

CREATE FUNCTION core.get_unit_code_by_unit_id(integer)
RETURNS text
AS
$$
BEGIN
        RETURN
                unit_code
        FROM
                core.units
        WHERE unit_id=$1;
END
$$
LANGUAGE plpgsql;

--SELECT core.get_unit_code_by_unit_id(1);