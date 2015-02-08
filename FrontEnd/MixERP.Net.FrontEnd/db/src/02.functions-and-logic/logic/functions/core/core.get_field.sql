DROP FUNCTION IF EXISTS core.get_field(this HSTORE, _column_name text);

CREATE FUNCTION core.get_field(this HSTORE, _column_name text)
RETURNS text
AS
$$
   DECLARE _field_value text;
BEGIN
    _field_value := this->_column_name;
    RETURN _field_value;
END
$$
LANGUAGE plpgsql;
