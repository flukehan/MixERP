DROP FUNCTION IF EXISTS core.get_field(this public.hstore, _column_name text);

CREATE FUNCTION core.get_field(_hstore public.hstore, _column_name text)
RETURNS text
AS
$$
   DECLARE _field_value text;
BEGIN
    _field_value := _hstore->_column_name;
    RETURN _field_value;
END
$$
LANGUAGE plpgsql;
