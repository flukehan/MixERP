DROP FUNCTION IF EXISTS core.append_if_not_null(text, text);

CREATE FUNCTION core.append_if_not_null(_source text, _to_append text)
RETURNS text
IMMUTABLE
AS
$$
BEGIN
	IF($1 IS NULL) THEN
	    RETURN '';
	END IF;

	RETURN $1 || $2;
END
$$
LANGUAGE plpgsql;


