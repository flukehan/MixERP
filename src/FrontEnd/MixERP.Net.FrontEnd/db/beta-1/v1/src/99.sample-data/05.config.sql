DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO office.configuration(config_id, office_id, value, configuration_details)
        SELECT 1, office_id, 'Perpetual', ''
        FROM office.offices
        WHERE parent_office_id IS NOT NULL;


        INSERT INTO office.configuration(config_id, office_id, value, configuration_details)
        SELECT 2, office_id, 'LIFO', ''
        FROM office.offices
        WHERE parent_office_id IS NOT NULL;
    END IF;
END
$$
LANGUAGE plpgsql;