--TODO:Drop this function. We now have frequency setup.
DROP FUNCTION IF EXISTS core.get_current_year();
CREATE FUNCTION core.get_current_year()
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN(SELECT EXTRACT(year FROM current_date)::integer);
END
$$
LANGUAGE plpgsql;

