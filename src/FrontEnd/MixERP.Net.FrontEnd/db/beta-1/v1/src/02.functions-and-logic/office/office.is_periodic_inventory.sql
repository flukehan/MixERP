DROP FUNCTION IF EXISTS office.is_periodic_inventory(_office_id integer);

CREATE FUNCTION office.is_periodic_inventory(_office_id integer)
RETURNS boolean
AS
$$
    DECLARE config boolean;
BEGIN
    SELECT value = 'Periodic' INTO config
    FROM office.configuration
    WHERE config_id=1
    AND office_id=$1;

    IF(config IS NULL) THEN
        RETURN false;
    END IF;

    RETURN config;
END
$$
LANGUAGE plpgsql;