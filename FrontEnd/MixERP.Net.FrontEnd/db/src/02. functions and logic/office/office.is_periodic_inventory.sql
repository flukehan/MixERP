DROP FUNCTION IF EXISTS office.is_periodic_inventory(_office_id integer);

CREATE FUNCTION office.is_periodic_inventory(_office_id integer)
RETURNS boolean
AS
$$
        DECLARE config boolean;
BEGIN
        SELECT configuration = 'Periodic' INTO config
        FROM office.configuration
        WHERE config_id=1
        AND office_id=$1;

        IF(config IS NULL) THEN
                RAISE EXCEPTION '%', 'ERROR M9001: Invalid MixERP database schema. Please consult with your administrator.';
        END IF;

        RETURN config;
END
$$
LANGUAGE plpgsql;