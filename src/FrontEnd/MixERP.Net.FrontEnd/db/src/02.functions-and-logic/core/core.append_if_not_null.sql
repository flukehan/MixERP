CREATE FUNCTION core.append_if_not_null(text, text)
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


