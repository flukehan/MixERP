CREATE FUNCTION core.get_base_unit_id_by_unit_name(text)
RETURNS integer
STABLE
AS
$$
DECLARE _unit_id integer;
BEGIN
    _unit_id := core.get_unit_id_by_unit_name($1);

    RETURN core.get_root_unit_id(_unit_id);
END
$$
LANGUAGE plpgsql;
