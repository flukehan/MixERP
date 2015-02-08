CREATE FUNCTION core.get_root_unit_id(integer)
RETURNS integer
AS
$$
    DECLARE root_unit_id integer;
BEGIN
    SELECT base_unit_id INTO root_unit_id
    FROM core.compound_units
    WHERE compare_unit_id=$1;

    IF(root_unit_id IS NULL) THEN
        RETURN $1;
    ELSE
        RETURN core.get_root_unit_id(root_unit_id);
    END IF; 
END
$$
LANGUAGE plpgsql;
