CREATE FUNCTION core.get_unit_id_by_unit_code(text)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT
            core.units.unit_id
        FROM
            core.units
        WHERE
            core.units.unit_code=$1
    );
END
$$
LANGUAGE plpgsql;

