
DROP FUNCTION IF EXISTS core.get_current_year();
CREATE FUNCTION core.get_current_year()
RETURNS integer
AS
$$
BEGIN
	RETURN(SELECT EXTRACT(year FROM current_date)::integer);
END
$$
LANGUAGE plpgsql;

