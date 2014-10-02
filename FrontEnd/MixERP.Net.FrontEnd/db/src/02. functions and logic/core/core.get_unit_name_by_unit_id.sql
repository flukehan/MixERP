DROP FUNCTION IF EXISTS core.get_unit_name_by_unit_id(integer);

CREATE FUNCTION core.get_unit_name_by_unit_id(integer)
RETURNS text
AS
$$
BEGIN
        RETURN
                unit_name
        FROM
                core.units
        WHERE unit_id=$1;
END
$$
LANGUAGE plpgsql;

--SELECT core.get_unit_name_by_unit_id(1);