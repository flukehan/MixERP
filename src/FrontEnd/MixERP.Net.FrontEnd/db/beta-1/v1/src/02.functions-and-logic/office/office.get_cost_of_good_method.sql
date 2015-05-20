DROP FUNCTION IF EXISTS office.get_cost_of_good_method(_office_id integer);

CREATE FUNCTION office.get_cost_of_good_method(_office_id integer)
RETURNS text
AS
$$
BEGIN
        RETURN value
        FROM office.configuration
        WHERE office_id=$1
        AND config_id=2;
END
$$
LANGUAGE plpgsql;

