DROP FUNCTION IF EXISTS core.is_leap_year(integer);
CREATE FUNCTION core.is_leap_year(integer)
RETURNS boolean
AS
$$
BEGIN
    RETURN (SELECT date_part('day', (($1::text || '-02-01')::date + '1 month'::interval - '1 day'::interval)) = 29);
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;


DROP FUNCTION IF EXISTS core.is_leap_year();
CREATE FUNCTION core.is_leap_year()
RETURNS boolean
AS
$$
BEGIN
    RETURN core.is_leap_year(core.get_current_year());
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;
