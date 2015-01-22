CREATE FUNCTION core.get_base_quantity_by_unit_name(text, integer)
RETURNS decimal
STABLE
AS
$$
	DECLARE _unit_id integer;
	DECLARE _root_unit_id integer;
	DECLARE _factor decimal;
BEGIN
    _unit_id := core.get_unit_id_by_unit_name($1);
    _root_unit_id = core.get_root_unit_id(_unit_id);
    _factor = core.convert_unit(_unit_id, _root_unit_id);

    RETURN _factor * $2;
END
$$
LANGUAGE plpgsql;
